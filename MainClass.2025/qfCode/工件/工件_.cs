using Sunny.UI.Win32;
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

    public class 工件_<T>
    {

        public qfNet.文件_<T> Gj_sys;
        public qfmain._初始化状态_ _初始化状态 = qfmain._初始化状态_.未初始化;


        public 工件_(string 文件夹, string 后缀, _em_配方_工件_文件类型_ 类型 = _em_配方_工件_文件类型_.SQLite)
        {
            初始化(文件夹, 后缀, 类型);
        }
        public 工件_()
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


        public (DialogResult s, string m, string FileName) 另存为_弹窗(T t, Func<string, (bool s, string m)> Event_删除文件 = null)
        {
            DialogResult dlt = Gj_sys._Iwork.另存为_弹窗(t, out string NewFileName, out string msgErr, Event_删除文件);
            return (dlt, msgErr, NewFileName);
        }



        /// <summary>
        /// 设置窗体中,操作后是否需要保存
        /// <para>true:需要保存,false:不需要保存</para>
        /// </summary>
        public bool _Is_是否需要保存 = false;

        /// <summary>       
        ///  <para>con : 大小 650*550</para>
        /// </summary> 
        public DialogResult Win_设置(Control con)
        {
            using (Form_工件 forms = new Form_工件(con))
            {
                forms.Load += (s, e) =>
                {
                    Event_forms_Load?.Invoke(forms);
                };

                forms.ui_工具栏_文件操作1.Event_新建 += () =>
                {
                    #region 新建
                    if (!Event_forms_是否需要保存(forms))
                    {
                        return;
                    }
                   else  if (MessageBox.Show(Language_.Get语言("新建?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        On_是否需要保存(forms);
                        Event_forms_新建?.Invoke(forms);
                    }

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_打开 += () =>
                {
                    #region 打开

                    if (!Event_forms_是否需要保存(forms))
                    {
                        return;
                    }
                    var rt = 打开_弹窗();
                    if (rt.s == DialogResult.No)
                    {
                        MessageBox.Show(rt.m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (rt.s == DialogResult.Yes)
                    {
                        Event_forms_打开?.Invoke(forms, rt.FileName, rt.cfg);
                    }
                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_保存 += () =>
                {
                    #region 保存 

                    if (string.IsNullOrWhiteSpace(forms.Text))
                    {
                        另存为(forms);
                        return;
                    }
                    Event_forms_保存?.Invoke(forms);

                    #endregion
                };
                forms.ui_工具栏_文件操作1.Event_另存为 += () =>
                {
                    另存为(forms);
                };
                forms.ui_工具栏_文件操作1.Event_删除 += () =>
                {
                    #region 删除 

                    if (MessageBox.Show(Language_.Get语言("删除?"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Event_forms_删除当前文件?.Invoke(forms);
                    }

                    #endregion
                }; 
                forms.ui_工具栏_文件操作1.Event_关闭 += () =>
                {
                    #region 退出

                     if (Event_forms_Close != null)
                    {
                        if (Event_forms_Close.Invoke(forms))
                        {
                            forms.Close();
                            return;
                        }
                    }
                    #endregion
                };


                DialogResult dlt = forms.ShowDialog();
                return dlt;
            }
        }

        #region 窗体

        void 另存为(Form_工件 forms)
        {
            Event_forms_另存为?.Invoke(forms);
        }

        bool On_是否需要保存(Form_工件 forms)
        {

            return Event_forms_是否需要保存 is null ? false : true;
        }




        #endregion


        #region 事件

        public event Action<Form_工件> Event_forms_Load;

        public event Func<Form_工件, bool> Event_forms_Close;


        /// <summary>
        /// 在操作前,确认是否需要保存
        /// </summary>
        public event Func<Form_工件, bool> Event_forms_是否需要保存;
        public event Action<Form_工件> Event_forms_保存;



        public event Action<Form_工件> Event_forms_删除当前文件;

        /// <summary>
        /// 未弹出对话框了的
        /// </summary>
        public event Action<Form_工件> Event_forms_另存为;
        public event Action<Form_工件> Event_forms_新建;

        /// <summary>
        /// 参数(Form_配方,文件名称,信息)
        /// <para>已弹出对话框了的,已打开,不要重复打开</para>
        /// </summary>
        public event Action<Form_工件, string, T> Event_forms_打开;




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

        #region 本地方法






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
