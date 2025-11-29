using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public event Action<_状态_, string> Event_标题栏状态;
        void On_标题栏状态(_状态_ State, string value)
        {
            this._当前系统状态 = State;
            Event_标题栏状态?.Invoke(State, value);
        }


        List<_cfg_标题栏状态_> lst标题栏状态 = new List<_cfg_标题栏状态_>();
        private readonly object _lock = new object();
        List<string> lstName = new List<string>();
        public _状态_ _当前系统状态 = _状态_.None;

        /// <summary>
        /// 放在异步线程中
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="State"></param>
        public void Add(_cfg_标题栏状态_[] cfg, int State)
        {
            lock (_lock)
            {
                _cfg_标题栏状态_[] m = cfg.Where(p => p.State == State).ToArray();
                //如果没有找到,就退出,
                if (m.Length == 0)
                {
                    return;
                }
                _cfg_标题栏状态_ info = m[0];
                Add(info, State);

            }
        }

        /// <summary>
        /// 放在异步线程中
        /// </summary>
        /// <param name="info"></param>
        /// <param name="state"></param>
        void Add(_cfg_标题栏状态_ info, int state)
        {

            int a = this.lstName.IndexOf(info.Name);
            if (a == -1 && state != 0)
            {
                lstName.Add(info.Name);
                lst标题栏状态.Add(info);
            }
            else if (a > -1 && state != 0)
            {
                lst标题栏状态[a] = info;
            }
            else if (a > -1 && state == 0)
            {
                lst标题栏状态.RemoveAt(a);
                lstName.RemoveAt(a);
            }

            计算();
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
