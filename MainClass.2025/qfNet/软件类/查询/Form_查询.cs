using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace qfNet
{
    public partial class Form_查询 : Sunny.UI.UIForm
    { 
        

        public Form_查询()
        {
            InitializeComponent();
            this.Padding = new Padding(5, 40, 5, 5);

            设置显示信息("" );

            this.toolStripButton_查询.Click += (s, e) =>
            {
                Event_查询?.Invoke(this);
            };

            this.toolStripButton_导出.Click += (s, e) =>
            {
                Event_导出?.Invoke(this);
            };

            this.toolStripButton_关闭.Click += (s, e) =>
            {
                if (MessageBox.Show(Language_.Get语言("是否关闭?"), "", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Event_FormClosing_关闭时?.Invoke(this);
                this.Close();
            };

            this.ui_分页1.Event_上一页 += () =>
            {
                Event_上一页?.Invoke(this);
            };
            this.ui_分页1.Event_下一页 += () =>
            {
                Event_下一页?.Invoke(this);
            };
            this.ui_分页1.Event_到指定页 += (s) =>
            {
                Event_到指定页?.Invoke(this, s);
            };

            this.Load += (s, e) =>
            {
                this.Event_Load_进入时?.Invoke(this);
            };

            this.FormClosing += (s, e) =>
            {
                this.Event_FormClosing_关闭时?.Invoke(this);
            };
            
 
        }


        public event Action<Form_查询> Event_FormClosing_关闭时;
        public event Action<Form_查询> Event_Load_进入时;


        public event Action<Form_查询> Event_查询;
        public event Action<Form_查询> Event_导出;

        public event Action<Form_查询> Event_上一页;
        public event Action<Form_查询> Event_下一页;
        /// <summary>
        /// (int 页索引,DataGridView )
        /// </summary>
        public event Action<Form_查询, uint> Event_到指定页;

       
        public void 设置显示信息(string value)
        { 
            this.ui_分页1.设置显示信息(value);
        }
         

        public string 生成显示信息(qfmain.List分页_._PageInfo_ 页信息)
        {
            List<uint> lst = new List<uint>();
            for (uint i = 0; i < 页信息.总页数; i++)
            {
                lst.Add(i + 1);
            }
            this.ui_分页1.uiComboBox_所有页码.DataSource = lst;
            this.ui_分页1.uiComboBox_所有页码.SelectedIndex =(int) 页信息.当前页;

            StringBuilder sb = new StringBuilder();
            sb.Append($"【{Language_.Get语言("共")}{页信息.总行数}{Language_.Get语言("行")}】");
            sb.Append($"【{Language_.Get语言("共")}{页信息.总页数}{Language_.Get语言("页")}】");
            sb.Append($"【{页信息.每页行数}{Language_.Get语言("行")}/{Language_.Get语言("页")}】");
            uint a = 页信息.当前页 == 页信息.总页数 - 1 ? 页信息.最后一页行数 : 页信息.每页行数;
            sb.Append($"【{a}{Language_.Get语言("行")}/{Language_.Get语言("当前页")}】");
            return sb.ToString();
        }

       
    }
}
