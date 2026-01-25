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

        internal _功能_ _功能;
        internal _初始化状态_ _初始化状态 = _初始化状态_.未初始化;

        /// <summary>
        /// 编码文件
        /// </summary>
        internal 文件_统一接口 _文件;
         

        /// <summary>
        /// <para>Db : 使用数据库在存储时必须要传入</para>
        /// </summary> 
        public 编码_(_文件夹_._属性_ typeFile, _功能_ 功能 )
        {
            new Language_();
            this._功能 = 功能; 
            this._文件夹_属性 = typeFile;

           
            new _文件夹_(this);
            this._文件类 = new 文件类(this); 
            this._文件 = new 文件_统一接口(this);


           






        }


        #region 计算


        internal (bool s, string m, List<_对象_内容_> lstObject) 计算编码(_文件_属性_ 配方文件, DateTime dates, _em_计算类型_ 计算类型)
        {
            List<_对象_内容_> lstObject = new List<_对象_内容_>();
            bool rt = true;
            string msg = string.Empty;

            _班次_[] 班次规则 = this._文件类.Get_班次(配方文件.班次文件);
            DateTime.TryParse(配方文件.Datetimes, out DateTime 最后加工时间);

            for (int i = 0; i < 配方文件.对象.Count; i++)
            {
                _对象_ s = 配方文件.对象[i];
                string ObjectName = s.对象名;
                string v = "";


                for (global::System.Int32 j = 0; j < s.元素.Count; j++)
                {
                    string y = s.元素[j];
                    var rtType = new Json序列化().转成Json<_元素_.工具>(y);
                    _元素_.工具 type = rtType.cfg;

                    switch (type.Tool)
                    {
                        case _em_工具箱_.文本:

                            #region 文本

                            var rt文本 = new 编码_计算(this).文本(y);
                            rt = rt文本.s;
                            msg = rt文本.m;
                            v = rt文本.v;

                            #endregion

                            break;
                        case _em_工具箱_.序列号:

                            #region 序列号

                            //_序列号_._em_操作_ sn操作 = 计算类型 == _em_计算类型_.测试 ? _序列号_._em_操作_.判断复位 :
                            //                            计算类型 == _em_计算类型_.加工 ? _序列号_._em_操作_.计算递增 :
                            //                             _序列号_._em_操作_.判断复位;
                            string snStr = y;
                            //先判断复位,获取出来内容,
                            var rtSn = new 编码_计算(this).序列号(ref snStr, _序列号_._em_操作_.判断复位, dates, 最后加工时间, 班次规则);
                            rt = rtSn.s;
                            msg = rtSn.m;
                            v = rtSn.v;

                            if (rt && 计算类型 == _em_计算类型_.加工)
                            {
                                //加工时再递增计算下
                                rtSn = new 编码_计算(this).序列号(ref snStr, _序列号_._em_操作_.加工数量递增, dates, 最后加工时间, 班次规则);
                                rt = rtSn.s;
                                msg = rtSn.m;
                                //计算完成后将值反馈回去,保存时使用
                                配方文件.对象[i].元素[j] = snStr;
                            }

                            #endregion

                            break;
                        case _em_工具箱_.日期:

                            #region 日期

                            var rtDate = new 编码_计算(this).日期(y, dates);
                            rt = rtDate.s;
                            msg = rtDate.m;
                            v = rtDate.v;

                            #endregion

                            break;
                        case _em_工具箱_.时间:

                            #region 时间

                            var rtTime = new 编码_计算(this).时间(y, dates);
                            rt = rtTime.s;
                            msg = rtTime.m;
                            v = rtTime.v;

                            #endregion

                            break;
                        case _em_工具箱_.班次:

                            #region 班次

                            var rtClasses = new 编码_计算(this).班次(y, 班次规则, dates);
                            rt = rtClasses.s;
                            msg = rtClasses.m;
                            v = rtClasses.v;

                            #endregion

                            break;
                        case _em_工具箱_.关联对象:

                            #region 关联对象

                            var rtObjectGN = new 编码_计算(this).关联对象(y, lstObject);
                            rt = rtObjectGN.s;
                            msg = rtObjectGN.m;
                            v = rtObjectGN.v;

                            #endregion

                            break;
                    }




                }

                 
                lstObject.Add(new _对象_内容_
                {
                    对象 = s,
                    Value = v,
                });

            }
            return (rt, msg, lstObject);
        }



        #endregion


    }
}
