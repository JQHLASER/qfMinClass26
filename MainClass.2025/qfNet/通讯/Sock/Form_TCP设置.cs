using Sunny.UI.Win32;
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
    public partial class Form_TCP设置 : Sunny.UI.UIForm
    {
        private readonly viewModel_Socket _DataContext = new viewModel_Socket();
        qfmain.Socket_Client Client;
        qfmain.Socket_Server Server;

        /// <summary>
        /// 返回 = DialogResult.No / = DialogResult.Yes
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Client_"></param>
        /// <param name="Server_"></param>
        public Form_TCP设置(string Title = "TCP/IP", qfmain.Socket_Client Client_ = null, qfmain.Socket_Server Server_ = null)
        {
            InitializeComponent();
            this.DataContext = this._DataContext;

            this.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.Title), false);
            this.uiTextBox_IP.DataBindings.Add("Text", this._DataContext, nameof(this._DataContext.IP), false, DataSourceUpdateMode.OnPropertyChanged);
            this.uiTextBox_Port.DataBindings.Add("IntValue", this._DataContext, nameof(this._DataContext.Port), false, DataSourceUpdateMode.OnPropertyChanged);

            this.uiLabel_IP.DataBindings.Add("Visible", this._DataContext, nameof(this._DataContext.IsClient), false);
            this.uiTextBox_IP.DataBindings.Add("Visible", this._DataContext, nameof(this._DataContext.IsClient), false);


            this.uiButton_No.Click += (s, e) => No();
            this.uiButton_Yes.Click += (s, e) => Yes();
            this.FormClosing += (s, e) => FormClosing_();

            this._DataContext.Title = Title;
            this.Client = Client_;
            this.Server = Server_;
            if (this.Client != null)
            {
                this._DataContext.IP = this.Client._参数.IP;
                this._DataContext.Port = this.Client._参数.Port;
                this._DataContext.IsClient = true;
            }
            else if (this.Server != null)
            {
                this._DataContext.IP = this.Server._参数.IP;
                this._DataContext.Port = this.Server._参数.Port;
                this._DataContext.IsClient = false;


            }


        }

        #region 方法

        void No()
        {
            this.DialogResult = DialogResult.No;
        }

        void Yes()
        {
            if (this.Client != null)
            {
                this.Client._参数.Port = this.uiTextBox_Port.IntValue;
                this.Client._参数.IP = this.uiTextBox_IP.Text.Trim();
                this.Client.参数读写(0);
            }
            else if (this.Server != null)
            {
                this.Server._参数.Port = this.uiTextBox_Port.IntValue;
                this.Server._参数.IP = string.Empty;
                this.Server.参数读写(0);
            }
            MessageBox.Show("OK");
            this.DialogResult = DialogResult.Yes;
        }

        #endregion


        private void Form_TCP设置_Load(object sender, EventArgs e)
        {

        }

        private void FormClosing_()
        {
            this.Client = null;
            this.Server = null;
        }
    }
}
