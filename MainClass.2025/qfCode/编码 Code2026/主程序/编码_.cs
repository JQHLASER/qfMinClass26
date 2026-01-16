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
        internal _文件夹_._属性_ _文件夹_属性;
        internal qfSqlSugar.SqlSugar_DB _Db_sqlSugar;

        public _功能_ _功能;
        public _初始化状态_ _初始化状态 = _初始化状态_.未初始化; 





        /// <summary>
        /// <para>Db : 使用数据库在存储时必须要传入</para>
        /// </summary> 
        public 编码_(_文件夹_._属性_ typeFile, _功能_ 功能, qfSqlSugar.SqlSugar_DB Db = null)
        {
            this._功能 = 功能;
            this._Db_sqlSugar = Db;


            #region 初始化

            this._文件夹_属性 = typeFile;
            new _文件夹_(typeFile);
            this._配置文件 = new 配置文件(typeFile);

            #endregion






        }






    }
}
