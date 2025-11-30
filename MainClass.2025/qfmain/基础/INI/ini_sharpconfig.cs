using NPOI.SS.Formula.Functions;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qfmain
{
    /// <summary>
    /// sharpconfig
    /// </summary>
    public class ini_sharpconfig
    {
        private readonly string _filePath;
        private readonly object _lock = new object();
        private Configuration _config;

        public ini_sharpconfig(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        /// <summary>
        /// 加载或重载配置文件（线程安全）
        /// </summary>
        public void Load()
        {
            lock (_lock)
            {
                if (File.Exists(_filePath))
                {
                    _config = Configuration.LoadFromFile(_filePath);
                }
                else
                {
                    _config = new Configuration();
                }
            }
        }

        #region 常用 


        public string Read(string sectionName, string settingName, string 默认值 = "")
        {
            (bool rt, string value, string msgErr) rt = ReadStr(sectionName, settingName, 默认值);
            return rt.value;
        }
        public (bool rt, string value, string msgErr) ReadStr(string sectionName, string settingName, string 默认值 = "")
        {
            lock (_lock)
            {
                bool rt = true;
                string value = 默认值;
                string msgErr = string.Empty;
                try
                {
                    var section = _config[sectionName];
                    if (section == null || !section.Contains(settingName))
                    {
                        return (rt, value, msgErr);
                    }

                    value = section[settingName].RawValue;
                    //return section[settingName].GetValue<string>();
                    return (rt, value, msgErr);
                }
                catch (Exception ex)
                {
                    rt = false;
                    msgErr = ex.Message;
                    // 记录日志或处理类型转换失败的情况
                    return (rt, value, msgErr);
                }
            }
        }

        /// <summary>
        /// 需要配置是否保存
        /// </summary>
        public void Write<T>(string sectionName, string settingName, T value, bool 是否保存  )
        {
            lock (_lock)
            {
                _config[sectionName][settingName].SetValue(value);
                if (是否保存)
                {
                    Save();
                }
            }
        }


        #endregion


        /// <summary>
        /// 读取配置值（带类型转换和默认值）
        /// </summary>
        public T Read_T<T>(string sectionName, string settingName, T 默认值 = default)
        {
            lock (_lock)
            {
                try
                {
                    var section = _config[sectionName];
                    if (section == null || !section.Contains(settingName))
                        return 默认值;
                    return section[settingName].GetValue<T>();
                }
                catch (Exception ex)
                {
                    // 记录日志或处理类型转换失败的情况
                    return 默认值;
                }
            }
        }

        public T[] Read_T<T>(string sectionName, string settingName, T[] 默认值 = default)
        {
            lock (_lock)
            {
                try
                {
                    var section = _config[sectionName];
                    if (section == null || !section.Contains(settingName))
                        return 默认值;

                    return section[settingName].GetValueArray<T>();
                }
                catch (Exception ex)
                {

                    // 记录日志或处理类型转换失败的情况
                    return 默认值;
                }
            }
        }





        /// <summary>
        /// 保存
        /// <para>将更改持久化到磁盘（高效写入）</para>
        /// </summary>
        public void Save()
        {
            lock (_lock)
            {
                try
                {
                    // 确保目录存在
                    var dir = Path.GetDirectoryName(_filePath);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    _config.SaveToFile(_filePath);
                }
                catch (IOException ex)
                {
                    // Log the error or handle it as needed
                    Console.WriteLine($"Error saving configuration file: {ex.Message}");
                }
            }





        }





    }
}
