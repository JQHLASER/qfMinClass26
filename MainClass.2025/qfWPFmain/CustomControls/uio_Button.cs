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
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:qfWPFmain.CustomControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:qfWPFmain.CustomControls;assembly=qfWPFmain.CustomControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:uio_Button/>
    ///
    /// </summary>
    public class uio_Button : TextBox
    {
        static uio_Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(uio_Button), new FrameworkPropertyMetadata(typeof(uio_Button)));

            // 重写Text属性的元数据，添加我们自己的回调函数
            WidthProperty.OverrideMetadata(typeof(uio_Button),
                new FrameworkPropertyMetadata(200d,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    On_Width_Changed));

            HeightProperty.OverrideMetadata(typeof(uio_Button),
              new FrameworkPropertyMetadata(40d,
                  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        


        }
       
        public static readonly DependencyProperty CornerRadiusProperty =
                DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(uio_Button),
                new FrameworkPropertyMetadata(new CornerRadius(5), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, On_CornerRadius_Changed));
        private static void On_CornerRadius_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void On_Width_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }




        #region 属性


        /// <summary>
        /// 圆角半径
        /// </summary>
        [Category("ui")]
        [Description("圆角半径")]
        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }


        /// <summary>
        /// 宽度
        /// </summary>
        [Category("ui")]
        [Description("宽度")]
        public new double Width
        {
            get
            {
                return (double)GetValue(WidthProperty );
            }
            set
            {
                SetValue(WidthProperty, value);
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        [Category("ui")]
        [Description("高度")]
        public new double Height
        {
            get
            {
                return (double)GetValue(HeightProperty );
            }
            set
            {
                SetValue(HeightProperty, value);
            }
        }




        #endregion

    }
}
