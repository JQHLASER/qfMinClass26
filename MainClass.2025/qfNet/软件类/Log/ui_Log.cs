using Sunny.UI;
using System;
using System.Collections;
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
    public partial class ui_Log : UserControl
    {
        /// <summary>
        /// 日志状态
        /// </summary>
        public enum _状态log_
        {
            Ok,
            Ng,
            /// <summary>
            /// 警告
            /// </summary>
            Wg,
        }



        public bool _显示时间 = true;
        public bool _显示年月日 = false;
        public bool _显示状态 = false;
        public int _最大显示行数 = 200;

        public Font _font = new Font("新宋体", 11f);
        public string _分割符 = " ";


        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        private List<qfmain.log日志._logValue_> _lstLogInfo = new List<qfmain.log日志._logValue_>();

        readonly viewModel_Log _DataContext = new viewModel_Log();
        private object DataContext { set; get; }

        public ui_Log()
        {
            InitializeComponent();
            this.DataContext = this._DataContext;

            this.listBox1.DataBindings.Clear();

            this.listBox1.DataBindings.Add("SelectedIndex", this._DataContext, nameof(this._DataContext.SelectedIndex), false, DataSourceUpdateMode.OnPropertyChanged);
            this.listBox1.DataBindings.Add("BackColor", this._DataContext, nameof(this._DataContext.BackColor), false);
            this.listBox1.DataBindings.Add("ItemHeight", this._DataContext, nameof(this._DataContext.ItemHeight), false);
            this.listBox1.DataBindings.Add("IntegralHeight", this._DataContext, nameof(this._DataContext.IntegralHeight), false);

            this.Font = this._font;
            this.listBox1.DrawItem += (s, e) => listBox1_DrawItem(s, e);
            this.listBox1.DoubleClick += (s, e) => DoubleClick();


            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.SelectionMode = SelectionMode.One;



            System.Windows.Forms.Timer _timer = new Timer();
            _timer.Interval = this._UI刷新时间;
            _timer.Tick +=(s,e)=> Timer_Tick(s,e);
            _timer.Start();
        }



        /// <summary>
        ///  每 100ms 刷一次 UI,保证流畅
        /// </summary>
        public int _UI刷新时间 = 100;
        private readonly Queue<qfmain.log日志._logValue_> _Queue_buffer = new Queue<qfmain.log日志._logValue_>();
        private readonly object _lock = new object();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        public void Add(qfmain.log日志._logValue_ info)
        {
            lock (_lock)
            {
                this._Queue_buffer.Enqueue(info);
            }
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                this._Queue_buffer.Enqueue(new qfmain.log日志._logValue_(qfmain.log日志.enum状态.Clear, DateTime.Now, ""));
            }
        }

        #region 本地方法

        private void DoubleClick()
        {
            if (this._DataContext.SelectedIndex < 0)
            {
                return;
            }
            qfmain.log日志._logValue_ info = this._lstLogInfo[this._DataContext.SelectedIndex];
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{info.状态}]");
            sb.Append($"[{info.时间.ToString("yyyy/MM/dd HH:mm:ss:fff")}]");
            sb.Append($"\r\n");
            sb.Append($"{info.内容}");

            Color ForeColor = Color.Green;
            switch (info.状态)
            {
                case qfmain.log日志.enum状态.Info:
                    break;
                case qfmain.log日志.enum状态.Error:
                    ForeColor = Color.Red;
                    break;
                case qfmain.log日志.enum状态.Warning:
                    ForeColor = Color.Yellow;
                    break;
            }

            using (Form_查看信息 form = new Form_查看信息("Log", sb.ToString(), ForeColor))
            {
                form.ShowDialog();
            }

        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem_(e);
        }

        void DrawItem_(DrawItemEventArgs e)
        {
            try
            {
                int a = e.Index;
                if (a < 0  ) return;

                Color color_ = Color.Black;
                Brush mybsh = new SolidBrush(Color.Black);
                switch (this._lstLogInfo[a].状态)
                {
                    case qfmain.log日志.enum状态.Info:
                        mybsh = new SolidBrush(Color.Green);
                        color_ = Color.Green;
                        break;
                    case qfmain.log日志.enum状态.Error:
                        mybsh = new SolidBrush(Color.Red);
                        color_ = Color.Red;
                        break;
                    case qfmain.log日志.enum状态.Warning:
                        mybsh = new SolidBrush(Color.Orange);
                        color_ = Color.Orange;
                        break;
                }


                StringBuilder sb = new StringBuilder();
                if (this._显示时间 && this._显示年月日)
                {
                    sb.Append($"[{this._lstLogInfo[a].时间.ToString("yyyy/MM/dd HH:mm:ss")}]{this._分割符}");
                }
                else if (this._显示时间 && !this._显示年月日)
                {
                    sb.Append($"[{this._lstLogInfo[a].时间.ToString("HH:mm:ss:fff")}]{this._分割符}");
                }

                if (this._显示状态)
                {
                    sb.Append($"{this._lstLogInfo[a].状态.ToString().PadLeft(8, ' ')}{this._分割符}");
                }

                sb.Append($"{this._lstLogInfo[a].内容}");







                // 焦点框
                //   e.DrawFocusRectangle();//启用后,单击时会留下残影
                //文本 
                //  e.Graphics.DrawString(this.listBox1.Items[a].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);

                // e.DrawBackground();  //绘制背景色,选中时会带颜色
                // 使用 TextRenderer 绘制不换行文本 
                // e.Graphics.Save();
                TextRenderer.DrawText(
                    e.Graphics,
                    sb.ToString(),
                     e.Font,
                    e.Bounds,
                    color_,
                    TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter
                );

                //  e.DrawFocusRectangle();//启用后,单击时会留下残影


            }
            catch (Exception)
            {


            }
        }



        #endregion


        bool _IsDraw = false;

        /// <summary>
        /// UI刷新线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            List<qfmain.log日志._logValue_> tmp;
            lock (_lock)
            {
                if (this._Queue_buffer.Count == 0)
                {
                    return;
                }
                tmp = new List<qfmain.log日志._logValue_>(this._Queue_buffer);
                this._Queue_buffer.Clear();
            }
             
            _IsDraw = tmp.Count > 0 ? true : false;
            listBox1.BeginUpdate();
            foreach (var item in tmp)
            {
                if (item.状态 == qfmain.log日志.enum状态.Clear)
                {
                    this._lstLogInfo.Clear();
                    this.listBox1.Items.Clear();
                    this.listBox1.TopIndex = -1;
                    continue;
                }
                else if (this._lstLogInfo.Count >= this._最大显示行数)
                {
                    _lstLogInfo.RemoveAt(0);
                    this.listBox1.Items.RemoveAt(0);
                }

                this._lstLogInfo.Add(item);
                listBox1.Items.Add(""); // 自绘            
            }

            if (!_IsDraw)
            {
                return;
            }

            listBox1.EndUpdate();

            if (_IsDraw)
            {
                this.listBox1.BeginInvoke((Action)(() =>
                {
                    this.listBox1.TopIndex = this._lstLogInfo.Count <= 0 ? -1 : this._lstLogInfo.Count - 1;
                    this.listBox1.SelectedIndex = this._lstLogInfo.Count <= 0 ? -1 : this._lstLogInfo.Count - 1;
                }));
                //this.listBox1.Refresh();  // 强制让 WinForms 消息立即处理
            }

            _IsDraw = false;
        }


        private Color _backColor = Color.Black;
        [Description(""), Category("")]
        public new Color _BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                this._DataContext.BackColor = _backColor;
                Invalidate();
            }
        }



        private int _itemHeight = 27;
        [Description(""), Category("")]
        public new int _ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                this._DataContext.ItemHeight = _itemHeight;
                Invalidate();
            }
        }






    }
}
