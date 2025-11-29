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
    /// ui_CheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class ui_CheckBox : UserControl
    {
        public ui_CheckBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ui_ContentProperty =
DependencyProperty.Register(nameof(ui_Content), typeof(string), typeof(ui_CheckBox),
new FrameworkPropertyMetadata("CheckBox", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Content_Changed));
        private static void On_ui_Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.Content = e.NewValue;
        }

        public static readonly DependencyProperty ui_IsCheckedProperty =
DependencyProperty.Register(nameof(ui_IsChecked), typeof(bool), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_IsChecked_Changed));
        private static void On_ui_IsChecked_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.IsChecked = (bool)e.NewValue;
        }


        public static readonly DependencyProperty ui_FontSizeProperty =
DependencyProperty.Register(nameof(ui_FontSize), typeof(double), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(12d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontSize_Changed));
        private static void On_ui_FontSize_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.FontSize = (double)e.NewValue;
        }


        public static readonly DependencyProperty ui_FontFamilyProperty =
DependencyProperty.Register(nameof(ui_FontFamily), typeof(FontFamily), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(new FontFamily("微软雅黑"), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_FontFamily_Changed));
        private static void On_ui_FontFamily_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.FontFamily = (FontFamily)e.NewValue;
        }


        public static readonly DependencyProperty ui_ThicknessProperty =
DependencyProperty.Register(nameof(ui_Thickness), typeof(Thickness), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(new Thickness(1.5), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Thickness_Changed));
        private static void On_ui_Thickness_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.BorderThickness = (Thickness)e.NewValue;
        }

        public static readonly DependencyProperty ui_BorderBrushProperty =
DependencyProperty.Register(nameof(ui_BorderBrush), typeof(Brush), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_BorderBrush_Changed));
        private static void On_ui_BorderBrush_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.BorderBrush = (Brush)e.NewValue;
        }


        public static readonly DependencyProperty ui_ForegroundProperty =
DependencyProperty.Register(nameof(ui_Foreground), typeof(Brush), typeof(ui_CheckBox),
new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_ui_Foreground_Changed));
        private static void On_ui_Foreground_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ui_CheckBox control = d as ui_CheckBox;
            control._CheckBox.Foreground = (Brush)e.NewValue;
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
        public bool ui_IsChecked
        {
            get
            {
                return (bool)GetValue(ui_IsCheckedProperty);
            }
            set
            {
                SetValue(ui_IsCheckedProperty, value);

            }
        }

        [Category("ui")]
        [Description("")]
        public string ui_Content
        {
            get
            {
                return (string)GetValue(ui_ContentProperty);
            }
            set
            {
                SetValue(ui_ContentProperty, value);

            }
        }

        private void _CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.ui_IsChecked = true;
            Event_CheckedChanged?.Invoke(true);

        }

        public event Action<bool> Event_CheckedChanged;
        public event Action Event_PreviewMouseLeftButtonUp;
        private void _CheckBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
         
            Event_PreviewMouseLeftButtonUp?.Invoke();

        }

        private void _CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ui_IsChecked = false;
            Event_CheckedChanged?.Invoke(false);

        }
    }
}
