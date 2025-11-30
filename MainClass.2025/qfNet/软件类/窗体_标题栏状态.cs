using SqlSugar.SplitTableExtensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static qfmain.log日志;
using static System.Windows.Forms.AxHost;

namespace qfNet
{

    public class _cfg_标题栏状态_
    {
        /// <summary>
        /// 名称,用于区分类型
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Value { set; get; }

        /// <summary>
        /// 小于0:红色,=0:默认颜色,大于0:黄色
        /// </summary>
        public int State { set; get; }

        public _cfg_标题栏状态_()
        {

        }
        public _cfg_标题栏状态_(string _Name, string _Value, int _state)
        {
            this.Name = _Name;
            this.Value = _Value;
            this.State = _state;
        }

    }


    public class 窗体_标题栏状态
    {
        public enum _状态_
        {
            /// <summary>
            /// 默认
            /// </summary>
            None,
            红色,
            黄色,

        }


        #region 事件

        public event Action<_状态_, string> Event_标题栏状态;
        void On_标题栏状态(_状态_ State, string value)
        {
            this._当前系统状态 = State;
            Event_标题栏状态?.Invoke(State, value);
        }

        #endregion

        /// <summary>
        /// (_cfg_标题栏状态_,状态)
        /// </summary>
        private readonly BlockingCollection<(_cfg_标题栏状态_[], int)> _queue = new BlockingCollection<(_cfg_标题栏状态_[], int)>();     //Queue队列
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();   //令牌

        List<_cfg_标题栏状态_> lst标题栏状态 = new List<_cfg_标题栏状态_>();
        private readonly object _lock = new object();
        List<string> lstName = new List<string>();
        public _状态_ _当前系统状态 = _状态_.None;
        bool _IniStiall = false;

        public void 初始化()
        {
            // 启动线程
            Task.Run(On_Queue处理, _cts.Token);
            _IniStiall = true;
        }

        public void 释放()
        {
            if (!_IniStiall)
            {
                return;
            }
            _cts.Cancel();   //释放令牌
            _queue.CompleteAdding();   //自动退出循环      会在消费完所有剩余数据后 自动退出 foreach 循环。
        }

        //处理队列事件
        private async Task On_Queue处理()
        {
            lock (_lock)
            {
                foreach (var s in _queue.GetConsumingEnumerable())
                {
                    //处理事件
                    _cfg_标题栏状态_[] infoBeff = s.Item1;
                    int state = s.Item2;
                    _cfg_标题栏状态_ info = new _cfg_标题栏状态_();
                    string[] work = new string[]
                    {
                    "解析",
                    "处理",
                    "计算",
                    };
                    foreach (var x in work)
                    {
                        if (x == "解析")
                        {
                            #region 解析

                            _cfg_标题栏状态_[] m = infoBeff.Where(p => p.State == state).ToArray();
                            //如果没有找到,就退出,
                            if (m.Length == 0)
                            {
                                break;
                            }
                            info = m[0];

                            #endregion
                        }
                        else if (x == "处理")
                        {
                            #region 处理

                            int a = this.lstName.IndexOf(info.Name);
                            if (a == -1 && state != 0)
                            {
                                this.lstName.Add(info.Name);
                                this.lst标题栏状态.Add(info);
                            }
                            else if (a > -1 && state != 0)
                            {
                                this.lst标题栏状态[a] = info;
                            }
                            else if (a > -1 && state == 0)
                            {
                                this.lst标题栏状态.RemoveAt(a);
                                this.lstName.RemoveAt(a);
                            }

                            #endregion
                        }
                        else if (x == "计算")
                        {
                            计算();
                        }
                    }

                }
            }
        }



        public void Add(_cfg_标题栏状态_[] cfg, int State)
        {
            this._queue.Add((cfg, State));
        }



        void 计算()
        {
            _状态_ State = _状态_.None;
            string value = "";

            #region 计算

            foreach (var s in this.lst标题栏状态)
            {
                //状态为0时不参与计算
                if (s.State == 0)
                {
                    continue;
                }
                value += $"【{s.Value}】";
                if (s.State < 0 && (int)State >= 0)
                {
                    State = _状态_.红色;
                }
                else if (s.State > 0 && (int)State == 0)
                {
                    State = _状态_.黄色;
                }
            }


            #endregion

            On_标题栏状态(State, value);

        }


        public _状态_ 获取系统状态()
        {
            return this._当前系统状态;
        }


    }
}
