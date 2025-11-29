using System;
using System.Collections.Generic;
using System.Drawing;
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
        public void Win_帮助及软件信息(string 信息, string 版本信息 = "(QF.L)250910.5")
        {
            using (Form_查看信息 form = new Form_查看信息(版本信息, 信息, Color.Black))
            {
                form.ShowDialog();
            }


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
        public DialogResult Win_文件类弹窗(string File, string 文件类型, string 后缀, out string FileName, _文件弹窗类型_ 类型 = _文件弹窗类型_.打开)
        {
            using (Label lab = new Label())
            {
                using (Form_文件_弹窗 forms = new Form_文件_弹窗(lab, File, 文件类型, 后缀, 类型))
                {
                    DialogResult dlt = forms.ShowDialog();
                    FileName = lab.Text;
                    return dlt;
                }
            }
        }

    }
}
