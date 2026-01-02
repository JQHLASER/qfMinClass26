using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet
{
    public partial class Form_查询 : Sunny.UI.UIForm
    {
        public Form_查询()
        {
            InitializeComponent();
            this.Padding = new Padding(5, 40, 5, 5);

            设置信息("");

            this.toolStripButton_查询.Click += (s, e) =>
            {
                Event_查询?.Invoke();
            };

            this.toolStripButton_导出.Click += (s, e) =>
            {
                Event_导出?.Invoke();
            };

            this.toolStripButton_关闭.Click += (s, e) =>
            {
                Event_关闭?.Invoke(this);
            };

            this.ui_分页1.Event_上一页 += () =>
            {
                Event_上一页?.Invoke();
            };
            this.ui_分页1.Event_下一页 += () =>
            {
                Event_下一页?.Invoke();
            };
            this.ui_分页1.Event_到指定页 += (s) =>
            {
                Event_到指定页?.Invoke(s);
            };
        }

        private void Form_查询_Load(object sender, EventArgs e)
        {

        }

        public event Action<Form> Event_关闭;
        public event Action Event_查询;
        public event Action Event_导出;

        public event Action Event_上一页;
        public event Action Event_下一页;
        /// <summary>
        /// (int 页索引)
        /// </summary>
        public event Action<int> Event_到指定页;



        public void 设置信息(string value)
        {
            this.ui_分页1.设置显示信息(value );
        }

      

    }
}
