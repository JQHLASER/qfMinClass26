using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 安装 ini-parser
    /// </summary>
    public class ini
    {

        /// <summary>
        /// 读取INI文件
        /// <para>Encoding: 默认为Default</para>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="默认值"></param>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="文件不存在时新建"></param>
        /// <returns></returns>
        public virtual string Read(string section, string key, string 默认值, string filePath, Encoding encoding_ = null, bool 文件不存在时新建 = false)
        {

            string someValue = string.Empty;

            try
            {

                if (文件不存在时新建 && !new 文件_文件夹().文件_是否存在(filePath))
                {
                    Write(section, key, 默认值, filePath);
                }

                if (encoding_ is null)
                {
                    encoding_ = Encoding.Default;
                }

                // 初始化解析器
                var parser = new FileIniDataParser();
                // 读取INI文件
                IniData data = parser.ReadFile(filePath, encoding_);
                // 获取指定Section的Key值
                someValue = data[section][key];
                if (someValue == null)
                {
                    someValue = 默认值;
                }
                 
            }
            catch (Exception ex)
            {
            }
            return someValue;

        }


        /// <summary>
        /// 读取INI文件
        /// <para>Encoding: 默认为Default</para>
        /// </summary> 
        public virtual bool Read(string section, string key, string 默认值, System.IO.StreamReader reader, out string Value, out string msgErr)
        {
            msgErr = string.Empty;
            Value = string.Empty;
            bool rt = true;
            try
            {
                // 初始化解析器
                var parser = new FileIniDataParser();
                // 读取INI文件
                IniData data = parser.ReadData(reader);
                // 获取指定Section的Key值
                Value = data[section][key];
                if (Value == null || string.IsNullOrEmpty(Value))
                {
                    Value = 默认值;
                }

            }
            catch (Exception ex)
            {
                msgErr = string.Empty;
                rt = false;
            }
            return rt;

        }


        /// <summary>
        /// 写INI
        /// <para>Encoding: 默认为Default</para>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        public virtual void Write(string section, string key, string value, string filePath, Encoding encoding_ = null)
        {

            bool rt = true;
            var parser = new FileIniDataParser();
            IniData data = null;
            encoding_ = Encoding_格式(encoding_);

            List<string> lstWork = new List<string>()
           {
               "get",
               "write",
           };

            foreach (var s in lstWork)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "get")
                {
                    try
                    {
                        data = parser.ReadFile(filePath, encoding_); // 用于存数据,不存在的key会在后面添加
                    }
                    catch (Exception ex)
                    {
                        data = new IniData();//清空全部的数据
                    }
                }
                else if (s == "write")
                {
                    // 写入INI文件
                    data[section][key] = value;
                    parser.WriteFile(filePath, data);
                 
                }
            }

        }





        // 使用ini-parser的高效写入方式
        void Write(string filePath, IniData data)
        {
            // 禁用自动保存，手动控制写入时机
            var parser = new FileIniDataParser();

            // 使用using确保资源释放，减少I/O阻塞
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
            using (var writer = new StreamWriter(stream, Encoding.ASCII))
            {
                parser.WriteData(writer, data);
            }
        }

        // 对于超大型文件，手动构建内容可能更快
        void UltraFastIniWrite(string filePath, IniData data)
        {
            var sb = new StringBuilder();

            // 手动构建INI内容
            foreach (var section in data.Sections)
            {
                sb.AppendLine($"[{section.SectionName}]");
                foreach (var key in section.Keys)
                {
                    sb.AppendLine($"{key.KeyName}={key.Value}");
                }
                sb.AppendLine();
            }

            // 一次性写入文件
            File.WriteAllText(filePath, sb.ToString(), Encoding.ASCII);
        }






        #region 本地方法

        void Write(bool 是否创建, string section, string key, string value, string filePath, Encoding encoding_ = null)
        {
            if (是否创建 && !new 文件_文件夹().文件_是否存在(filePath))
            {
                Write(section, key, value, filePath, encoding_);
            }
        }

        /// <summary>
        /// 编码格式
        /// </summary>
        /// <param name="encoding_"></param>
        /// <returns></returns>
        Encoding Encoding_格式(Encoding encoding_ = null)
        {
            return encoding_ is null ? Encoding.Default : encoding_;
        }

        #endregion
    }
}
