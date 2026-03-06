using SqlSugar;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class 导入_导出<T>
    {
        /// <summary>
        /// 反回  DialogResult.OK:成功,No:失败
        /// </summary> 
        public (DialogResult dlt, string msg, string 文件名称) 导出(T t, string 后缀 = "*.ave")
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = Language_.Get语言("导出");
            save.Filter = $"|{后缀}";
            var dlt = save.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string path = save.FileName;
                var rt = new qfmain.文件_文件夹().WriteReadJson(path, 0, ref t, out string msgErr);
                  dlt = rt ? DialogResult.OK : DialogResult.No;
                new qfmain.文件_文件夹().文件_获取文件名_不含后缀(path, out string name, out string msgErr1);
                return (dlt, msgErr, name);
            }
            return (dlt, "", "");
        }

        /// <summary>
        /// 反回  DialogResult.OK:成功,No:失败
        /// </summary> 
        public (DialogResult dlt, string msg, T cfg, string 文件名称) 导入(string 后缀 = "*.ave")
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = Language_.Get语言("导入");
            T t = qfmain.T_实例化泛型.FastNew<T>.Create();
            var dlt = open.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string path = open.FileName;
                var rt = new qfmain.文件_文件夹().WriteReadJson(path, 1, ref t, out string msgErr);
                new qfmain.文件_文件夹().文件_获取文件名_不含后缀(path, out string name, out string msgErr1);
                dlt = rt ? DialogResult.OK : DialogResult.No;
                return (dlt, msgErr, t, name);
            }
            return (dlt , "", t, "");
        }


        /// <summary>
        /// 反回  DialogResult.OK:成功,No:失败
        /// </summary> 
        public (DialogResult dlt, string msg, string 文件名称) 导出全部(T[] t, string 后缀 = "*.aveAll")
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = Language_.Get语言("导出");
            save.Filter = $"|{后缀}";
            var dlt = save.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string path = save.FileName;
                var rt = new qfmain.文件_文件夹().WriteReadJson(path, 0, ref t, out string msgErr);
                dlt = rt ? DialogResult.OK : DialogResult.No;
                new qfmain.文件_文件夹().文件_获取文件名_不含后缀(path, out string name, out string msgErr1);
                return (dlt, msgErr, name);
            }
            return (dlt, "", "");
        }

        /// <summary>
        /// 反回  DialogResult.OK:成功,No:失败
        /// </summary> 
        public (DialogResult dlt, string msg, T[] cfg, string 文件名称) 导入全部(string 后缀 = "*.aveAll")
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = Language_.Get语言("导入");
            T[] cfg = new T[0];
            var dlt = open.ShowDialog();
            if (dlt == DialogResult.OK)
            {
                string path = open.FileName;
                var rt = new qfmain.文件_文件夹().WriteReadJson(path, 1, ref cfg, out string msgErr);
                new qfmain.文件_文件夹().文件_获取文件名_不含后缀(path, out string name, out string msgErr1);
                dlt = rt ? DialogResult.OK : DialogResult.No;
                return (dlt, msgErr, cfg, name);
            }
            return (dlt, "", cfg,"");
        }


    }
}
