using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainclassqf 
{
    /// <summary>
    /// 根据窗体变换自动更新控件大小
    /// </summary>
    public class Win_窗体自动调整大小1
    {
        class Set参数
        {

            public static float X { set; get; }//当前窗体的宽度
            public static float Y { set; get; }//当前窗体的高度
            public static bool Display_State_Lock { set; get; } = false;//显示状态锁
        }

        //将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }

        //根据窗体大小调整控件大小
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = (float)(System.Convert.ToSingle(mytag[0]) * newx);//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = (float)(System.Convert.ToSingle(mytag[1]) * newy);//高度
                con.Height = (int)(a);
                a = (float)(System.Convert.ToSingle(mytag[2]) * newx);//左边距离
                con.Left = (int)(a);
                a = (float)(System.Convert.ToSingle(mytag[3]) * newy);//上边缘距离
                con.Top = (int)(a);
                Single currentSize = (Single)(System.Convert.ToSingle(mytag[4]) * newy);//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }



        /// <summary>
        /// 把此方法放进窗体的“Resize”事件中
        /// </summary>
        /// <param name="form">参数写入 this 即可</param>
        public void Form_Resize(Form form)
        {
            if (Set参数.Display_State_Lock)
            {
                float newx = (float)((form.Width) / Set参数.X); //窗体宽度缩放比例
                float newy = (float)((form.Height) / Set参数.Y);//窗体高度缩放比例
                setControls(newx, newy, form);//随窗体改变控件大小              
            }
        }


        /// <summary>
        /// 把此方法放进窗体的“Load”事件中
        /// </summary>
        /// <param name="form">参数写入 this 即可</param>
        public void Form_Load(Form form)
        {

            Set参数.X = form.Width;//获取窗体的宽度
            Set参数.Y = form.Height;//获取窗体的高度
            setTag(form);//调用方法
            Set参数.Display_State_Lock = true;
        }


        /// <summary>
        /// 将此方法放进窗体的“Load”事件中
        /// </summary>
        /// <param name="form">参数输入 this </param>
        /// <param name="FormMaximized">此参数是：是否把窗体设置为最大化 bool值</param>
        public void Form_Load_1(Form form, bool FormMaximized)
        {
            Set参数.X = form.Width;//获取窗体的宽度
            Set参数.Y = form.Height;//获取窗体的高度
            setTag(form);//调用方法
            if (FormMaximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }
            Set参数.Display_State_Lock = true;
        }


        class Info_控件信息_
        {
            public Control con控件 { set; get; }           
        }

        public class info_窗体设计大小_
        {
            public int width { set; get; }
            public int height { set; get; }
        }



        public void Form_Load_2(Form forms,info_窗体设计大小_ size)
        {
           

        }



    }

}
