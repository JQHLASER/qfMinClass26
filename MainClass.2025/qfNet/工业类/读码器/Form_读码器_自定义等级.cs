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
            this.uiTextBox_等级.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < codeBeff.Length; i++)
            {
                qfNet.读码器._cfg_等级作假_ info = codeBeff[i];
                if (i == 0)
                {
                    sb.Append($"{info.Name}%{info.Value}");
                }
                else
                {
                    sb.Append($"\r\n");
                    sb.Append($"{info.Name}%{info.Value}");
                }
            }
            this.uiTextBox_等级.Text = sb.ToString();

        }

        void Save()
        {
            string[] beff = this.uiTextBox_等级.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<qfNet.读码器._cfg_等级作假_> lst = new List<qfNet.读码器._cfg_等级作假_>();

            foreach (var item in beff)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                try
                {
                    string[] sBeff = item.Trim().Split(new string[] { "%" }, StringSplitOptions.None);
                    qfNet.读码器._cfg_等级作假_ info = new qfNet.读码器._cfg_等级作假_(sBeff[0], sBeff[1]);
                    lst.Add(info);
                }
                catch (Exception)
                {
                }
            }

            this._readcode._参数.自定义等级 = lst.ToArray();

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
