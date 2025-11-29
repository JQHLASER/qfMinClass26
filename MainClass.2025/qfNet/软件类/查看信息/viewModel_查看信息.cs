using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    internal class viewModel_查看信息 : ViewModelBase
    {

        private string _Title = "";
        /// <summary>
        /// 窗体标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged();
            }

        }


        private string _ShowValue = "";
        /// <summary>
        /// 显示内容
        /// </summary>
        public string ShowValue
        {
            get { return _ShowValue; }
            set
            {
                _ShowValue = value;
                OnPropertyChanged();
            }

        }


        private Color _ForeColor = Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                OnPropertyChanged();
            }

        }



    }
}
