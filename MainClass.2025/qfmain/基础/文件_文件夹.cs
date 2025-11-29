using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace qfmain
{
    public class 文件_文件夹
    {

        #region DLL申明



        [DllImport("User32.dll ", EntryPoint = "SetParent")]
        private static extern IntPtr _SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll ", EntryPoint = "ShowWindow")]
        private static extern int _ShowWindow(IntPtr hwnd, int nCmdShow);
        Process p = new Process();


        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")] private static extern bool _CloseHandle(IntPtr hObject);

        private const int OF_READWRITE = 2;
        private const int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        #endregion


        #region 文件夹

        public virtual bool 文件夹_是否存在(string FilesPath, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                Directory.Exists(FilesPath);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 存在时不新建,不存在时新建
        /// </summary>
        /// <param name="FileFile"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool 文件夹_新建(string FileFile, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {
                if (!Directory.Exists(FileFile))
                {
                    Directory.CreateDirectory(FileFile);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }


            return rt;
        }

        public virtual bool 文件夹_删除(string pathFile, out string msgErr, bool 非空时是否删除 = true)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {
                Directory.Delete(pathFile, 非空时是否删除);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件夹_获取子目录(string FilesPath, out string[] 子目录, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            子目录 = new string[0];
            try
            {
                List<string> lst = new List<string>();
                DirectoryInfo root = new DirectoryInfo(FilesPath);
                DirectoryInfo[] dics = root.GetDirectories();
                foreach (DirectoryInfo item in dics)
                {
                    lst.Add(item.FullName);
                }

                子目录 = lst.ToArray();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;

        }

        public virtual bool 文件夹_获取文件夹名_不含路径(string pathFile, out string 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = string.Empty;
            try
            {
                DirectoryInfo root = new DirectoryInfo(pathFile);
                string dicName = root.Name;
                文件名 = dicName;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件夹_获取文件夹名_含路径(string pathFile, out string 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = string.Empty;
            try
            {
                DirectoryInfo root = new DirectoryInfo(pathFile);
                string dicName = root.FullName;
                文件名 = dicName;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }



        #endregion


        #region 文件


        /// <summary>
        /// 判断文件是否被进程占用,
        /// <para>=0:OK,=1:被占用,=-1:文件不存在</para>
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public virtual int 文件_是否被进程占用(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                return -1;// 不存在文件         
            }
            IntPtr handle = _lopen(fileFullName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (handle == HFILE_ERROR)
            {
                return 1;// 表示被占用           

            }
            _CloseHandle(handle);
            return 0;
        }

        /// <summary>
        ///  重载,可传窗体最大化的参数,如,System.Diagnostics.ProcessWindowStyle.Maximized
        ///  <para>快捷方式文件的路径 = @"d:\Test.lnk";</para>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="WinState">打开显示方式,如:最小化显示  System.Diagnostics.ProcessWindowStyle.Minimized</param>
        public virtual bool 文件_打开(string path, out string msgErr, string 参数 = "", System.Diagnostics.ProcessWindowStyle WinState = ProcessWindowStyle.Maximized)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true,
                    WindowStyle = WinState,//加上这句效果更好 
                    Arguments = 参数,
                };
                Process.Start(processStartInfo);

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public virtual void 文件_打开_当前目录(string path, out string msgErr)
        {
            //System.Diagnostics.Process.Start(Name, 获取_程序目录());
            文件_打开(path, out msgErr, new 程序().获取_程序目录());
        }

        public virtual bool 文件_是否存在(string path)
        {
            bool rt = File.Exists(path);
            return rt;
        }

        public virtual void 文件_新建(string path)
        {
            FileStream f = File.Create(path);
            f.Close();
            f.Dispose();
        }

        public virtual void 文件_新建(string path, Encoding encoding_ = null)
        {
            if (encoding_ is null)
            {
                encoding_ = Encoding.Default;
            }
            File.WriteAllText(path, "", encoding_);
        }

        /// <summary>
        /// 如"GB2312 ,UTF8等"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="格式">如"GB2312 ,UTF8等"</param>
        public virtual void 文件_新建_1(string path, Encoding encoding_ = null)
        {
            if (encoding_ is null)
            {
                encoding_ = Encoding.Default;
            }
            string fileName = path;
            using (StreamWriter sw = new StreamWriter(fileName, false, encoding_))
            {

            }
        }

        public virtual bool 文件_获取文件目录(string FilePath, out string[] 目录, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            目录 = new string[0];
            try
            {
                DirectoryInfo root = new DirectoryInfo(FilePath);
                FileInfo[] files = root.GetFiles();
                List<string> lst = new List<string>();
                foreach (FileInfo item in files)
                {
                    lst.Add(item.FullName);
                }
                目录 = lst.ToArray();
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }


            return rt;

        }

        /// <summary>
        /// 后缀表示:*.jpg
        /// </summary>
        /// <param name="后缀"></param>
        /// <returns></returns>
        public virtual bool 文件_获取文件名_指定后缀(string FilePath, string 后缀, out string[] 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = new string[0];
            try
            {
                文件名 = Directory.GetFiles(FilePath, 后缀);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;

        }

        /// <summary>
        /// <para>后缀: 格式... *.jpg , 为空时获取全部</para> 
        ///  <para>输出含后缀 : =true:输出的文件名含后缀,=false:输出的文件名不含后缀</para>
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="后缀"></param>
        /// <param name="文件名"></param>
        /// <param name="msgErr"></param>
        /// <param name="输出含后缀"></param>
        /// <returns></returns>
        public virtual bool 文件_获取文件名_不含路径(string FilePath, string 后缀, out string[] 文件名, out string msgErr, bool 输出含后缀 = true)
        {

            msgErr = string.Empty;
            List<string> lst = new List<string>();
            bool rt = true;
            if (!string.IsNullOrEmpty(后缀))
            {
                rt = 文件_获取文件名_指定后缀(FilePath, 后缀, out 文件名, out msgErr);
            }
            else
            {
                rt = 文件_获取_所有文件名(FilePath, out 文件名, out msgErr);
            }
            if (rt)
            {

                foreach (var s in 文件名)
                {
                    string xt = string.Empty;
                    if (输出含后缀)
                    {
                        文件_获取文件名_含后缀(s, out xt, out msgErr);
                    }
                    else
                    {
                        文件_获取文件名_不含后缀(s, out xt, out msgErr);
                    }

                    lst.Add(xt);
                }
            }

            文件名 = lst.ToArray();
            return rt;

        }

        public virtual bool 文件_获取扩展名(string path, out string 扩展名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            扩展名 = string.Empty;
            try
            {
                扩展名 = System.IO.Path.GetExtension(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件_获取文件名_含后缀(string path, out string 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = string.Empty;
            try
            {
                文件名 = System.IO.Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件_获取文件名_不含后缀(string path, out string 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = string.Empty;
            try
            {
                文件名 = System.IO.Path.GetFileNameWithoutExtension(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件_获取文件夹路径(string path, out string FilePath, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            FilePath = string.Empty;
            try
            {
                FilePath = System.IO.Path.GetDirectoryName(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        public virtual string[] 文件_获取_文件夹下所有文件夹名(string FilePath)
        {
            string[] folders = Directory.GetDirectories(FilePath);
            return folders;
        }
        public virtual bool 文件夹_获取_文件夹下所有文件夹名(string FilePath, out string[] 文件夹名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件夹名 = new string[0];
            try
            {
                文件夹名 = 文件_获取_文件夹下所有文件夹名(FilePath);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件_获取_所有文件名(string FilePath, out string[] 文件名, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = new string[0];
            try
            {
                文件名 = Directory.GetFiles(FilePath);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        public virtual string[] 文件_获取_文件夹下所有文件名(string path, string 匹配的字符串="", SearchOption 包含的目录= System.IO.SearchOption.TopDirectoryOnly)
        {
            string[] files = Directory.GetFiles(path, 匹配的字符串, 包含的目录);
            return files;
        }
        public virtual bool 文件_获取_文件夹下所有文件名(string FilePath, out string[] 文件名, out string msgErr, string 匹配的字符串="", SearchOption 包含的目录= System.IO.SearchOption.TopDirectoryOnly)
        {
            bool rt = true;
            msgErr = string.Empty;
            文件名 = new string[0];
            try
            {
                文件名 = 文件_获取_文件夹下所有文件名(FilePath, 匹配的字符串, 包含的目录);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual bool 文件_复制文件(string path_源, string path_新, out string msgErr, bool 是否覆盖现有文件 = true)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                FileInfo fi1 = new FileInfo(path_源);

                //if (File.Exists(path_新文件))
                //{
                //    File.Delete(path_新文件);
                //}

                fi1.CopyTo(path_新, 是否覆盖现有文件);
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;


        }


        public virtual bool 文件_删除文件(string path, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public virtual string 文件_读取文件(string path)
        {

            System.IO.StreamReader st;
            st = new System.IO.StreamReader(path, System.Text.Encoding.Default);
            //UTF-8通用编码
            string str = st.ReadToEnd();
            st.Close();
            return str;

        }
        public virtual bool 文件_读取文件(string path, out string value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            value = string.Empty;
            try
            {
                value = 文件_读取文件(path);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        /// <summary>
        /// 快捷方式文件的路径 = @"d:\Test.lnk";
        /// </summary>
        /// <param name="快捷方式文件的路径"></param>
        /// <returns></returns>       
        public virtual bool 文件_获取快捷方式文件指向的路径(string 快捷方式文件的路径, out string value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            value = string.Empty;
            try
            {
                //快捷方式文件的路径 = @"d:\Test.lnk";
                if (System.IO.File.Exists(快捷方式文件的路径))
                {

                    // 在项目中添加“Windows Script Host Object Model”的引用
                    //在项目上单击右键，选择“添加引用”，在“添加引用”对话框中选择“COM”组件选项卡，然后单击选择“Windows Script Host Object Model”，最后确定。在项目中就会出现“IWshRuntimeLibrary”.

                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut 当前快捷方式文件IWshShortcut类 = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(快捷方式文件的路径);
                    //快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;
                    //快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                    value = 当前快捷方式文件IWshShortcut类.TargetPath;
                }
                else
                {
                    value = "";
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }


        #region 文件与byte[]互转

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="FilePath">文件路径文件名称</param>
        /// <returns></returns>
        public byte[] 文件_转成byte数组(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                //MessageBoxHelper.ShowPrompt(ex.Message);
                return null;
            }
            finally
            {
                if (fs != null)
                {

                    //关闭资源
                    fs.Close();
                }
            }


        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="FilePath">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public byte[] 文件_转成byte数组_1(string FilePath)
        {
            // using (FileStream fs = new FileStream( FilePath, FileMode.Open))

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }
        }

        /// <summary>
        /// 将数组转成文件保存起来
        /// </summary>
        /// <param name="path"></param>
        /// <param name="byte数组"></param>
        public void byte数组转成文件(string path, byte[] byte数组)
        {
            //  File.WriteAllBytes(path, byte数组);

            //string path = Server.MapPath(@"\a.pdf");
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(byte数组, 0, byte数组.Length);
            fs.Dispose();


        }

        #endregion

        /// <summary>
        /// <para>按创建时间,顺序</para>
        /// <para>按创建时间,倒序</para>
        /// <para>按修改时间,顺序</para>
        /// <para>按修改时间,倒序</para>
        /// <para>按文件名,顺序</para>
        /// <para>按文件名,倒序</para>
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual string[] 文件_获取文件夹下所有文件名_排序(string FilePath, enum排序 model)
        {
            FileInfo[] files = new DirectoryInfo(FilePath).GetFiles();
            if (model == enum排序.按创建时间_顺序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => x.CreationTime.CompareTo(y.CreationTime));
            }
            else if (model == enum排序.按创建时间_倒序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => y.CreationTime.CompareTo(x.CreationTime));
            }
            else if (model == enum排序.按修改时间_顺序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => x.LastWriteTime.CompareTo(y.LastWriteTime));
            }
            else if (model == enum排序.按修改时间_倒序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => y.LastWriteTime.CompareTo(x.LastWriteTime));
            }
            else if (model == enum排序.按文件名_顺序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => x.Name.CompareTo(y.Name));
            }
            else if (model == enum排序.按文件名_倒序)
            {
                Array.Sort<FileInfo>(files, (FileInfo x, FileInfo y) => y.Name.CompareTo(x.Name));
            }
            List<string> list = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                list.Add(files[i].Name);
            }
            return list.ToArray();
        }
        /// <summary>
        /// <para>按创建时间,顺序</para>
        /// <para>按创建时间,倒序</para>
        /// <para>按修改时间,顺序</para>
        /// <para>按修改时间,倒序</para>
        /// <para>按文件名,顺序</para>
        /// <para>按文件名,倒序</para>
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool 文件_获取文件夹下所有文件名_排序(string FilePath, enum排序 model, out string[] value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            value = new string[0];
            try
            {
                value = 文件_获取文件夹下所有文件名_排序(FilePath, model);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }



        /// <summary>
        /// <para>按文件夹名,顺序</para>
        /// <para>按文件夹名,倒序</para>
        /// <para>按创建时间,顺序</para>
        /// <para>按创建时间,倒序</para>
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual string[] 文件_获取文件夹下所有文件夹名_排序(string FilePath, enum排序 model)
        {
            DirectoryInfo[] directories = new DirectoryInfo(FilePath).GetDirectories();
            if (model == enum排序.按文件夹名_顺序)
            {
                Array.Sort<DirectoryInfo>(directories, (DirectoryInfo x, DirectoryInfo y) => x.Name.CompareTo(y.Name));
            }
            else if (model == enum排序.按文件夹名_倒序)
            {
                Array.Sort<DirectoryInfo>(directories, (DirectoryInfo x, DirectoryInfo y) => y.Name.CompareTo(x.Name));
            }
            else if (model == enum排序.按创建时间_顺序)
            {
                Array.Sort<DirectoryInfo>(directories, (DirectoryInfo x, DirectoryInfo y) => x.CreationTime.CompareTo(y.CreationTime));
            }
            else if (model == enum排序.按创建时间_倒序)
            {
                Array.Sort<DirectoryInfo>(directories, (DirectoryInfo x, DirectoryInfo y) => y.CreationTime.CompareTo(x.CreationTime));
            }
            List<string> list = new List<string>();
            for (int i = 0; i < directories.Length; i++)
            {
                list.Add(directories[i].Name);
            }
            return list.ToArray();
        }
        /// <summary>
        /// <para>按文件夹名,顺序</para>
        /// <para>按文件夹名,倒序</para>
        /// <para>按创建时间,顺序</para>
        /// <para>按创建时间,倒序</para>
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool 文件_获取文件夹下所有文件夹名_排序(string FilePath, enum排序 model, out string[] value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            value = new string[0];
            try
            {
                value = 文件_获取文件夹下所有文件夹名_排序(FilePath, model);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public enum enum排序
        {
            按文件夹名_顺序 = 0,
            按文件夹名_倒序 = 1,
            按创建时间_顺序 = 2,
            按创建时间_倒序 = 3,
            按修改时间_顺序 = 4,
            按修改时间_倒序 = 5,
            按文件名_顺序 = 6,
            按文件名_倒序 = 7,

        }


        #endregion


        #region 文件的读写

        /// <summary>
        ///  Model: =0写,=1读
        /// <para>读写文件,以json格式保存,system.IO.json.txt</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="Model"></param>
        /// <param name="cfg"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="加密"></param>
        /// <param name="密码"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public virtual bool WriteReadJsonText<T>(string path, ushort Model, ref T cfg, out string msgErr, Encoding encoding_ = null, bool 加密 = false, string 密码 = "QIFENG8888", int bufferSize = 65535)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }
                        rt = new Json_().JsonTo_T(cfg, out string vxt, out msgErr);
                        if (rt)
                        {
                            if (加密)
                            {
                                vxt = new 加解密_AES().加密_1(vxt, 密码);
                            }
                            new 文本().Save(path, vxt, true, out msgErr, encoding_);
                        }

                    }
                    else if (s == "读")
                    {
                        rt = new 文本().ReadFile(path, out string rxt, out msgErr, encoding_);
                        if (rt && !string.IsNullOrEmpty(rxt))
                        {
                            if (加密)
                            {
                                rxt = new 加解密_AES().解密_1(rxt, 密码);
                            }

                            rt = new Json_().T_ToJson(ref cfg, rxt, out msgErr);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 写文件,以json格式保存,system.IO.json.txt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public virtual async Task<TRuest> WriteJsonText<T>(string path, T cfg, bool 加密 = false, Encoding encoding_ = null, string 密码 = "QFLASER")
        {
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("写");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }

                    else if (s == "写")
                    {
                        rt = new Json_().JsonTo_T(cfg, out string vxt, out msgErr);

                        if (rt)
                        {
                            if (加密)
                            {
                                vxt = new 加解密().AES加密2(vxt, 密码);
                            }
                            await new 文本().Save_Async(path, vxt, true, encoding_);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            TRuest ruest = new TRuest();
            ruest.state = rt;
            ruest.msg = msgErr;

            return ruest;
        }



        /// <summary>
        ///  Model: =0写,=1读
        /// <para>读写文件,以json格式保存,Newtonsoft.Json</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="Model"></param>
        /// <param name="cfg"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="加密"></param>
        /// <param name="密码"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public virtual bool WriteReadJson<T>(string path, ushort Model, ref T cfg, out string msgErr, Encoding encoding_ = null, bool 加密 = false, string 密码 = "QIFENG8888", int bufferSize = 65535)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }

                        string vxt = JsonConvert.SerializeObject(cfg, Formatting.Indented);
                        if (加密)
                        {
                            vxt = new 加解密().AES加密2(vxt, 密码);
                        }
                        new 文本().Save(path, vxt, true, out msgErr, encoding_);


                    }
                    else if (s == "读")
                    {
                        rt = new 文本().ReadFile(path, out string rxt, out msgErr, encoding_);

                        if (rt && !string.IsNullOrEmpty(rxt))
                        {
                            if (加密)
                            {
                                rxt = new 加解密_AES().解密_1(rxt, 密码);
                            }

                            try
                            {
                                JToken jToken = JToken.Parse(rxt);
                            }
                            catch (Exception ex)
                            {
                                rt = false;
                                msgErr = ex.Message;
                                break;
                            }
                            cfg = JsonConvert.DeserializeObject<T>(rxt);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 写文件,以json格式保存,Newtonsoft.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public virtual async Task<TRuest> WriteJson<T>(string path, T cfg, bool 加密 = false, Encoding encoding_ = null, string 密码 = "QFLASER")
        {
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("写");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }

                    else if (s == "写")
                    {
                        string vxt = JsonConvert.SerializeObject(cfg, Formatting.Indented);

                        if (加密)
                        {
                            vxt = new 加解密().AES加密2(vxt, 密码);
                        }
                        await new 文本().Save_Async(path, vxt, true, encoding_);

                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            TRuest ruest = new TRuest();
            ruest.state = rt;
            ruest.msg = msgErr;

            return ruest;
        }










        /// <summary>
        ///  Model: =0写,=1读
        /// <para>读写文件,以json格式保存</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="Model"></param>
        /// <param name="cfg"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="加密"></param>
        /// <param name="密码"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public virtual bool WriteReadText(string path, ushort Model, ref string cfg, out string msgErr, Encoding encoding_ = null, bool 加密 = false, string 密码 = "QIFENG8888", int bufferSize = 65535)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }
                        string vxt = cfg;

                        if (加密)
                        {
                            vxt = new 加解密_AES().加密_1(vxt, 密码);
                        }
                        new 文本().Save(path, vxt, true, out msgErr, encoding_);


                    }
                    else if (s == "读")
                    {
                        rt = new 文本().ReadFile(path, out string rxt, out msgErr, encoding_);
                        if (rt && !string.IsNullOrEmpty(rxt))
                        {
                            if (加密)
                            {
                                rxt = new 加解密_AES().解密_1(rxt, 密码);
                            }

                            cfg = rxt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 写文件,以json格式保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public virtual async Task<TRuest> WriteText(string path, string cfg, bool 加密 = false, Encoding encoding_ = null, string 密码 = "QFLASER")
        {
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("写");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }

                    else if (s == "写")
                    {
                        string vxt = cfg;
                        if (加密)
                        {
                            vxt = new 加解密().AES加密2(vxt, 密码);
                        }
                        await new 文本().Save_Async(path, vxt, true, encoding_);

                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            TRuest ruest = new TRuest();
            ruest.state = rt;
            ruest.msg = msgErr;

            return ruest;
        }


        /// <summary>
        ///  Model: =0写,=1读
        /// <para>读写文件,以json格式保存</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="Model"></param>
        /// <param name="cfg"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="加密"></param>
        /// <param name="密码"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public virtual bool WriteReadIni<T>(string path, ushort Model, ref T cfg, out string msgErr, string section = "信息", string key_ = "data", Encoding encoding_ = null, int bufferSize = 65535)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {


                List<string> lstWork = new List<string>();
                lstWork.Add("是否强制写");
                lstWork.Add("写");
                lstWork.Add("读");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }
                    else if (s == "是否强制写")
                    {
                        if (Model != 0 && !new 文件_文件夹().文件_是否存在(path))
                        {
                            Model = 0;
                        }
                    }
                    else if (s == "写")
                    {
                        if (Model != 0)
                        {
                            continue;
                        }
                        string vxt = JsonConvert.SerializeObject(cfg, Formatting.None);
                        new ini().Write(section, key_, vxt, path, encoding_);


                    }
                    else if (s == "读")
                    {
                        string rxt = new ini().Read(section, key_, JsonConvert.SerializeObject(cfg), path, encoding_);
                        if (!string.IsNullOrEmpty(rxt))
                        {
                            cfg = JsonConvert.DeserializeObject<T>(rxt);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 写文件,以json格式保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public virtual async Task<TRuest> WriteIni(string path, string cfg, string section = "信息", string key_ = "data", bool 加密 = false, Encoding encoding_ = null)
        {
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                List<string> lstWork = new List<string>();
                lstWork.Add("写");

                foreach (var s in lstWork)
                {
                    if (!rt)
                    {
                        break;
                    }

                    else if (s == "写")
                    {
                        string vxt = JsonConvert.SerializeObject(cfg, Formatting.None);
                        new ini().Write(section, key_, vxt, path, encoding_);

                    }

                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            TRuest ruest = new TRuest();
            ruest.state = rt;
            ruest.msg = msgErr;

            return ruest;
        }

        #endregion





    }
}
