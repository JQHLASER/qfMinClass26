using qfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    public class Win_标题栏状态
    {

        /// <summary>
        ///  qfWork._连接状态_ 
        /// </summary>
        /// <param name="ui标题栏"></param>         
        /// <param name="Name">字段名</param>
        /// <param name="名称">显示名称</param>
        /// <param name="状态">qfWork._连接状态_ </param>
        public void 连接状态_qfWork(ui_window_Title ui标题栏, string Name, string 名称, qf_Laser._连接状态_ 状态)
        {
            qfWPFmain._windowInfo_[] info = new _windowInfo_[]
           {
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._连接状态_.已连接,$"{名称}{Language_ .Get语言("已连接")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._连接状态_.连接中,$"{名称}{Language_ .Get语言("连接中")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._连接状态_.未连接,$"{名称}{Language_ .Get语言("未连接")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._连接状态_.功能码不匹配,$"{名称}{Language_ .Get语言("硬件不匹配")}"  ),
           };
            ui标题栏.Add(info, (int)状态);
        }


             public void 连接状态_qfWork(ui_window_Title ui标题栏, string Name, string 名称, qfWork ._连接状态_ 状态)
        {
            qfWPFmain._windowInfo_[] info = new _windowInfo_[]
           {
              new qfWPFmain._windowInfo_(Name,(int)qfWork._连接状态_.已连接,$"{名称}{Language_ .Get语言("已连接")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qfWork._连接状态_.连接中,$"{名称}{Language_ .Get语言("连接中")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qfWork._连接状态_.未连接,$"{名称}{Language_ .Get语言("未连接")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qfWork._连接状态_.功能码不匹配,$"{名称}{Language_ .Get语言("硬件不匹配")}"  ),
           };
            ui标题栏.Add(info, (int)状态);
        }



        public void 初始化状态_qfWork(ui_window_Title ui标题栏, string Name, string 名称, qf_Laser._初始化状态_ 状态)
        {
            qfWPFmain._windowInfo_[] info = new _windowInfo_[]
           {
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._初始化状态_ .已初始化 ,$"{名称}{Language_ .Get语言("已初始化")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._初始化状态_.未初始化,$"{名称}{Language_ .Get语言("未初始化")}"  ),
              new qfWPFmain._windowInfo_(Name,(int)qf_Laser._初始化状态_.初始化中 ,$"{名称}{Language_ .Get语言("初始化中")}"  ),

           };
            ui标题栏.Add(info, (int)状态);
        }


        public void 标题栏状态 (ui_window_Title ui标题栏, _windowInfo_[] info, int state )
        {         
            ui标题栏.Add(info, state);
        }

    }
}
