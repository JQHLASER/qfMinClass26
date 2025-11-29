using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace qfWPFmain
{
    public partial class WindowTitle_ViewModel : ObservableObject
    {

        public WindowTitle_ViewModel()
        {

        }


        /// <summary>
        /// 数据源, //ObservableCollection 可以通知界面更新的
        /// </summary>
        [ObservableProperty]
        private _windowInfo_[] items_WindowInfo = new _windowInfo_[0];

        /// <summary>
        /// 标题栏
        /// </summary>
        [ObservableProperty]
        private string title = string.Empty;

        /// <summary>
        /// 背景颜色
        /// </summary>
        [ObservableProperty]
        private Brush background_grid = Brushes.Transparent;


        List<string> lstName = new List<string>();

        void Add(_windowInfo_ info, int state)
        {
            List<_windowInfo_> lstInfo = this.Items_WindowInfo.ToList();
            int a = lstName.IndexOf(info.Name);
            if (a == -1 && state != 0)
            {
                lstName.Add(info.Name);
                lstInfo.Add(info);
            }
            else if (a > -1 && state != 0)
            {
                lstInfo[a] = info;
            }
            else if (a > -1 && state == 0)
            {
                lstInfo.RemoveAt(a);
                lstName.RemoveAt(a);
            }            
            this.Items_WindowInfo = lstInfo.ToArray();
            计算();
        }

        // 用于同步的锁对象
        private readonly object _lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowInfo"></param>
        /// <param name="state">=0的状态也要写进去</param>
        internal void Add(_windowInfo_[] windowInfo, int state)
        {
            lock (_lock)
            {
                //查询一下,当前状态为哪一条状态
                _windowInfo_[] m = windowInfo.Where(p => p.状态 == state).ToArray();
                //如果没有找到,就退出,
                if (m.Length == 0)
                {
                    return;
                }
                _windowInfo_ info = m[0];
                Add(info, state);
            }
        }



        #region 方法


        internal string 默认标题 = "";
        internal Brush 默认背景色 = Brushes.WhiteSmoke;
        internal bool Is普通标题栏UI_ = true;
        internal WindowTitleState _windowTitleState = WindowTitleState.闲置中;


        void 计算()
        {

            if (this.Is普通标题栏UI_)
            {
                return;
            }

            #region 计算

            _windowInfo_[] beff = this.Items_WindowInfo;

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
                show += $"【{s.内容}】";
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



            this.Title = beff.Length == 0 ? this.默认标题 : show;
            this.Background_grid = a == -1 ? Brushes.Red : a == 1 ? Brushes.Yellow : this.默认背景色;
            _windowTitleState = a == -1 ? WindowTitleState.报警中 : a == 1 ? WindowTitleState.加工中 : WindowTitleState.闲置中;

        }


        #endregion


    }
}
