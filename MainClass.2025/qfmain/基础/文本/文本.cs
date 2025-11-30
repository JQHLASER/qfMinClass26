using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;

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

        public enum _对齐_
        {
            左对齐,
            右对齐,
            居中,
        }
        public string 对齐(string 内容, int 字符数量, Font font, _对齐_ 对齐方式, string 填充内容 = " ")
        {
            switch (对齐方式)
            {
                case _对齐_.左对齐:
                    return 左对齐(内容, 字符数量, font, 填充内容);
                case _对齐_.右对齐:
                    return 右对齐(内容, 字符数量, font, 填充内容);
                case _对齐_.居中:
                    return 居中(内容, 字符数量, font, 填充内容);
                default:
                    return 内容;
            }
        }


        private string 左对齐(string text, int targetWidthPx, Font font, string 填充内容 = " ")
        {
            string result = text;

            // 计算当前宽度
            int currentWidth = TextRenderer.MeasureText(result, font).Width;

            // 不断补空格直到达到目标像素宽度
            while (currentWidth < targetWidthPx)
            {
                result += 填充内容;
                currentWidth = TextRenderer.MeasureText(result, font).Width;
            }

            return result;
        }

        private string 右对齐(string text, int targetWidthPx, Font font, string 填充内容 = " ")
        {
            string result = text;

            // 计算当前像素宽度
            int currentWidth = TextRenderer.MeasureText(result, font).Width;

            // 不断补空格直到达到目标像素宽度
            while (currentWidth < targetWidthPx)
            {
                result = 填充内容 + result;
                currentWidth = TextRenderer.MeasureText(result, font).Width;
            }

            return result;
        }

        private string 居中(string text, int targetWidthPx, Font font, string 填充内容 = " ")
        {
            string result = text;

            // 计算当前像素宽度
            int currentWidth = TextRenderer.MeasureText(result, font).Width;

            // 不断补空格直到达到目标像素宽度
            while (currentWidth/2 < targetWidthPx/2)
            {
                result = 填充内容 + result+ 填充内容;
                currentWidth = TextRenderer.MeasureText(result, font).Width;
            }

            return result;
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


        #region Save25...超快写入

        /// <summary>
        /// 超快写入
        /// <para>10万行时,30ms - 100ms</para>
        /// <para>buffsize= 1024*100 表示100KB</para>
        /// </summary>
        /// <returns></returns>
        public virtual bool Save_25(string path, string text, bool 是否覆盖, out string msgErr, bool isLine = true, Encoding encoding = null, int buffSize = 1024 * 100)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                encoding = encoding ?? Encoding.Default;
                FileMode filemodel = 是否覆盖 ? FileMode.Create : FileMode.Append;


                using (var fs = new FileStream(path, filemodel, FileAccess.Write, FileShare.Read, buffSize))
                using (var sw = new StreamWriter(fs, encoding))
                {
                    if (isLine)
                    {
                        sw.WriteLine(text);
                    }
                    else
                    {
                        sw.Write(text);
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
        /// 超快写入
        /// <para>10万行时,30ms - 100ms</para>
        /// <para>buffsize= 1024*100 表示100KB</para>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<(bool rt, string msgErr)> Save_25(string path, string text, bool 是否覆盖, bool isLine = true, Encoding encoding = null, int buffSize = 1024 * 100)
        {
            bool rt = true;
            string msgErr = string.Empty;
            try
            {
                FileMode filemodel = 是否覆盖 ? FileMode.Create : FileMode.Append;
                encoding = encoding ?? Encoding.Default;
                using (var fs = new FileStream(path, filemodel, FileAccess.Write, FileShare.Read, buffSize))
                using (var sw = new StreamWriter(fs, encoding))
                {
                    if (isLine)
                    {
                        await sw.WriteLineAsync(text);
                    }
                    else
                    {
                        await sw.WriteAsync(text);
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return (rt, msgErr);
        }


        /// <summary>
        /// 持续写入、大文件、高性能需求
        /// <para>10万行时（50ms - 150ms）</para>
        /// </summary>  
        public virtual bool Save_25_2(string path, string text, bool 是否覆盖, out string msgErr, bool isLine = true, Encoding encoding = null, int buffSize = 1024 * 100)
        {

            bool rt = true;
            msgErr = string.Empty;
            try
            {
                encoding = encoding ?? Encoding.Default;
                using (var sw = new StreamWriter(path, 是否覆盖, encoding, buffSize))
                {
                    sw.WriteLine(text);
                }
            }

            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;


        }







        #endregion


        #region...快速读取

        /// <summary>
        /// 最快,最简单....小于100M
        /// </summary> 
        public virtual bool Read_25(string path, out string Text, out string msgErr, Encoding encoding = null)
        {
            bool rt = true;
            msgErr = string.Empty;
            Text = string.Empty;
            try
            {
                encoding = encoding ?? Encoding.Default;
                Text = File.ReadAllText(path, encoding);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr += ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 流方式，不占内存,一行一行读取,大文件也可以
        /// <para>100MB~1GB</para>
        /// </summary>        
        public virtual bool Read_25(string path, out List<string> lst, out string msgErr, Encoding encoding = null)
        {
            bool rt = true;
            msgErr = string.Empty;
            lst = new List<string>();
            try
            {
                encoding = encoding ?? Encoding.Default;
                foreach (var line in File.ReadLines(path, encoding))
                {
                    lst.Add(line);
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr += ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 逐行读取,
        /// </summary> 
        public virtual bool Read_25_StreamReader(string path, out List<string> lst, out string msgErr, Encoding encoding = null)
        {
            bool rt = true;
            msgErr = string.Empty;
            lst = new List<string>();
            try
            {
                encoding = encoding ?? Encoding.Default;
                using (var sr = new StreamReader(path, encoding))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr += ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 高性能、可处理超大文件（FileStream + BufferedStream）
        /// <para>大于1GB</para>
        /// </summary> 
        public virtual bool Read_25_处理大文件(string path, out List<string> lst, out string msgErr, Encoding encoding = null, int buffSize = 1024 * 1024)
        {
            bool rt = true;
            msgErr = string.Empty;
            lst = new List<string>();
            try
            {
                encoding = encoding ?? Encoding.Default;
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, buffSize))
                using (var bs = new BufferedStream(fs))
                using (var sr = new StreamReader(bs))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lst.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr += ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// （读原始字节最快，但你可能不需要）
        /// </summary>  
        public virtual bool Read_25_读字节(string path, out string text, out string msgErr, Encoding encoding = null, int buffSize = 1024 * 1024)
        {
            bool rt = true;
            msgErr = string.Empty;
            text = string.Empty;
            try
            {
                encoding = encoding ?? Encoding.Default;
                byte[] data = File.ReadAllBytes(path);
                text = encoding.GetString(data);
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr += ex.Message;
            }
            return rt;
        }




        #endregion


        public void 获取字符串高度和宽度(string txt, Font font, out float width, out float height)
        {
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                SizeF sf = g.MeasureString(txt, font);
                width = sf.Width;
                height = sf.Height;
            }
        }

    }
}
