using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainclassqf
{
    public class Win_窗体自动调整大小
    {
        public class info_Controls_
        {
            public Control[] cons { set; get; } = new Control[] { };
            public int Form_width { set; get; }
            public int Form_height { set; get; }
        }

        public class Config_
        {
            public info_Controls_ 原始 { set; get; }

            public int 最大改变窗体大小次数_开始修改控件 { set; get; } = 1;
        }

        public Config_ Config = new Config_();

        /// <summary>
        /// 放于窗体的show事件
        /// </summary>
        /// <param name="forms"></param>
        /// <param name="info"></param>
        void 获取窗体信息(Form forms, out info_Controls_ info)
        {

            info = new info_Controls_();
            List<Control> lst = new List<Control>();
            foreach (Control s in forms.Controls)
            {
                lst.Add(s);
            }

            info.cons = lst.ToArray();
            info.Form_width = forms.Width;
            info.Form_height = forms.Height;

        }
        void 计算(Form forms, info_Controls_ info原始)
        {

            获取窗体信息(forms, out info_Controls_ info当前);
            double 比例x = ((double)info当前.Form_width / (double)info原始.Form_width);
            double 比例y = ((double)info当前.Form_height / (double)info原始.Form_height);

            for (int i = 0; i < forms.Controls.Count; i++)
            {
                Control con原 = info原始.cons[i];
                Control con新 = forms.Controls[i];

                con新.Width = int.Parse(((double)con原.Width * 比例x).ToString("0"));
                con新.Height = int.Parse(((double)con原.Height * 比例y).ToString("0"));

                con新.Top = (int)((double)con原.Top * 比例y);
                con新.Left = (int)((double)con原.Left * 比例x);



                #region 下一级控件

                for (int a = 0; a < con新.Controls.Count; a++)
                {
                    Control con_ = con新.Controls[a];
                    con_.Width = int.Parse((con原.Controls[a].Width * 比例x).ToString("0"));
                    con_.Height = int.Parse((con原.Controls[a].Height * 比例y).ToString("0"));
                    con_.Top = int.Parse((con原.Controls[a].Top * 比例y).ToString("0"));
                    con_.Left = int.Parse((con原.Controls[a].Left * 比例x).ToString("0"));

                    #region 下一级控件
                    for (int a1 = 0; a1 < con_.Controls.Count; a1++)
                    {
                        Control con_1 = con_.Controls[a1];
                        con_1.Width = int.Parse((con原.Controls[a1].Width * 比例x).ToString("0"));
                        con_1.Height = int.Parse((con原.Controls[a1].Height * 比例y).ToString("0"));
                        con_1.Top = int.Parse((con原.Controls[a1].Top * 比例y).ToString("0"));
                        con_1.Left = int.Parse((con原.Controls[a1].Left * 比例x).ToString("0"));


                        #region 下一级控件
                        for (int a2 = 0; a1 < con_1.Controls.Count; a2++)
                        {
                            Control con_2 = con_1.Controls[a2];
                            con_2.Width = int.Parse((con原.Controls[a2].Width * 比例x).ToString("0"));
                            con_2.Height = int.Parse((con原.Controls[a2].Height * 比例y).ToString("0"));
                            con_2.Top = int.Parse((con原.Controls[a2].Top * 比例y).ToString("0"));
                            con_2.Left = int.Parse((con原.Controls[a2].Left * 比例x).ToString("0"));

                            #region 下一级控件
                            for (int a3 = 0; a3 < con_2.Controls.Count; a3++)
                            {
                                Control con_3 = con_2.Controls[a2];
                                con_3.Width = int.Parse((con原.Controls[a3].Width * 比例x).ToString("0"));
                                con_3.Height = int.Parse((con原.Controls[a3].Height * 比例y).ToString("0"));
                                con_3.Top = int.Parse((con原.Controls[a3].Top * 比例y).ToString("0"));
                                con_3.Left = int.Parse((con原.Controls[a3].Left * 比例x).ToString("0"));

                                #region 下一级控件
                                for (int a4 = 0; a4 < con_3.Controls.Count; a4++)
                                {
                                    Control con_4 = con_3.Controls[a4];
                                    con_4.Width = int.Parse((con原.Controls[a4].Width * 比例x).ToString("0"));
                                    con_4.Height = int.Parse((con原.Controls[a4].Height * 比例y).ToString("0"));
                                    con_4.Top = int.Parse((con原.Controls[a4].Top * 比例y).ToString("0"));
                                    con_4.Left = int.Parse((con原.Controls[a4].Left * 比例x).ToString("0"));

                                }
                                #endregion

                            }
                            #endregion



                        }
                        #endregion



                    }
                    #endregion

                }


                #endregion

            }

        }

        public void 获取窗体信息(Form forms)
        {
            获取窗体信息(forms, out info_Controls_ info);
            Config.原始 = info;
        }
        public void 计算(Form forms)
        {
            计算(forms, Config.原始);

        }

        int 改变大小次数 = 0;
        /// <summary>
        /// 放于窗体的Resize事件
        /// </summary>
        /// <param name="forms"></param>
        public void 计算_ReSize(Form forms)
        {
            if (改变大小次数 > Config.最大改变窗体大小次数_开始修改控件)
            {
                改变大小次数 = 3;
                计算(forms);
            }

            改变大小次数++;
        }
    }
}
