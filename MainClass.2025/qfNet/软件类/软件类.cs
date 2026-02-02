using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class 软件类
    {
        /// <summary>
        /// 弹出帮助窗体,显示help.txt信息
        /// </summary>
        /// <param name="信息"></param>
        /// <param name="版本信息"></param>
        public void Win_显示信息(string 信息, string Title = "")
        {
            using (Form_查看信息 form = new Form_查看信息(Title, 信息, Color.Black))
            {
                form.ShowDialog();
            }


        }

        /// <summary>
        /// 弹出帮助窗体,显示help.txt信息
        /// </summary>
        /// <param name="信息"></param>
        /// <param name="版本信息"></param>
        public void Win_软件信息及help(string[] 信息, string 版本信息 = "(QF.L)250910.5")
        {

            StringBuilder sb = new StringBuilder();
            foreach (var s in 信息)
            {
                sb.Append(s + "\r\n");
            }
            string path = $"{qfmain.软件类.Files_Cfg.Files_help}\\help.txt";
            if (!new qfmain.文件_文件夹().文件_是否存在(path))
            {
                new qfmain.文本().Save_25(path, "help", true, out string msgErr1);
            }
            new qfmain.文本().Read_25(path, out string vxt, out string msgErr);
            sb.Append(vxt);

            Win_显示信息(sb.ToString(), 版本信息);

        }


        /// <summary>
        /// 密码正确时返回 DialogResult = DialogResult.Yes
        /// </summary>
        /// <param name="正确的密码"></param>
        /// <param name="Title"></param>
        /// <param name="是否可以关闭"></param>
        /// <returns></returns>
        public DialogResult Win_密码输入框(string 正确的密码, string Title = "Password", bool 是否可以关闭 = true)
        {
            DialogResult result = DialogResult.None;
            using (Form_密码输入框 forms = new Form_密码输入框(Title, 正确的密码, 是否可以关闭))
            {
                result = forms.ShowDialog();
            }

            return result;
        }

        /// <summary>
        /// <para>File : 文件夹路径</para>
        /// <para>文件类型 : 文件类型 | 前面的内容</para>
        /// <para>FileName : 选中的文件路径</para>
        /// </summary> 
        public DialogResult Win_文件类弹窗(string File, string 文件类型, string 后缀, out string FileName, _文件弹窗类型_ 类型 = _文件弹窗类型_.打开, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            DialogResult dlt = DialogResult.None;
            FileName = string.Empty;
            bool rt = new qfmain.文件_文件夹().文件_获取_文件夹下所有文件名(File, out string[] 目录, out string msgerr, 后缀);
            if (rt)
            {
                using (Form_文件_弹窗 forms = new Form_文件_弹窗(目录, 文件类型, 后缀, 类型, Event_删除文件))
                {
                    dlt = forms.ShowDialog();
                    FileName = forms._selectedFileName; 
                }
            }
            return dlt;
        }


        /// <summary>
        /// <para>文件目录 : 文件目录</para>
        /// <para>文件类型 : 文件类型 | 前面的内容</para>
        /// <para>FileName : 选中的文件路径</para>
        /// <para>Event_删除文件 : 删除文件事件,参数(要删除的文件名),返回(状态,错误消息)</para>
        /// </summary> 
        public (DialogResult s, string 文件名) Win_文件类弹窗(string[] 文件目录, string 文件类型, string 后缀, _文件弹窗类型_ 类型 = _文件弹窗类型_.打开, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            using (Form_文件_弹窗 forms = new Form_文件_弹窗(文件目录, 文件类型, 后缀, 类型, Event_删除文件))
            {
                DialogResult dlt = forms.ShowDialog();
                string FileName = forms._selectedFileName;
                return (dlt, FileName);
            }

        }




    }
}
