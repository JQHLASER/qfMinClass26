using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfmain.log日志;

namespace qfNet
{
    public enum _文件弹窗类型_
    {
        打开,
        保存,
    }

    /// <summary>
    /// 显示文件信息
    /// </summary>
    public class _ShowInfo_
    {
        /// <summary>
        /// 文本显示的颜色
        /// </summary>
        public Color 颜色 { set; get; } = Color.Gray;
        public string 内容 { set; get; } = "";

        public Font fonts { set; get; } = null;


        /// <summary>
        /// =0:正常显示,-1:清除显示
        /// </summary>
        public int 状态 { set; get; } = 0;
        public _ShowInfo_()
        {

        }
        public _ShowInfo_(Color 颜色, string 内容, Font fonts = null)
        {
            this.颜色 = 颜色;
            this.内容 = 内容;
            if (fonts != null)
            {
                this.fonts = fonts;
            }

        }
        public _ShowInfo_(int 状态)
        {
            this.状态 = 状态;
        }
    }


}
