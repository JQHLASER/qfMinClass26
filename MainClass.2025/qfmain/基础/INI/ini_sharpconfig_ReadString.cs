using SharpConfig;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
 

namespace qfmain
{
    /// <summary>
    /// sharpconfig
    /// </summary>
    public class ini_sharpconfig_ReadString
    {
        string _value = "";
        private Configuration _config;
        private static readonly object _lock = new object();


        public ini_sharpconfig_ReadString(string value)
        {
            this._value = value;
            this._config = Configuration.LoadFromString(_value);  
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
                        return (rt, msgErr, value);
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
 

        #endregion

    }
}
