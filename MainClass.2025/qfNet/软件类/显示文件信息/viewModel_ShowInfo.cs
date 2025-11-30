using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    internal    class viewModel_ShowInfo : qfNet.ViewModelBase
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
         

        private Color _BackColor = Color.White ;
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

      
        private bool  _IntegralHeight = false  ;
        /// <summary>
        /// =true时,fill时,最下面会有一部分空白
        /// </summary>
        public bool  IntegralHeight
        {
            get => _IntegralHeight;
            set
            {
                _IntegralHeight = value;
                OnPropertyChanged();
            }
        }


        public Font _font = new Font("新宋体", 12f, FontStyle.Regular);
        public Font  Font
        {
            get => _font;
            set
            {
                _font = value;
                OnPropertyChanged();
            }
        }





    }
}
