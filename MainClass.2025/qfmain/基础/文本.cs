using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace qfmain
{
    public class 文本
    {

        /// <summary>
        ///  文本.Replace(旧文本, 新文本);
        /// </summary>
        /// <param name="文本"></param>
        /// <param name="旧文本"></param>
        /// <param name="新文本"></param>
        /// <returns></returns>
        public virtual string 替换(string 文本, string 旧文本, string 新文本)
        {
            return 文本.Replace(旧文本, 新文本);
        }

        /// <summary>
        ///  <para>string str = 文本.Remove(位置, 数量);</para>
        /// <para>return str.Insert(位置, 新文本);</para>
        /// </summary>
        /// <param name="文本"></param>
        /// <param name="位置"></param>
        /// <param name="数量"></param>
        /// <param name="新文本"></param>
        /// <returns></returns>
        public virtual string 替换(string 文本, int 位置, int 数量, string 新文本)
        {
            string str = 文本.Remove(位置, 数量);
            return str.Insert(位置, 新文本);
        }

        /// <summary>
        /// 文本.Substring(位置, 数量);
        /// </summary>
        /// <param name="文本"></param>
        /// <param name="位置"></param>
        /// <param name="数量"></param>
        /// <returns></returns>
        public virtual string 获取(string 文本, int 位置, int 数量)
        {
            return 文本.Substring(位置, 数量);
        }


        /// <summary>
        /// 获取两个字符串之间的文本
        /// </summary>
        /// <param name="文本"></param>
        /// <param name="文本start"></param>
        /// <param name="文本end"></param>
        /// <returns></returns>
        public virtual string[] 获取(string 文本, string 文本start, string 文本end)
        {
            string[] a = 文本.Split(new string[] { 文本start }, StringSplitOptions.None);
            List<string> lst = new List<string>();
            for (int i = 1; i < a.Length - 1; i++)
            {
                string s = a[i];
                string[] c = s.Split(new string[] { 文本end }, StringSplitOptions.None);
                if (文本start != 文本end && c.Length > 1)
                {
                    lst.Add(c[0]);
                }
                else if (文本start == 文本end)
                {
                    lst.Add(c[0]);
                }
            }
            return lst.ToArray();


        }


        /// <summary>
        /// 读取后保存,案例
        /// </summary>
        /// <param name="path"></param>
        /// <param name="分割符"></param>
        /// <param name="索引"></param>
        /// <param name="Str"></param>
        public virtual void 修改(string path, char 分割符, int 索引, string Str)
        {
            using (StreamReader srlogin = new StreamReader(path))
            {
                string msgs = srlogin.ReadToEnd();
                string msgHead = msgs.Substring(0, 6);
                string msgContent = msgs.Substring(6);
                string[] info = msgContent.Split(分割符);
                msgs = msgs.Replace(info[索引], Str);
                File.WriteAllText(path, msgs);
            }

        }

        /// <summary>
        /// AutoFlush ：Flush 用于获取或设置一个值，该值指示 StreamWriter 是否会在每次操作后将其缓冲区内容写入底层流。 调用 StreamWriter.Write(char) 方法的近似操作。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Str"></param>
        /// <param name="是否覆盖"></param>
        /// <param name="encoding_"></param>
        /// <param name="msgErr"></param>
        /// <param name="缓存大小"></param>
        /// <param name="AutoFlush">Flush 用于获取或设置一个值，该值指示 StreamWriter 是否会在每次操作后将其缓冲区内容写入底层流。 调用 StreamWriter.Write(char) 方法的近似操作。</param>
        /// <returns></returns>
        public virtual bool Save(string path, string Str, bool 是否覆盖, out string msgErr, Encoding encoding_ = null, int 缓存大小 = 65535, bool AutoFlush = false)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding.Default;
                }

                //是否覆盖=true:追加,rt=false:覆盖
                using (StreamWriter sw = new StreamWriter(path, !是否覆盖, encoding_, 缓存大小))
                {
                    sw.AutoFlush = AutoFlush;
                    sw.Write(Str);
                    //sw.Flush();
                    //sw.Close();
                   
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
        /// AutoFlush ：Flush 用于获取或设置一个值，该值指示 StreamWriter 是否会在每次操作后将其缓冲区内容写入底层流。 调用 StreamWriter.Write(char) 方法的近似操作。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Str"></param>
        /// <param name="是否覆盖"></param>
        /// <param name="encoding_"></param>
        /// <param name="msgErr"></param>
        /// <param name="缓存大小"></param>
        /// <param name="AutoFlush"> Flush 用于获取或设置一个值，该值指示 StreamWriter 是否会在每次操作后将其缓冲区内容写入底层流。 调用 StreamWriter.Write(char) 方法的近似操作。</param>
        /// <returns></returns>
        public virtual bool SaveLine(string path, string Str, bool 是否覆盖, out string msgErr, Encoding encoding_ = null, int 缓存大小 = 65535, bool AutoFlush = false)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding.Default;
                }
                //rt=true:追加,rt=false:覆盖
                using (StreamWriter sw = new StreamWriter(path, !是否覆盖, encoding_))
                {
                    sw.AutoFlush = AutoFlush;
                    sw.WriteLine(Str);
                    //sw.Flush();
                    //sw.Close();               
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
        /// 异步
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Str"></param>
        /// <param name="是否覆盖"></param>
        /// <param name="encoding_"></param>
        /// <param name="缓存大小"></param>
        /// <param name="AutoFlush"></param>
        public virtual async Task  Save_Async(string path, string Str, bool 是否覆盖, Encoding encoding_ = null, int 缓存大小 = 65535, bool AutoFlush = false)
        {
            if (encoding_ is null)
            {
                encoding_ = Encoding .Default;
            }

            //rt=true:追加,rt=false:覆盖
            using (StreamWriter sw = new StreamWriter(path, !是否覆盖, encoding_))
            {
                sw.AutoFlush = AutoFlush;
                await sw.WriteAsync(Str);
                //sw.Flush();
                //sw.Close();               
            }

        }


        /// <summary>
        /// 异步
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Str"></param>
        /// <param name="是否覆盖"></param>
        /// <param name="encoding_"></param>
        /// <param name="缓存大小"></param>
        /// <param name="AutoFlush"></param>
        public virtual async Task SaveLine_Async(string path, string Str, bool 是否覆盖, Encoding encoding_ = null, int 缓存大小 = 65535, bool AutoFlush = false)
        {
            if (encoding_ is null)
            {
                encoding_ = Encoding .Default;
            }
            bool rt = !是否覆盖;
            //rt=true:追加,rt=false:覆盖
            using (StreamWriter sw = new StreamWriter(path, rt, encoding_))
            {
                sw.AutoFlush = AutoFlush;
                await sw.WriteLineAsync(Str);
                //sw.Flush();
                //sw.Close();               
            }

        }


        public virtual bool Save(string path, byte[] bytes, FileMode fileMode_, string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(path, fileMode_))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
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
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public virtual bool ReadFile(string path, out string value, out string msgErr, Encoding encoding_ = null, int bufferSize = 65535)
        {
            string str = string.Empty;
            bool rt = true;
            msgErr = string.Empty;
            value = string.Empty;
            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding .Default;
                }
                using (System.IO.StreamReader st = new System.IO.StreamReader(path, encoding_  , true, bufferSize))
                {
                    //UTF-8通用编码
                    value = st.ReadToEnd();
                    //st.Close();
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
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding_"></param>
        /// <returns></returns>
        public async Task<string> ReadFile_async(string path, Encoding encoding_ = null, int bufferSize = 65535)
        {
            if (encoding_ is null)
            {
                encoding_ = Encoding .Default;
            }
            string value = string.Empty;
            using (System.IO.StreamReader st = new System.IO.StreamReader(path, encoding_, true, bufferSize))
            {
                //UTF-8通用编码
                value = await st.ReadToEndAsync();
                // st.Close();
            }

            return value;
        }

        /// <summary>
        /// 异步,读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding_"></param>
        /// <param name="value"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool ReadFile_async(string path, out string value, out string msgErr, Encoding encoding_ = null, int bufferSize = 1024 * 1024)
        {
            bool rt = true;
            value = string.Empty;
            msgErr = string.Empty;
           
            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding .Default;
                }
                Task<string> valueb = ReadFile_async(path, encoding_, bufferSize);
                value = valueb.Result;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 读出txt内容,设置读出文本的格式
        /// <para>File.ReadAllLines 方式读取</para>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding_">文本的编码 System.Text.Encoding.Default 为自动型 </param>
        /// <returns></returns>
        public virtual bool ReadFile(string path, out string[] value, out string msgErr, Encoding encoding_ = null)
        {
            value = new string[] { };
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding .Default;
                }

                value = File.ReadAllLines(path, encoding_);//获取文本中每一行数据存在数组中
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// StreamReader 方式读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding_"></param>
        /// <returns></returns>
        public virtual bool ReadFile_1(string path, out string[] value, out string msgErr, Encoding encoding_ = null)
        {
            bool rt = true;
            msgErr = string.Empty;
            value = new string[] { };

            try
            {
                if (encoding_ is null)
                {
                    encoding_ = Encoding .Default;
                }
                using (StreamReader sr = new StreamReader(path, encoding_))
                {
                    string line = string.Empty;
                    List<string> lst = new List<string>();

                    while ((line = sr.ReadLine()) != null)
                    {
                        lst.Add(line.ToString());
                    }

                    //sr.Close();
                    //sr.Dispose();

                    value = lst.ToArray();
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
        /// 分割字符串
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="分割符">char</param>
        /// <returns></returns>
        public virtual string[] Split(string Str, string 分割符, StringSplitOptions StringSplitOptions_ = StringSplitOptions.None)
        {
            return Str.Split(new string[] { 分割符 }, StringSplitOptions_);
        }


        /// <summary>
        /// 将intptr转成字符串,将内存指针转成字符串
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public virtual string IntPrtTostring(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        public virtual void 复制(string Str)
        {
            // Takes the selected text from a text box and puts it on the clipboard.
            if (!string.IsNullOrEmpty(Str))
            {
                System.Windows.Clipboard.SetDataObject(Str);
            }
        }

        public virtual string 粘贴()
        {
            System.Windows.IDataObject iData = System.Windows.Clipboard.GetDataObject();
            string rt = string.Empty;
            if (iData.GetDataPresent(System.Windows.DataFormats.Text))
            {
                rt = (String)iData.GetData(System.Windows.DataFormats.Text);

            }


            return rt;

        }


        /// <summary>
        /// 显示到控件里面才是正常的,使用messagebox显示有问题,
        /// <para>0:左填充,1:右填充,2:中</para>
        /// </summary>
        /// <param name="内容"></param>
        /// <param name="字符数量"></param>
        /// <param name="T右对齐F左对齐"></param>
        /// <param name="填充内容"></param>
        /// <returns></returns>
        public string 对齐(string 内容, int 字符数量, short 对齐方向, char 填充内容)
        {
            int byteCount = 字符串位数(内容);
            if (对齐方向 > 2 || 对齐方向 < 0)
            {
                对齐方向 = 0;
            }

            if (字符数量 - byteCount > 0)
            {
                if (对齐方向 == 0)
                {
                    return new string(填充内容, 字符数量 - byteCount) + 内容;
                }
                else if (对齐方向 == 1)
                {
                    return 内容 + new string(填充内容, 字符数量 - byteCount);
                }
                else if (对齐方向 == 2)
                {
                    int count = (字符数量 - byteCount) / 2;
                    string 填充 = new string(填充内容, count);
                    string str = 填充 + 内容 + 填充;
                    int a = 字符串位数(str);

                    return str + new string(填充内容, 字符数量 - a);

                }
            }

            return 内容;


        }


        /// <summary>
        /// 判断字符串位数,含中文的处理
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public int 字符串位数(string Str)
        {

            int i = 0;
            foreach (var s in Str)
            {
                if ((int)s > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
            }

            return i;
        }

        public string 排序_反向(string value)
        {
            return new string(value.ToCharArray().Reverse().ToArray());
        }

        public string 反转(string value)
        {
            return new string(value.ToCharArray().Reverse().ToArray());
        }

        public string 到大写(string 文本)
        {
            return 文本.ToUpper();
        }

        public string 到小写(string 文本)
        {
            return 文本.ToLower();
        }
    }
}
