using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        /// 编码文件和读写操作
        /// </summary>
        internal 文件_统一接口 _配方文件操作;


        /// <summary>
        /// <para>Db : 使用数据库在存储时必须要传入</para>
        /// </summary> 
        public 编码_(_文件夹_._属性_ typeFile, _功能_ 功能)
        {
            new Language_();
            this._功能 = 功能;
            this._文件夹_属性 = typeFile;


            new _文件夹_(this);
            this._文件类 = new 文件类(this);
            this._配方文件操作 = new 文件_统一接口(this);



        }

        /// <summary>
        /// 配方名称 : 当前打开的配方名称
        /// <para>模板对象名 : 模板中设置的对象名称</para>
        /// </summary> 
        public void 编辑(string 配方名称, string[] 模板对象名, bool Is父窗体 = false)
        {
            new 编辑_(this, type_编辑._交互类型_.本地, 模板对象名).Win_主窗体(配方名称, Is父窗体);
        }


        #region 配方

        public (bool s, string m) 配方_保存(_配方文件_属性_ 配方, string 配方名称, DateTime dates)
        {
            配方.Datetimes = dates.ToString("yyyy-MM-dd HH:mm:ss");
            return this._配方文件操作._Iwork文件.Save(配方名称, 配方);
        }

        public (bool s, string m) 配方_复制(string 配方名称, string New配方名称)
        {
            return this._配方文件操作._Iwork文件.复制(配方名称, New配方名称);
        }

        public (bool s, string m) 配方_删除(string 配方名称)
        {
            return this._配方文件操作._Iwork文件.Delete(配方名称);
        }

        public (bool s, string m, _配方文件_属性_ cfg) 配方_打开(string 配方名称)
        {
            return this._配方文件操作._Iwork文件.Read(配方名称);
        }

        #endregion


        #region 计算

        /// <summary>
        /// Is计算完保存  =true:计算完成后保存,=false:不保存,在一次性计算多次时,就不需要马上保存
        /// <para>已对更新日期进行了计算</para>
        /// </summary> 
        public (bool s, string m, List<_对象_内容_> lstObject) 计算编码(string 配方文件名, _配方文件_属性_ 配方文件, DateTime dates, _em_计算类型_ 计算类型, bool Is计算完保存 = false)
        {
            return 计算_编码(配方文件名, 配方文件, dates, 计算类型, Is计算完保存, "");
        }


        /// <summary>
        /// <para>测试模式,只是计算,不保存配方</para>
        /// <para>对象名 : 不为空时,计算指定对象及之前的内容</para>
        /// <para>已对更新日期进行了计算</para>
        /// </summary> 
        public (bool s, string m, List<_对象_内容_> lstObject) 计算编码_对象(_配方文件_属性_ 配方文件, DateTime dates, string 对象名)
        {
            return 计算_编码("", 配方文件, dates, _em_计算类型_.测试, false, 对象名);
        }

        /// <summary>
        /// <para>已对更新日期进行了计算</para>
        /// </summary> 
        public (bool s, string m, _元素_Str_ cfg) 计算元素(_配方文件_属性_ 配方文件, List<_对象_内容_> lst对象内容, DateTime dates, _对象_ 对象, string Json元素)
        {
            return 计算_元素(配方文件, lst对象内容, dates, 对象, Json元素);
        }


        public (bool s, string m, DateTime dates) 更新日期(_配方文件_属性_ 配方文件, DateTime now)
        {
            return 更新日期_(配方文件, now);
        }





        #endregion


        #region 修改

        /// <summary>
        /// Is修改完保存  =true:修改完成后保存,=false:不保存,在一次性修改多个对象时,就不需要马上保存
        /// </summary> 
        public (bool s, string m) 修改编码_对象(string 配方文件名, _配方文件_属性_ 配方文件, _对象_str_ cfg, bool Is修改完保存 = false)
        {
            return 修改_编码(配方文件名, 配方文件, cfg, Is修改完保存);
        }



        #endregion



        #region json与结构之间转换

        public string 转换_ToString<T>(T cfg)
        {
            return new Json序列化().转成String<T>(cfg);
        }

        public (bool s, string m, T cfg) 转换_ToJson<T>(string jsonStr) where T : new()
        {
            return new Json序列化().转成Json<T>(jsonStr);
        }

        #endregion


        #region 事件

        /// <summary>
        /// 外部数据通道
        /// <para>参数(_em_计算类型_,_对象_,文本内容),返回 文本内容</para>
        /// </summary>
        public event Func<_em_计算类型_, _对象_, string, string> Event_文本;

        /// <summary>
        /// 外部数据通道
        /// <para>参数(_em_计算类型_,_对象_),返回 时间</para>
        /// </summary>
        public event Func<_em_计算类型_, _对象_, DateTime> Event_日期时间;





        #endregion


        #region 本地方法

        /// <summary>
        /// <para>Is计算完保存  =true:计算完成后保存,=false:不保存,在一次性计算多次时,就不需要马上保存</para>
        /// <para>对象名 : 不为空时,计算指定对象及之前的内容</para>
        /// <para>已对更新日期进行了计算</para>
        /// </summary> 
        private (bool s, string m, List<_对象_内容_> lstObject) 计算_编码(string 配方文件名, _配方文件_属性_ 配方文件, DateTime date_, _em_计算类型_ 计算类型, bool Is计算完保存, string 对象名)
        {
            List<_对象_内容_> lstObject = new List<_对象_内容_>();
            bool rt = true;
            string msg = string.Empty;


            //深拷贝出来一份,用来防止源文件被意外修改
            _配方文件_属性_ 配方 = 配方文件.Clone();

            _班次_[] 班次规则 = this._文件类.Get_班次(配方.班次文件);

            DateTime.TryParse(配方.Datetimes, out DateTime 最后加工时间);
            var rtDatetime = 更新日期_(配方, date_);
            if (!rtDatetime.s)
            {
                return (rtDatetime.s, rtDatetime.m, default);
            }
            DateTime dates = rtDatetime.dates;




            for (int i = 0; i < 配方.对象.Count; i++)
            {
                _对象_ s = 配方.对象[i].Clone();
                string ObjectName = s.对象名;
                StringBuilder sb = new StringBuilder();


                #region 元素计算

                for (global::System.Int32 j = 0; j < s.元素.Count; j++)
                {
                    string v = "";
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

                            v = Event_文本 is null ?
                                rt文本.v :
                                Event_文本.Invoke(计算类型, s, rt文本.v);

                            #endregion

                            break;
                        case _em_工具箱_.序列号:

                            #region 序列号

                            #region 注释掉了的
                            //_序列号_._em_操作_ sn操作 = 计算类型 == _em_计算类型_.测试 ? _序列号_._em_操作_.判断复位 :
                            //                            计算类型 == _em_计算类型_.加工 ? _序列号_._em_操作_.计算递增 :
                            //                             _序列号_._em_操作_.判断复位;
                            #endregion

                            dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(计算类型, s);
                            string snStr = y;
                            //先判断复位,获取出来内容,
                            var rtSn = new 编码_计算(this).序列号(ref snStr, _序列号_._em_操作_.判断复位, dates, 最后加工时间, 班次规则);
                            rt = rtSn.s;
                            msg = rtSn.m;
                            v = rtSn.v;

                            //将更新的结果赋值对源数据,计算时使用新结果去计算
                            if (rt) 配方.对象[i].元素[j] = snStr;

                            if (rt && 计算类型 == _em_计算类型_.加工)
                            {
                                //加工时再递增计算下
                                rtSn = new 编码_计算(this).序列号(ref snStr, _序列号_._em_操作_.加工数量递增, dates, 最后加工时间, 班次规则);
                                rt = rtSn.s;
                                msg = rtSn.m;

                                //将更新的结果赋值对源数据,保存时使用
                                if (rt) 配方.对象[i].元素[j] = snStr;
                            }

                            #endregion

                            break;
                        case _em_工具箱_.日期:

                            #region 日期

                            dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(计算类型, s);
                            var rtDate = new 编码_计算(this).日期(y, dates);
                            rt = rtDate.s;
                            msg = rtDate.m;
                            v = rtDate.v;

                            #endregion

                            break;
                        case _em_工具箱_.时间:

                            #region 时间

                            dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(计算类型, s);
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

                    sb.Append(v);
                }

                #endregion


                #region 添加计算的结果到集合 

                //记录内容
                lstObject.Add(new _对象_内容_
                {
                    对象 = s,
                    Value = sb.ToString(),
                });

                #endregion


                #region 到指定对象后退出 

                //当为指定对象时,计算完成后退出
                if (!string.IsNullOrEmpty(对象名) && ObjectName == 对象名)
                {
                    break;
                }

                #endregion


            }

            #region 正常时,修改源配方,目的是为了保存时,保存最新的配方信息

            if (rt)
            {
                //正常时,修改源文件
                配方文件 = 配方.Clone();
            }

            #endregion

            #region 保存

            if (Is计算完保存)
            {
                if (string.IsNullOrEmpty(配方文件名))
                {
                    var rtSave = 配方_保存(配方, 配方文件名, dates);
                }
                else
                {
                    rt = false;
                    msg = Language_.Get语言("配方名不能为空");
                }
            }

            #endregion


            return (rt, msg, lstObject);
        }

        /// <summary>
        /// 已对更新日期进行了计算
        /// </summary> 
        private (bool s, string m, _元素_Str_ cfg) 计算_元素(_配方文件_属性_ 配方文件, List<_对象_内容_> lst对象内容, DateTime date_, _对象_ 对象, string Json元素)
        {
            bool rt = true;
            string msg = string.Empty;

            //深拷贝出来一份,用来防止源文件被意外修改
            _配方文件_属性_ 配方 = 配方文件.Clone();
            _班次_[] 班次规则 = this._文件类.Get_班次(配方.班次文件);
            DateTime.TryParse(配方.Datetimes, out DateTime 最后加工时间);

            var rtDatetime = 更新日期_(配方, date_);
            if (!rtDatetime.s)
            {
                return (rtDatetime.s, rtDatetime.m, default);
            }
            DateTime dates = rtDatetime.dates;

            string v = "";
            var rtType = new Json序列化().转成Json<_元素_.工具>(Json元素);
            _元素_.工具 type = rtType.cfg;

            switch (type.Tool)
            {
                case _em_工具箱_.文本:

                    #region 文本

                    var rt文本 = new 编码_计算(this).文本(Json元素);
                    rt = rt文本.s;
                    msg = rt文本.m;

                    v = Event_文本 is null ?
                        rt文本.v :
                        Event_文本.Invoke(_em_计算类型_.测试, 对象, rt文本.v);

                    #endregion

                    break;
                case _em_工具箱_.序列号:

                    #region 序列号

                    dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(_em_计算类型_.测试, 对象);
                    string snStr = Json元素;
                    //先判断复位,获取出来内容,
                    var rtSn = new 编码_计算(this).序列号(ref snStr, _序列号_._em_操作_.判断复位, dates, 最后加工时间, 班次规则);
                    rt = rtSn.s;
                    msg = rtSn.m;
                    v = rtSn.v;

                    #endregion

                    break;
                case _em_工具箱_.日期:

                    #region 日期

                    dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(_em_计算类型_.测试, 对象);
                    var rtDate = new 编码_计算(this).日期(Json元素, dates);
                    rt = rtDate.s;
                    msg = rtDate.m;
                    v = rtDate.v;

                    #endregion

                    break;
                case _em_工具箱_.时间:

                    #region 时间

                    dates = Event_日期时间 is null ? dates : Event_日期时间.Invoke(_em_计算类型_.测试, 对象);
                    var rtTime = new 编码_计算(this).时间(Json元素, dates);
                    rt = rtTime.s;
                    msg = rtTime.m;
                    v = rtTime.v;

                    #endregion

                    break;
                case _em_工具箱_.班次:

                    #region 班次

                    var rtClasses = new 编码_计算(this).班次(Json元素, 班次规则, dates);
                    rt = rtClasses.s;
                    msg = rtClasses.m;
                    v = rtClasses.v;

                    #endregion

                    break;
                case _em_工具箱_.关联对象:

                    #region 关联对象

                    var rtObjectGN = new 编码_计算(this).关联对象(Json元素, lst对象内容);
                    rt = rtObjectGN.s;
                    msg = rtObjectGN.m;
                    v = rtObjectGN.v;

                    #endregion

                    break;
            }




            _元素_Str_ _元素 = new _元素_Str_
            {
                工具 = type.Tool,
                Value = v,
            };

            return (rt, msg, _元素);




        }

        private (bool s, string m) 修改_编码(string 配方文件名, _配方文件_属性_ 配方文件, _对象_str_ cfg, bool Is计算完保存)
        {
            List<_对象_内容_> lstObject = new List<_对象_内容_>();
            bool rt = false;
            string msg = Language_.Get语言("未找到对象");

            //深拷贝出来一份,用来防止源文件被意外修改
            _配方文件_属性_ 配方 = 配方文件.Clone();

            _班次_[] 班次规则 = this._文件类.Get_班次(配方.班次文件);
            DateTime.TryParse(配方.Datetimes, out DateTime 最后加工时间);

            for (int i = 0; i < 配方.对象.Count; i++)
            {
                _对象_ s = 配方.对象[i].Clone();
                string ObjectName = s.对象名;
                if (cfg.Name != ObjectName)
                {
                    continue;
                }
                string v = "";
                rt = true;
                msg = "";



                #region 元素修改

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
                            rt文本.v = cfg.Value;

                            if (rt)
                            {
                                配方.对象[i].元素[j] = new Json序列化().转成String<_元素_.文本>(rt文本.cfg);
                            }

                            #endregion

                            break;

                    }

                }

                #endregion


                break;


            }

            #region 正常时,修改源配方,目的是为了保存时,保存最新的配方信息

            if (rt)
            {
                //正常时,修改源文件
                配方文件 = 配方.Clone();
            }

            #endregion



            return (rt, msg);
        }


        private (bool s, string m, DateTime dates) 更新日期_(_配方文件_属性_ 配方文件, DateTime now)
        {
            bool rt = true;
            string msgErr = string.Empty;
            DateTime nowNew = now;
            try
            {
                if (this._功能.日期时间.更新日期)
                {
                    DateTime 当前时间 = DateTime.Parse(now.ToString("HH:mm:ss"));
                    DateTime 更新时间 = DateTime.Parse(配方文件.更新时间);
                    if (当前时间 <= 更新时间)
                    {
                        nowNew = new qfmain.日期时间_().增减时间(now, 2, -1);
                    }
                }
            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return (rt, msgErr, nowNew);
        }


        #endregion

    }
}
