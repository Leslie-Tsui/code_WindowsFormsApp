using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvCodeReaderSDKNet;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.IO.Ports;

namespace code_WindowsFormsApp
{
    public partial class Form1 : Form
    {
        MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST m_stDeviceList = new MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST();
        private MvCodeReader m_MyCamera = new MvCodeReader();
        static MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 stFrameInfo = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
        static MvCodeReader.cbOutputEx2delegate ImageCallback;
        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        SerialPort Serialport = null;
        MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO m_stFrameInfo = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO();

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        byte[] m_BufForDriver = new byte[1024 * 1024 * 20];

        // 显示
        Bitmap bmp = null;
        Graphics gra = null;
        Pen pen = new Pen(Color.Blue, 3);                   // 画笔颜色
        Point[] stPointList = new Point[4];                 // 条码位置的4个点坐标

         
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            DeviceListAcq();
            ConnectDevice();
            ConnectSerial();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /*寻找读码器设备*/
        private void DeviceListAcq()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            //cbDeviceList.Items.Clear();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MvCodeReader.MV_CODEREADER_EnumDevices_NET(ref m_stDeviceList, MvCodeReader.MV_CODEREADER_GIGE_DEVICE);
            if (0 != nRet)
            {
                ShowErrorMsg("Enumerate devices fail!", nRet);
                return;
            }

            if (0 == m_stDeviceList.nDeviceNum)
            {
                ShowErrorMsg("None Device!", 0);
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MvCodeReader.MV_CODEREADER_DEVICE_INFO device = (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));
                if (device.nTLayerType == MvCodeReader.MV_CODEREADER_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO gigeInfo = (MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName != "")
                    {
                        label1.Text = gigeInfo.chUserDefinedName;
                        //cbDeviceList.Items.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        label1.Text = gigeInfo.chManufacturerName;
                        //cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_stDeviceList.nDeviceNum != 0)
            {
                //cbDeviceList.SelectedIndex = 0;
            }
        }
        /*连接读码器设备*/
        private void ConnectDevice() 
        {

            // ch:获取选择的设备信息 | en:Get selected device information
            MvCodeReader.MV_CODEREADER_DEVICE_INFO device =
                (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[0],
                                                              typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));

            // ch:打开设备 | en:Open device
            if (null == m_MyCamera)
            {
                m_MyCamera = new MvCodeReader();
                if (null == m_MyCamera)
                {
                    return;
                }
            }

            int nRet = m_MyCamera.MV_CODEREADER_CreateHandle_NET(ref device);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                return;
            }

            nRet = m_MyCamera.MV_CODEREADER_OpenDevice_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_MyCamera.MV_CODEREADER_DestroyHandle_NET();
                ShowErrorMsg("Device open fail!", nRet);
                return;
            }

