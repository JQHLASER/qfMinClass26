using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 标题栏状态_
    {
        /// <summary>
        /// 当前所有信息
        /// </summary>
        _windowInfo_[] _Items_windowInfo_ = new _windowInfo_[0];
        string _默认标题内容 = string.Empty;
        string _标题内容 = string.Empty;
        WindowTitleState _标题栏状态 = WindowTitleState.闲置中;


        public 标题栏状态_(string 默认标题内容_)
        {
            _默认标题内容 = 默认标题内容_;
        }

        /// <summary>
        /// 参数(WindowTitleState)状态,(string)标题内容
        /// </summary>
        public event Action<WindowTitleState, string> Event_处理;
        void On_处理(WindowTitleState state_,string 标题内容_)
        {
            Event_处理?.Invoke(state_, 标题内容_);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowInfos">信息</param>
        /// <param name="TitleDefault_">默认标题内容</param>
        /// <param name="Title_">显示的标题内容</param>
        /// <param name="_WindowTitleState">标题状态</param>
        void 计算(_windowInfo_[] windowInfos)
        {

            #region 计算

            _windowInfo_[] beff = windowInfos;
            string show = "";
            //=0:None,=-1:红色,=1:黄色
            int a = 0;
            foreach (var s in beff)
            {
                //状态为0时不参与计算
                if (s.状态 == 0)
                {
                    continue;
                }
                show += $"【{s.Name}{s.内容}】";
                if (s.状态 < 0 && a >= 0)
                {
                    a = -1;//红色 
                }
                else if (s.状态 > 0 && a == 0)
                {
                    a = 1;//黄色
                }
            }


            #endregion

            _标题内容 = beff.Length == 0 ? _默认标题内容 : show;
            _标题栏状态 = a == -1 ? WindowTitleState.报警中 : a == 1 ? WindowTitleState.加工中 : WindowTitleState.闲置中;
            On_处理(_标题栏状态, _标题内容);
        }

          
        // 用于同步的锁对象
        private readonly object _lock = new object();

        /// <summary>
        /// 添加标题状态
        /// </summary>
        /// <param name="windowInfo"></param>
        /// <param name="state">状态</param>
        /// <param name="Items_windowInfo_">当前显示的信息</param>
        void Add(_windowInfo_[] windowInfo, int state)
        {

            //查询一下,当前状态为哪一条状态
            _windowInfo_[] m = windowInfo.Where(p => p.状态 == state).ToArray();

            //如果没有找到,就退出,
            if (m.Length == 0)
            {
                return;
            }
            _windowInfo_ info = m[0];

            lock (_lock)
            {
                List<_windowInfo_> lst_WindowInfo = _Items_windowInfo_.ToList();

                bool isGets = false;
                for (int i = 0; i < lst_WindowInfo.Count; i++)
                {
                    var s = lst_WindowInfo[i];
                    if (info.Name == s.Name)
                    {
                        if (state == 0)
                        {
                            lst_WindowInfo.RemoveAt(i);//当状态=0时,删除这一条
                            isGets = true;
                        }
                        else
                        {
                            //如果里面已经有了,就替换为新的状态
                            lst_WindowInfo[i] = info;
                            isGets = true;
                        }
                    }
                }

                //如果里面原来没有就添加一个新的
                if (!isGets)
                {
                    lst_WindowInfo.Add(info);
                }
                _Items_windowInfo_ = lst_WindowInfo.ToArray();
                计算(_Items_windowInfo_);
              
            }
        }

         

        /// <summary>
        /// 获取系统状态
        /// </summary>
        /// <returns></returns>
        public WindowTitleState Get系统状态()
        {
            return _标题栏状态;
        }

         

    }
}
