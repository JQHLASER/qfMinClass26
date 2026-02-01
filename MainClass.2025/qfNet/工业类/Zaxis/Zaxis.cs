using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qfNet
{
    /// <summary>
    /// 正运动
    /// </summary>
    public class Zaxis : qfWork.Zaxis
    {
        public Zaxis(string 控制器名称, int IO输入组数 = 2, int IO输出组数 = 2, string pathCfg = "", bool is匹配功能码_ = true) : base(控制器名称, IO输入组数, IO输出组数, pathCfg, is匹配功能码_)
        {

        }


        /// <summary>
        /// 
        /// </summary>       
        public void 标题栏状态_生成状态参数(out qfNet._cfg_标题栏状态_[] 信息, string 名称 = "Zaxis")
        {
            信息 = new qfNet._cfg_标题栏状态_[]
                {
                     new qfNet._cfg_标题栏状态_(名称,名称+Language_ .Get语言 ("未连接"),(int)qfmain  ._连接状态_.未连接 ),
                     new qfNet._cfg_标题栏状态_(名称,名称+Language_ .Get语言 ("连接中"),(int)qfmain  ._连接状态_.连接中  ),
                     new qfNet._cfg_标题栏状态_(名称,名称+Language_ .Get语言 ("已连接"),(int)qfmain  ._连接状态_.已连接  ),
                     new qfNet._cfg_标题栏状态_(名称,名称+Language_ .Get语言 ("硬件不匹配"),(int)qfmain  ._连接状态_.功能码不匹配   ),
                };

        }


        public void 窗体_查看IO()
        {
            using (Form_Zaxis_IO查看 forms = new Form_Zaxis_IO查看(this))
            {
                forms.ShowDialog();
            }
        }





    }
}
