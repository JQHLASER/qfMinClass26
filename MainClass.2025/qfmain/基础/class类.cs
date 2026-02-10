using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace qfmain
{
    /// <summary>
    /// 使用方法 string name = 属性显示名工具.GetDisplayName(喷码信息("加工数量");
    /// </summary>
    public class class类_属性显示名工具
    {
         
        private readonly ConcurrentDictionary<PropertyInfo, string> _cache = new ConcurrentDictionary<PropertyInfo, string>();

        /// <summary>
        /// 获取属性显示名
        /// </summary>
        public string Get_DisplayName(PropertyInfo prop)
        {
            return _cache.GetOrAdd(prop, p =>
            {
                // DisplayName
                var dn = p.GetCustomAttribute<DisplayNameAttribute>();
                if (dn != null && !string.IsNullOrEmpty(dn.DisplayName))
                    return dn.DisplayName;

                // Display
                var d = p.GetCustomAttribute<DisplayAttribute>();
                if (d != null && !string.IsNullOrEmpty(d.Name))
                    return d.Name;

                return p.Name;
            });
        }

        /// <summary>
        /// 获取某个类所有属性和显示名
        /// </summary>
        public Dictionary<string, string> Get_AllDisplayNames<T>()
        {
            return typeof(T)
                .GetProperties()
                .ToDictionary(
                    p => p.Name,
                    p => Get_DisplayName(p)
                );
        }

        /// <summary>
        /// 根据属性名获取显示名
        /// </summary>
        public string Get_DisplayName<T>(string propertyName)
        {
            var prop = typeof(T).GetProperty(propertyName);
            if (prop == null) return propertyName;
            return Get_DisplayName(prop);
        }


    }
}
