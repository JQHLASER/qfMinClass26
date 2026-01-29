using System;
using System.Windows.Forms;
using System.Reflection;


namespace qfNet
{

    /// <summary>
    /// 
    /// </summary>
    public static class winForm双缓冲设置
    {



        /// <summary>
        /// 自动给窗体及其所有子控件开启双缓冲，减少闪烁
        /// <para>1. 给窗体本身开启双缓冲</para>
        /// <para>this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);</para>
        /// <para>form.UpdateStyles();</para>
        /// </summary> 
        public static void EnableFormDoubleBuffer(Form form)
        {
            if (form == null) return;

            //// 1. 给窗体本身开启双缓冲
            //form.SetStyle(ControlStyles.AllPaintingInWmPaint |
            //              ControlStyles.UserPaint |
            //              ControlStyles.OptimizedDoubleBuffer, true);
            //form.UpdateStyles();

            // 2. 给所有子控件开启双缓冲（通过反射设置非公开 DoubleBuffered 属性）
            EnableControlDoubleBuffer(form);
        }

        private static void EnableControlDoubleBuffer(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                PropertyInfo pi = ctrl.GetType().GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (pi != null) pi.SetValue(ctrl, true, null);

                // 递归处理容器控件
                if (ctrl.HasChildren)
                    EnableControlDoubleBuffer(ctrl);
            }
        }
    }


}
