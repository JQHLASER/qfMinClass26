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
        ///  配置文件及编码文件
        /// </summary>
        internal 文件类 _文件类;

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
            new Language_();
            this._功能 = 功能;
            this._Db_sqlSugar = Db;
            this._文件夹_属性 = typeFile;

            #region 初始化


            new _文件夹_(this);
            this._文件类 = new 文件类(this);

            #endregion






        }





    }
}
