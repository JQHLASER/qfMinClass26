using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    internal class 文件_ini<T> : Iwork_文件_<T>
    {

        string _File = Path.Combine(qfmain.软件类.Files_Cfg.Files_LogMyApp, "gj");
        string _后缀名 = ".fls";
        string _文件类型 = "fls";
        public qfmain._初始化状态_ _初始化状态 { set; get; } = qfmain._初始化状态_.未初始化;

        /// <summary>
        /// File:存放文件的文件夹
        /// <para>文件类型 : 用于显示文件类型,如图片等 </para>
        /// <para>后缀名:文件的后缀</para>
        /// </summary> 
        public 文件_ini(string File, string 文件类型, string 后缀名 = ".fls")
        {
            On_初始化状态(qfmain._初始化状态_.初始化中 );

            this._File = File;
            this._后缀名 = 后缀名;
            this._文件类型 = 文件类型;
            new qfmain.文件_文件夹().文件夹_新建(this._File, out string msgErr);

            On_初始化状态(qfmain._初始化状态_.已初始化);

        }


        public string 获取文件路径(string FileName)
        {
            return this._File + $"\\{FileName}{this._后缀名}";
        }

        public bool 文件是否存在(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            string path = 获取文件路径(fileName);
            return new qfmain.文件_文件夹().文件_是否存在(path);
        }

        public bool 读写(string FileName, ushort model, ref T t, out string msgerr)
        {
            bool rt = true;
            string path = Path.Combine(this._File, $"{FileName}{this._后缀名}");
            rt = new qfmain.文件_文件夹().WriteReadIni(path, model, ref t, out msgerr);
            return rt;
        }


        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        public DialogResult 打开_弹窗(ref T t, out string FileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            msgerr = string.Empty;
            FileName = string.Empty;
            DialogResult dlt = new qfNet.软件类().Win_文件类弹窗(this._File, this._文件类型, this._后缀名, out FileName, _文件弹窗类型_.打开, Event_删除文件);
            if (dlt == DialogResult.OK)
            {
                bool rt = this.读写(FileName, 1, ref t, out msgerr);
                dlt = rt ? DialogResult.Yes : DialogResult.No;
            }

            return dlt;
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// <para>FileName:源文件名称,为空时为弹窗保存</para>
        /// </summary> 
        public DialogResult 另存为_弹窗(string FileName, T t, out string NewFileName, out string msgerr, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            msgerr = string.Empty;
            NewFileName = string.Empty;
            bool rt = false;
            DialogResult dlt = new qfNet.软件类().Win_文件类弹窗(this._File, this._文件类型, this._后缀名, out NewFileName, _文件弹窗类型_.保存, Event_删除文件);
            if (dlt == DialogResult.OK)
            {
                rt = true;
                if (!string.IsNullOrEmpty(FileName))
                {
                    rt = 另存为(FileName, NewFileName,  out msgerr);
                }
                else
                {
                    rt = 保存(NewFileName, t, out msgerr);
                }

                dlt = rt ? DialogResult.Yes : DialogResult.No;
            }

            return dlt;
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// 文件名不为空时直接保存
        /// <para>文件名为空时弹窗另存为</para>
        /// </summary> 
        public DialogResult 保存_弹窗(string FileName, T t, out string NewFileName, out string msgerr)
        {
            DialogResult dr = DialogResult.None;
            if (string.IsNullOrEmpty(FileName))
            {
                dr = 另存为_弹窗(FileName, t, out NewFileName, out msgerr);
            }
            else
            {
                NewFileName = FileName;
                bool rt = 保存(FileName, t, out msgerr);
                dr = rt ? DialogResult.Yes : DialogResult.No;
            }

            return dr;
        }




        public bool 打开(string FileName, ref T t, out string msgerr)
        {
            return this.读写(FileName, 1, ref t, out msgerr);
        }

        public bool 保存(string FileName, T t, out string msgerr)
        {
            return this.读写(FileName, 0, ref t, out msgerr);
        }


        /// <summary>
        /// <para>FileName:源文件名称</para>
        /// <para>NewFileName:新文件名称</para>
        /// </summary> 
        public bool 另存为(string FileName, string NewFileName,   out string msgErr)
        {
            string path = 获取文件路径(FileName);
            string Newpath = 获取文件路径(NewFileName);
            bool rt = new qfmain.文件_文件夹().文件_复制文件(path, Newpath, out msgErr);
            return rt;
        }

        public bool 删除(string FileName, out string msgErr)
        {
            string path = 获取文件路径(FileName);
            bool rt = new qfmain.文件_文件夹().文件_删除文件(path, out msgErr);
            return rt;
        }


       


        #region 事件

        public event Action<qfmain._初始化状态_> Event_初始化状态;
        private void On_初始化状态(qfmain._初始化状态_ state)
        {
            Event_初始化状态?.Invoke(state);
        }

        #endregion

    }
}
