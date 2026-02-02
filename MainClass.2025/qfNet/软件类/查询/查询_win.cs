using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public class 查询_win<T> where T : class
    {
        /// <summary>
        /// 每页显示多少行
        /// </summary>
        uint _每页行数 = 1000;
        Form_查询 _forms = null;

        /// <summary>
        /// 当前显示页的数据
        /// </summary>
        public List<T> _lst_当前页数据 = new List<T>();

        /// <summary>
        /// 所有查询到的数据
        /// </summary>
        public List<T> _lst所有数据 = new List<T>();

        public qfmain.List分页_._PageInfo_ _页信息 = new qfmain.List分页_._PageInfo_();
        public qfNet.DataGridview_ _datagridviewSys;

        /// <summary>
        /// 显示到DataGridview中的数据
        /// </summary>
        public BindingSource _BindingSource = new BindingSource();

        public 查询_win(uint 每页多少行_)
        {
            this._每页行数 = 每页多少行_;
        }


        public void 窗体()
        {
            using (this._forms = new Form_查询())
            {
                this._forms.Event_Load_进入时 += (s) =>
                {
                    _datagridviewSys = new DataGridview_(this._forms.dataGridView1).格式化();


                    this._BindingSource.DataSource = this._lst_当前页数据;
                    s.dataGridView1.DataSource = this._BindingSource;

                    _datagridviewSys.使能修改列宽(true)
                            .设置字体_整体(new Font("微软雅黑", 10f, System.Drawing.FontStyle.Regular))
                            .设置行高(30)
                            .列为只读();


                    this.Event_Load_进入时?.Invoke(s);
                };

                this._forms.Event_FormClosing_关闭时 += (s) =>
                {
                    this._lst所有数据.Clear();
                    this._lst_当前页数据.Clear();
                    this._页信息 = new qfmain.List分页_._PageInfo_();
                    this.Event_FormClosing_关闭时?.Invoke(s);
                };

                this._forms.Event_查询 += (s) =>
                {
                    List<T> rt = this.Event_查询?.Invoke(s);
                    if (rt != null && rt.Count > 0)
                    {
                        this._lst所有数据 = rt;
                        this.到指定页(s, 0);
                    }
                };

                this._forms.Event_导出 += (s) =>
                {
                    this.Event_导出?.Invoke(s, this._lst所有数据);
                };

                this._forms.Event_上一页 += (s) =>
                {

                    #region 上一页

                    if (!Err_当前已在第一页(out string msgErr))
                    {
                        MessageBox.Show(msgErr);
                        return;
                    }
                    uint a = _页信息.当前页 <= 0 ? 0 : _页信息.当前页 - 1;
                    到指定页(s, a);

                    #endregion 

                    this.Event_上一页?.Invoke(s);
                };

                this._forms.Event_下一页 += (s) =>
                {

                    #region 下一页

                    if (!Err_当前已在最后一页(out string msgErr))
                    {
                        MessageBox.Show(msgErr);
                        return;
                    }
                    int a = _页信息.当前页 > this._页信息.总页数 ? (int)(this._页信息.总页数 - 1) : (int)(_页信息.当前页 + 1);
                    if (a < 0)
                    {
                        a = 0;
                    }
                    到指定页(s, (uint)a);

                    #endregion

                    this.Event_下一页?.Invoke(s);
                };

                this._forms.Event_到指定页 += (f, s) =>
                {

                    #region 到指定页

                    if (s <= 0 && !Err_当前已在第一页(out string msgErr))
                    {
                        MessageBox.Show(msgErr);
                        return;
                    }
                    else if ((s >= this._页信息.总页数 - 1) && !Err_当前已在最后一页(out msgErr))
                    {
                        MessageBox.Show(msgErr);
                        return;
                    }
                    到指定页(f, s);

                    #endregion

                    this.Event_到指定页?.Invoke(f, s);
                };

                this._forms.ShowDialog();
            }

        }

        public event Action<Form_查询> Event_FormClosing_关闭时;
        public event Action<Form_查询> Event_Load_进入时;

        public event Func<Form_查询, List<T>> Event_查询;
        /// <summary>
        /// 参数(forms,所有数据)
        /// </summary>
        public event Action<Form_查询, List<T>> Event_导出;


        public event Action<Form_查询> Event_上一页;
        public event Action<Form_查询> Event_下一页;
        /// <summary>
        /// (int 页索引 )
        /// </summary>
        public event Action<Form_查询, uint> Event_到指定页;

        public async Task 导出(List<string[]> lstCsv)
        {
            #region 导出

            if (!Err_无数据(out string msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "csv|*.csv";
            DialogResult drt = save.ShowDialog();
            if (drt == DialogResult.OK)
            {
                string path = save.FileName;
                (bool state, string msg) rt = await new qfmain.Csv_高效().WriteLinesAsync(path, lstCsv, false);
                if (!rt.state)
                {
                    MessageBox.Show(rt.msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show($"导出成功\r\n共{this._lst所有数据.Count}行");
                    return;
                }
            }

            #endregion
        }


        #region 本地方法



        /// <summary>
        /// 页码 : 从0开始
        /// </summary> 
        (bool state, string msg, T[] value, qfmain.List分页_._PageInfo_ 页信息) 分页(List<T> lst, uint 页索引)
        {
            try
            {
                T[] beff = new qfmain.List分页_().分页_仅获取指定页_大数据量<T>(lst, 页索引, this._每页行数, out qfmain.List分页_._PageInfo_ 页信息);
                return (true, default, beff, 页信息);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, default, default);
            }

        }

        /// <summary>
        /// 调用 生成显示信息 来显示信息
        /// </summary>
        /// <param name="value"></param>
        void 设置信息(string value)
        {
            this._forms.设置显示信息(value);
        }

        /// <summary>
        ///  生成后,调用设置信息显示
        /// </summary> 
        string 生成显示信息(qfmain.List分页_._PageInfo_ 页信息)
        {
            return this._forms.生成显示信息(页信息);
        }
        /// <summary>
        /// 赋值并显示当前页数据
        /// </summary> 
        public void 显示当前页数据(List<T> lst)
        {
            this._lst_当前页数据 = new List<T>(lst);
            _BindingSource.DataSource = this._lst_当前页数据;
        }

        /// <summary>
        /// 页码从0开始
        /// </summary>
        /// <param name="页码"></param>
        void 到指定页(qfNet.Form_查询 forms, uint 页码)
        {
            if (!Err_无数据(out string msgErr) || !Err_页码超出范围(页码, out msgErr))
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            (bool s, string msg, T[] beff, qfmain.List分页_._PageInfo_ 页信息) rt =
                分页(this._lst所有数据, 页码);
            if (!rt.s)
            {
                MessageBox.Show(rt.msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                显示当前页数据(rt.beff.ToList());
                _页信息 = rt.页信息;
                string show = forms.生成显示信息(_页信息);
                forms.Text = show;
            }


        }


        #endregion


        #region Err

        private bool Err_当前已在最后一页(out string msgErr)
        {
            msgErr = "";
            if (_页信息.当前页 >= _页信息.总页数 - 1)
            {
                msgErr = "当前已在最后一页";
                return false;
            }
            return true;
        }

        private bool Err_当前已在第一页(out string msgErr)
        {
            msgErr = "";
            if (_页信息.当前页 <= 0)
            {
                msgErr = "当前已在第一页";
                return false;
            }
            return true;
        }

        private bool Err_无数据(out string msgErr)
        {
            msgErr = string.Empty;
            if (this._lst所有数据.Count == 0)
            {
                msgErr = "无数据";
                return false;
            }
            return true;
        }
        private bool Err_页码超出范围(uint 页码, out string msgErr)
        {
            msgErr = string.Empty;
            if (页码 > this._页信息.总页数 || 页码 < 0)
            {
                msgErr = "页码超出范围";
                return false;
            }
            return true;
        }



        #endregion
    }
}
