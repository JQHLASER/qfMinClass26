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
    internal partial class Form_读码器_自定义等级 : Sunny.UI.UIForm
    {
        qfNet.读码器 _readcode;

        internal Form_读码器_自定义等级(qfNet.读码器 readcode_)
        {
            InitializeComponent();
            this._readcode = readcode_;
            Show();
            this.uiButton_No.Click += (s, e) => No();
            this.uiButton_Yes.Click += (s, e) => Save();
            this.FormClosing += (s, e) => FormClosing_();
        }

        private void Form_读码器_自定义等级_Load(object sender, EventArgs e)
        {

        }
        private void FormClosing_()
        {
            this._readcode = null;
        }

        #region 方法

        void Show()
        {
            qfNet.读码器._cfg_等级作假_[] codeBeff = this._readcode._参数.自定义等级;

            foreach (var S in codeBeff)
            {
                switch (S.Name)
                {
                    case "A":
                        this.uiTextBox_A.Text = S.Value;
                        break;
                    case "B":
                        this.uiTextBox_B.Text = S.Value;
                        break;
                    case "C":
                        this.uiTextBox_C.Text = S.Value;
                        break;
                    case "D":
                        this.uiTextBox_D.Text = S.Value;
                        break;
                    case "E":
                        this.uiTextBox_E.Text = S.Value;
                        break;
                    case "F":
                        this.uiTextBox_F.Text = S.Value;
                        break;
                }

            }

        }

        void Save()
        {
            List<qfNet.读码器._cfg_等级作假_> lstCstr = new List<qfNet.读码器._cfg_等级作假_>();
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_A.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "A",
                    Value = this.uiTextBox_A.Text.Trim(),
                });
            }
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_B.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "B",
                    Value = this.uiTextBox_B.Text.Trim(),
                });
            }
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_C.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "C",
                    Value = this.uiTextBox_C.Text.Trim(),
                });
            }
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_D.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "D",
                    Value = this.uiTextBox_D.Text.Trim(),
                });
            }
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_E.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "E",
                    Value = this.uiTextBox_E.Text.Trim(),
                });
            }
            if (!string.IsNullOrWhiteSpace(this.uiTextBox_F.Text.Trim()))
            {
                lstCstr.Add(new qfWork.读码器._cfg_等级作假_
                {
                    Name = "F",
                    Value = this.uiTextBox_F.Text.Trim(),
                });
            }

            this._readcode._参数.自定义等级 = lstCstr.ToArray();

            MessageBox.Show("OK");
            Show();
            this.DialogResult = DialogResult.OK;
        }

        void No()
        {
            this.Close();
        }

        #endregion



    }
}
