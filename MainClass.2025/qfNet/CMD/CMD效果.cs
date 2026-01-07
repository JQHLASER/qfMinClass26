using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfNet 
{
    public class CMD效果
    {
        Sunny.UI.UIRichTextBox _richTextBox;

        public CMD效果(Sunny.UI.UIRichTextBox richTextBox)
        {
            this._richTextBox = richTextBox;
            this._richTextBox.KeyDown += (s, e) => On_KeyDown(s, e);
            this._richTextBox.Text = _提示符;
            this._richTextBox.SelectionStart = this._richTextBox.Text.Length;
            this._richTextBox.KeyPress += (s, e) => On_KeyPress();

            WriteLine("***** QiFengContol *****");
            Event_Load?.Invoke();

        }

        #region 本地方法


        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            int caret = this._richTextBox.SelectionStart;
            int _提示符Pos = this._richTextBox.Text.LastIndexOf(_提示符);

            // --- 防止删除提示符 ---
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Left || e.KeyCode == Keys.Delete)
            {
                if (caret <= _提示符Pos + _提示符.Length)
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // --- 回车执行命令 ---
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                var lastLine = this._richTextBox.Lines.Last();
                string command = lastLine.Substring(_提示符.Length);

                if (!string.IsNullOrWhiteSpace(command))
                {
                    _history.Add(command);
                    _historyIndex = _history.Count;
                }

                // 触发外部命令事件
                Event_CommandEntered?.Invoke(command);

                this._richTextBox.AppendText("\n" + _提示符);
                this._richTextBox.SelectionStart = this._richTextBox.Text.Length;
                return;
            }

            // --- ↑ 历史上一条 ---
            if (e.KeyCode == Keys.Up)
            {
                if (_history.Count > 0 && _historyIndex > 0)
                {
                    _historyIndex--;
                    ReplaceCurrentLine(_history[_historyIndex]);
                }
                e.SuppressKeyPress = true;
                return;
            }

            // --- ↓ 历史下一条 ---
            if (e.KeyCode == Keys.Down)
            {
                if (_historyIndex < _history.Count - 1)
                {
                    _historyIndex++;
                    ReplaceCurrentLine(_history[_historyIndex]);
                }
                else
                {
                    _historyIndex = _history.Count;
                    ReplaceCurrentLine("");
                }

                e.SuppressKeyPress = true;
                return;
            }
        }

        private void ReplaceCurrentLine(string text)
        {
            int _提示符Pos = this._richTextBox.Text.LastIndexOf(_提示符) + _提示符.Length;
            this._richTextBox.SelectionStart = _提示符Pos;
            this._richTextBox.SelectionLength = this._richTextBox.Text.Length - _提示符Pos;

            this._richTextBox.SelectedText = text;
        }

        private void On_KeyPress()
        {
            // 光标行号
            int line = this._richTextBox.GetLineFromCharIndex(this._richTextBox.SelectionStart);

            // 如果不是最后一行，则强制移动到最后
            if (line != this._richTextBox.Lines.Length - 1)
            {
                this._richTextBox.SelectionStart = this._richTextBox.Text.Length;
            }
        }


        #endregion

        #region 变量

        private string _提示符 = ">   ";
        private List<string> _history = new List<string>();
        private int _historyIndex = 0;

        #endregion

        #region 事件

        /// <summary>
        /// 输入指令时
        /// </summary>
        public event Action<string> Event_CommandEntered;

        /// <summary>
        /// 进入时
        /// </summary>
        public event Action Event_Load;

        #endregion




        #region 对外方法


        /// <summary>
        /// 输出文本（带换行）
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            this._richTextBox.AppendText(text);
            this._richTextBox.AppendText("\n");
            this._richTextBox.AppendText(_提示符);
            this._richTextBox.SelectionStart = this._richTextBox.Text.Length;
            // 自动滚动到底部
            this._richTextBox.ScrollToCaret();
            this._richTextBox.Focus();
        }

        /// <summary>
        /// 输出文本并设置颜色,(带换行)
        /// </summary> 
        public void WriteLine(string text, Color? color)
        { 

            this._richTextBox.SelectionColor = color ?? Color.White;
            this._richTextBox.AppendText(text);
            this._richTextBox.AppendText("\n");
            this._richTextBox.AppendText(_提示符);
            this._richTextBox.SelectionStart = this._richTextBox.Text.Length;

            // 自动滚动到底部
            this._richTextBox.ScrollToCaret();
            this._richTextBox.Focus();
        }


        /// <summary>
        /// 输出文本（不换行）
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            this._richTextBox.AppendText(text);
            this._richTextBox.SelectionStart = this._richTextBox.Text.Length;
            // 自动滚动到底部
            this._richTextBox.ScrollToCaret();
            this._richTextBox.Focus();
        }


        /// <summary>
        /// 输出文本并设置颜色,(不带换行)
        /// </summary> 
        public void Write(string text, Color? color = null)
        {
            this._richTextBox.SelectionColor = color ?? Color.White;
            this._richTextBox.AppendText(text);
            this._richTextBox.SelectionStart = this._richTextBox.Text.Length;

            // 自动滚动到底部
            this._richTextBox.ScrollToCaret();
            this._richTextBox.Focus();
        }

        #endregion

    }
}
