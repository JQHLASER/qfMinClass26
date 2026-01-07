using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace qf_Laser
{
    public partial class Form_jcz多头_卡ID设置 : Sunny.UI.UIForm
    {
       MultilineMarkEzd _markEzd;
        public Form_jcz多头_卡ID设置(MultilineMarkEzd markEzd_)
        {
            InitializeComponent();
            this._markEzd = markEzd_;

            #region 初始化



            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.panel6.Visible = false;
            this.panel7.Visible = false;
            this.panel8.Visible = false;


            this.uiComboBox1.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox2.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox3.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox4.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox5.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox6.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox7.DataSource = this._markEzd._CardSN_原始;
            this.uiComboBox8.DataSource = this._markEzd._CardSN_原始;


            #endregion

            #region 数据

            int a = this._markEzd.获取卡数量();
            for (int i = 0; i < a; i++)
            {
                int cardId = this._markEzd._CardID[i];

                try
                {
                    if (i == 0)
                    {
                        处理(true, this.panel1, this.uiComboBox1, cardId);
                    }
                    else if (i == 1)
                    {
                        处理(true, this.panel2, this.uiComboBox2, cardId);
                    }
                    else if (i == 2)
                    {
                        处理(true, this.panel3, this.uiComboBox3, cardId);
                    }
                    else if (i == 3)
                    {
                        处理(true, this.panel4, this.uiComboBox4, cardId);
                    }
                    else if (i == 4)
                    {
                        处理(true, this.panel5, this.uiComboBox5, cardId);
                    }
                    else if (i == 5)
                    {
                        处理(true, this.panel6, this.uiComboBox6, cardId);

                    }
                    else if (i == 6)
                    {
                        处理(true, this.panel7, this.uiComboBox7, cardId);
                    }
                    else if (i == 7)
                    {
                        处理(true, this.panel8, this.uiComboBox8, cardId);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #endregion

            this.uiButton_OK.Click += (s, e) => save();
            this.FormClosing += (s, e) => FormClosing_();

        }

        private void Form_jcz多头_卡ID设置_Load(object sender, EventArgs e)
        {

        }
        private void FormClosing_()
        {
            this._markEzd = null;
        }

        #region 方法

        void 处理(bool visible, Panel pl, Sunny.UI.UIComboBox combobox, int card索引)
        {
            pl.Visible = true;
            combobox.SelectedIndex = combobox.Items.IndexOf(card索引);
        }

        void 处理save(Sunny.UI.UIComboBox combobox, int card索引)
        {
            if (int.TryParse(combobox.SelectedText, out int b))
            {
                this._markEzd._CardID[card索引] = b;
            }
        }


        void save()
        {
            int a = this._markEzd.获取卡数量();

            for (int i = 0; i < a; i++)
            {

                if (i == 0)
                {
                    处理save(this.uiComboBox1, i);
                }
                else if (i == 1)
                {
                    处理save(this.uiComboBox2, i);
                }
                else if (i == 2)
                {
                    处理save(this.uiComboBox3, i);
                }
                else if (i == 3)
                {
                    处理save(this.uiComboBox4, i);
                }
                else if (i == 4)
                {
                    处理save(this.uiComboBox5, i);
                }
                else if (i == 5)
                {
                    处理save(this.uiComboBox6, i);
                }
                else if (i == 6)
                {
                    处理save(this.uiComboBox7, i);
                }
                else if (i == 7)
                {
                    处理save(this.uiComboBox8, i);
                }

            }

            if (this._markEzd.读写参数_CardID索引码(0, out string msgErr))
            {
                MessageBox.Show("Ok");
            }
            else
            {
                MessageBox.Show(msgErr, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion







    }
}
