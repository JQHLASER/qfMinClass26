using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace qfWPFmain
{
    /// <summary>
    /// win_MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class win_MessageBox : Window
    {

        // 重写 ShowDialog() 方法，直接返回 MessageBoxResult
        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            // 根据实际按钮点击返回对应结果
            return GetResult(); // 需要自己实现 GetResult() 逻辑
        }

        private MessageBoxResult GetResult()
        {
            // 根据内部状态返回对应的 MessageBoxResult
            // 例如根据用户点击的按钮判断
            return _selectedResult; // _selectedResult 是内部存储的结果变量
        }

        private MessageBoxResult _selectedResult = MessageBoxResult.None;
        private MessageboxButton MessageboxButton = MessageboxButton.Ok;



        public win_MessageBox(string message, string Caption, MessageboxButton messageboxButton_ = MessageboxButton.Ok, MessageboxState MessageboxState_ = MessageboxState.None,bool is最大化=false  ,int width = 450, int height = 300)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, is最大化, true, false);
            this.Width = width;
            this.Height = height;



            this._Button_No.Visibility = Visibility.Collapsed;
            this._Button_Yes.Visibility = Visibility.Collapsed;
            this._Button_Ok.Visibility = Visibility.Collapsed;

            MessageboxButton = messageboxButton_;
            switch (messageboxButton_)
            {
                case MessageboxButton.Ok:
                    this._Button_Ok.Visibility = Visibility.Visible;
                    this._标题栏 .ui_IsCloseButton = true;
                    break;
                case MessageboxButton.YesNo:
                    this._Button_Yes.Visibility = Visibility.Visible;
                    this._Button_No.Visibility = Visibility.Visible;
                    this._标题栏.ui_IsCloseButton = false ;
                    break;
            }

            this._标题栏.ui_Text = Caption;
            this._消息框.Text = message;

            switch (MessageboxState_)
            {
                case MessageboxState.None:
                    break;
                case MessageboxState.Green:
                    this._标题栏.Background = Brushes.Lime;
                    break;
                case MessageboxState.Red:
                    this._标题栏.Background = Brushes.Red;
                    break;
                case MessageboxState.Yellow:
                    this._标题栏.Background = Brushes.Yellow;
                    break;
            }

        }

        //Yes
        private void _Button_Yes_PreviewMouseLeftButtonUp(object arg1, MouseButtonEventArgs arg2)
        {
           
            _selectedResult = MessageBoxResult.Yes;
            this.Close();
        }
        //No
        private void _Button_No_PreviewMouseLeftButtonUp(object arg1, MouseButtonEventArgs arg2)
        {
          
            _selectedResult = MessageBoxResult.No;
            this.Close();
        }
        //OK
        private void _Button_Ok_PreviewMouseLeftButtonUp(object arg1, MouseButtonEventArgs arg2)
        {
           
            _selectedResult = MessageBoxResult.OK;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((MessageboxButton == MessageboxButton.YesNo) && (e.Key == Key.Y || e.Key == Key.Enter))
            {
                _Button_Yes_PreviewMouseLeftButtonUp(null, null);
            }
            else if ((MessageboxButton == MessageboxButton.YesNo) && e.Key == Key.N)
            {
                _Button_No_PreviewMouseLeftButtonUp(null, null);
            }
            else if ((MessageboxButton == MessageboxButton.Ok) && (e.Key == Key.Enter || e.Key == Key.O))
            {
                _Button_Ok_PreviewMouseLeftButtonUp(null, null);
            }


        }
    }
}
