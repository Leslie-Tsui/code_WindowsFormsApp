using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;

namespace code_WindowsFormsApp
{


    public class PropertyManageCls : CollectionBase, ICustomTypeDescriptor
    {
        public void Add(Property value)
        {
            int flag = -1;
            if (value != null)
            {
                if (base.List.Count > 0)
                {
                    IList<Property> mList = new List<Property>();
                    for (int i = 0; i < base.List.Count; i++)
                    {
                        Property p = base.List[i] as Property;
                        if (value.Name == p.Name)
                        {
                            flag = i;
                        }
                        mList.Add(p);
                    }
                    if (flag == -1)
                    {
                        mList.Add(value);
                    }
                    base.List.Clear();
                    foreach (Property p in mList)
                    {
                        base.List.Add(p);
                    }
                }
                else
                {
                    base.List.Add(value);
                }
            }
        }
        public void Remove(Property value)
        {
            if (value != null && base.List.Count > 0)
                base.List.Remove(value);
        }
        public Property this[int index]
        {
            get
            {
                return (Property)base.List[index];
            }
            set
            {
                base.List[index] = (Property)value;
            }
        }
        #region ICustomTypeDescriptor 成员
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                Property prop = (Property)this[i];
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
            }
            return new PropertyDescriptorCollection(newProps);
        }
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }

    /*public void ProCreat(string load_flag)
    {
        switch (load_flag)
        {
            case "gongcheng":
                gcProCreat();
                break;
            case "chanpin":
                cpProCreat();
                break;
            default:
                break;
        }



        this.Refresh();
    }

    private void gcProCreat()
    {
        PropertyManageCls pmc = new PropertyManageCls();

        Property p1 = new Property("当前工程的标题，作为输出的副标题，工程名称不能为空！", "[工程名称]", false, true);
        p1.Category = "\t工程信息";
        p1.DisplayName = "\t\t工程名称";
        pmc.Add(p1);

        Property p2 = new Property("工程所在的省份，用于确定基本风压，年温差等信息", "北京", false, true);
        p2.Category = "城市基本信息";
        p2.DisplayName = "\t\t所属省";
        string[] s = new string[] { "北京", "上海", "山东", "广东" };
        p2.Converter = new DropDownListConverter(s);
        pmc.Add(p2);

        Property p3 = new Property("工程所在的城市，用于确定基本风压，年温差等信息", "北京市", false, true);
        p3.Category = "城市基本信息";
        p3.DisplayName = "\t\t所属市";
        string[] s2 = new string[] { "北京市", "济南", "淄博" };
        p3.Converter = new DropDownListConverter(s2);
        pmc.Add(p3);

        Property p4 = new Property("工程所在的城市的风压设计年限", "50年", false, true);
        p4.Category = "城市基本信息";
        p4.DisplayName = "\t风压设计年限";
        string[] s3 = new string[] { "50年", "100年" };
        p4.Converter = new DropDownListConverter(s3);
        pmc.Add(p4);


        Property p5 = new Property("工程所在的城市的基本风压", "", false, true);
        p5.Category = "城市基本信息";
        p5.DisplayName = "城市基本风压";
        pmc.Add(p5);

        Property p6 = new Property("工程所在的城市的基本雪压", "", false, true);
        p6.Category = "城市基本信息";
        p6.DisplayName = "城市基本雪压";
        pmc.Add(p6);

        Property p7 = new Property("工程所在的城市的雪压区域分布", "Ⅱ", false, true);
        p7.Category = "城市基本信息";
        p7.DisplayName = "城市雪压区域";
        string[] s4 = new string[] { "-", "Ⅰ", "Ⅱ" };
        p7.Converter = new DropDownListConverter(s4);
        pmc.Add(p7);

        propertyGrid1.SelectedObject = pmc;
    }

    private void cpProCreat()
    {
        PropertyManageCls pmc = new PropertyManageCls();

        Property p1 = new Property("当前产品的标题，作为输出的主标题，产品名称不能为空！", "[产品名称]", false, true);
        p1.Category = "\t产品信息";
        p1.DisplayName = "\t产品名称";
        pmc.Add(p1);

        Property p2 = new Property("产品的类别，用于限制产品的具体结构以及试用材料计算方法", "幕墙", false, true);
        p2.Category = "产品结构信息";
        p2.DisplayName = "产品类别";
        string[] s = new string[] { "幕墙", "门窗", "采光顶", "雨棚" };
        p2.Converter = new DropDownListConverter(s);
        pmc.Add(p2);

        propertyGrid1.SelectedObject = pmc;

    }*/


    public class Property
    {
        private string _name = string.Empty;
        private object _value = null;
        private bool _readonly = false;
        private bool _visible = true;
        private string _category = string.Empty;
        TypeConverter _converter = null;
        object _editor = null;
        private string _displayname = string.Empty;
        public Property(string sName, object sValue)
        {
            this._name = sName;
            this._value = sValue;
        }
        public Property(string sName, object sValue, bool sReadonly, bool sVisible)
        {
            this._name = sName;
            this._value = sValue;
            this._readonly = sReadonly;
            this._visible = sVisible;
        }
        public string Name  //获得属性名  
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string DisplayName   //属性显示名称  
        {
            get
            {
                return _displayname;
            }
            set
            {
                _displayname = value;
            }
        }
        public TypeConverter Converter  //类型转换器，我们在制作下拉列表时需要用到  
        {
            get
            {
                return _converter;
            }
            set
            {
                _converter = value;
            }
        }
        public string Category  //属性所属类别  
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        public object Value  //属性值  
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public bool ReadOnly  //是否为只读属性  
        {
            get
            {
                return _readonly;
            }
            set
            {
                _readonly = value;
            }
        }
        public bool Visible  //是否可见  
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        public virtual object Editor   //属性编辑器  
        {
            get
            {
                return _editor;
            }
            set
            {
                _editor = value;
            }
        }
    }

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        Property m_Property;
        public CustomPropertyDescriptor(ref Property myProperty, Attribute[] attrs)
            : base(myProperty.Name, attrs)
        {
            m_Property = myProperty;
        }
        #region PropertyDescriptor 重写方法
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }
        public override object GetValue(object component)
        {
            return m_Property.Value;
        }
        public override string Description
        {
            get
            {
                return m_Property.Name;
            }
        }
        public override string Category
        {
            get
            {
                return m_Property.Category;
            }
        }
        public override string DisplayName
        {
            get
            {
                return m_Property.DisplayName != "" ? m_Property.DisplayName : m_Property.Name;
            }
        }
        public override bool IsReadOnly
        {
            get
            {
                return m_Property.ReadOnly;
            }
        }
        public override void ResetValue(object component)
        {
            //Have to implement  
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        public override void SetValue(object component, object value)
        {
            m_Property.Value = value;
        }
        public override TypeConverter Converter
        {
            get
            {
                return m_Property.Converter;
            }
        }
        public override Type PropertyType
        {
            get { return m_Property.Value.GetType(); }
        }
        public override object GetEditor(Type editorBaseType)
        {
            return m_Property.Editor == null ? base.GetEditor(editorBaseType) : m_Property.Editor;
        }
        #endregion
    }

    public class DropDownListConverter : StringConverter
    {
        object[] m_Objects;
        public DropDownListConverter(object[] objects)
        {
            m_Objects = objects;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override
        System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(m_Objects);//我们可以直接在内部定义一个数组，但并不建议这样做，这样对于下拉框的灵活性有很大影响  
        }
    }

    //try
    //        {


    //            PropertyManageCls pmc = new PropertyManageCls();

    //Property p1 = new Property("IO卡波特率相关设定", 9600, false, true);
    //p1.Category = "\tI/O卡设定";
    //            p1.DisplayName = "\t\tI/O卡串口波特率";
    //            pmc.Add(p1);

    //            /*Property p2 = new Property("工程所在的省份，用于确定基本风压，年温差等信息", "北京", false, true);
    //            p2.Category = "城市基本信息";
    //            p2.DisplayName = "\t\t所属省";
    //            string[] s = new string[] { "北京", "上海", "山东", "广东" };
    //            p2.Converter = new DropDownListConverter(s);
    //            pmc.Add(p2);*/

    //            Property p2 = new Property("IO卡串口号相关设定", "com3", false, true);
    //p2.Category = "\tI/O卡设定";
    //            p2.DisplayName = "\t\tI/O卡串口名";
    //            pmc.Add(p2);


    //            Property p3 = new Property("IO卡信号反转设定", "true", false, true);
    //p3.Category = "\tI/O卡设定";
    //            p3.DisplayName = "\t\tX0信号反转";
    //            pmc.Add(p3);


    //            Property p4 = new Property("产线定义设置", "21895467020", false, true);
    //p4.Category = "产线定义";
    //            p4.DisplayName = "\tLot号(产线定义)";
    //            pmc.Add(p4);


    //            Property p5 = new Property("光源相关设置", 128, false, true);
    //p5.Category = "光源设定";
    //            p5.DisplayName = "光源亮度";
    //            pmc.Add(p5);

    //            Property p6 = new Property("生产服务器相关设置", "UD V1912", false, true);
    //p6.Category = "生产服务器设置";
    //            p6.DisplayName = "程序编号";
    //            pmc.Add(p6);

    //            Property p7 = new Property("当前设备编号", "1012", false, true);
    //p7.Category = "生产服务器设置";
    //            p7.DisplayName = "当前设备编号";
    //            pmc.Add(p7);


    //            Property p8 = new Property("制程编号", "19", false, true);
    //p8.Category = "生产服务器设置";
    //            p8.DisplayName = "当前制程编号";
    //            pmc.Add(p8);

    //            Property p9 = new Property("工号", "100860", false, true);
    //p9.Category = "生产服务器设置";
    //            p9.DisplayName = "当前制程编号";
    //            pmc.Add(p9);

    //            Property p10 = new Property("接口授权码", "1X21201X211X21211D1D1D1F1D", false, true);
    //p10.Category = "生产服务器设置";
    //            p10.DisplayName = "接口授权码";
    //            pmc.Add(p10);

    //            Property p11 = new Property("前工站的制程编号", "", false, true);
    //p11.Category = "生产服务器设置";
    //            p11.DisplayName = "前工站的制程编号";
    //            pmc.Add(p11);

    //            Property p12 = new Property("是否检测混Lot号", true, false, true);
    //p12.Category = "生产服务器设置";
    //            p12.DisplayName = "是否检测混Lot号";
    //            pmc.Add(p12);

    //            Property p13 = new Property("是否取前面工位数据", true, false, true);
    //p13.Category = "生产服务器设置";
    //            p13.DisplayName = "是否取前面工位数据";
    //            pmc.Add(p13);

    //            Property p14 = new Property("是否为首工站", true, false, true);
    //p14.Category = "生产服务器设置";
    //            p14.DisplayName = "是否为首工站";
    //            pmc.Add(p14);

    //            Property p15 = new Property("数据库服务器IP", "172.16.13.1", false, true);
    //p15.Category = "生产服务器设置";
    //            p15.DisplayName = "数据库服务器IP";
    //            pmc.Add(p15);

    //            Property p16 = new Property("数据库名", "PIMD", false, true);
    //p16.Category = "生产服务器设置";
    //            p16.DisplayName = "数据库名";
    //            pmc.Add(p16);

    //            Property p17 = new Property("条码枪IP", "10.0.0.1", false, true);
    //p17.Category = "条码枪设定";
    //            p17.DisplayName = "条码枪IP";
    //            pmc.Add(p17);

    //            Property p18 = new Property("条码枪端口", "5000", false, true);
    //p18.Category = "条码枪设定";
    //            p18.DisplayName = "条码枪端口";
    //            pmc.Add(p18);



    //            this.propertyGrid.SelectedObject = pmc;
    //}
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.ToString());
    //        }

