using NPOI.SS.Formula.Functions;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace qfmain
{
    /// <summary>
    /// sharpconfig
    /// </summary>
    public class ini_sharpconfig
    {
        private  string _filePath;
        private static readonly object _lock = new object();
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
            (bool rt, string msgErr, string value) rt = ReadStr(sectionName, settingName, 默认值);
            return rt.value;
        }
        public (bool s, string m, string value) ReadStr(string sectionName, string settingName, string 默认值 = "")
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
                        return (rt, msgErr, value );
                    }

                    value = section[settingName].RawValue;
                    //return section[settingName].GetValue<string>();
                    return (rt, msgErr, value);
                }
                catch (Exception ex)
                {
                    rt = false;
                    msgErr = ex.Message;
                    // 记录日志或处理类型转换失败的情况
                    return (rt, msgErr, value);
                }
            }
        }

        /// <summary>
        /// 需要配置是否保存
        /// </summary>
        public (bool s, string m) Write<T>(string sectionName, string settingName, T value, bool 是否保存)
        {
            lock (_lock)
            {
                try
                {

                    if (!_config.Contains(sectionName))
                        _config.Add(sectionName);

                    if (!_config[sectionName].Contains(settingName))
                        _config[sectionName].Add(settingName, "");

                    _config[sectionName][settingName].SetValue(value);
                    if (是否保存)
                    {
                        Save(false);
                    }

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
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
        /// 读大文件
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="行数"></param>
        /// <returns></returns>
        public (bool state, string msg, Dictionary<int, string> value) Read_Dictionary(string sectionName, int 行数)
        {
            lock (_lock)
            {
                try
                {


                    Configuration cfg = Configuration.LoadFromFile(_filePath);
                    var section = cfg[sectionName];
                    var dict = new Dictionary<int, string>(行数);
                    foreach (var setting in section)
                    {
                        dict[int.Parse(setting.Name)] = setting.StringValue;
                    }
                    return (true, default, dict);

                }
                catch (Exception ex)
                {
                    return (false, ex.Message, default);
                }
            }
        }

        /// <summary>
        /// 读多行,超快,key需是从0开始的连续数字
        /// </summary> 
        public (bool state, string msg, string[] value) Read_Array(string sectionName, int 行数)
        {
            lock (_lock)
            {
                try
                {

                    Configuration cfg = Configuration.LoadFromFile(_filePath);
                    string[] data = new string[行数];

                    foreach (var s in cfg[sectionName])
                    {
                        int idx = int.Parse(s.Name);
                        data[idx] = s.StringValue;
                    }
                    return (true, default, data);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message, default);
                }
            }
        }

        /// <summary>
        /// 一次性获取整个 INI 结构
        /// </summary>
        public (bool s, string m, Dictionary<string, Dictionary<string, string>> v) GetAll()
        {
            lock (_lock)
            {
                try
                {
                    var dict = _config.ToDictionary(
                        sec => sec.Name,
                        sec => sec.ToDictionary(st => st.Name, st => st.StringValue)
                    );

                    return (true, "", dict);
                }
                catch (Exception ex)
                {
                    return (false, ex.Message, default);
                }
            }
        }



        /// <summary>
        /// 保存
        /// <para>将更改持久化到磁盘（高效写入）</para>
        /// </summary>
        public (bool state, string msg) Save(bool isLock = true)
        {
            if (isLock)
            {
                lock (_lock)
                {
                    return Save1();
                }
            }
            else
            {
                return Save1();
            }

        }

        (bool state, string msg) Save1()
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
                return (true, default);
            }
            catch (IOException ex)
            {
                return (false, ex.Message);
            }


        }



    }
}
