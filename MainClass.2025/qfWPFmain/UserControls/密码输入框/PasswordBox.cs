using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    public class PasswordBox
    {
        /// <summary>
        /// 反馈: Yes=密码正常,No=未输入密码退出
        /// </summary>
        /// <param name="d"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window d, string password,string Caption="请输入密码")
        {
            return new win_passwordBox(password, Caption) { Owner = Window.GetWindow(d) }.ShowDialog();
        }


        /// <summary>
        /// 反馈: Yes=密码正常,No=未输入密码退出
        /// </summary>
        /// <param name="d"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string password, string Caption= "请输入密码")
        {
            return new win_passwordBox(password, Caption).ShowDialog();
        }


    }
}