            else
            {
                checkBox1.Checked = true;
                // ch:注册回调函数 | en:Register image callback
                ImageCallback = new MvCodeReader.cbOutputEx2delegate(ImageCallbackFunc);
                nRet = m_MyCamera.MV_CODEREADER_RegisterImageCallBackEx2_NET(ImageCallback, IntPtr.Zero);
                if (MvCodeReader.MV_CODEREADER_OK != nRet)
                {
                    Console.WriteLine("Register image callback failed!");
                    
                }

                // ch:开启抓图 || en: start grab image
                nRet = m_MyCamera.MV_CODEREADER_StartGrabbing_NET();
                if (MvCodeReader.MV_CODEREADER_OK != nRet)
                {
                    Console.WriteLine("Start grabbing failed:{0:x8}", nRet);
                    
                }


            }
                

        }

        /*给继电器发送指令时，将字符串指令转化为二进制指令*/
        private byte[] HexStringToByteArray(string s)

        {
            s = s.Replace(" ", "");

            byte[] buffer = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)

                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);

            return buffer;

        }

        /*打开IO卡*/
        private void ConnectSerial()
        {
            string[] sPorts = SerialPort.GetPortNames();
            Console.WriteLine(sPorts[0]);
            Console.ReadLine();


            SerialPort port = new SerialPort();
            port.BaudRate = 9600;//波特率
            port.PortName = sPorts[0];
            port.Parity = Parity.None;//校验法：无
            port.DataBits = 8;//数据位：8
            port.StopBits = StopBits.One;//停止位：1
            try
            {
                port.Open();//打开串口
                port.DtrEnable = true;//设置DTR为高电平
                port.RtsEnable = true;//设置RTS位高电平
            }
            catch (Exception ex) {
                ShowErrorMsg("IO卡打开失败！", 0);
            }
            this.Serialport = port;

        }



        // ch:显示错误信息 | en:Show error message
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MvCodeReader.MV_CODEREADER_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MvCodeReader.MV_CODEREADER_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MvCodeReader.MV_CODEREADER_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MvCodeReader.MV_CODEREADER_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MvCodeReader.MV_CODEREADER_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MvCodeReader.MV_CODEREADER_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MvCodeReader.MV_CODEREADER_E_NODATA: errorMsg += " No data "; break;
                case MvCodeReader.MV_CODEREADER_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MvCodeReader.MV_CODEREADER_E_VERSION: errorMsg += " Version mismatches "; break;
                case MvCodeReader.MV_CODEREADER_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MvCodeReader.MV_CODEREADER_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MvCodeReader.MV_CODEREADER_E_GC_GENERIC: errorMsg += " General error "; break;
                case MvCodeReader.MV_CODEREADER_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MvCodeReader.MV_CODEREADER_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MvCodeReader.MV_CODEREADER_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MvCodeReader.MV_CODEREADER_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }



        // 判断字符编码
        public static bool IsTextUTF8(byte[] inputStream)
        {
            int encodingBytesCount = 0;
            bool allTextsAreASCIIChars = true;

            for (int i = 0; i < inputStream.Length; i++)
            {
                byte current = inputStream[i];

                if ((current & 0x80) == 0x80)
                {
                    allTextsAreASCIIChars = false;
                }
                // First byte
                if (encodingBytesCount == 0)
                {
                    if ((current & 0x80) == 0)
                    {
                        // ASCII chars, from 0x00-0x7F
                        continue;
                    }

                    if ((current & 0xC0) == 0xC0)
                    {
                        encodingBytesCount = 1;
                        current <<= 2;

                        // More than two bytes used to encoding a unicode char.
                        // Calculate the real length.
                        while ((current & 0x80) == 0x80)
                        {
                            current <<= 1;
                            encodingBytesCount++;
                        }
                    }
                    else
                    {
                        // Invalid bits structure for UTF8 encoding rule.
                        return false;
                    }
                }
                else
                {
                    // Following bytes, must start with 10.
                    if ((current & 0xC0) == 0x80)
                    {
                        encodingBytesCount--;
                    }
                    else
                    {
                        // Invalid bits structure for UTF8 encoding rule.
                        return false;
                    }
                }
            }

            if (encodingBytesCount != 0)
            {
                // Invalid bits structure for UTF8 encoding rule.
                // Wrong following bytes count.
                return false;
            }

            // Although UTF8 supports encoding for ASCII chars, we regard as a input stream, whose contents are all ASCII as default encoding.
            return !allTextsAreASCIIChars;
        }



        public void ImageCallbackFunc(IntPtr pData, IntPtr pstFrameInfoEx2, IntPtr pUser)
        {
            stFrameInfo = (MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)Marshal.PtrToStructure(pstFrameInfoEx2, typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2));
            Console.WriteLine("Get one frame: ChannelID[" + Convert.ToString(stFrameInfo.nChannelID) + "] , Width[" + Convert.ToString(stFrameInfo.nWidth) + "] , Height[" + Convert.ToString(stFrameInfo.nHeight)
                                + "] , FrameNum[" + Convert.ToString(stFrameInfo.nFrameNum) + "], TriggerIndex[" + Convert.ToString(stFrameInfo.nTriggerIndex) + "]");

            MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2 stBcrResult = (MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2)Marshal.PtrToStructure(stFrameInfo.UnparsedBcrList.pstCodeListEx2, typeof(MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2));

            //Console.WriteLine("CodeNum[" + Convert.ToString(stBcrResult.nCodeNum) + "]");
            if (stBcrResult.nCodeNum==0) {
                txInfoarea.AppendText("无有效条码\n");
            }
            
            
            for (Int32 i = 0; i < stBcrResult.nCodeNum; i++)
            {
                bool bIsValidUTF8 = IsTextUTF8(stBcrResult.stBcrInfoEx2[i].chCode);
                if (bIsValidUTF8)
                {
                    string strCode = Encoding.UTF8.GetString(stBcrResult.stBcrInfoEx2[i].chCode);
                    //Console.WriteLine("Get CodeNum: " + "CodeNum[" + i.ToString() + "], CodeString[" + strCode.Trim().TrimEnd('\0') + "]");
                    
                    txInfoarea.AppendText(DateTime.Now.ToString("HH:mm:ss")+">>>>"+strCode+"\n");
                    txInfoarea.AppendText("\n");

                }
                else
                {
                    string strCode = Encoding.GetEncoding("GB2312").GetString(stBcrResult.stBcrInfoEx2[i].chCode);
                    /*Console.WriteLine("Get CodeNum: " + "CodeNum[" + i.ToString() + "], CodeString[" + strCode.Trim().TrimEnd('\0') + "]");*/
                    txInfoarea.AppendText(DateTime.Now.ToString("HH:mm:ss") + ">>>>" + strCode + "\n");
                    txInfoarea.AppendText("\n");

                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txInfoarea_TextChanged(object sender, EventArgs e)
        {

        }

        private void bnStart_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY0.Checked == true) 
            {
                string s = "01 05 00 00 FF 00 8C 3A";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);
                
            }
            if (this.cbY0.Checked == false)
            {
                string s = "01 05 00 00 00 00 CD CA";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }

        }

        private void cbY1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY1.Checked == true)
            {
                string s = "01 05 00 01 FF 00 DD FA";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY1.Checked == false)
            {
                string s = "01 05 00 01 00 00 9C 0A";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }

        }

        private void cbY2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY2.Checked == true)
            {
                string s = "01 05 00 02 FF 00 2D FA";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY2.Checked == false)
            {
                string s = "01 05 00 02 00 00 6C 0A";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
        }

        private void cbY3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY3.Checked == true)
            {
                string s = "01 05 00 03 FF 00 7C 3A";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY3.Checked == false)
            {
                string s = "01 05 00 03 00 00 3D CA";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
        }

        private void cbY4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY4.Checked == true)
            {
                string s = "01 05 00 04 FF 00 CD FB";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY4.Checked == false)
            {
                string s = "01 05 00 04 00 00 8C 0B";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
        }

        private void cbY5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY5.Checked == true)
            {
                string s = "01 05 00 05 FF 00 9C 3B";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY5.Checked == false)
            {
                string s = "01 05 00 05 00 00 DD CB";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }

        }

        private void cbY6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY6.Checked == true)
            {
                string s = "01 05 00 06 FF 00 6C 3B";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY6.Checked == false)
            {
                string s = "01 05 00 06 00 00 2D CB";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
        }

        private void cbY7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbY7.Checked == true)
            {
                string s = "01 05 00 01 FF 00 DD FA";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
            if (this.cbY7.Checked == false)
            {
                string s = "01 05 00 01 00 00 9C 0A";
                byte[] array = HexStringToByteArray(s);
                Serialport.Write(array, 0, array.Length);

            }
        }
    }
}
