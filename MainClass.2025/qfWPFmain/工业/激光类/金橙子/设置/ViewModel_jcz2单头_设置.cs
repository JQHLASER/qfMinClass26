using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfWPFmain
{
    internal partial class ViewModel_jcz2单头_设置 : ObservableObject
    {
        MarkEzd markezd;

        public class _语言_
        {
            #region IN

            public string 启动_脚踏 { get; set; } = Language_.Get语言("启动_脚踏");
            public string 复位 { get; set; } = Language_.Get语言("复位");
            public string 停止 { get; set; } = Language_.Get语言("停止");

            #endregion


            #region OUT

            public string 红光 { get; set; } = Language_.Get语言("红光");
            public string 标刻中 { get; set; } = Language_.Get语言("标刻中");
            public string 标刻完成 { get; set; } = Language_.Get语言("标刻完成");
            public string 报警 { get; set; } = Language_.Get语言("报警");
            public string 输出脉宽 { get; set; } = Language_.Get语言("输出脉宽") + "(ms)";


            #endregion


            #region 选择框

            public string 进入时加载激光模板 { get; set; } = Language_.Get语言("进入时加载激光模板");
            public string 双击查看图像 { get; set; } = Language_.Get语言("双击查看图像");
            public string 红光指示轮廓 { get; set; } = Language_.Get语言("红光指示轮廓");
            public string 加工时使能红光 { get; set; } = Language_.Get语言("加工时使能红光");



            #endregion

            #region 其它

            public string 线程周期 { get; set; } = Language_.Get语言("线程周期") + "(ms)";
            public string 连续加工周期 { get; set; } = Language_.Get语言("连续加工周期") + "(ms)";
            public string 激光软件名称 { get; set; } = Language_.Get语言("激光软件名称");

            #endregion

        }



        public ViewModel_jcz2单头_设置(MarkEzd markezd_)
        {
            this.markezd = markezd_;
            this.markezd.读写参数(1);
            show();

        }

        [ObservableProperty]
        private _语言_ language_语言 = new _语言_();

        #region Value

        /// <summary>
        /// 端口
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<string> items_Port = new ObservableCollection<string>()
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15","Null"
        };

        [ObservableProperty]
        private short io_IN_启动标刻 = 16;
        [ObservableProperty]
        private short io_IN_停止 = 16;
        [ObservableProperty]
        private short io_IN_复位 = 16;

        [ObservableProperty]
        private short io_OUT_软件准备好 = 16;
        [ObservableProperty]
        private short io_OUT_红光 = 16;
        [ObservableProperty]
        private short io_OUT_标刻中 = 16;
        [ObservableProperty]
        private short io_OUT_标刻完成 = 16;
        [ObservableProperty]
        private short io_OUT_报警 = 16;
        [ObservableProperty]
        private int io_OUT_输出脉宽 = 200;

        [ObservableProperty]
        private bool checkBox_进入时加载激光模板 = false;
        [ObservableProperty]
        private bool checkBox_双击查看图像 = false;
        [ObservableProperty]
        private bool checkBox_红光指示轮廓 = false;
        [ObservableProperty]
        private bool checkBox_加工时使能红光 = false;

        [ObservableProperty]
        private int ms_线程周期 = 100;
        [ObservableProperty]
        private int ms_连续加工周期 = 100;

        [ObservableProperty]
        private string name_激光软件名称 = "EzCad2";

        #endregion



        /// <summary>
        /// 显示数据
        /// </summary>
        void show()
        {
            this.Io_IN_启动标刻 = (short)this.markezd._参数.IN.启动标刻;
            this.Io_IN_停止 = (short)this.markezd._参数.IN.停止;
            this.Io_IN_复位 = (short)this.markezd._参数.IN.复位;

            this.Io_OUT_软件准备好 = (short)this.markezd._参数.OUT.软件准备好;
            this.Io_OUT_红光 = (short)this.markezd._参数.OUT.红光;
            this.Io_OUT_标刻中 = (short)this.markezd._参数.OUT.标刻中;
            this.Io_OUT_标刻完成 = (short)this.markezd._参数.OUT.标刻完成;
            this.Io_OUT_报警 = (short)this.markezd._参数.OUT.报警;
            this.Io_OUT_输出脉宽 = this.markezd._参数.OUT.输出脉宽;

            this.CheckBox_进入时加载激光模板 = this.markezd._参数.进入时加载激光模板;
            this.CheckBox_双击查看图像 = this.markezd._参数.双击查看图像;
            this.CheckBox_红光指示轮廓 = this.markezd._参数.红光指示轮廓;
            this.CheckBox_加工时使能红光 = this.markezd._参数.加工时使能红光;

            this.Ms_线程周期 = this.markezd._参数.线程周期;
            this.Ms_连续加工周期 = this.markezd._参数.连续加工周期;
            this.Name_激光软件名称 = this.markezd._参数.激光软件名称;

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        internal void Save()
        {
            this.markezd._参数.IN.启动标刻 =(ushort ) this.Io_IN_启动标刻;
            this.markezd._参数.IN.停止 = (ushort)this.Io_IN_停止;
            this.markezd._参数.IN.复位 = (ushort)this.Io_IN_复位;

            this.markezd._参数.OUT.软件准备好 = (ushort)this.Io_OUT_软件准备好;
            this.markezd._参数.OUT.红光 = (ushort)this.Io_OUT_红光;
            this.markezd._参数.OUT.标刻中 = (ushort)this.Io_OUT_标刻中;
            this.markezd._参数.OUT.标刻完成 = (ushort)this.Io_OUT_标刻完成;
            this.markezd._参数.OUT.报警 = (ushort)this.Io_OUT_报警;
            this.markezd._参数.OUT.输出脉宽 = this.Io_OUT_输出脉宽;

            this.markezd._参数.进入时加载激光模板 = this.CheckBox_进入时加载激光模板;
            this.markezd._参数.双击查看图像 = this.CheckBox_双击查看图像;
            this.markezd._参数.红光指示轮廓 = this.CheckBox_红光指示轮廓;
            this.markezd._参数.加工时使能红光 = this.CheckBox_加工时使能红光;

            this.markezd._参数.线程周期 = this.Ms_线程周期;
            this.markezd._参数.连续加工周期 = this.Ms_连续加工周期;
            this.markezd._参数.激光软件名称 = this.Name_激光软件名称;
            this.markezd.读写参数(0);

            show();
            MessageBox.Show("OK");
        }

    }
}
