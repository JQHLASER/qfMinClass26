using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public interface Iwork_文件_<T>
    {
        qfmain._初始化状态_ _初始化状态 { set; get; }

        event Action<qfmain._初始化状态_, string> Event_初始化状态;



        void 初始化(string File, string 文件类型, string 后缀);

        string 获取文件路径(string FileName);
        bool 文件是否存在(string fileName);
        bool 读写(string FileName, ushort model, ref T t, out string msgerr);


        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        DialogResult 打开_弹窗(ref T t, out string FileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null);

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// <para>FileName:源文件名称,为空时为弹窗保存</para>
        /// </summary> 
        DialogResult 另存为_弹窗(string FileName, T t, out string NewFileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null);

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// 文件名不为空时直接保存
        /// <para>文件名为空时弹窗另存为</para>
        /// </summary> 
        DialogResult 保存_弹窗(string FileName, T t, out string NewFileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null);
        bool 打开(string FileName, ref T t, out string msgerr);
        bool 保存(string FileName, T t, out string msgerr);
        /// <summary>
        /// <para>FileName:源文件名称</para>
        /// <para>NewFileName:新文件名称</para>
        /// </summary> 
        bool 另存为(string FileName, string NewFileName, out string msgErr);
        bool 删除(string FileName, out string msgErr);


    }
}
