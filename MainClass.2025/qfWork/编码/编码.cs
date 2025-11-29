
using qfmain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class 编码
    {
        public 编码(_BM_功能_ 功能)
        {
            this._功能 = 功能;
            this._ac_文件 = new 编码_文件(this);
            this._ac_计算 = new 编码_计算(this);
            this._ac_Err = new 编码_Err(this);
        }


        #region 变量


        internal _BM_功能_ _功能 { set; get; } = new _BM_功能_();

        /// <summary>
        /// 方法
        /// </summary>
        internal 编码_文件 _ac_文件;

        /// <summary>
        /// 方法
        /// </summary>
        internal 编码_计算 _ac_计算;

        /// <summary>
        /// 方法
        /// </summary>
        internal 编码_Err _ac_Err;


        #endregion


        #region 文件

        private readonly object _lock = new object();

        public bool 文件_打开(string 文件名, out _BM_文件信息_ cfg, out string msgErr)
        {
            lock (_lock)
            {
                return this._ac_文件.Read_edm(文件名, out cfg, out msgErr);
            }
        }
        public bool 文件_保存(string 文件名, _BM_文件信息_ cfg, out string msgErr)
        {
            lock (this._lock)
            {
                return this._ac_文件.Write_edm(文件名, cfg, out msgErr);
            }
        }
        public bool 文件_删除(string 文件名, out _BM_文件信息_ cfg, out string msgErr)
        {
            return this._ac_文件.Delete_edm(文件名, out cfg, out msgErr);
        }
        public bool 文件_复制(string 文件名, string 新文件名, out string msgErr)
        {
            return this._ac_文件.Copy_edm(文件名, 新文件名, out msgErr);
        }

        #endregion


        #region 加工生成


        public bool 生成数据(_BM_序列号操作_ 序列号操作, bool 触发事件, ref _BM_文件信息_ 文件, DateTime 时间, _BM_班次_结构_[] 班次结构, out string[] 对象名称列表, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            List<string> lst对象名 = new List<string>();
            try
            {

                //对象
                for (int a = 0; a < 文件.对象.Length; a++)
                {
                    StringBuilder StrBu = new StringBuilder();
                    if (!rt)
                    {
                        break;
                    }
                    _BM_对象信息_ info对象信息 = 文件.对象[a];
                    lst对象名.Add(info对象信息.对象名);

                    //元素
                    for (int i = 0; i < info对象信息.元素.Length; i++)
                    {
                        if (!rt)
                        {
                            break;
                        }
                        string strJson = info对象信息.元素[i];
                        string 结果 = string.Empty;
                        _BM_元素_班次_ info = new 编码_json().json_解析<_BM_元素_班次_>(strJson);
                        switch (info.工具)
                        {
                            case _BM_工具箱_.文本:

                                #region 文本

                                _BM_元素_文本_ info文本 = new 编码_json().json_解析<_BM_元素_文本_>(strJson);
                                rt = new 编码_计算(this).计算(info文本, out 结果, out msgErr);
                                if (rt && 触发事件)
                                {
                                    //加工时或测试复位数据时触发事件
                                    结果 = this.On_文本(info对象信息.对象名, info文本);
                                }

                                #endregion

                                break;
                            case _BM_工具箱_.序列号:

                                #region 序列号

                                _BM_元素_序列号_ infoSn = new 编码_json().json_解析<_BM_元素_序列号_>(strJson);
                                rt = new 编码_计算(this).计算(ref infoSn, 序列号操作, 时间, 文件.dateTimes, 班次结构, out 结果, out msgErr);
                                if (rt)
                                {
                                    文件.对象[a].元素[i] = new 编码_json().json_生成<_BM_元素_序列号_>(infoSn);
                                }

                                #endregion

                                break;
                            case _BM_工具箱_.日期:

                                #region 日期

                                _BM_元素_日期_ info日期 = new 编码_json().json_解析<_BM_元素_日期_>(strJson);
                                rt = new 编码_计算(this).计算(info日期, 时间, out 结果, out msgErr);

                                #endregion

                                break;
                            case _BM_工具箱_.时间:

                                #region 时间

                                _BM_元素_时间_ info时间 = new 编码_json().json_解析<_BM_元素_时间_>(strJson);
                                rt = new 编码_计算(this).计算(info时间, 时间, out 结果, out msgErr);

                                #endregion

                                break;
                            case _BM_工具箱_.班次:

                                #region 班次

                                //new 编码_json处理(this).json_解析(b, out info_班次_ info_关班次);
                                rt = new 编码_计算(this).计算(班次结构, 时间, out 结果, out msgErr);

                                #endregion

                                break;
                            case _BM_工具箱_.关联对象:

                                #region 关联对象

                                _BM_元素_关联对象_ info关联对象 = new 编码_json().json_解析<_BM_元素_关联对象_>(strJson);
                                string 要关联的对象名 = info关联对象.关联对象;
                                int index_ = -1;
                                if (!string.IsNullOrEmpty(要关联的对象名))
                                {
                                    index_ = lst对象名.IndexOf(info关联对象.关联对象);
                                    if (index_ >= 0)
                                    {
                                        string 要关联的内容 = 文件.对象[index_].Value;
                                        rt = new 编码_计算(this).计算(info关联对象, info对象信息.对象名, 要关联的内容, out 结果, out msgErr);
                                    }
                                }
                                #endregion

                                break;

                        }

                        StrBu.Append(结果);
                    }

                    //将内容赋给对象                  
                    文件.对象[a].Value = On_计算对象(info对象信息, StrBu.ToString());

                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }
            对象名称列表 = lst对象名.ToArray();
            return rt;
        }

        public bool 生成数据(_BM_生成数据类型_ 生成类型, ref _BM_文件信息_ 文件, DateTime 时间, _BM_班次_结构_[] 班次结构, out string[] 对象名称列表, out string msgErr)
        {
            bool 触发事件 = (生成类型 == _BM_生成数据类型_.加工递增 || 生成类型 == _BM_生成数据类型_.测试并判断复位)
                ? true : false;
            _BM_序列号操作_ 序列号操作 = (生成类型 == _BM_生成数据类型_.测试并判断复位) ? _BM_序列号操作_.判断复位
                 : (生成类型 == _BM_生成数据类型_.加工递增) ? _BM_序列号操作_.加工数量递增
                 : _BM_序列号操作_.不操作;

            return 生成数据(序列号操作, 触发事件, ref 文件, 时间, 班次结构, out 对象名称列表, out msgErr);
        }


        #region 参考


        //public bool 生成数据(_BM_生成数据类型_ 生成类型, ref _BM_文件信息_ 文件, DateTime 时间, _BM_班次_结构_[] 班次结构, out string[] 对象名称列表, out string msgErr)
        //{
        //    bool rt = true;
        //    msgErr = string.Empty;
        //    List<string> lst对象名 = new List<string>();
        //    try
        //    {

        //        //对象
        //        for (int a = 0; a < 文件.对象.Length; a++)
        //        {
        //            StringBuilder StrBu = new StringBuilder();
        //            if (!rt)
        //            {
        //                break;
        //            }
        //            _BM_对象信息_ info对象信息 = 文件.对象[a];
        //            lst对象名.Add(info对象信息.对象名);

        //            //元素
        //            for (int i = 0; i < info对象信息.元素.Length; i++)
        //            {
        //                if (!rt)
        //                {
        //                    break;
        //                }
        //                string strJson = info对象信息.元素[i];
        //                string 结果 = string.Empty;
        //                _BM_元素_文本_ info = new 编码_json().json_解析<_BM_元素_文本_>(strJson);
        //                switch (info.工具)
        //                {
        //                    case _BM_工具箱_.文本:

        //                        #region 文本

        //                        rt = new 编码_计算(this).计算(info, out 结果, out msgErr);

        //                        if (rt &&
        //                            生成类型 == _BM_生成数据类型_.加工递增 ||
        //                            生成类型 == _BM_生成数据类型_.测试并判断复位)
        //                        {
        //                            //加工时或测试复位数据时触发事件
        //                            结果 = this.On_文本(info对象信息.对象名, info);
        //                        }

        //                        #endregion

        //                        break;
        //                    case _BM_工具箱_.序列号:

        //                        #region 序列号

        //                        _BM_元素_序列号_ infoSn = new 编码_json().json_解析<_BM_元素_序列号_>(strJson);

        //                        if (生成类型 == _BM_生成数据类型_.测试并判断复位)
        //                        {
        //                            rt = new 编码_计算(this).计算(ref infoSn, _BM_序列号操作_.判断复位, 时间, 文件.dateTimes, 班次结构, out 结果, out msgErr);

        //                            break;
        //                        }
        //                        else if (生成类型 == _BM_生成数据类型_.测试不判断复位)
        //                        {
        //                            rt = new 编码_计算(this).计算(ref infoSn, _BM_序列号操作_.不操作, 时间, 文件.dateTimes, 班次结构, out 结果, out msgErr);

        //                        }
        //                        else if (生成类型 == _BM_生成数据类型_.加工递增)
        //                        {
        //                            //加工时递增,不需要显示结果,因为每次显示的是最后一次加工的结果
        //                            rt = new 编码_计算(this).计算(ref infoSn, _BM_序列号操作_.加工数量递增, 时间, 文件.dateTimes, 班次结构, out 结果, out msgErr);

        //                        }

        //                        if (rt)
        //                        {
        //                            文件.对象[a].元素[i] = new 编码_json().json_生成<_BM_元素_序列号_>(infoSn);
        //                        }

        //                        #endregion

        //                        break;
        //                    case _BM_工具箱_.日期:

        //                        #region 日期

        //                        _BM_元素_日期_ info日期 = new 编码_json().json_解析<_BM_元素_日期_>(strJson);
        //                        rt = new 编码_计算(this).计算(info日期, 时间, out 结果, out msgErr);

        //                        #endregion

        //                        break;
        //                    case _BM_工具箱_.时间:

        //                        #region 时间

        //                        _BM_元素_时间_ info时间 = new 编码_json().json_解析<_BM_元素_时间_>(strJson);
        //                        rt = new 编码_计算(this).计算(info时间, 时间, out 结果, out msgErr);

        //                        #endregion

        //                        break;
        //                    case _BM_工具箱_.班次:

        //                        #region 班次

        //                        //new 编码_json处理(this).json_解析(b, out info_班次_ info_关班次);
        //                        rt = new 编码_计算(this).计算(班次结构, 时间, out 结果, out msgErr);

        //                        #endregion

        //                        break;
        //                    case _BM_工具箱_.关联对象:

        //                        #region 关联对象

        //                        _BM_元素_关联对象_ info关联对象 = new 编码_json().json_解析<_BM_元素_关联对象_>(strJson);
        //                        string 要关联的对象名 = info关联对象.关联对象;
        //                        int index_ = -1;
        //                        if (!string.IsNullOrEmpty(要关联的对象名))
        //                        {
        //                            index_ = lst对象名.IndexOf(info关联对象.关联对象);
        //                            if (index_ >= 0)
        //                            {
        //                                string 要关联的内容 = 文件.对象[index_].Value;
        //                                rt = new 编码_计算(this).计算(info关联对象, info对象信息.对象名, 要关联的内容, out 结果, out msgErr);
        //                            }
        //                        }
        //                        #endregion

        //                        break;

        //                }

        //                StrBu.Append(结果);
        //            }


        //            //将内容赋给对象
        //            文件.对象[a].Value = StrBu.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        msgErr = ex.Message;
        //        rt = false;
        //    }
        //    对象名称列表 = lst对象名.ToArray();
        //    return rt;
        //}


        #endregion


        public bool 生成数据(bool 加工中, ref _BM_文件信息_ 文件, DateTime 时间, _BM_班次_结构_[] 班次结构, out string[] 对象名称列表, out string msgErr)
        {
            _BM_生成数据类型_ 生成类型 = 加工中 ? _BM_生成数据类型_.加工递增 : _BM_生成数据类型_.测试并判断复位;
            return 生成数据(生成类型, ref 文件, 时间, 班次结构, out 对象名称列表, out msgErr);
        }





        #endregion


        #region 定制

        /// <summary>
        /// 定制功能,每天达到指定时间点后更新日期
        /// </summary>  
        public bool 定制_日期更新时间点(out DateTime 日期时间, _BM_文件信息_ 文件, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            日期时间 = DateTime.Now;
            try
            {
                if (this._功能.定制.日期更新时间点)
                {
                    this._ac_文件.Get_日期更新时间点(out _BM_日期更新时间点_ cfg, 文件.Times);
                    DateTime 当前时间 = DateTime.Parse(日期时间.ToString("HH:mm:ss"));
                    DateTime 更新时间 = DateTime.Parse(cfg.时间);
                    if (当前时间 < 更新时间)
                    {
                        日期时间 = new qfmain.日期时间_().增减时间(日期时间, 2, -1);
                    }
                }

            }
            catch (Exception ex)
            {
                msgErr = ex.Message;
                rt = false;
            }

            return rt;
        }

        /// <summary>
        /// 校验对象内容位数与实际位数是否一致
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool 定制_校验对象位数(_BM_对象信息_ info)
        {
            int a = new qfmain.文本().字符串位数(info.Value);
            return (a != info.属性.校验_位数) ? false : true;
        }

        /// <summary>
        /// 校验对象内容是否与设置的内容是否一致
        /// </summary>
        /// <param name="info"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 定制_校验对象关键字(_BM_对象信息_ info, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            if (!string.IsNullOrEmpty(info.属性.校验_关键字.Trim()) &&
                info.Value.Trim() != info.属性.校验_关键字)
            {
                rt = false;
                msgErr = Language_.Get语言("校验关键字不匹配");
            }
            return rt;
        }

        public bool 定制_获取对象内容(string 对象名, _BM_文件信息_ 文件, out string 内容, out string msgErr)
        {
            msgErr = string.Empty;
            内容 = string.Empty;
            bool rt = true;

            _BM_对象信息_[] m = 文件.对象.Where(p => p.对象名 == 对象名).ToArray();
            if (m.Length == 0)
            {
                rt = false;
                msgErr = Language_.Get语言("未找到对象");
            }
            else
            {
                内容 = m[0].Value;
            }
            return rt;
        }

        public bool 定制_设置对象内容(ref _BM_文件信息_ 文件, _BM_对象内容_[] 新内容, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            bool 是否找到对象 = false;

            for (int i = 0; i < 文件.对象.Length; i++)
            {
                _BM_对象信息_ s = 文件.对象[i];
                _BM_对象内容_[] m = 新内容.Where(p => p.对象名 == s.对象名).ToArray();
                if (m.Length > 0)
                {
                    是否找到对象 = true;
                    if (s.元素.Length == 0)
                    {
                        rt = false;
                        msgErr = Language_.Get语言("未设置元素");
                    }
                    else
                    {
                        string jsonStr = "";
                        switch (m[0].工具)
                        {
                            case _BM_工具箱_.文本:

                                #region 文本

                                _BM_元素_文本_ info文本 = new 编码_json().json_解析<_BM_元素_文本_>(s.元素[0]);
                                info文本.内容 = m[0].对象内容;
                                jsonStr = new 编码_json().json_生成<_BM_元素_文本_>(info文本);
                                文件.对象[i].元素[0] = jsonStr;

                                #endregion

                                break;

                            case _BM_工具箱_.序列号:

                                #region 序列号

                                _BM_元素_序列号_ info序列号 = new 编码_json().json_解析<_BM_元素_序列号_>(s.元素[0]);
                                info序列号.当前序号 = m[0].对象内容;
                                jsonStr = new 编码_json().json_生成<_BM_元素_序列号_>(info序列号);
                                rt = this._ac_Err.Err_序列号(info序列号, out msgErr);
                                if (rt)
                                {
                                    文件.对象[i].元素[0] = jsonStr;
                                }

                                #endregion

                                break;


                            default:
                                rt = false;
                                msgErr = Language_.Get语言("未找到元素");
                                break;
                        }


                    }
                }
            }

            msgErr = !是否找到对象 ? Language_.Get语言("未找到对象") : msgErr;
            return rt;
        }




        #endregion


        #region 事件

        /// <summary>
        /// 参数(对象名,文本元素),返回计算后的内容
        /// </summary>
        public event Func<string, _BM_元素_文本_, string> Event_文本;
        string On_文本(string 对象名, _BM_元素_文本_ cfg)
        {
            string msg = Event_文本 is null ? cfg.内容 : Event_文本.Invoke(对象名, cfg);
            return msg;
        }

        /// <summary>
        /// 参数(_BM_对象信息_) 反回计算后的内容
        /// </summary>
        public event Func<_BM_对象信息_, string> Event_计算对象;
        string On_计算对象(_BM_对象信息_ cfg, string 对象内容)
        {
            return Event_计算对象 is null ? 对象内容 : Event_计算对象.Invoke(cfg);
        }






        #endregion

    }
}
