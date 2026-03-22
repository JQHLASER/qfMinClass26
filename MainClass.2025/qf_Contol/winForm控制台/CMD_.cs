using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Contol
{
    public class CMD_
    {
        public Form_Control forms;
        Timer _timer = new Timer
        {
            Interval = 100,
        };
        public int _最大显示行 = 100;

        string _控制符 = ">";
        public CMD_(string 控制符 = ">", int 最大显示行 = 100)
        {
            this._控制符 = 控制符;
            this._最大显示行 = 最大显示行;
        }
        public void 主窗体(string Title,bool Is显示窗体=false )
        {
            if (forms != null)
            {
                return;
            }
            _timer.Tick += Timer_Tick;
            _timer.Start();
            using (forms = new Form_Control(this, Title, Is显示窗体))
            {
                 
                forms.ShowDialog();
            }
            _timer.Stop();
            _timer = null;
        }

        public void 关闭窗体()
        {
            if (forms is null)
            {
                return;
            }
            forms.Close();
            forms.Dispose();
        }


        public void WriteLine(string v, bool Is控制符 = true)
        {
            if (Is控制符)
            {
                _queue.Enqueue($"{this._控制符} {v}");
            }
            else
            {
                _queue.Enqueue($"{v}");
            }
        }

        public void 窗体显示(bool Is显示)
        {
            int opacity = 100;
            bool ShowInTaskbar = true;
            if (!Is显示)
            {
                opacity = 0;
                ShowInTaskbar = false;
            }
            forms.Invoke((Action)(() =>
            {
                forms.Opacity = opacity;
                forms.ShowInTaskbar = ShowInTaskbar;
            }));


        }

        #region 事件

        public event Action<string> Event_WriteLine;
        internal void On_WriteLine(string v)
        {
            WriteLine(v, true);
            Event_WriteLine?.Invoke(v);
        }

        public event Action Event_Load;
        internal void On_Load()
        {
            Event_Load?.Invoke();
        }
        public event Action Event_FormClosing;
        internal void On_FormClosing()
        {
            Event_FormClosing?.Invoke();
        }



        #endregion


        Queue<string> _queue = new Queue<string>();
        readonly object _lock = new object();
        void Timer_Tick(object sender, EventArgs e)
        {

            List<string> tmp;
            lock (_lock)
            {
                if (this._queue.Count == 0)
                    return;

                tmp = new List<string>(this._queue);
                this._queue.Clear();
            }
            if (tmp.Count == 0)
            {
                return;
            }

            forms.listBox1.BeginUpdate();
            foreach (var item in tmp)
            {
                if (forms is null)
                {
                    continue;
                }
                forms.listBox1.Items.Add($"{item}");
                if (forms.listBox1.Items.Count > this._最大显示行)
                {
                    forms.listBox1.Items.RemoveAt(0);
                }
            }

            forms.listBox1.TopIndex = forms.listBox1.Items.Count <= 0 ? -1 : forms.listBox1.Items.Count - 1;
            forms.listBox1.EndUpdate();
        }







    }
}
