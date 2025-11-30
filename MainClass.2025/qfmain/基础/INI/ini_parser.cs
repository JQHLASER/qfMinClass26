using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 安装 ini-parser
    /// </summary>
    public class ini_parser
    {
        string _path = string.Empty;
        public ini_parser(string path)
        {
            this._path = path;
        }



        /// <summary>
        /// 读取INI文件
        /// <para>Encoding: 默认为Default</para>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="默认值"></param>
        /// <param name="filePath"></param>
        /// <param name="encoding_"></param>
        /// <param name="文件不存在时新建"></param>
        /// <returns></returns>
        public virtual string Read(string section, string key, string 默认值, Encoding encoding_ = null, bool 文件不存在时新建 = false)
        {
            string someValue = 默认值;

            try
            {
                if (文件不存在时新建 && !File.Exists(this._path))
                {
                    Write(section, key, 默认值, encoding_);
                }

                encoding_ = encoding_ ?? Encoding.Default; // If encoding_ is null, use default encoding.

                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(this._path, encoding_);
                someValue = data[section][key] ?? 默认值;
            }
            catch (Exception ex)
            {
                // Log the exception or handle as needed.
                someValue = 默认值;
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
            Value = 默认值;
            bool rt = true;
            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadData(reader);
                Value = data[section][key] ?? 默认值;
            }
            catch (Exception ex)
            {
                msgErr = ex.Message; // Capture the actual error message
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
        /// <param name="encoding_"></param>
        public virtual void Write(string section, string key, string value, Encoding encoding_ = null)
        {
            encoding_ = encoding_ ?? Encoding.Default;

            try
            {
                var parser = new FileIniDataParser();
                IniData data = File.Exists(this._path) ? parser.ReadFile(this._path, encoding_) : new IniData();

                data[section][key] = value;
                parser.WriteFile(this._path, data);
            }
            catch (Exception ex)
            {

            }
        }

        // 高效写入INI文件，避免重复的I/O操作
        void Write(IniData data)
        {
            try
            {
                var sb = new StringBuilder();

                foreach (var section in data.Sections)
                {
                    sb.AppendLine($"[{section.SectionName}]");
                    foreach (var key in section.Keys)
                    {
                        sb.AppendLine($"{key.KeyName}={key.Value}");
                    }
                    sb.AppendLine();
                }

                // 一次性写入文件，使用UTF-8编码避免乱码
                File.WriteAllText(this._path, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // Handle errors here if necessary
            }
        }

        /// <summary>
        /// 超高效INI写入方式，适用于大文件
        /// </summary>
        void UltraFastIniWrite(string filePath, IniData data)
        {
            try
            {
                var sb = new StringBuilder();

                foreach (var section in data.Sections)
                {
                    sb.AppendLine($"[{section.SectionName}]");
                    foreach (var key in section.Keys)
                    {
                        sb.AppendLine($"{key.KeyName}={key.Value}");
                    }
                    sb.AppendLine();
                }

                // 使用UTF-8编码一次性写入，确保内容没有错误
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
            }
        }



    }
}
