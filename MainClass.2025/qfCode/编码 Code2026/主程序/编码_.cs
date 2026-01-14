using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class 编码_
    {
        /// <summary>
        ///  获取配置文件路径
        /// </summary>
        internal 配置文件 _配置文件;
        /// <summary>
        /// 系统的文件夹
        /// </summary>
        internal _文件夹_ _文件夹_编码;
        public _功能_ _功能;

        public 编码_(_文件夹_._属性_ typeFile, _功能_ 功能)
        {
            this._功能 = 功能;

            #region 初始化

            this._文件夹_编码 = new _文件夹_(typeFile);
            this._配置文件 = new 配置文件(typeFile);

            #endregion

           




        }






    }
}
