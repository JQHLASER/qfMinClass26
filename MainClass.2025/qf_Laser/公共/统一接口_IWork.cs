using qfNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qf_Laser
{
    /// <summary>
    /// 公共接口
    /// </summary>
    public interface IWork_LaserMark
    {
        _激光参数_ _参数 { set; get; }
        /// <summary>
        /// 激光模板路径
        /// </summary>
        string _Path_激光模板 { set; get; }
        /// <summary>
        /// 最后一次加载的激光模板路径
        /// </summary>
        string _Path_激光模板_最后一次 { set; get; }
        string _激光编辑软件名称 { set; get; }
        string _激光模板后缀 { set; get; }
        _激光加工状态_ _激光加工状态 { set; get; }
        _初始化状态_ _初始化状态 { set; get; }

        /// <summary>
        /// 输入端口
        /// </summary>
        bool[] _IO_InPut { set; get; }
        /// <summary>
        /// 输出端口
        /// </summary>
        bool[] _IO_OutPut { set; get; }


        /// <summary>
        /// 最小端口
        /// </summary>
        ushort _minPort { set; get; }
        /// <summary>
        /// 最大端口
        /// </summary>
        ushort _maxPort { set; get; }



        string 功能说明();


        bool 端口是否有效(ushort Port);




        Task 打开激光编辑软件();

        /// <summary>
        /// 获取IO线程
        /// </summary>
        /// <param name="使能线程"></param>
        void 初始化(bool 使能线程);
        void 释放();

        (bool s, string m) 激光参数();

        (bool s, string m) 初始化打标卡();
        (bool s, string m) 释放打标卡();


        void win_设置();
        void win_调试();
        (bool s, string m) 打开模板(string path, bool Is图像, bool Is显示日志);
        (bool s, string m) 打开模板_openFileDialog(bool Is图像, bool Is显示日志);
        (bool s, string m) 保存模板(string path, bool Is显示日志);
        void ui_图像控件(UserControl control);

        (bool s, string m) 红光指示(bool is日志);
        (bool s, string m) 停止();
        (bool s, string m) 标刻(bool bFlyMark, bool is加工状态 = true);

        void 输出脉冲式(ushort port);
        (bool s, string m) 输出(ushort port,bool NF);
        void 输出_标刻中(bool NF);
        void 输出_红光(bool NF);
        void 输出_Ready(bool NF);
        void 输出_报警(bool NF);
        void 输出_报警();
        void 输出_标刻完成();
        _激光参数_ 读参数();


        void 刷新图形(_激光_获取图像_ state = _激光_获取图像_.获取);
        (bool s, string m, Bitmap v) 获取图形(int width, int height);


        (bool s, string m) 修改对象内容(string 对象名, string 内容);
        (bool s, string m, string v) 获取对象内容(string 对象名, int lenght = 255);
        (bool s, string m, string v) 获取对象名称(int 对象索引, int lenght = 255);
        (bool s, string m, int v) 获取对象总数();

        (bool s, string m) 设置绝对坐标(double x, double y, double xCenter, double yCenter, double a);


        _变量信息_[] 获取所有变量对象信息();



        bool Err_未初始化(out string msg, bool 是否日志 = true);
        bool Err_初始化中(out string msg, bool 是否日志 = true);
        bool Err_加载激光模板中(out string msg, bool 是否日志 = true);
        bool Err_出激光标刻中(out string msg, bool 是否日志 = true);
        bool Err_无可加工数据(out string msg, bool 是否日志 = true);
        bool Err_未加载激光模板(out string msg, bool 是否日志 = true);
        bool Err_红光指示中(out string msg, bool 是否日志 = true);
        bool Err_dll是否存在(out string msg, bool 是否日志 = true);

        event Action<bool[]> Event_IO_IN;
        event Action<bool[]> Event_IO_OUT;
        event Action<_初始化状态_> Event_初始化状态;
        event Action<bool, string> Event_Log;
        event Action<_激光加工状态_> Event_加工状态;
        event Action<string> Event_加载激光模板成功;
        event Action<_激光_获取图像_> Event_获取图像;
        event Action<_cfg_标题栏状态_[], _初始化状态_> Event_标题栏状态_初始化状态;
        event Action<_cfg_标题栏状态_[], _激光加工状态_> Event_标题栏状态_加工状态;



    }
}
