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
    /// ui_Border.xaml 的交互逻辑
    /// </summary>
    public partial class ui_Border : UserControl
    {
        public ui_Border()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        #region 属性


        private Brush BackGroundUi_ = Brushes.White;
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("ui")]
        [Description("背景颜色")]
        public Brush ui_BackGround
        {
            get
            {
                return BackGroundUi_;
            }
            set
            {
                BackGroundUi_ = value;
                  this.Border.Background = BackGroundUi_;  
            }
        }

        private Brush BorderBrushUi_ = Brushes.Gray;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("ui")]
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
                  this.Border.BorderBrush = BorderBrushUi_;  
            }
        }

        private double CornerRadiusUi_ = 5;
        /// <summary>
        /// 圆角半径
        /// </summary>
        [Category("ui")]
        [Description("圆角半径")]
        public double ui_CornerRadius
        {
            get
            {
                return CornerRadiusUi_;
            }
            set
            {
                CornerRadiusUi_ = value;
              this.Border.CornerRadius = new CornerRadius(CornerRadiusUi_);  
            }
        }

        private Thickness BorderThicknessUi_ = new Thickness(1);
        /// <summary>
        /// 边框宽度
        /// </summary>
        [Category("ui")]
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
               this.Border.BorderThickness = BorderThicknessUi_; 
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
               this.Border.Margin = MarginUi_;  
            }
        }


        private Thickness PaddingUi_ = new Thickness(5);
        /// <summary>
        /// Padding
        /// </summary>
        [Category("ui")]
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
              this.Border.Padding = PaddingUi_; 
            }
        }



        #endregion

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            this.Border.Focus();
        }


    }
}
