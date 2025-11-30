using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class 文件_<T>
    {
        string _File = qfmain.软件类.Files_Cfg.Files_LogMyApp + "\\files";
        string _后缀名 = ".fls";


        /// <summary>
        /// File : 存放文件的文件夹
        /// </summary> 
        public 文件_(string File, string 后缀名 = ".fls")
        {
            this._File = File;
            this._后缀名 = 后缀名;

            new qfmain.文件_文件夹().文件夹_新建(this._File, out string msgErr);

        }

        public string 获取文件路径(string FileName)
        {
            return this._File + $"\\{FileName}{this._后缀名}";
        }

        public bool 读写(string FileName, ushort model, ref T t, out string msgerr)
        {
            bool rt = true;
            string path = this._File + $"\\{FileName}{this._后缀名}";
            rt = new qfmain.文件_文件夹().WriteReadIni(path, model, ref t, out msgerr);
            return rt;
        }


        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        public DialogResult 打开_弹窗(ref T t, out string FileName, out string msgerr)
        {
            msgerr = string.Empty;
            FileName = string.Empty;
            DialogResult dlt = new qfNet.软件类().Win_文件类弹窗(this._File, "", this._后缀名, out FileName, _文件弹窗类型_.打开);

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
        public DialogResult 另存为_弹窗(string FileName, T t, out string NewFileName, out string msgerr)
        {
            msgerr = string.Empty;

            NewFileName = string.Empty;
            DialogResult dlt = new qfNet.软件类().Win_文件类弹窗(this._File, "", this._后缀名, out NewFileName, _文件弹窗类型_.保存);
            if (dlt == DialogResult.OK)
            {
                bool rt = true;
                if (!string.IsNullOrEmpty(FileName))
                {
                    rt = 另存为(FileName, NewFileName, t, out msgerr);
                }
                else
                {
                    rt = 保存(NewFileName, t, out msgerr);
                }

                dlt = rt ? DialogResult.Yes : DialogResult.No;                 
            }

            return dlt;


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
        public bool 另存为(string FileName, string NewFileName, T t, out string msgErr)
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



    }
}
