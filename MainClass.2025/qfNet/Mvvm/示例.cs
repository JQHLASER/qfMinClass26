using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace qfNet 
{
    /// <summary>
    /// model
    /// </summary>
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// viewModel
    /// </summary>
    internal class ViewMovel : qfNet.ViewModelBase
    {

        // 私有字段
        private string _userName = "rr";
        private int _userAge;
        private string _message;


        // 绑定到 View 的属性（自动通知变化）
        public string UserName
        {
            get => _userName;
            set {                
                SetProperty(ref _userName, value);
                OnPropertyChanged(); // 通知视图属性已更新...通知窗体控件,属性已更新
            }
        }

        public int UserAge
        {
            get => _userAge;
            set => SetProperty(ref _userAge, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        // 命令：按钮点击事件（简化版，实际可使用 ICommand 接口）
        public void SaveUser()
        {



            // 业务逻辑：验证并保存用户
            if (string.IsNullOrEmpty(UserName))
            {
                Message = "用户名不能为空！";
                return;
            }

            // 模拟保存到模型
            var user = new User { Name = UserName, Age = UserAge };
            Message = $"保存成功：{user.Name}（{user.Age}岁）";
        }


        // 保存命令
        public ICommand SaveCommand { get; }

    }

    //view

    //public partial class FormMain : Sunny.UI.UIForm
    //{
    //    // 视图模型实例
    //    private readonly ViewMovel _viewModel;
    //    // 自定义 DataContext 属性（用于存储绑定源）
    //    public object DataContext { get; set; }

    //    public FormMain()
    //    {
    //        InitializeComponent();

    //        // 初始化 ViewModel
    //        _viewModel = new ViewMovel();
    //        // 设置数据上下文（绑定源）
    //        this.DataContext = _viewModel;

    //        // 绑定控件到 ViewModel 属性
    //         this.DataBindings.Clear();//清绑所有绑定
    //         this.DataBindings.Remove(this.DataBindings["Text"]);//绑定前删除绑定，防止重复绑定，
    //         this.uiTextBox1.DataBindings.Add("Text", _viewModel, nameof(_viewModel.UserName), false, DataSourceUpdateMode.OnPropertyChanged);


    //        //txtName.DataBindings.Add("Text", _viewModel, nameof(_viewModel.UserName), false, DataSourceUpdateMode.OnPropertyChanged);
    //        //nudAge.DataBindings.Add("Value", _viewModel, nameof(_viewModel.UserAge), false, DataSourceUpdateMode.OnPropertyChanged);
    //        //lblMessage.DataBindings.Add("Text", _viewModel, nameof(_viewModel.Message));

    //        //// 绑定按钮点击到 ViewModel 命令
    //        this.uiButton1.Click += (s, e) => _viewModel.SaveUser();
    //        this.elementHost_Log.Child = ui_log;

    //    }


    //    private void FormMain_Load(object sender, EventArgs e)
    //    {


    //    }
    //}

}
