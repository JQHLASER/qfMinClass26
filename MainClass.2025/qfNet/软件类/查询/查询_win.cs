using System;
using System.Collections.Generic;
using System.Linq;
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
        uint _每页行数 = 200;
        Form_查询 _forms = null;


        public 查询_win(uint 每页多少行_)
        {
            this._每页行数 = 每页多少行_;
        }


        public void 窗体()
        {
            using (this._forms = new Form_查询())
            {
                this._forms.Event_查询 += (s) =>
                {
                    Event_查询?.Invoke(s);
                };

                this._forms.Event_FormClosing_关闭时 += (s) =>
                {
                    this.Event_FormClosing_关闭时?.Invoke(s);
                };

                this._forms.Event_导出 += (s) =>
                {
                    this.Event_导出?.Invoke(s);
                };

                this._forms.Event_上一页 += (s) =>
                {
                    this.Event_上一页?.Invoke(s);
                };

                this._forms.Event_下一页 += (s) =>
                {
                    this.Event_下一页?.Invoke(s);
                };

                this._forms.Event_到指定页 += (f, s) =>
                {
                    this.Event_到指定页?.Invoke(f, s);
                };

                this._forms.ShowDialog();
            }

        }

        public event Action<Form> Event_FormClosing_关闭时;
        public event Action<Form> Event_Load_进入时;

        public event Action<Form> Event_查询;
        public event Action<Form> Event_导出;

        public event Action<Form> Event_上一页;
        public event Action<Form> Event_下一页;
        /// <summary>
        /// (int 页索引 )
        /// </summary>
        public event Action<Form, int> Event_到指定页;


        /// <summary>
        /// 页码 : 从0开始
        /// </summary> 
        public (bool state, string msg, T[] value, qfmain.List分页_._PageInfo_ 页信息) 分页(List<T> lst, uint 页索引)
        {
            try
            {
                T[] beff = new qfmain.List分页_().分页_仅获取指定页_大数据量<T>(lst, 页索引, this._每页行数, out qfmain.List分页_._PageInfo_ 页信息);
                return (true, default, beff, 页信息);
            }
            catch (Exception ex)
            {
                return (false,ex.Message, default, default);
            } 

        }

        /// <summary>
        /// 调用 生成显示信息 来显示信息
        /// </summary>
        /// <param name="value"></param>
        public void 设置信息(string value)
        {
            this._forms.设置显示信息 (value);
        }

        /// <summary>
        ///  生成后,调用设置信息显示
        /// </summary> 
        public string 生成显示信息(qfmain.List分页_._PageInfo_ 页信息)
        {
            return this._forms.生成显示信息(页信息);
        }



    }
}
