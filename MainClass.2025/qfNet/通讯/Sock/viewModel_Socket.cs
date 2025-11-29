using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
   

    internal class viewModel_Socket : ViewModelBase
    {


        private bool _isClient = true;
        /// <summary>
        /// 是否为客户端
        /// </summary>
        public bool IsClient
        {
            get { return _isClient; }
            set
            {
                _isClient = value;
                OnPropertyChanged();
            }
        }


        private string _Title = "TCP/IP";
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

        private string _IP = "127.0.0.1";
        /// <summary>
        /// 是否为客户端
        /// </summary>
        public string  IP
        {
            get { return _IP; }
            set
            {
                _IP = value;
                OnPropertyChanged();
            }
        }

        private int  _Port = 0;
        /// <summary>
        /// 是否为客户端
        /// </summary>
        public int  Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                OnPropertyChanged();
            }
        }



    }
}
