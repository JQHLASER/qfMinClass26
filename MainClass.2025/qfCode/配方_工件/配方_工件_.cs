using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public enum _em_配方_工件_文件类型_
    {
        ini,
        SQLite,
    }

    public class 配方_工件_<T>
    {

        public qfNet.文件_<T> Gj_sys;
        public qfmain._初始化状态_ _初始化状态 = qfmain._初始化状态_.未初始化;


        public 配方_工件_(string 文件夹, string 后缀, _em_配方_工件_文件类型_ 类型 = _em_配方_工件_文件类型_.SQLite)
        {
            初始化(文件夹, 后缀, 类型);
        }
        public 配方_工件_()
        {

        }



        public void 初始化(string 文件夹, string 后缀, _em_配方_工件_文件类型_ 类型 = _em_配方_工件_文件类型_.SQLite)
        {
            
            Gj_sys = new qfNet.文件_<T>();
            Gj_sys.Event_初始化状态 += (s, e) => this.On_初始化状态(s, e);


            switch (类型)
            {
                case _em_配方_工件_文件类型_.ini:
                    Gj_sys.初始化_ini(文件夹, "", 后缀);
                    break;
                case _em_配方_工件_文件类型_.SQLite:
                    Gj_sys.初始化_SQLite(文件夹, "");
                    break;
            }
             
        }





        public (bool s, string m) 保存(string FileName, T Cfg)
        {
            bool rt = Gj_sys._Iwork.保存(FileName, Cfg, out string msgErr);
            return (rt, msgErr);
        }

        public (bool s, string m) 另存为(string FileName, string NewFileName)
        {
            bool rt = Gj_sys._Iwork.另存为(FileName, NewFileName, out string msgErr);
            return (rt, msgErr);
        }

        public (bool s, string m) 删除(string FileName)
        {
            bool rt = Gj_sys._Iwork.删除(FileName, out string msgErr);
            return (rt, msgErr);
        }

        public (bool s, string m, T cfg) 打开(string FileName)
        {
            T cfg = qfmain.T_实例化泛型.FastNew<T>.Create();
            bool rt = Gj_sys._Iwork.打开(FileName, ref cfg, out string msgErr);
            return (rt, msgErr, cfg);
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// </summary> 
        public (DialogResult s, string m, string FileName, T cfg) 打开_弹窗()
        {
            T cfg = qfmain.T_实例化泛型.FastNew<T>.Create();
            var rt = Gj_sys._Iwork.打开_弹窗(ref cfg, out string FileName, out string msgErr, On_弹窗时删除);
            return (rt, msgErr, FileName, cfg);
        }


        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// 文件名不为空时直接保存
        /// <para>文件名为空时弹窗另存为</para>
        /// </summary> 
        public (DialogResult s, string m, string FileName) 保存_弹窗(string FileName, T cfg)
        {
            var rt = Gj_sys._Iwork.保存_弹窗(FileName, cfg, out string NewFileName, out string msgErr, On_弹窗时删除);
            return (rt, msgErr, NewFileName);
        }

        /// <summary>
        /// <para> 返回 DialogResult.Yes ,成功</para>
        /// <para> 返回 DialogResult.No ,失败</para>
        /// <para> 返回 其它,None</para>
        /// <para>FileName:源文件名称,为空时为弹窗保存</para>
        /// </summary> 
        public (DialogResult s, string msgErr, string NewFileName) 另存为_弹窗(string FileName, T cfg)
        {
            var rt = Gj_sys._Iwork.保存_弹窗(FileName, cfg, out string NewFileName, out string msgErr, On_弹窗时删除);
            return (rt, msgErr, NewFileName);
        }




        /// <summary>
        /// 设置窗体中,操作后是否需要保存
        /// <para>true:需要保存,false:不需要保存</para>
        /// </summary>
        public bool _Is_是否需要保存 = false;

        /// <summary>
        ///  配方名称 : 要打开的配方名称,空时为新建
        ///  <para>con : 大小 650*550</para>
        /// </summary> 
        public DialogResult Win_设置(Control con, string 配方名称)
        {
            T _cfg = qfmain.T_实例化泛型.FastNew<T>.Create();

            using (Form_配方 forms = new Form_配方(con))
            {
                forms.Event_进入时 += () =>
                {
                    #region  进入时

                    Event_新建(forms);
                    if (!string.IsNullOrEmpty(配方名称))
                    {
                        var rt = 打开(配方名称);
                        if (rt.s)
                        {
                            _cfg = rt.cfg;
                            On_显示信息(配方名称, _cfg, forms);
                        }
                        else
                        {
                            MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    #endregion
                };

                forms.ui_工具栏_文件操作1.Event_新建 += () =>
                {
                    #region 新建

                    if (MessageBox.Show(Language_.Get语言("新建?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (On_保存确认(forms, _cfg))
                        {
                            _cfg = qfmain.T_实例化泛型.FastNew<T>.Create();
                            Event_新建(forms);
                            On_显示信息("", _cfg, forms);
                        }
                    }

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_打开 += () =>
                {
                    #region 打开

                    if (On_保存确认(forms, _cfg))
                    {
                        var rt = 打开_弹窗();
                        if (rt.s != DialogResult.Yes)
                        {
                            MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        _cfg = rt.cfg;
                        On_显示信息("", _cfg, forms);

                    }

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_保存 += () =>
                {
                    #region 保存 

                    保存(forms, _cfg);

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_另存为 += () =>
                {
                    #region 另存为

                    var rt = 另存为_弹窗(forms._配方文件名, _cfg);
                    if (rt.s != DialogResult.Yes)
                    {
                        MessageBox.Show(rt.msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    On_显示信息(forms._配方文件名, _cfg, forms);

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_删除 += () =>
                {
                    #region 删除 

                    if (MessageBox.Show(Language_.Get语言("删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (On_保存确认(forms, _cfg))
                        {
                            var rt = 删除(forms._配方文件名);
                            if (rt.s)
                            {
                                MessageBox.Show(Language_.Get语言("删除成功"));
                                _cfg = qfmain.T_实例化泛型.FastNew<T>.Create();
                                Event_新建(forms);
                                return;
                            }
                            else
                            {
                                MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_关闭 += () =>
                {
                    #region 退出

                    if (On_保存确认(forms, _cfg))
                    {
                        forms.Close();
                        return;
                    }

                    #endregion
                };


                DialogResult dlt = forms.ShowDialog();
                return dlt;
            }
        }

        #region 本地方法

        bool 保存(Form_配方 forms, T cfg)
        {
            this._Is_是否需要保存 = false;
            var rt = 保存_弹窗(forms._配方文件名, cfg);
            if (rt.s != DialogResult.Yes)
            {
                MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }


        bool On_保存确认(Form_配方 forms, T cfg)
        {
            if (this._Is_是否需要保存 && MessageBox.Show(Language_.Get语言("是否保存?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                保存(forms, cfg);
            }
            return true;
        }




        #endregion




        #region 事件




        /// <summary>
        /// 清空全部
        /// </summary>
        public event Action<Form_配方> Event_新建;
        void On_新建(Form_配方 forms)
        {
            forms._配方文件名 = "";
            forms.Text = "";
            Event_新建?.Invoke(forms);
        }

        public event Action<T, Form_配方> Event_显示信息;
        void On_显示信息(string 配方名称, T cfg, Form_配方 forms)
        {
            forms._配方文件名 = 配方名称;
            forms.Text = 配方名称;
            Event_显示信息?.Invoke(cfg, forms);
        }


        /// <summary>
        /// 响应事件
        /// </summary> 
        (bool s, string m) On_弹窗时删除(string FIleName)
        {
            return 删除(FIleName);
        }

        public event Action<qfmain._初始化状态_, string> Event_初始化状态;
        void On_初始化状态(qfmain._初始化状态_ state, string msgErr)
        {
            this._初始化状态 = state;
            Event_初始化状态?.Invoke(state, msgErr);
        }






        #endregion

        #region Err

        public bool Err_未初始化(string Name, out string msgErr)
        {
            msgErr = "";
            if (this._初始化状态 != qfmain._初始化状态_.已初始化)
            {
                msgErr = $"{Name},{Language_.Get语言("未初始化")}";
                return false;
            }
            return true;
        }


        #endregion

    }
}
