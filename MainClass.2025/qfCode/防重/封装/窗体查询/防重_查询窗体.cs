using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public class 防重_查询窗体
    {
        static uint 每页行数 = 1000;
        static int 最大查询月数 = 3;

        public static bool _功能_清除过期数据 = false;

        public static bool _功能_删除指定数据 = false;


        static bool _使能_右键菜单 = false;
        public static string _密码 = "QF8888";


        /// <summary>
        /// 防重的窗体
        /// </summary>
        public static qfNet.查询_win<qfCode.表_防重_.FC26> FC查询_sys = new qfNet.查询_win<qfCode.表_防重_.FC26>(每页行数);
        static 防重_封装_ _防重;

        internal static void 初始化(防重_封装_ 防重_)
        {
            _防重 = 防重_;
            if (_功能_删除指定数据)
                _使能_右键菜单 = true;


            FC查询_sys.Event_右键菜单 += (s) =>
            {
                
            };


            FC查询_sys.Event_删除指定数据 += (s) =>
            {
                if (_功能_删除指定数据)
                {
                    return false;
                }
                var lstFc = new List<表_防重_.FC26>()
                            {
                              FC查询_sys._lst_当前页数据[s]
                            };
                var rtde = _防重.删除(lstFc);
                if (!rtde.s)
                {
                    MessageBox.Show(rtde.m, "Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("OK");
                }

                return rtde.s;
            };
             
            FC查询_sys.Event_Load_进入时 += (s) =>
            {
                格式化(s);


                if (_功能_清除过期数据)
                {
                    if (new qfNet.软件类().Win_密码输入框(_密码, "") == DialogResult.Yes)
                    {
                        using (var forms = new Form_防重_参数())
                        {
                            forms.Load += (s1, e1) =>
                            {
                                forms.uiTextBox_保存天数.IntValue = _防重._参数.数据保存时间;
                            };

                            forms.uiButton_保存.Click += (s1, e1) =>
                            {
                                _防重._参数.数据保存时间 = forms.uiTextBox_保存天数.IntValue;
                                _防重.读写参数(0);
                                MessageBox.Show("OK");
                            };

                            #region 删除清理

                            forms.uiButton_执行清理数据.Click += (s1, e1) =>
                            {
                                DateTime nowDate = new qfmain.日期时间_().增减时间(DateTime.Now, 2, forms.uiTextBox_保存天数.IntValue);
                                var rtDe = _防重.删除_清除过期数据(nowDate);
                                if (rtDe.s)
                                {
                                    MessageBox.Show("OK");
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show($"{rtDe.m}", "NG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            };

                            #endregion
                        }
                    }
                }


            };

            FC查询_sys.Event_导出 += (s, e) =>
            {
                #region 导出


                List<string[]> lstCsv = new List<string[]>();
                lstCsv.Add(new string[]
                {
                    "时间",
                    "内容",
                    "信息",
                });

                foreach (var a in e)
                {
                    lstCsv.Add(new string[]
                    {
                        a.时间 ,
                        a.内容 ,
                        a.信息 ,
                    });
                }

                FC查询_sys.导出(lstCsv);

                #endregion
            };

            FC查询_sys.Event_查询 += (s) =>
            {
                List<qfCode.表_防重_.FC26> lst = new List<qfCode.表_防重_.FC26>();
                using (Form_防重查询 forms = new Form_防重查询())
                {
                    #region 查询事件

                    forms.uiButton_查询.Click += (a, b) =>
                    {
                        DateTime start = forms.uiDatePicker_Start.Value;
                        DateTime end = forms.uiDatePicker_End.Value;
                        if (start > end)
                        {
                            MessageBox.Show(Language_.Get语言("时间范围非法"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (start.AddMonths(最大查询月数) < end)
                        {
                            MessageBox.Show(Language_.Get语言("超出查询范围"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        qfCode._Type防重_._查询信息_ cfg = new qfCode._Type防重_._查询信息_
                        {
                            start = start,
                            end = end,
                            内容 = forms.uiTextBox_内容.Text,
                            Is模糊查询 = forms.uiCheckBox_模糊查询.Checked,
                        };

                        var rt = _防重.查询(cfg);
                        if (!rt.s)
                        {
                            MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (rt.lst.Count == 0)
                        {
                            MessageBox.Show(Language_.Get语言("未查询到数据"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        lst = rt.lst;
                        forms.DialogResult = System.Windows.Forms.DialogResult.OK;
                    };

                    #endregion

                    forms.ShowDialog();
                }
                return lst;
            };


        }


        internal static void 窗体()
        {
            FC查询_sys.窗体(_使能_右键菜单);
        }

        static void 格式化(qfNet.Form_查询 forms)
        {
            FC查询_sys._datagridviewSys.显示or隐藏列("GUID", false)
                .设置列宽("时间", 200)
                .设置列宽("内容", 350)
                .设置列宽("信息", 600)
                .使能修改列宽(true)
                ;

        }





    }
}
