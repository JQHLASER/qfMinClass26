using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// ui_IO.xaml 的交互逻辑
    /// </summary>
    public partial class ui_IO : UserControl
    {

        public ui_IO()
        {
            InitializeComponent();

        }

        #region 名称

        public static readonly DependencyProperty ui_Name_0Property =
       DependencyProperty.Register(nameof(ui_Name_0), typeof(string), typeof(ui_IO),
           new FrameworkPropertyMetadata("0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name0_Changed));
        private static void ui_Name0_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_0.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_1Property =
      DependencyProperty.Register(nameof(ui_Name_1), typeof(string), typeof(ui_IO),
          new FrameworkPropertyMetadata("1", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name1_Changed));
        private static void ui_Name1_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_1.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_2Property =
   DependencyProperty.Register(nameof(ui_Name_2), typeof(string), typeof(ui_IO),
       new FrameworkPropertyMetadata("2", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name2_Changed));
        private static void ui_Name2_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_2.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_3Property =
 DependencyProperty.Register(nameof(ui_Name_3), typeof(string), typeof(ui_IO),
     new FrameworkPropertyMetadata("3", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name3_Changed));
        private static void ui_Name3_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_3.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_4Property =
DependencyProperty.Register(nameof(ui_Name_4), typeof(string), typeof(ui_IO),
new FrameworkPropertyMetadata("4", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name4_Changed));
        private static void ui_Name4_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_4.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_5Property =
DependencyProperty.Register(nameof(ui_Name_5), typeof(string), typeof(ui_IO),
new FrameworkPropertyMetadata("5", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name5_Changed));
        private static void ui_Name5_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_5.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_6Property =
DependencyProperty.Register(nameof(ui_Name_6), typeof(string), typeof(ui_IO),
new FrameworkPropertyMetadata("6", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name6_Changed));
        private static void ui_Name6_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_6.ui_Content = e.NewValue.ToString();
        }

        public static readonly DependencyProperty ui_Name_7Property =
DependencyProperty.Register(nameof(ui_Name_7), typeof(string), typeof(ui_IO),
new FrameworkPropertyMetadata("7", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Name7_Changed));
        private static void ui_Name7_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_7.ui_Content = e.NewValue.ToString();
        }






        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_0
        {
            get
            {
                return (string)GetValue(ui_Name_0Property);
            }
            set
            {
                SetValue(ui_Name_0Property, value);

            }
        }

        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_1
        {
            get
            {
                return (string)GetValue(ui_Name_1Property);
            }
            set
            {
                SetValue(ui_Name_1Property, value);

            }
        }

        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_2
        {
            get
            {
                return (string)GetValue(ui_Name_2Property);
            }
            set
            {
                SetValue(ui_Name_2Property, value);

            }
        }


        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_3
        {
            get
            {
                return (string)GetValue(ui_Name_3Property);
            }
            set
            {
                SetValue(ui_Name_3Property, value);

            }
        }
        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_4
        {
            get
            {
                return (string)GetValue(ui_Name_4Property);
            }
            set
            {
                SetValue(ui_Name_4Property, value);

            }
        }

        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_5
        {
            get
            {
                return (string)GetValue(ui_Name_5Property);
            }
            set
            {
                SetValue(ui_Name_5Property, value);

            }
        }

        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_6
        {
            get
            {
                return (string)GetValue(ui_Name_6Property);
            }
            set
            {
                SetValue(ui_Name_6Property, value);

            }
        }

        [Category("ui")]
        [Description("文本内容")]
        public string ui_Name_7
        {
            get
            {
                return (string)GetValue(ui_Name_7Property);
            }
            set
            {
                SetValue(ui_Name_7Property, value);

            }
        }


        #endregion


        #region Width

        public static readonly DependencyProperty ui_Width_Property =
DependencyProperty.Register(nameof(ui_Width), typeof(double), typeof(ui_IO),
new FrameworkPropertyMetadata(50d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Width_Changed));
        private static void ui_Width_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_0.Width = (double)e.NewValue;
            control._IO_1.Width = (double)e.NewValue;
            control._IO_2.Width = (double)e.NewValue;
            control._IO_3.Width = (double)e.NewValue;
            control._IO_4.Width = (double)e.NewValue;
            control._IO_5.Width = (double)e.NewValue;
            control._IO_6.Width = (double)e.NewValue;
            control._IO_7.Width = (double)e.NewValue;
        }




        [Category("ui")]
        [Description("")]
        public double ui_Width
        {
            get
            {
                return (double)GetValue(ui_Width_Property);
            }
            set
            {
                SetValue(ui_Width_Property, value);

            }
        }


        #endregion



        #region Value

        public static readonly DependencyProperty ui_Value0_Property =
