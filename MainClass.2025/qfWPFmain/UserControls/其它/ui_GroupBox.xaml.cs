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
    /// ui_GroupBox.xaml 的交互逻辑
    /// </summary>
    public partial class ui_GroupBox : UserControl
    {
        public ui_GroupBox()
        {
            InitializeComponent();

        }

        #region 属性


        private string HeaderUi_ = "GroupBox";
        /// <summary>
        /// 显示文本
        /// </summary>
        [Category("ui")]
        [Description("显示文本")]
        public string ui_Header
        {
            get
            {
                return HeaderUi_;
            }
            set
            {
                HeaderUi_ = value;
             this.GroupBox.Header = HeaderUi_; 
            }
        }

        private FontFamily FontFamilyUi_ = new FontFamily("微软雅黑");
        /// <summary>
        /// 字体
        /// </summary>
        [Description("字体")]
        public FontFamily ui_FontFamily
        {
            get
            {
                return FontFamilyUi_;
            }
            set
            {
                FontFamilyUi_ = value;
             this.GroupBox.FontFamily = FontFamilyUi_; 
            }
        }

        private double FontSizeUi_ = 14;
        /// <summary>
        /// 字体大小
        /// </summary>
        [Description("字体大小")]
        public double ui_FontSize
        {
            get
            {
                return FontSizeUi_;
            }
            set
            {
                FontSizeUi_ = value;
             this.GroupBox.FontSize = FontSizeUi_;  
            }
        }

        private Brush ForegroundUi_ = Brushes.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [Description("文本颜色")]
        public Brush ui_Foreground
        {
            get
            {
                return ForegroundUi_;
            }
            set
            {
                ForegroundUi_ = value;
             this.GroupBox.Foreground = ForegroundUi_; 
            }
        }


        private Brush BackgroundUi_ = Brushes.White;
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Description("背景颜色")]
        public Brush ui_Background
        {
            get
            {
                return BackgroundUi_;
            }
            set
            {
                BackgroundUi_ = value;
             this.GroupBox.Background = BackgroundUi_;  
            }
        }

        private Brush BorderBrushUi_ = Brushes.Silver;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色")]
        public Brush ui_BorderBrush
        {
            get
            {
                return BorderBrushUi_;
            }
            set
            {
                BorderBrushUi_ = value;
             this.GroupBox.BorderBrush = BorderBrushUi_;  
            }
        }

        private Thickness BorderThicknessUi_ = new Thickness(1);
        /// <summary>
        /// 边框宽度
        /// </summary>
        [Description("边框宽度")]
        public Thickness ui_BorderThickness
        {
            get
            {
                return BorderThicknessUi_;
            }
            set
            {
                BorderThicknessUi_ = value;
             this.GroupBox.BorderThickness = BorderThicknessUi_;  
            }
        }


        private VerticalAlignment VerticalAlignmentUi_ = VerticalAlignment.Stretch;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Description("垂直对齐")]
        public VerticalAlignment ui_VerticalAlignment
        {
            get
            {
                return VerticalAlignmentUi_;
            }
            set
            {
                VerticalAlignmentUi_ = value;
             this.GroupBox.VerticalAlignment = VerticalAlignmentUi_;  
            }
        }


        private HorizontalAlignment HorizontalAlignmentUi_ = HorizontalAlignment.Stretch;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Description("水平对齐")]
        public HorizontalAlignment ui_HorizontalAlignment
        {
            get
            {
                return HorizontalAlignmentUi_;
            }
            set
            {
                HorizontalAlignmentUi_ = value;
             this.GroupBox.HorizontalAlignment = HorizontalAlignmentUi_;  
            }
        }


        private Thickness PaddingUi_ = new Thickness(5);
        /// <summary>
        /// Padding
        /// </summary>
        [Description("Padding")]
        public Thickness ui_Padding
        {
            get
            {
                return PaddingUi_;
            }
            set
            {
                PaddingUi_ = value;
             this.GroupBox.Padding = PaddingUi_;  
            }
        }

        private Thickness MarginUi_ = new Thickness(0);
        /// <summary>
        /// Margin
        /// </summary>
        [Description("Margin")]
        public Thickness ui_Margin
        {
            get
            {
                return MarginUi_;
            }
            set
            {
                MarginUi_ = value;
             this.GroupBox.Margin = MarginUi_;  
            }
        }


        private FontWeight FontWeightUi_ = FontWeights.SemiBold;
        /// <summary>
        /// 字体粗细,FontWeights. SemiBold
        /// </summary>
        [Description("字体粗细,FontWeights. SemiBold")]
        public FontWeight ui_FontWeight
        {
            get
            {
                return FontWeightUi_;
            }
            set
            {
                FontWeightUi_ = value;
             this.GroupBox.FontWeight = FontWeightUi_; 
            }
        }






        #endregion

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.GroupBox.Focus();
        }
    }
}
