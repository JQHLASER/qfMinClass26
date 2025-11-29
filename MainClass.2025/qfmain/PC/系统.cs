using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfmain 
{
    public  class 系统
    {
        public virtual  void 获取显示器尺寸(out int width, out int height, bool 是否含任务栏 = false)
        {
            if (是否含任务栏)
            {
                //不含任务栏
                Rectangle taskbarRect = Screen.PrimaryScreen.WorkingArea;
                width = taskbarRect.Width;
                height = taskbarRect.Height;
            }
            else
            {
                //不计算任务栏
                Screen screen = Screen.PrimaryScreen;
                width = screen.Bounds.Width;
                height = screen.Bounds.Height;
            }

        }

        public virtual string char数组toString(char[] charB_)
        {
            return new string(charB_);
        }

        public virtual void Ico_使能项目图标(Form form)
        {
            try
            {
                form.Icon = Icon.ExtractAssociatedIcon(Environment.CurrentDirectory);
            }
            catch (Exception ex)
            {
            }
        }


        public virtual string stringTo大写(string data)
        {
            return data.ToUpper();
        }

        public virtual string stringTo小写(string data)
        {
            return data.ToLower();
        }


        public virtual List<string> 获取系统中所有的字体()
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            var MyFontFamilies = MyFont.Families;
            List<String> list = new List<String>();
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                string FontName = MyFontFamilies[i].Name;
                list.Add(FontName);
            }

            return list;

        }


        public virtual double mmTo像素(int DPI, double value_mm)
        {
            double a = (double)((double)value_mm / 25.4);
            double px = (double)((double)a * DPI);
            return px;
        }

        public virtual double 像素To_mm(int DPI, int 像素)
        {
            double 英寸 = (double)((double)像素 / (double)DPI);
            double mm = 英寸 * 25.4;

            return mm;
        }

    }
}
