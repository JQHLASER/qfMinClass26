using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal partial class viewMoel_jcz2_调试 : ObservableObject
    {
        MarkEzd markezd;

        public class _语言_
        {
            public string 标刻 { set; get; } = Language_.Get语言("标刻");
            public string 红光 { set; get; } = Language_.Get语言("红光");
            public string 设置 { set; get; } = Language_.Get语言("设置");
            public string 标题 { set; get; } = Language_.Get语言("调试");
        }
        

        public viewMoel_jcz2_调试(MarkEzd markezd_)
        {
            this.markezd = markezd_;
        }

        [ObservableProperty]
        private _语言_ language_语言 = new _语言_();

        [ObservableProperty]
        private bool io_IN_0 = false;
        [ObservableProperty]
        private bool io_IN_1 = false;
        [ObservableProperty]
        private bool io_IN_2 = false;
        [ObservableProperty]
        private bool io_IN_3 = false;
        [ObservableProperty]
        private bool io_IN_4 = false;
        [ObservableProperty]
        private bool io_IN_5 = false;
        [ObservableProperty]
        private bool io_IN_6 = false;
        [ObservableProperty]
        private bool io_IN_7 = false;
        [ObservableProperty]
        private bool io_IN_8 = false;
        [ObservableProperty]
        private bool io_IN_9 = false;
        [ObservableProperty]
        private bool io_IN_10 = false;
        [ObservableProperty]
        private bool io_IN_11 = false;
        [ObservableProperty]
        private bool io_IN_12 = false;
        [ObservableProperty]
        private bool io_IN_13 = false;
        [ObservableProperty]
        private bool io_IN_14= false;
        [ObservableProperty]
        private bool io_IN_15 = false;


        [ObservableProperty]
        private bool io_OUT_0 = false;
        [ObservableProperty]
        private bool io_OUT_1 = false;
        [ObservableProperty]
        private bool io_OUT_2 = false;
        [ObservableProperty]
        private bool io_OUT_3 = false;         
        [ObservableProperty]
        private bool io_OUT_4 = false;
        [ObservableProperty]
        private bool io_OUT_5 = false;
        [ObservableProperty]
        private bool io_OUT_6 = false;
        [ObservableProperty]
        private bool io_OUT_7 = false;
        [ObservableProperty]
        private bool io_OUT_8 = false;
        [ObservableProperty]
        private bool io_OUT_9 = false;
        [ObservableProperty]
        private bool io_OUT_10 = false;
        [ObservableProperty]
        private bool io_OUT_11 = false;
        [ObservableProperty]
        private bool io_OUT_12 = false;
        [ObservableProperty]
        private bool io_OUT_13 = false;
        [ObservableProperty]
        private bool io_OUT_14 = false;
        [ObservableProperty]
        private bool io_OUT_15 = false;





        internal void On_In(bool[] value)
        {
            this.Io_IN_0 = value[0];
            this.Io_IN_1 = value[1];
            this.Io_IN_2 = value[2];
            this.Io_IN_3 = value[3];
            this.Io_IN_4 = value[4];
            this.Io_IN_5 = value[5];
            this.Io_IN_6 = value[6];
            this.Io_IN_7= value[7];
            this.Io_IN_8 = value[8];
            this.Io_IN_9 = value[9];
            this.Io_IN_10 = value[10];
            this.Io_IN_11 = value[11];
            this.Io_IN_12 = value[12];
            this.Io_IN_13 = value[13];
            this.Io_IN_14 = value[14];
            this.Io_IN_15 = value[15];
        }

        internal void On_Out(bool[] value)
        {
            this.Io_OUT_0 = value[0];
            this.Io_OUT_1 = value[1];
            this.Io_OUT_2 = value[2];
            this.Io_OUT_3 = value[3];
            this.Io_OUT_4 = value[4];
            this.Io_OUT_5 = value[5];
            this.Io_OUT_6 = value[6];
            this.Io_OUT_7 = value[7];
            this.Io_OUT_8 = value[8];
            this.Io_OUT_9 = value[9];
            this.Io_OUT_10 = value[10];
            this.Io_OUT_11 = value[11];
            this.Io_OUT_12 = value[12];
            this.Io_OUT_13 = value[13];
            this.Io_OUT_14 = value[14];
            this.Io_OUT_15 = value[15];
        }

        internal void On_SetOut_0(ushort port )
        {
            bool value1 = false ;
            switch (port)
            {
                case 0:
                    value1 = this.Io_OUT_0;
                    break;
                case 1:
                    value1 = this.Io_OUT_1;
                    break;
                case 2:
                    value1 = this.Io_OUT_2;
                    break;
                case 3:
                    value1 = this.Io_OUT_3;
                    break;
                case 4:
                    value1 = this.Io_OUT_4;
                    break;
                case 5:
                    value1 = this.Io_OUT_5;
                    break;
                case 6:
                    value1 = this.Io_OUT_6;
                    break;
                case 7:
                    value1 = this.Io_OUT_7;
                    break;
            }

            this.markezd.输出(port, !value1);
        }


        internal void On_SetOut_1(ushort port )
        {
            bool value1 = false ;
            switch (port)
            {
                case 0:
                    value1 = this.Io_OUT_0;
                    break;
                case 1:
                    value1 = this.Io_OUT_1;
                    break;
                case 2:
                    value1 = this.Io_OUT_2;
                    break;
                case 3:
                    value1 = this.Io_OUT_3;
                    break;
                case 4:
                    value1 = this.Io_OUT_4;
                    break;
                case 5:
                    value1 = this.Io_OUT_5;
                    break;
                case 6:
                    value1 = this.Io_OUT_6;
                    break;
                case 7:
                    value1 = this.Io_OUT_7;
                    break;
            }

            this.markezd.输出(port + 8, value1);
        }


    }
}
