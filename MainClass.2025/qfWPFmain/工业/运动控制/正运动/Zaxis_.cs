using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    /// <summary>
    /// 正运动
    /// </summary>
    public class Zaxis_ : qfWork.Zaxis
    {
        public Zaxis_(string 控制器名称, int IO输入组数 = 2, int IO输出组数 = 2, string pathCfg = "", bool is匹配功能码_ = true) : base(控制器名称, IO输入组数, IO输出组数, pathCfg, is匹配功能码_)
        {

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui标题栏"></param>
        /// <param name="Name">字段名</param>
        /// <param name="名称">显示名称</param>
        public void 标题栏状态(ui_window_Title ui标题栏, string 名称)
        {
            new Win_标题栏状态().连接状态_qfWork(ui标题栏, "正运动", 名称, this._连接状态);
        }


        public void 窗体_查看IO(Window d)
        {       
            new Win_Zaxis_IO查看(this ) { Owner = Window.GetWindow(d) }.ShowDialog();
        }



    }
}
