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
    /// ui_Combobox.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Combobox : UserControl
    {
        public ui_Combobox()
        {
            InitializeComponent();
        }



        public static readonly DependencyProperty ui_TextProperty =
DependencyProperty.Register(nameof(ui_Text), typeof(string), typeof(ui_Combobox),
new FrameworkPropertyMetadata("ComBobox", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Text_Changed));
        private static void On_ui_Text_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.Text = e.NewValue.ToString();
        }




        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_Combobox),
new FrameworkPropertyMetadata(14d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.FontSize = (double)e.NewValue;
        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_Combobox),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.FontFamily = (FontFamily)e.NewValue;
        }


        public static readonly DependencyProperty ui_ThicknessProperty =
DependencyProperty.Register(nameof(ui_Thickness), typeof(Thickness), typeof(ui_Combobox),
new FrameworkPropertyMetadata(new Thickness(1.5), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Thickness_Changed));
        private static void On_ui_Thickness_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.BorderThickness = (Thickness)e.NewValue;
        }

        public static readonly DependencyProperty ui_BorderBrushProperty =
DependencyProperty.Register(nameof(ui_BorderBrush), typeof(Brush), typeof(ui_Combobox),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BorderBrush_Changed));
        private static void On_ui_BorderBrush_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.BorderBrush = (Brush)e.NewValue;
        }


        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_Combobox),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.Foreground = (Brush)e.NewValue;
        }


        public static readonly DependencyProperty ui_SelectedIndexProperty =
DependencyProperty.Register(nameof(ui_SelectedIndex), typeof(int), typeof(ui_Combobox),
new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_SelectedIndex_Changed));
        private static void On_ui_SelectedIndex_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.SelectedIndex = (int)e.NewValue;
        }

        public static readonly DependencyProperty ui_ItemsSourceProperty =
DependencyProperty.Register(nameof(ui_ItemsSource), typeof(System.Collections.IEnumerable), typeof(ui_Combobox),
new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_ItemsSource_Changed));
        private static void On_ui_ItemsSource_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.ItemsSource = (System.Collections.IEnumerable)e.NewValue;
        }

        public static readonly DependencyProperty ui_HorizontalContentAlignmentProperty =
DependencyProperty.Register(nameof(ui_HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(ui_Combobox),
new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_HorizontalContentAlignment_Changed));
        private static void On_ui_HorizontalContentAlignment_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_Combobox control = d as ui_Combobox;
            control._ComBobox.HorizontalContentAlignment = (HorizontalAlignment)e.NewValue;
        }







        [Category("ui")]
        [Description("")]
        public HorizontalAlignment ui_HorizontalContentAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ui_HorizontalContentAlignmentProperty);
            }
            set
            {
                SetValue(ui_HorizontalContentAlignmentProperty, value);

            }
        }


        [Category("ui")]
        [Description("")]
        public System.Collections.IEnumerable ui_ItemsSource
        {
            get
            {
                return (System.Collections.IEnumerable)GetValue(ui_ItemsSourceProperty);
            }
            set
            {
                SetValue(ui_ItemsSourceProperty, value);

            }
        }


        [Category("ui")]
        [Description("")]
        public int ui_SelectedIndex
        {
            get
            {
                return (int)GetValue(ui_SelectedIndexProperty);
            }
            set
            {
                SetValue(ui_SelectedIndexProperty, value);

            }
        }

        [Category("ui")]
        [Description("")]
        public Brush ui_Foreground
        {
            get
            {
                return (Brush)GetValue(ui_ForegroundProperty);
            }
            set
            {
                SetValue(ui_ForegroundProperty, value);

            }
        }

        [Category("ui")]
        [Description("")]
        public Brush ui_BorderBrush
        {
            get
            {
                return (Brush)GetValue(ui_BorderBrushProperty);
            }
            set
            {
                SetValue(ui_BorderBrushProperty, value);

            }
        }


        [Category("ui")]
        [Description("")]
        public Thickness ui_Thickness
        {
            get
            {
                return (Thickness)GetValue(ui_ThicknessProperty);
            }
            set
            {
                SetValue(ui_ThicknessProperty, value);

            }
        }

        [Category("ui")]
        [Description("")]
        public FontFamily ui_FontFamily
        {
            get
            {
                return (FontFamily)GetValue(ui_FontFamilyProperty);
            }
            set
            {
                SetValue(ui_FontFamilyProperty, value);

            }
        }


        [Category("ui")]
        [Description("")]
        public double ui_FontSize
        {
            get
            {
                return (double)GetValue(ui_FontSizeProperty);
            }
            set
            {
                SetValue(ui_FontSizeProperty, value);

            }
        }



        [Category("ui")]
        [Description("")]
        public string ui_Text
        {
            get
            {
                return (string)GetValue(ui_TextProperty);
            }
            set
            {
                SetValue(ui_TextProperty, value);

            }
        }

        private void _ComBobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ui_SelectedIndex = this._ComBobox.SelectedIndex;
            this.ui_Text = this._ComBobox.Text;
            Event_SelectionChanged?.Invoke(this.ui_SelectedIndex);
        }




        public event Action<int> Event_SelectionChanged;





    }
}
