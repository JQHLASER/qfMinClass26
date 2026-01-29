using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    internal    class viewModel_Log : qfNet.ViewModelBase
    {
        private string[] _ItemsLog = new string[0];
        public string[] ItemsLog
        {
            get => _ItemsLog;
            set
            {
                _ItemsLog = value;
                OnPropertyChanged();
            }
        }

   
        private int _SelectedIndex = -1;
        public int SelectedIndex
        {
            get => _SelectedIndex;
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged();
            }
        }


        private Color _BackColor = Color.Black;
        public Color BackColor
        {
            get => _BackColor;
            set
            {
                _BackColor = value;
                OnPropertyChanged();
            }
        }

        private int _ItemHeight = 27;
        public int ItemHeight
        {
            get => _ItemHeight;
            set
            {
                _ItemHeight = value;
                OnPropertyChanged();
            }
        }

        private bool _IntegralHeight = false;
        /// <summary>
        /// =true时,fill时,最下面会有一部分空白
        /// </summary>
        public bool IntegralHeight
        {
            get => _IntegralHeight;
            set
            {
                _IntegralHeight = value;
                OnPropertyChanged();
            }
        }



    }
}