public class IOsetting
    {
        private int IoRate = 9600;
        private string ComName = "com3";
        private bool SignalReverse = true;
        public int ioRate
        {
            get { return IoRate; }
            set { IoRate = value; }
        }

        public string comName
        {
            get { return ComName; }
            set { ComName = value; }
        }

        public bool signalReverse
        {
            get { return SignalReverse; }
            set { SignalReverse = value; }
        }

    }

 
    public class Production
    {
        private string LotNum = "21895467020";

        public string lotNum
        {
            get { return LotNum; }
            set { LotNum = value; }
        }

    }

    public class Lightsetting
    {
        private int LightNum = 128;
        public int lightNum 
        {
            get { return lightNum; }
            set { lightNum = value; }
        }

    }

    public class ServerConfig 
    {

        
        private System.Windows.Forms.TreeView treeView = new System.Windows.Forms.TreeView();
        

        

    }



    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.main_run = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gpStatisticBox = new System.Windows.Forms.GroupBox();
            this.btClear = new System.Windows.Forms.Button();
            this.tbUPH = new System.Windows.Forms.TextBox();
            this.tbFailed = new System.Windows.Forms.TextBox();
            this.tbPassed = new System.Windows.Forms.TextBox();
            this.lbUPH = new System.Windows.Forms.Label();
            this.lbFailed = new System.Windows.Forms.Label();
            this.lbPassed = new System.Windows.Forms.Label();
            this.bnChanguser = new System.Windows.Forms.Button();
            this.bnCloseLight = new System.Windows.Forms.Button();
            this.bnSavecode = new System.Windows.Forms.Button();
            this.bnReset = new System.Windows.Forms.Button();
            this.bnStart = new System.Windows.Forms.Button();
            this.txLotarea = new System.Windows.Forms.RichTextBox();
            this.txInfoarea = new System.Windows.Forms.RichTextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txLot_num = new System.Windows.Forms.TextBox();
            this.lbLot_num = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txWorker_num = new System.Windows.Forms.TextBox();
            this.lbWorker_num = new System.Windows.Forms.Label();
            this.setting_test = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.light_control = new System.Windows.Forms.TabPage();
            this.checkBox27 = new System.Windows.Forms.CheckBox();
            this.button13 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button12 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox25 = new System.Windows.Forms.CheckBox();
            this.cbY7 = new System.Windows.Forms.CheckBox();
            this.checkBox23 = new System.Windows.Forms.CheckBox();
            this.cbY6 = new System.Windows.Forms.CheckBox();
            this.checkBox21 = new System.Windows.Forms.CheckBox();
            this.cbY5 = new System.Windows.Forms.CheckBox();
            this.checkBox19 = new System.Windows.Forms.CheckBox();
            this.cbY4 = new System.Windows.Forms.CheckBox();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.cbY3 = new System.Windows.Forms.CheckBox();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.cbY2 = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.cbY1 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.cbY0 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.user_change = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.tabControl.SuspendLayout();
            this.main_run.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gpStatisticBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.setting_test.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.light_control.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.user_change.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.main_run);
            this.tabControl.Controls.Add(this.setting_test);
            this.tabControl.Controls.Add(this.light_control);
            this.tabControl.Controls.Add(this.user_change);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1155, 676);
            this.tabControl.TabIndex = 0;
            // 
            // main_run
            // 
            this.main_run.Controls.Add(this.panel1);
            this.main_run.Location = new System.Drawing.Point(4, 22);
            this.main_run.Name = "main_run";
            this.main_run.Padding = new System.Windows.Forms.Padding(3);
            this.main_run.Size = new System.Drawing.Size(1147, 650);
            this.main_run.TabIndex = 0;
            this.main_run.Text = "主页运行";
            this.main_run.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.gpStatisticBox);
            this.panel1.Controls.Add(this.bnChanguser);
            this.panel1.Controls.Add(this.bnCloseLight);
            this.panel1.Controls.Add(this.bnSavecode);
            this.panel1.Controls.Add(this.bnReset);
            this.panel1.Controls.Add(this.bnStart);
            this.panel1.Controls.Add(this.txLotarea);
            this.panel1.Controls.Add(this.txInfoarea);
            this.panel1.Controls.Add(this.checkBox4);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.txLot_num);
            this.panel1.Controls.Add(this.lbLot_num);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txWorker_num);
            this.panel1.Controls.Add(this.lbWorker_num);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1141, 644);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // gpStatisticBox
            // 
            this.gpStatisticBox.Controls.Add(this.btClear);
            this.gpStatisticBox.Controls.Add(this.tbUPH);
            this.gpStatisticBox.Controls.Add(this.tbFailed);
            this.gpStatisticBox.Controls.Add(this.tbPassed);
            this.gpStatisticBox.Controls.Add(this.lbUPH);
            this.gpStatisticBox.Controls.Add(this.lbFailed);
            this.gpStatisticBox.Controls.Add(this.lbPassed);
            this.gpStatisticBox.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpStatisticBox.Location = new System.Drawing.Point(895, 463);
            this.gpStatisticBox.Name = "gpStatisticBox";
            this.gpStatisticBox.Size = new System.Drawing.Size(241, 178);
            this.gpStatisticBox.TabIndex = 18;
            this.gpStatisticBox.TabStop = false;
            this.gpStatisticBox.Text = "实时生产统计";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(23, 147);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(191, 29);
            this.btClear.TabIndex = 6;
            this.btClear.Text = "复位";
            this.btClear.UseVisualStyleBackColor = true;
            // 
            // tbUPH
            // 
            this.tbUPH.Location = new System.Drawing.Point(113, 109);
            this.tbUPH.Name = "tbUPH";
            this.tbUPH.Size = new System.Drawing.Size(100, 31);
            this.tbUPH.TabIndex = 5;
            this.tbUPH.Text = "0.00%";
            // 
            // tbFailed
            // 
            this.tbFailed.Location = new System.Drawing.Point(113, 69);
            this.tbFailed.Name = "tbFailed";
            this.tbFailed.Size = new System.Drawing.Size(100, 31);
            this.tbFailed.TabIndex = 4;
            this.tbFailed.Text = "0";
            // 
            // tbPassed
            // 
            this.tbPassed.Location = new System.Drawing.Point(113, 31);
            this.tbPassed.Name = "tbPassed";
            this.tbPassed.Size = new System.Drawing.Size(100, 31);
            this.tbPassed.TabIndex = 3;
            this.tbPassed.Text = "0";
            // 
            // lbUPH
            // 
            this.lbUPH.AutoSize = true;
            this.lbUPH.BackColor = System.Drawing.Color.Gold;
            this.lbUPH.Location = new System.Drawing.Point(52, 109);
            this.lbUPH.Name = "lbUPH";
            this.lbUPH.Size = new System.Drawing.Size(43, 21);
            this.lbUPH.TabIndex = 2;
            this.lbUPH.Text = "UPH";
            // 
            // lbFailed
            // 
            this.lbFailed.AutoSize = true;
            this.lbFailed.BackColor = System.Drawing.Color.Red;
            this.lbFailed.ForeColor = System.Drawing.Color.Black;
            this.lbFailed.Location = new System.Drawing.Point(19, 72);
            this.lbFailed.Name = "lbFailed";
            this.lbFailed.Size = new System.Drawing.Size(76, 21);
            this.lbFailed.TabIndex = 1;
            this.lbFailed.Text = "Failed";
            // 
            // lbPassed
            // 
            this.lbPassed.AutoSize = true;
            this.lbPassed.BackColor = System.Drawing.Color.Chartreuse;
            this.lbPassed.ForeColor = System.Drawing.Color.Black;
            this.lbPassed.Location = new System.Drawing.Point(19, 34);
            this.lbPassed.Name = "lbPassed";
            this.lbPassed.Size = new System.Drawing.Size(76, 21);
            this.lbPassed.TabIndex = 0;
            this.lbPassed.Text = "Passed";
            // 
            // bnChanguser
            // 
            this.bnChanguser.Font = new System.Drawing.Font("黑体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnChanguser.Location = new System.Drawing.Point(932, 401);
            this.bnChanguser.Name = "bnChanguser";
            this.bnChanguser.Size = new System.Drawing.Size(177, 56);
            this.bnChanguser.TabIndex = 17;
            this.bnChanguser.Text = "切换用户";
            this.bnChanguser.UseVisualStyleBackColor = true;
            // 
            // bnCloseLight
            // 
            this.bnCloseLight.BackColor = System.Drawing.Color.Aquamarine;
            this.bnCloseLight.Font = new System.Drawing.Font("黑体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnCloseLight.Location = new System.Drawing.Point(931, 336);
            this.bnCloseLight.Name = "bnCloseLight";
            this.bnCloseLight.Size = new System.Drawing.Size(177, 59);
            this.bnCloseLight.TabIndex = 16;
            this.bnCloseLight.Text = "关闭光源";
            this.bnCloseLight.UseVisualStyleBackColor = false;
            // 
            // bnSavecode
            // 
            this.bnSavecode.BackColor = System.Drawing.Color.Lime;
            this.bnSavecode.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnSavecode.Location = new System.Drawing.Point(932, 267);
            this.bnSavecode.Name = "bnSavecode";
            this.bnSavecode.Size = new System.Drawing.Size(177, 61);
            this.bnSavecode.TabIndex = 15;
            this.bnSavecode.Text = "手动保存条码数据";
            this.bnSavecode.UseVisualStyleBackColor = false;
            // 
            // bnReset
            // 
            this.bnReset.BackColor = System.Drawing.Color.Gold;
            this.bnReset.Location = new System.Drawing.Point(932, 201);
            this.bnReset.Name = "bnReset";
            this.bnReset.Size = new System.Drawing.Size(177, 60);
            this.bnReset.TabIndex = 14;
            this.bnReset.Text = "设备复位";
            this.bnReset.UseVisualStyleBackColor = false;
            // 
            // bnStart
            // 
            this.bnStart.BackColor = System.Drawing.Color.IndianRed;
            this.bnStart.Location = new System.Drawing.Point(931, 131);
            this.bnStart.Name = "bnStart";
            this.bnStart.Size = new System.Drawing.Size(178, 64);
            this.bnStart.TabIndex = 13;
            this.bnStart.Text = "设备启动";
            this.bnStart.UseVisualStyleBackColor = false;
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // txLotarea
            // 
            this.txLotarea.Location = new System.Drawing.Point(463, 487);
            this.txLotarea.Margin = new System.Windows.Forms.Padding(2);
            this.txLotarea.Name = "txLotarea";
            this.txLotarea.ReadOnly = true;
            this.txLotarea.Size = new System.Drawing.Size(428, 157);
            this.txLotarea.TabIndex = 12;
            this.txLotarea.Text = "";
            // 
            // txInfoarea
            // 
            this.txInfoarea.Font = new System.Drawing.Font("黑体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txInfoarea.Location = new System.Drawing.Point(19, 490);
            this.txInfoarea.Margin = new System.Windows.Forms.Padding(2);
            this.txInfoarea.Name = "txInfoarea";
            this.txInfoarea.ReadOnly = true;
            this.txInfoarea.Size = new System.Drawing.Size(440, 157);
            this.txInfoarea.TabIndex = 11;
            this.txInfoarea.Text = "";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Font = new System.Drawing.Font("黑体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox4.ForeColor = System.Drawing.Color.LightSalmon;
            this.checkBox4.Location = new System.Drawing.Point(934, 95);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(194, 26);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "电机运行:运行中";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Font = new System.Drawing.Font("黑体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2.ForeColor = System.Drawing.Color.LightSalmon;
            this.checkBox2.Location = new System.Drawing.Point(934, 65);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(183, 26);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "生产服务器连接";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("黑体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.ForeColor = System.Drawing.Color.LightSalmon;
            this.checkBox1.Location = new System.Drawing.Point(934, 35);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(150, 26);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "条码(1)状态";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(936, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.Location = new System.Drawing.Point(19, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(871, 412);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 125;
            // 
            // txLot_num
            // 
            this.txLot_num.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txLot_num.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txLot_num.Location = new System.Drawing.Point(613, 15);
            this.txLot_num.Name = "txLot_num";
            this.txLot_num.Size = new System.Drawing.Size(283, 31);
            this.txLot_num.TabIndex = 4;
            // 
            // lbLot_num
            // 
            this.lbLot_num.AutoSize = true;
            this.lbLot_num.BackColor = System.Drawing.Color.SandyBrown;
            this.lbLot_num.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLot_num.Location = new System.Drawing.Point(450, 2);
            this.lbLot_num.Name = "lbLot_num";
            this.lbLot_num.Size = new System.Drawing.Size(157, 42);
            this.lbLot_num.TabIndex = 3;
            this.lbLot_num.Text = "输入当前生产的\r\nLotNum";
            this.lbLot_num.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbLot_num.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(340, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txWorker_num
            // 
            this.txWorker_num.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txWorker_num.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txWorker_num.Location = new System.Drawing.Point(91, 15);
            this.txWorker_num.Name = "txWorker_num";
            this.txWorker_num.Size = new System.Drawing.Size(231, 31);
            this.txWorker_num.TabIndex = 1;
            this.txWorker_num.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lbWorker_num
            // 
            this.lbWorker_num.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbWorker_num.AutoSize = true;
            this.lbWorker_num.BackColor = System.Drawing.Color.SandyBrown;
            this.lbWorker_num.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWorker_num.Location = new System.Drawing.Point(15, 14);
            this.lbWorker_num.Name = "lbWorker_num";
            this.lbWorker_num.Size = new System.Drawing.Size(71, 29);
            this.lbWorker_num.TabIndex = 0;
            this.lbWorker_num.Text = "工号";
            this.lbWorker_num.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbWorker_num.Click += new System.EventHandler(this.label1_Click);
            // 
            // setting_test
            // 
            this.setting_test.Controls.Add(this.panel2);
            this.setting_test.Location = new System.Drawing.Point(4, 22);
            this.setting_test.Name = "setting_test";
            this.setting_test.Padding = new System.Windows.Forms.Padding(3);
            this.setting_test.Size = new System.Drawing.Size(1147, 650);
            this.setting_test.TabIndex = 1;
            this.setting_test.Text = "设置与调试";
            this.setting_test.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1141, 644);
            this.panel2.TabIndex = 0;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button8.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Location = new System.Drawing.Point(864, 549);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(272, 95);
            this.button8.TabIndex = 4;
            this.button8.Text = "设定本地数据保存文件夹";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.PaleGreen;
            this.button7.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(589, 549);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(272, 95);
            this.button7.TabIndex = 3;
            this.button7.Text = "保存所有";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MistyRose;
            this.button2.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(5, 555);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(423, 84);
            this.button2.TabIndex = 2;
            this.button2.Text = "重连生产服务器";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(583, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 527);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手动操作";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 357);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(541, 170);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.checkBox10);
            this.groupBox3.Controls.Add(this.checkBox9);
            this.groupBox3.Controls.Add(this.checkBox8);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(6, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(541, 322);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "其它设定";
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(387, 273);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 39);
            this.button6.TabIndex = 14;
            this.button6.Text = "上传";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(187, 280);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 21);
            this.textBox3.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(17, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "手动上传产品条码";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(341, 158);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(194, 55);
            this.button5.TabIndex = 11;
            this.button5.Text = "模拟触发";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(341, 87);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(194, 55);
            this.button4.TabIndex = 10;
            this.button4.Text = "IO X2/X3输出-OFF";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(341, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(194, 55);
            this.button3.TabIndex = 9;
            this.button3.Text = "IO X2/X3输出-ON";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(173, 234);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(124, 21);
            this.textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(174, 202);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 21);
            this.textBox1.TabIndex = 7;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox10.Location = new System.Drawing.Point(22, 239);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(152, 18);
            this.checkBox10.TabIndex = 6;
            this.checkBox10.Text = "启用条码前三位验证";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox9.Location = new System.Drawing.Point(22, 207);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(138, 18);
            this.checkBox9.TabIndex = 5;
            this.checkBox9.Text = "启用条码长度验证";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox8.Location = new System.Drawing.Point(22, 167);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(180, 23);
            this.checkBox8.TabIndex = 4;
            this.checkBox8.Text = "启用自动气缸控制";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox7.Location = new System.Drawing.Point(22, 131);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(104, 23);
            this.checkBox7.TabIndex = 3;
            this.checkBox7.Text = "启用验证";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox6.Location = new System.Drawing.Point(22, 101);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(104, 23);
            this.checkBox6.TabIndex = 2;
            this.checkBox6.Text = "启用验证";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox5.Location = new System.Drawing.Point(22, 68);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(180, 23);
            this.checkBox5.TabIndex = 1;
            this.checkBox5.Text = "启用生产数据验证";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox3.Location = new System.Drawing.Point(22, 37);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(142, 23);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "启用条码验证";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.propertyGrid);
            this.groupBox1.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(5, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 533);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器连接";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.propertyGrid.Location = new System.Drawing.Point(0, 29);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(572, 498);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.Click += new System.EventHandler(this.propertyGrid1_Click_1);
            try
            {
                PropertyManageCls pmc = new PropertyManageCls();

                Property p1 = new Property("IO卡波特率相关设定", 9600, false, true);
                p1.Category = "\tI/O卡设定";
                p1.DisplayName = "\t\tI/O卡串口波特率";
                pmc.Add(p1);

                /*Property p2 = new Property("工程所在的省份，用于确定基本风压，年温差等信息", "北京", false, true);
                p2.Category = "城市基本信息";
                p2.DisplayName = "\t\t所属省";
                string[] s = new string[] { "北京", "上海", "山东", "广东" };
                p2.Converter = new DropDownListConverter(s);
                pmc.Add(p2);*/

                Property p2 = new Property("IO卡串口号相关设定", "com3", false, true);
                p2.Category = "\tI/O卡设定";
                p2.DisplayName = "\t\tI/O卡串口名";
                pmc.Add(p2);


                Property p3 = new Property("IO卡信号反转设定", "true", false, true);
                p3.Category = "\tI/O卡设定";
                p3.DisplayName = "\t\tX0信号反转";
                pmc.Add(p3);


                Property p4 = new Property("产线定义设置", "21895467020", false, true);
                p4.Category = "产线定义";
                p4.DisplayName = "\tLot号(产线定义)";
                pmc.Add(p4);


                Property p5 = new Property("光源相关设置", 128, false, true);
                p5.Category = "光源设定";
                p5.DisplayName = "光源亮度";
                pmc.Add(p5);

                Property p6 = new Property("生产服务器相关设置", "UD V1912", false, true);
                p6.Category = "生产服务器设置";
                p6.DisplayName = "程序编号";
                pmc.Add(p6);

                Property p7 = new Property("当前设备编号", "1012", false, true);
                p7.Category = "生产服务器设置";
                p7.DisplayName = "当前设备编号";
                pmc.Add(p7);


                Property p8 = new Property("制程编号", "19", false, true);
                p8.Category = "生产服务器设置";
                p8.DisplayName = "当前制程编号";
                pmc.Add(p8);

                Property p9 = new Property("工号", "100860", false, true);
                p9.Category = "生产服务器设置";
                p9.DisplayName = "当前制程编号";
                pmc.Add(p9);

                Property p10 = new Property("接口授权码", "1X21201X211X21211D1D1D1F1D", false, true);
                p10.Category = "生产服务器设置";
                p10.DisplayName = "接口授权码";
                pmc.Add(p10);

                Property p11 = new Property("前工站的制程编号", "", false, true);
                p11.Category = "生产服务器设置";
                p11.DisplayName = "前工站的制程编号";
                pmc.Add(p11);

                Property p12 = new Property("是否检测混Lot号", true, false, true);
                p12.Category = "生产服务器设置";
                p12.DisplayName = "是否检测混Lot号";
                pmc.Add(p12);

                Property p13 = new Property("是否取前面工位数据", true, false, true);
                p13.Category = "生产服务器设置";
                p13.DisplayName = "是否取前面工位数据";
                pmc.Add(p13);

                Property p14 = new Property("是否为首工站", true, false, true);
                p14.Category = "生产服务器设置";
                p14.DisplayName = "是否为首工站";
                pmc.Add(p14);

                Property p15 = new Property("数据库服务器IP", "172.16.13.1", false, true);
                p15.Category = "生产服务器设置";
                p15.DisplayName = "数据库服务器IP";
                pmc.Add(p15);

                Property p16 = new Property("数据库名", "PIMD", false, true);
                p16.Category = "生产服务器设置";
                p16.DisplayName = "数据库名";
                pmc.Add(p16);

                Property p17 = new Property("条码枪IP", "10.0.0.1", false, true);
                p17.Category = "条码枪设定";
                p17.DisplayName = "条码枪IP";
                pmc.Add(p17);

                Property p18 = new Property("条码枪端口", "5000", false, true);
                p18.Category = "条码枪设定";
                p18.DisplayName = "条码枪端口";
                pmc.Add(p18);

                this.propertyGrid.SelectedObject = pmc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // 
            // light_control
            // 
            this.light_control.Controls.Add(this.checkBox27);
            this.light_control.Controls.Add(this.button13);
            this.light_control.Controls.Add(this.groupBox5);
            this.light_control.Controls.Add(this.groupBox4);
            this.light_control.Controls.Add(this.textBox4);
            this.light_control.Controls.Add(this.label3);
            this.light_control.Location = new System.Drawing.Point(4, 22);
            this.light_control.Name = "light_control";
            this.light_control.Size = new System.Drawing.Size(1147, 650);
            this.light_control.TabIndex = 2;
            this.light_control.Text = "光源控制";
            this.light_control.UseVisualStyleBackColor = true;
            // 
            // checkBox27
            // 
            this.checkBox27.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox27.Location = new System.Drawing.Point(64, 547);
            this.checkBox27.Name = "checkBox27";
            this.checkBox27.Size = new System.Drawing.Size(137, 33);
            this.checkBox27.TabIndex = 18;
            this.checkBox27.Text = "启用IO监控";
            this.checkBox27.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.YellowGreen;
            this.button13.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button13.Location = new System.Drawing.Point(729, 547);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(341, 79);
            this.button13.TabIndex = 18;
            this.button13.Text = "保存所有";
            this.button13.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button12);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.trackBar1);
            this.groupBox5.Controls.Add(this.checkBox25);
            this.groupBox5.Controls.Add(this.cbY7);
            this.groupBox5.Controls.Add(this.checkBox23);
            this.groupBox5.Controls.Add(this.cbY6);
            this.groupBox5.Controls.Add(this.checkBox21);
            this.groupBox5.Controls.Add(this.cbY5);
            this.groupBox5.Controls.Add(this.checkBox19);
            this.groupBox5.Controls.Add(this.cbY4);
            this.groupBox5.Controls.Add(this.checkBox17);
            this.groupBox5.Controls.Add(this.cbY3);
            this.groupBox5.Controls.Add(this.checkBox15);
            this.groupBox5.Controls.Add(this.cbY2);
            this.groupBox5.Controls.Add(this.checkBox14);
            this.groupBox5.Controls.Add(this.cbY1);
            this.groupBox5.Controls.Add(this.checkBox12);
            this.groupBox5.Controls.Add(this.cbY0);
            this.groupBox5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(28, 157);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1105, 356);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "I/O控制卡输入输出";
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.RosyBrown;
            this.button12.Location = new System.Drawing.Point(463, 157);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(341, 79);
            this.button12.TabIndex = 17;
            this.button12.Text = "关闭光源";
            this.button12.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gold;
            this.label4.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(285, 48);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(172, 49);
            this.label4.TabIndex = 5;
            this.label4.Text = "光源亮度";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(463, 52);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(587, 45);
            this.trackBar1.TabIndex = 16;
            // 
            // checkBox25
            // 
            this.checkBox25.AutoSize = true;
            this.checkBox25.Location = new System.Drawing.Point(182, 297);
            this.checkBox25.Name = "checkBox25";
            this.checkBox25.Size = new System.Drawing.Size(54, 29);
            this.checkBox25.TabIndex = 15;
            this.checkBox25.Text = "X7";
            this.checkBox25.UseVisualStyleBackColor = true;
            // 
            // cbY7
            // 
            this.cbY7.AutoSize = true;
            this.cbY7.Location = new System.Drawing.Point(36, 297);
            this.cbY7.Name = "cbY7";
            this.cbY7.Size = new System.Drawing.Size(53, 29);
            this.cbY7.TabIndex = 14;
            this.cbY7.Text = "Y7";
            this.cbY7.UseVisualStyleBackColor = true;
            this.cbY7.CheckedChanged += new System.EventHandler(this.cbY7_CheckedChanged);
            // 
            // checkBox23
            // 
            this.checkBox23.AutoSize = true;
            this.checkBox23.Location = new System.Drawing.Point(182, 262);
            this.checkBox23.Name = "checkBox23";
            this.checkBox23.Size = new System.Drawing.Size(54, 29);
            this.checkBox23.TabIndex = 13;
            this.checkBox23.Text = "X6";
            this.checkBox23.UseVisualStyleBackColor = true;
            // 
            // cbY6
            // 
            this.cbY6.AutoSize = true;
            this.cbY6.Location = new System.Drawing.Point(36, 262);
            this.cbY6.Name = "cbY6";
            this.cbY6.Size = new System.Drawing.Size(53, 29);
            this.cbY6.TabIndex = 12;
            this.cbY6.Text = "Y6";
            this.cbY6.UseVisualStyleBackColor = true;
            this.cbY6.CheckedChanged += new System.EventHandler(this.cbY6_CheckedChanged);
            // 
            // checkBox21
            // 
            this.checkBox21.AutoSize = true;
            this.checkBox21.Location = new System.Drawing.Point(182, 227);
            this.checkBox21.Name = "checkBox21";
            this.checkBox21.Size = new System.Drawing.Size(54, 29);
            this.checkBox21.TabIndex = 11;
            this.checkBox21.Text = "X5";
            this.checkBox21.UseVisualStyleBackColor = true;
            // 
            // cbY5
            // 
            this.cbY5.AutoSize = true;
            this.cbY5.Location = new System.Drawing.Point(36, 227);
            this.cbY5.Name = "cbY5";
            this.cbY5.Size = new System.Drawing.Size(53, 29);
            this.cbY5.TabIndex = 10;
            this.cbY5.Text = "Y5";
            this.cbY5.UseVisualStyleBackColor = true;
            this.cbY5.CheckedChanged += new System.EventHandler(this.cbY5_CheckedChanged);
            // 
            // checkBox19
            // 
            this.checkBox19.AutoSize = true;
            this.checkBox19.Location = new System.Drawing.Point(182, 192);
            this.checkBox19.Name = "checkBox19";
            this.checkBox19.Size = new System.Drawing.Size(54, 29);
            this.checkBox19.TabIndex = 9;
            this.checkBox19.Text = "X4";
            this.checkBox19.UseVisualStyleBackColor = true;
            // 
            // cbY4
            // 
            this.cbY4.AutoSize = true;
            this.cbY4.Location = new System.Drawing.Point(36, 192);
            this.cbY4.Name = "cbY4";
            this.cbY4.Size = new System.Drawing.Size(53, 29);
            this.cbY4.TabIndex = 8;
            this.cbY4.Text = "Y4";
            this.cbY4.UseVisualStyleBackColor = true;
            this.cbY4.CheckedChanged += new System.EventHandler(this.cbY4_CheckedChanged);
            // 
            // checkBox17
            // 
            this.checkBox17.AutoSize = true;
            this.checkBox17.Location = new System.Drawing.Point(182, 157);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(54, 29);
            this.checkBox17.TabIndex = 7;
            this.checkBox17.Text = "X3";
            this.checkBox17.UseVisualStyleBackColor = true;
            // 
            // cbY3
            // 
            this.cbY3.AutoSize = true;
            this.cbY3.Location = new System.Drawing.Point(36, 157);
            this.cbY3.Name = "cbY3";
            this.cbY3.Size = new System.Drawing.Size(53, 29);
            this.cbY3.TabIndex = 6;
            this.cbY3.Text = "Y3";
            this.cbY3.UseVisualStyleBackColor = true;
            this.cbY3.CheckedChanged += new System.EventHandler(this.cbY3_CheckedChanged);
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Location = new System.Drawing.Point(182, 122);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(54, 29);
            this.checkBox15.TabIndex = 5;
            this.checkBox15.Text = "X2";
            this.checkBox15.UseVisualStyleBackColor = true;
            // 
            // cbY2
            // 
            this.cbY2.AutoSize = true;
            this.cbY2.Location = new System.Drawing.Point(36, 122);
            this.cbY2.Name = "cbY2";
            this.cbY2.Size = new System.Drawing.Size(53, 29);
            this.cbY2.TabIndex = 4;
            this.cbY2.Text = "Y2";
            this.cbY2.UseVisualStyleBackColor = true;
            this.cbY2.CheckedChanged += new System.EventHandler(this.cbY2_CheckedChanged);
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Location = new System.Drawing.Point(182, 87);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(54, 29);
            this.checkBox14.TabIndex = 3;
            this.checkBox14.Text = "X1";
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // cbY1
            // 
            this.cbY1.AutoSize = true;
            this.cbY1.Location = new System.Drawing.Point(36, 87);
            this.cbY1.Name = "cbY1";
            this.cbY1.Size = new System.Drawing.Size(53, 29);
            this.cbY1.TabIndex = 2;
            this.cbY1.Text = "Y1";
            this.cbY1.UseVisualStyleBackColor = true;
            this.cbY1.CheckedChanged += new System.EventHandler(this.cbY1_CheckedChanged);
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(182, 52);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(54, 29);
            this.checkBox12.TabIndex = 1;
            this.checkBox12.Text = "X0";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // cbY0
            // 
            this.cbY0.AutoSize = true;
            this.cbY0.Checked = true;
            this.cbY0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbY0.Location = new System.Drawing.Point(36, 52);
            this.cbY0.Name = "cbY0";
            this.cbY0.Size = new System.Drawing.Size(53, 29);
            this.cbY0.TabIndex = 0;
            this.cbY0.Text = "Y0";
            this.cbY0.UseVisualStyleBackColor = true;
            this.cbY0.CheckedChanged += new System.EventHandler(this.checkBox11_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button11);
            this.groupBox4.Controls.Add(this.button10);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(710, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 134);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "气缸控制";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(168, 30);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(255, 98);
            this.button11.TabIndex = 2;
            this.button11.Text = "右边";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(19, 81);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(143, 47);
            this.button10.TabIndex = 1;
            this.button10.Text = "右边";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(19, 28);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(143, 47);
            this.button9.TabIndex = 0;
            this.button9.Text = "左边";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(282, 26);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(312, 49);
            this.textBox4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gold;
            this.label3.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 26);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(252, 49);
            this.label3.TabIndex = 1;
            this.label3.Text = "请输入当前生产的LotNum";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // user_change
            // 
            this.user_change.Controls.Add(this.groupBox6);
            this.user_change.Location = new System.Drawing.Point(4, 22);
            this.user_change.Name = "user_change";
            this.user_change.Size = new System.Drawing.Size(1147, 650);
            this.user_change.TabIndex = 3;
            this.user_change.Text = "用户切换";
            this.user_change.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.listBox1);
            this.groupBox6.Controls.Add(this.maskedTextBox1);
            this.groupBox6.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(273, 43);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(598, 394);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "用户切换";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 28);
            this.label6.TabIndex = 3;
            this.label6.Text = "密码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 28);
            this.label5.TabIndex = 2;
            this.label5.Text = "用户";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 28;
            this.listBox1.Location = new System.Drawing.Point(157, 131);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(247, 32);
            this.listBox1.TabIndex = 1;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(157, 191);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(247, 35);
            this.maskedTextBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 676);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "条码数据管理系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.main_run.ResumeLayout(false);
            this.main_run.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gpStatisticBox.ResumeLayout(false);
            this.gpStatisticBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.setting_test.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.light_control.ResumeLayout(false);
            this.light_control.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.user_change.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage main_run;
        private System.Windows.Forms.TabPage setting_test;
        private System.Windows.Forms.TabPage light_control;
        private System.Windows.Forms.TabPage user_change;
        private System.Windows.Forms.Panel panel1;//main_run页面下的面版
        private System.Windows.Forms.Label lbWorker_num;//工号lable
        private System.Windows.Forms.TextBox txWorker_num;//工号lable对应的text框
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbLot_num;//Lot号码lable
        private System.Windows.Forms.TextBox txLot_num;//Lot号码的输入框
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.RichTextBox txInfoarea;
        private System.Windows.Forms.RichTextBox txLotarea;
        private System.Windows.Forms.Button bnChanguser;
        private System.Windows.Forms.Button bnCloseLight;
        private System.Windows.Forms.Button bnSavecode;
        private System.Windows.Forms.Button bnReset;
        private System.Windows.Forms.Button bnStart;
        private System.Windows.Forms.GroupBox gpStatisticBox;
        private System.Windows.Forms.TextBox tbUPH;
        private System.Windows.Forms.TextBox tbFailed;
        private System.Windows.Forms.TextBox tbPassed;
        private System.Windows.Forms.Label lbUPH;
        private System.Windows.Forms.Label lbFailed;
        private System.Windows.Forms.Label lbPassed;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private Button button2;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button6;
        private TextBox textBox3;
        private Label label2;
        private RichTextBox richTextBox1;
        private Button button7;
        private Button button8;
        private TextBox textBox4;
        private Label label3;
        private Button button9;
        private GroupBox groupBox4;
        private Button button10;
        private Button button11;
        private GroupBox groupBox5;
        private CheckBox checkBox19;
        private CheckBox cbY4;
        private CheckBox checkBox17;
        private CheckBox cbY3;
        private CheckBox checkBox15;
        private CheckBox cbY2;
        private CheckBox checkBox14;
        private CheckBox cbY1;
        private CheckBox checkBox12;
        private CheckBox cbY0;
        private CheckBox checkBox23;
        private CheckBox cbY6;
        private CheckBox checkBox21;
        private CheckBox cbY5;
        private CheckBox checkBox25;
        private CheckBox cbY7;
        private TrackBar trackBar1;
        private Label label4;
        private Button button13;
        private Button button12;
        private CheckBox checkBox27;
        private GroupBox groupBox6;
        private Label label6;
        private Label label5;
        private ListBox listBox1;
        private MaskedTextBox maskedTextBox1;
    }
}