DependencyProperty.Register(nameof(ui_Value0), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value0_Changed));
        private static void ui_Value0_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_0.ui_IsChecked = ((bool)e.NewValue);  
        }

        public static readonly DependencyProperty ui_Value1_Property =
DependencyProperty.Register(nameof(ui_Value1), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value1_Changed));
        private static void ui_Value1_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_1.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value2_Property =
DependencyProperty.Register(nameof(ui_Value2), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value2_Changed));
        private static void ui_Value2_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_2.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value3_Property =
DependencyProperty.Register(nameof(ui_Value3), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value3_Changed));
        private static void ui_Value3_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_3.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value4_Property =
DependencyProperty.Register(nameof(ui_Value4), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value4_Changed));
        private static void ui_Value4_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_4.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value5_Property =
DependencyProperty.Register(nameof(ui_Value5), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value5_Changed));
        private static void ui_Value5_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_5.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value6_Property =
DependencyProperty.Register(nameof(ui_Value6), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value6_Changed));
        private static void ui_Value6_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_6.ui_IsChecked = ((bool)e.NewValue);
        }
        public static readonly DependencyProperty ui_Value7_Property =
DependencyProperty.Register(nameof(ui_Value7), typeof(bool), typeof(ui_IO),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ui_Value7_Changed));
        private static void ui_Value7_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_IO control = d as ui_IO;
            control._IO_7.ui_IsChecked = ((bool)e.NewValue);
        }



        [Category("ui")]
        [Description("值")]
        public bool ui_Value0
        {
            get
            {
                return (bool)GetValue(ui_Value0_Property);
            }
            set
            {
                SetValue(ui_Value0_Property, value);

            }
        }

        [Category("ui")]
        [Description("值")]
        public bool ui_Value1
        {
            get
            {
                return (bool)GetValue(ui_Value1_Property);
            }
            set
            {
                SetValue(ui_Value1_Property, value);

            }
        }

        [Category("ui")]
        [Description("值")]
        public bool ui_Value2
        {
            get
            {
                return (bool)GetValue(ui_Value2_Property);
            }
            set
            {
                SetValue(ui_Value2_Property, value);

            }
        }


        [Category("ui")]
        [Description("值")]
        public bool ui_Value3
        {
            get
            {
                return (bool)GetValue(ui_Value3_Property);
            }
            set
            {
                SetValue(ui_Value3_Property, value);

            }
        }


        [Category("ui")]
        [Description("值")]
        public bool ui_Value4
        {
            get
            {
                return (bool)GetValue(ui_Value3_Property);
            }
            set
            {
                SetValue(ui_Value3_Property, value);

            }
        }


        [Category("ui")]
        [Description("值")]
        public bool ui_Value5
        {
            get
            {
                return (bool)GetValue(ui_Value5_Property);
            }
            set
            {
                SetValue(ui_Value5_Property, value);

            }
        }


        [Category("ui")]
        [Description("值")]
        public bool ui_Value6
        {
            get
            {
                return (bool)GetValue(ui_Value6_Property);
            }
            set
            {
                SetValue(ui_Value6_Property, value);

            }
        }



        [Category("ui")]
        [Description("值")]
        public bool ui_Value7
        {
            get
            {
                return (bool)GetValue(ui_Value7_Property);
            }
            set
            {
                SetValue(ui_Value7_Property, value);

            }
        }



        #endregion


        #region 事件

        public event Action<ushort > Event_操作;
        void On_操作(ushort port )
        {     
            Event_操作?.Invoke(port );            
        }






        #endregion


        private void CheckBox_0_PreviewMouseLeftButtonUp()
        {
            On_操作(0 );
        }
        private void CheckBox_1_PreviewMouseLeftButtonUp()
        {
            On_操作(1 );
        }
        private void CheckBox_2_PreviewMouseLeftButtonUp()
        {
            On_操作(2 );
        }
        private void CheckBox_3_PreviewMouseLeftButtonUp()
        {
            On_操作(3 );
        }
        private void CheckBox_4_PreviewMouseLeftButtonUp()
        {
            On_操作(4 );
        }
        private void CheckBox_5_PreviewMouseLeftButtonUp()
        {
            On_操作(5 );
        }
        private void CheckBox_6_PreviewMouseLeftButtonUp()
        {
            On_操作(6 );
        }
        private void CheckBox_7_PreviewMouseLeftButtonUp()
        {
            On_操作(7 );
        }

    }
}
