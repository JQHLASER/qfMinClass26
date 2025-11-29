using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class ini_win
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region 读写多个



        /// <summary>
        /// 获取所有节点名称(Section)
        /// </summary>
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>
        /// 获取某个指定节点(Section)中所有KEY和Value
        /// </summary>
        /// <param name="lpAppName">节点名称</param>
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件名</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString1(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        //另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,
        //所以对于lpAppName或lpKeyName为null的情况就不适用
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        //再一种声明，使用string作为缓冲区的类型同char[]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 将指定的键值对写到指定的节点，如果已经存在则替换。
        /// </summary>
        /// <param name="lpAppName">节点，如果不存在此节点，则创建此节点</param>
        /// <param name="lpString">Item键值对，多个用\0分隔,形如key1=value1\0key2=value2
        /// <para>如果为string.Empty，则删除指定节点下的所有内容，保留节点</para>
        /// <para>如果为null，则删除指定节点下的所有内容，并且删除该节点</para>
        /// </param>
        /// <param name="lpFileName">INI文件</param>
        /// <returns>是否成功写入</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]     //可以没有此行
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);








        #endregion


        /************************************/

        public virtual void Write(string section, string key, string Value, string path)
        {
            WritePrivateProfileString(section, key, Value, path);
        }

        public virtual string Read(string section, string key, string 默认值, string path, int 长度 = 1024 * 1024)
        {
            string xt = string.Empty;
            try
            {
                StringBuilder temp = new StringBuilder(长度);
                int i = GetPrivateProfileString(section, key, 默认值, temp, 长度, path);
                xt = temp.ToString();
            }
            catch (Exception)
            {
                xt = 默认值;
            }
            return xt.ToString();

        }


        /************************************/

        /// <summary>
        /// 数据
        /// </summary>
        public class Info_ini_
        {
            /// <summary>
            /// 键
            /// </summary>
            public string key { set; get; }
            /// <summary>
            /// 内容
            /// </summary>
            public string Value { set; get; }
        }


        /// <summary>
        /// 在INI文件中，将指定的键值对写到指定的节点，如果已经存在则替换
        /// <para>键值对，多个用\0分隔,形如key1=value1\0key2=value2 </para>
        /// </summary>
        /// <param name="path">INI文件</param>
        /// <param name="section">节点，如果不存在此节点，则创建此节点</param>
        /// <param name="items">键值对，多个用\0分隔,形如key1=value1\0key2=value2</param>
        /// <returns></returns>
        bool Write(string section, string items, string path, out string msgErr)
        {
            msgErr = string.Empty;
            if (string.IsNullOrEmpty(section))
            {
                msgErr = "section,必须指定节点名称";
            }

            if (string.IsNullOrEmpty(items))
            {
                msgErr = "items,必须指定键值对";
            }
            return WritePrivateProfileSection(section, items, path);
        }

        /// <summary>
        /// lst键值对: a=1
        /// </summary>
        /// <param name="节名称"></param>
        /// <param name="lst键值对"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual bool Write_多(string section, List<Info_ini_> lst键值对, string path, out string msgErr)
        {
            string value = string.Empty;
            for (int i = 0; i < lst键值对.Count; i++)
            {
                //多个用\0分隔,形如 key1 = value1\0key2 = value2
                if (i == 0)
                {
                    value = lst键值对[i].key;
                    value += "=" + lst键值对[i].Value;
                }
                else if (i > 0)
                {
                    value += "\0";
                    value += lst键值对[i].key;
                    value += "=" + lst键值对[i].Value;
                }
            }
            return Write(path, section, value, out msgErr);
        }


        /// <summary>
        /// 获取INI文件中指定节点(Section)中的所有条目(key=value形式)
        /// </summary>
        /// <param name="path">Ini文件</param>
        /// <param name="section">节点名称</param>
        /// <returns>指定节点中的所有项目,没有内容返回string[0]</returns>
        public virtual string[] Read_key(string section, string path, uint 长度 = 1024 * 1024)
        {
            //返回值形式为 key=value,例如 Color=Red
            uint MAX_BUFFER = 长度;    //默认为32767

            string[] items = new string[0];      //返回值

            //分配内存
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, path);

            if (!(bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {

                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnedString);     //释放内存

            return items;
        }

        /// <summary>
        /// 获取INI文件中指定节点(Section)中的所有条目的Key列表
        /// </summary>
        /// <param name="iniFile">Ini文件</param>
        /// <param name="section">节点名称</param>
        /// <returns>如果没有内容,反回string[0]</returns>
        public virtual bool Read_Key名(string section, string path, out string[] key, out string msgErr, uint 长度 = 1024 * 1024)
        {

            uint SIZE = 长度;
            msgErr = string.Empty;
            key = new string[0];

            bool rt = true;
            if (string.IsNullOrEmpty(section))
            {
                rt = false;
                msgErr = "section,必须指定节点名称";
            }
            else
            {

                char[] chars = new char[SIZE];

                int bytesReturned = GetPrivateProfileString1(section, null, null, chars, SIZE, path);

                if (bytesReturned != 0)
                {
                    key = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                }
                chars = null;
            }
            return rt;
        }
        public virtual string[] Read_All(string path, uint 长度 = 1024 * 1024)
        {
            uint MAX_BUFFER = 长度;    //默认为32767

            string[] sections = new string[0];      //返回值

            //申请内存
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned != 0)
            {
                //读取指定内存的内容
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();


                //每个节点之间用\0分隔,末尾有一个\0
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //释放内存
            Marshal.FreeCoTaskMem(pReturnedString);

            List<string> lst = sections.ToList();
            lst.RemoveAt(0);
            sections = lst.ToArray();
            return sections;
        }
        //public bool Read_多(string section, string path, out string msgErr, out List<Info_ini_> lst, uint 长度 = 1024 * 1024)
        //{
        //    lst = new List<Info_ini_>();
        //    bool rt = true;
        //    string[] a = Read_key(section, path, 长度);
        //    rt = Read_Key名(section, path, out string[] b, out msgErr, 长度);
        //    if (rt)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < a.Length; i++)
        //            {
        //                Info_ini_ keyValue = new Info_ini_();
        //                keyValue.key = b[i];
        //                keyValue.Value = a[i].Replace(b[i] + "=", "");

        //                lst.Add(keyValue);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            rt = false;
        //            msgErr = ex.Message;
        //        }
        //    }
        //    return rt;
        //}



        public class info_字段_
        {
            public string name { set; get; }
            public string value { set; get; }
        }

        public class info_信息_
        {
            public string 节点 { set; get; }
            public info_字段_[] 字段 { set; get; } = new info_字段_[0];
        }

        /// <summary>
        /// 读取全部
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lst"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool Read_多(string path, out List<info_信息_> lst, out string msgErr)
        {
            lst = new List<info_信息_>();
            bool rt = true;
            msgErr = string.Empty;

            try
            {

                List<string> lstWork = new List<string>();
                lstWork.Add("获取所有节点");
                lstWork.Add("获取字段");

                List<string> lst_section = new List<string>();
                foreach (var s in lstWork)
                {
                    if (s == "获取所有节点")
                    {
                        lst_section = new ini_win_多个().读取_所有节点名称(path).ToList();
                    }
                    else if (s == "获取字段")
                    {
                        foreach (var item in lst_section)
                        {
                            List<info_字段_> lst字段 = new List<info_字段_>();
                            string[] keys = new ini_win_多个().获取_指定节点所有条目的Key列表(path, item);
                            foreach (var s0 in keys)
                            {
                                info_字段_ info字段 = new info_字段_();
                                info字段.name = s0;
                                info字段.value = Read(item, s0, "", path);
                                lst字段.Add(info字段);
                            }

                            info_信息_ info = new info_信息_();
                            info.节点 = item;
                            info.字段 = lst字段.ToArray();
                            lst.Add(info);
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }


            return rt;
        }






    }
}
