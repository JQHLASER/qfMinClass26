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
    /// win_passwordBox.xaml 的交互逻辑
    /// </summary>
    public partial class win_passwordBox : Window
    {
        string password = "";


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


        public win_passwordBox(string password_, string Caption)
        {
            InitializeComponent();
            this._标题栏.Inistiall(this, false, false , false, "", false);
            this.password = password_;
            this._标题栏.ui_Text = Caption;
            this._passwordBox.Focus();
        }

        private void _buttonOk_PreviewMouseLeftButtonUp(object arg1, MouseButtonEventArgs arg2)
        {


            string pasd = this._passwordBox.GetPassword();
            if (string.IsNullOrEmpty(pasd))
            {
                MessageBox.Show(this, Language_.Get语言("密码不能为空"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (pasd != password)
            {
                MessageBox.Show(this, Language_.Get语言("密码错误"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this._selectedResult = MessageBoxResult.Yes;
            this.Close();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _buttonOk_PreviewMouseLeftButtonUp(null, null);
            }
        }


    }
}
