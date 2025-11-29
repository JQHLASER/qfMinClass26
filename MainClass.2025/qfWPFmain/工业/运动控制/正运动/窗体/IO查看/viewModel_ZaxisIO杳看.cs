using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal partial class viewModel_ZaxisIO杳看 : ObservableObject
    {


        public class _IO_port_
        {
            public bool _0 { set; get; } = false;
            public bool _1 { set; get; } = false;
            public bool _2 { set; get; } = false;
            public bool _3 { set; get; } = false;
            public bool _4 { set; get; } = false;
            public bool _5 { set; get; } = false;
            public bool _6 { set; get; } = false;
            public bool _7 { set; get; } = false;
            public bool _8 { set; get; } = false;
            public bool _9 { set; get; } = false;
            public bool _10 { set; get; } = false;
            public bool _11 { set; get; } = false;
            public bool _12 { set; get; } = false;
            public bool _13 { set; get; } = false;
            public bool _14 { set; get; } = false;
            public bool _15 { set; get; } = false;
        }

        [ObservableProperty]
        private _IO_port_ state_IN = new _IO_port_();

        [ObservableProperty]
        private _IO_port_ state_OUT = new _IO_port_();


        internal void On_输入(bool[] state)
        {
            _IO_port_ ioIn = new _IO_port_();
            ioIn._0 = state[0];
            ioIn._1 = state[1];
            ioIn._2 = state[2];
            ioIn._3 = state[3];
            ioIn._4 = state[4];
            ioIn._5 = state[5];
            ioIn._6 = state[6];
            ioIn._7 = state[7];

            ioIn._8 = state[8];
            ioIn._9 = state[9];
            ioIn._10 = state[10];
            ioIn._11 = state[11];
            ioIn._12 = state[12];
            ioIn._13 = state[13];
            ioIn._14 = state[14];
            ioIn._15 = state[15];
            this.State_IN = ioIn;
        }


        internal void On_输出(bool[] state)
        {
            _IO_port_ ioOut = new _IO_port_();
            ioOut._0 = state[0];
            ioOut._1 = state[1];
            ioOut._2 = state[2];
            ioOut._3 = state[3];
            ioOut._4 = state[4];
            ioOut._5 = state[5];
            ioOut._6 = state[6];
            ioOut._7 = state[7];

            ioOut._8 = state[8];
            ioOut._9 = state[9];
            ioOut._10 = state[10];
            ioOut._11 = state[11];
            ioOut._12 = state[12];
            ioOut._13 = state[13];
            ioOut._14 = state[14];
            ioOut._15 = state[15];
            this.State_OUT = ioOut;
        }



        internal void 操作(int port, Zaxis_ zaxis)
        {
            uint a = 0;
            switch (port)
            {
                case 0:
                    a = this.State_OUT._0 ? (uint)0 : (uint)1;
                    break;
                case 1:
                    a = this.State_OUT._1 ? (uint)0 : (uint)1;
                    break;
                case 2:
                    a = this.State_OUT._2 ? (uint)0 : (uint)1;
                    break;
                case 3:
                    a = this.State_OUT._3 ? (uint)0 : (uint)1;
                    break;
                case 4:
                    a = this.State_OUT._4 ? (uint)0 : (uint)1;
                    break;
                case 5:
                    a = this.State_OUT._5 ? (uint)0 : (uint)1;
                    break;
                case 6:
                    a = this.State_OUT._6 ? (uint)0 : (uint)1;
                    break;
                case 7:
                    a = this.State_OUT._7 ? (uint)0 : (uint)1;
                    break;

                case 8:
                    a = this.State_OUT._8 ? (uint)0 : (uint)1;
                    break;
                case 9:
                    a = this.State_OUT._9 ? (uint)0 : (uint)1;
                    break;
                case 10:
                    a = this.State_OUT._10 ? (uint)0 : (uint)1;
                    break;
                case 11:
                    a = this.State_OUT._11 ? (uint)0 : (uint)1;
                    break;
                case 12:
                    a = this.State_OUT._12 ? (uint)0 : (uint)1;
                    break;
                case 13:
                    a = this.State_OUT._13 ? (uint)0 : (uint)1;
                    break;
                case 14:
                    a = this.State_OUT._14 ? (uint)0 : (uint)1;
                    break;
                case 15:
                    a = this.State_OUT._15 ? (uint)0 : (uint)1;
                    break;
            }



            zaxis.IO_设置输出口状态(port, a);
        }





    }
}
