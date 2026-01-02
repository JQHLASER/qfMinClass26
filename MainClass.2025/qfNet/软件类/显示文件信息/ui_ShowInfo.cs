
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
    public partial class ui_ShowInfo : UserControl
    {








        //双缓冲显示窗体所有子控件
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }
        private List<_ShowInfo_> _lstShowInfo = new List<_ShowInfo_>();

        readonly viewModel_ShowInfo _DataContext = new viewModel_ShowInfo();
        private object DataContext { set; get; }

        public ui_ShowInfo()
        {
            InitializeComponent();
            this.DataContext = this._DataContext;

            this.listBox1.DataBindings.Clear();
            this.listBox1.DataBindings.Add("BackColor", this._DataContext, nameof(this._DataContext.BackColor), false);
            this.listBox1.DataBindings.Add("ItemHeight", this._DataContext, nameof(this._DataContext.ItemHeight), false);
            this.listBox1.DataBindings.Add("IntegralHeight", this._DataContext, nameof(this._DataContext.IntegralHeight), false);

            this.listBox1.DataBindings.Add("Font", this._DataContext, nameof(this._DataContext.Font), false);

            this.listBox1.DrawItem += (s, e) => listBox1_DrawItem(s, e);

            System.Windows.Forms.Timer _timer = new Timer();
            _timer.Interval = this._UI刷新时间;
            _timer.Tick += (s, e) => Timer_Tick(s, e);
            _timer.Start();
        }



        /// <summary>
        ///  每 100ms 刷一次 UI,保证流畅
        /// </summary>
        public int _UI刷新时间 = 100;
        private readonly Queue<_ShowInfo_> _Queue_buffer = new Queue<_ShowInfo_>();
        private readonly object _lock = new object();


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        public void Add(_ShowInfo_ info)
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
                this._Queue_buffer.Enqueue(new _ShowInfo_(-1));
            }
        }

        #region 本地方法


        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem_(e);
        }

        void DrawItem_(DrawItemEventArgs e)
        {
            try
            {
                int a = e.Index;
                if (a < 0) return;

                Font fonts = this._lstShowInfo[a] is null ? this.Font : this._lstShowInfo[a].fonts;
                Color color_ = this._lstShowInfo[a].颜色;
                StringBuilder sb = new StringBuilder();
                sb.Append($"{this._lstShowInfo[a].内容}");


                // 焦点框
                //   e.DrawFocusRectangle();//启用后,单击时会留下残影
                //文本 
                //  e.Graphics.DrawString(this.listBox1.Items[a].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);

                // e.DrawBackground();  //绘制背景色,选中时会带颜色
                // 使用 TextRenderer 绘制不换行文本


                e.Graphics.Save();
                TextRenderer.DrawText(
                    e.Graphics,
                    sb.ToString(),
                    fonts,
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
            List<_ShowInfo_> tmp;
            lock (_lock)
            {
                if (this._Queue_buffer.Count == 0 || _IsDraw)
                    return;

                tmp = new List<_ShowInfo_>(this._Queue_buffer);
                this._Queue_buffer.Clear();
            }

            _IsDraw = tmp.Count > 0 ? true : false;
            if (!_IsDraw)
            {
                return;
            }
            listBox1.BeginUpdate();
            foreach (var item in tmp)
            {
                if (item.状态 == -1)
                {
                    this.listBox1.Items.Clear();
                    this._lstShowInfo.Clear();
                    continue;
                }
                else
                {
                    this._lstShowInfo.Add(item);
                    this.listBox1.Items.Add(""); // 自绘
                }
            }
            listBox1.EndUpdate();
            _IsDraw = false;
        }




        private Color _backColor = Color.White;
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

        private Font _font = new Font("新宋体", 12f, FontStyle.Regular);
        [Description(""), Category("")]
        public new Font Font
        {
            get => _font;
            set
            {
                _font = value;
                this._DataContext.Font = _font;
                Invalidate();
            }
        }

    }
}
