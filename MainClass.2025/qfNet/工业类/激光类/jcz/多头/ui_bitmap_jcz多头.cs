using qfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace qfNet
{
    public partial class ui_bitmap_jcz多头 : UserControl
    {
        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        qfNet.MultilineMarkEzd _markEzd;
        string _Files_存放Ezd文件夹 = qfmain.软件类.Files_Cfg.Files_Template;
        int _CardIndex = 0;


        /// <summary>
        /// 日志控件
        /// </summary>
        public qfNet.ui_Log ui_log = new ui_Log()
        {
            _BackColor = Color.White,
            Dock = DockStyle.Fill,
            _ItemHeight = 20,
        };

        public ui_bitmap_jcz多头(qfNet.MultilineMarkEzd markEzd_, int CardIndex_, string files_存放Ezd文件夹)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this._markEzd = markEzd_;
            this._CardIndex = CardIndex_;
            this._Files_存放Ezd文件夹 = files_存放Ezd文件夹;

            this.toolStripSplitButton_Laser.Text = $"Laser {this._CardIndex + 1}";
            this._markEzd.Event_刷新图像 += On_刷新图像;
            this._markEzd.Event_加工状态 += On_加工状态;
            this._markEzd.Event_加载Ezd += On_加载Ezd;
            this.panel1.DoubleClick += (s, e) => On_双击查看图像();


            this.ezCad2exeToolStripMenuItem.Text = this._markEzd._EzCad软件名称;
            this.ezCad2exeToolStripMenuItem.Click += (s, e) => On_打开EzCad软件();
            this.打开ToolStripMenuItem.Click += (s, e) => On_打开();
            this.参数ToolStripMenuItem.Click += (s, e) => On_参数();

            this.调试ToolStripMenuItem.Click += (s, e) => On_调试();
            this.红光ToolStripMenuItem.Click += (s, e) => On_红光();
            this.停止ToolStripMenuItem.Click += (s, e) => On_停止();


            this.toolStripLabel_ezdName.Text = string.Empty;
            this.toolStripLabel_加工状态.Text = string.Empty;

            this.工具栏1.BackColor = Color.WhiteSmoke;

            Log_初始化();
            this.uiPanel_Log.Controls.Clear();
            this.uiPanel_Log.Controls.Add(ui_log);




        }

        public void 设置_日志高度(int Height, int 行高度)
        {
            this.uiPanel_Log.Height = Height;
            this.ui_log._ItemHeight = 行高度;
        }
        public void 设置_日志高度(int Height)
        {
            this.uiPanel_Log.Height = Height;
        }


        #region 事件响应

        //  private readonly object _lock = new object();
        async void On_刷新图像(int cardindex, qfWork._激光_获取图像_ state)
        {
            if (this._CardIndex != cardindex || this._markEzd._初始化状态 != _初始化状态_.已初始化)
            {
                return;
            }

            var rt = await getBitmap(this._CardIndex, state);

        }

        async Task<bool> getBitmap(int cardindex, qfWork._激光_获取图像_ state)
        {
            try
            {

                Task t0 = Task.Run(() =>
                {
                    Bitmap bp = null;
                    if (state == qfWork._激光_获取图像_.获取)
                    {
                        try
                        {
                            bp = this._markEzd.获取图像(this._CardIndex, (int)this.panel1.Width, (int)this.panel1.Height);
                        }
                        catch (Exception ex)
                        {
                            //Add(false, ex.Message);
                        }

                    }

                    this.Invoke((Action)(() =>
                    {
                        if (this.panel1.BackgroundImage != null)
                        {
                            this.panel1.BackgroundImage.Dispose();
                            this.panel1.BackgroundImage = null;
                        }
                        this.panel1.BackgroundImage = bp;

                    }));
                });
                await t0;
            }
            catch (Exception)
            {


            }

            return true;
        }


        void On_双击查看图像()
        {
            if (!this._markEzd._lst_参数[this._CardIndex]._参数.双击查看图像)
            {
                return;
            }

            using (Form_jcz单头_双击查看图像 forms = new Form_jcz单头_双击查看图像(null, this._markEzd, this._CardIndex))
            {
                forms.ShowDialog();
            }

        }

        async void On_打开EzCad软件()
        {
            var t0 = await  this._markEzd.EzCad2软件_打开(this._CardIndex);
        }

        void On_打开()
        {
            DialogResult dlt = this._markEzd.Win_打开(this._CardIndex, this._Files_存放Ezd文件夹, out string msgErr);

        }

        void On_参数()
        {
            this._markEzd.参数SetDevCfg(this._CardIndex);
        }
        void On_加载Ezd(int Cardindex, string ezdPath, qfWork._Err_jczMarkEzd2_ rt)
        {
            if (this._CardIndex != Cardindex)
            {
                return;
            }
            this.Invoke((Action)(() =>
            {
                new qfmain.文件_文件夹().文件_获取文件名_含后缀(this._markEzd._lst_参数[this._CardIndex]._Path_ezd, out string name, out string msgErr);
                this.toolStripLabel_ezdName.Text = name;
            }));

            if (rt == _Err_jczMarkEzd2_.成功)
            {
                this.Add(true, $"{Language_.Get语言("加载激光模板")},{JczLmc_Multiline.ErrMsg(rt)}, {this._markEzd._lst_参数[this._CardIndex]._Path_ezd}");
            }
            else
            {
                this.Add(false, $"{Language_.Get语言("加载激光模板")},{JczLmc_Multiline.ErrMsg(rt)},{this._markEzd._lst_参数[this._CardIndex]._Path_ezd}");
            }


        }

        void On_加工状态(int Cardindex, qfWork._激光加工状态_ state)
        {
            if (this._CardIndex != Cardindex)
            {
                return;
            }
            // Add(false, $"{state}");
            this.Invoke((Action)(() =>
            {
                switch (state)
                {
                    case qfWork._激光加工状态_.加载激光模板中:
                        this.toolStripLabel_加工状态.Text = Language_.Get语言("加载激光模板中");
                        this.工具栏1.BackColor = Color.Yellow;
                        break;
                    case qfWork._激光加工状态_.出激光标刻中:
                        this.toolStripLabel_加工状态.Text = Language_.Get语言("出激光标刻中");
                        this.工具栏1.BackColor = Color.Yellow;
                        break;
                    case qfWork._激光加工状态_.红指示光中:
                        this.toolStripLabel_加工状态.Text = Language_.Get语言("红光指示中");
                        this.工具栏1.BackColor = Color.Yellow;
                        break;
                    case qfWork._激光加工状态_.闲置:
                        this.toolStripLabel_加工状态.Text = "";
                        this.工具栏1.BackColor = Color.WhiteSmoke;
                        break;
                }
            }));
        }


        void On_调试()
        {
            this._markEzd.Win_调试(this._CardIndex);

        }

        void On_红光()
        {
            var t0 = red();
        }
        async Task<bool> red()
        {
            Task t0 = Task.Run(() =>
            {
                bool rt = this._markEzd.连续_红光指示(this._CardIndex, out string msgErr);
                if (!rt)
                {
                    this._markEzd.On_Log_指定卡(this._CardIndex, rt, msgErr);
                }
            });
            await t0;
            return true;
        }

        void On_停止()
        {
            this._markEzd.停止标刻和红光(this._CardIndex);
        }

        #endregion


        #region 日志



        qfmain.log日志 log_sys;
        void Log_初始化()
        {
            string files = qfmain.软件类.Files_Cfg.Files_LogMyApp + $"\\Laser {this._CardIndex + 1}";
            new qfmain.文件_文件夹().文件夹_新建(files, out string msgErr);
            qfmain.log日志._cfg_ cfg = new qfmain.log日志._cfg_();
            cfg.Files_Log = files;
            cfg.保存天数 = 31;
            cfg.使能_清除线程 = true;
            cfg.使能_保存 = true;
            cfg.文件标识 = "";
            log_sys = new qfmain.log日志(cfg);
            log_sys.Event_添加日志 += On_Log_Add;

        }


        public void Add(bool state, string LogValue, bool 显示到日志栏 = true)
        {

            LogValue = 显示到日志栏 ? LogValue : $"{qfmain.log日志._不显示到日志栏}  {LogValue}";
            log_sys.Add(state, LogValue);
        }
        public void Add(string LogValue, bool 显示到日志栏 = true, bool state = true)
        {
            LogValue = 显示到日志栏 ? LogValue : $"{qfmain.log日志._不显示到日志栏}  {LogValue}";
            log_sys.Add(state, LogValue);
        }






        /// <summary>
        /// 清除日志显示框
        /// </summary>
        /// <param name="state"></param>
        /// <param name="LogValue"></param>
        public void Clear(bool state, string LogValue)
        {
            this.ui_log.Clear();
        }

        public void 释放()
        {
            this.uiPanel_Log.Controls.Clear();
            log_sys.释放();
        }


        #region 事件响应

        void On_Log_Add(qfmain.log日志._logValue_ info)
        {
            this.ui_log.Add(info);
        }








        #endregion

        #endregion


    }
}
