using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet 
{
    /// <summary>
    /// 控件双缓冲扩展工具类
    /// </summary>
    public static class winForm双缓冲设置
    {
        // 双缓冲扩展样式常量（替代魔法数字，提高可读性）
        private const int WS_EX_COMPOSITED = 0x02000000;

        /// <summary>
        /// 为控件启用双缓冲（解决闪烁问题）
        /// </summary>
        /// <param name="control">需要启用双缓冲的控件（Form、Panel、UserControl等）</param>
        /// <param name="enable">是否启用（true=启用，false=禁用）</param>
        public static void SetDoubleBuffer(this Control control, bool enable = true)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control), "控件不能为空");

            // 方式1：通过修改 CreateParams（原生高效，支持所有控件）
            var field = typeof(Control).GetField("CreateParams",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (field != null)
            {
                CreateParams cp = (CreateParams)field.GetValue(control);
                if (enable)
                    cp.ExStyle |= WS_EX_COMPOSITED; // 添加双缓冲样式
                else
                    cp.ExStyle &= ~WS_EX_COMPOSITED; // 移除双缓冲样式
                field.SetValue(control, cp);
            }

          
        }
    }
}
