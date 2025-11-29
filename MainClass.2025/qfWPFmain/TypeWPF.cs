using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace qfWPFmain
{
    /// <summary>
    /// 窗体标题栏状态
    /// </summary>
    public enum WindowTitleState
    {
        闲置中,
        报警中,
        加工中,
    }


    /// <summary>
    /// MessageBox按钮
    /// </summary>
    public enum MessageboxButton
    {
        YesNo,
        Ok,
    }
    /// <summary>
    /// MessageBox状态
    /// </summary>
    public enum MessageboxState
    {
        None,
        Green,
        Red,
        Yellow,
    }

    /// <summary>
    /// 用户权限
    /// </summary>
    public enum LoginUserType
    {
        操作员,
        技术员,
        管理员,
        超级管理员,
        开发者,
    }

    /// <summary>
    /// 登陆方式
    /// </summary>
    public enum LoginModelType
    {
        /// <summary>
        /// 用户信息保存在本地
        /// </summary>
        本地数据,
        /// <summary>
        /// 用户信息保存在服务器
        /// </summary>
        远程数据,
    }

    /// <summary>
    /// 登陆界面/切换界面
    /// </summary>
    public enum LoginShowType
    {
        用户登陆,
        用户切换,
    }




    /// <summary>
    /// 输入文本类型,ui_TextBox
    /// </summary>
    public enum 输入值类型
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 短整型
        /// </summary>
        Int,
        /// <summary>
        /// 长整型
        /// </summary>
        Long,
        /// <summary>
        /// 双精度型
        /// </summary>
        Double,

    }



    /// <summary>
    /// 窗体标题栏信息
    /// </summary>
    public class _windowInfo_
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name_">功能名称</param>
        /// <param name="状态_">=0:无,=小于0:红,=大于0:黄色</param>     
        /// <param name="内容_"></param>
        public _windowInfo_(string Name_, int 状态_, string 内容_)
        {
            Name = Name_;
            状态 = 状态_;
            内容 = 内容_;
        }

        /// <summary>
        /// 功能标题,表示状态的种类
        /// </summary>
        public string Name { set; get; } = "";

        public string 内容 { set; get; } = "";

        /// <summary>
        /// =0:无,=小于0:红,=大于0:黄色
        /// </summary>
        public int 状态 { set; get; } = 0;

    }

    /// <summary>
    /// 功能栏的时间显示
    /// </summary>
    public class _功能栏_时间类_
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Datetimes_">当前时间</param>
        /// <param name="RunTime_">系统运行时间</param>
        public _功能栏_时间类_(DateTime Datetimes_, TimeSpan RunTime_)
        {
            this.Datetimes = Datetimes_;
            this.RunTime = RunTime_;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime Datetimes { set; get; }
        /// <summary>
        /// 系统运行时间
        /// </summary>
        public TimeSpan RunTime { set; get; }

    }

    /// <summary>
    /// 状态栏内容
    /// </summary>
    public class _状态栏_功能栏_Info_
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name_">功能名称</param>
        /// <param name="内容_"></param>
        public _状态栏_功能栏_Info_(string Name_, string 内容_)
        {
            Name = Name_;
            内容 = 内容_;
        }

        /// <summary>
        /// 功能标题,表示状态的种类
        /// </summary>
        public string Name { set; get; } = "";

        public string 内容 { set; get; } = "";

    }

    /// <summary>
    /// 显示加工信息到画布上
    /// </summary>
    public class _信息显示_
    {
        public string Value_ { set; get; } = "";
        /// <summary>
        /// double.NaN 为自动
        /// </summary>
        public double Width_ { set; get; } = double.NaN;
        public double FontSize_ { set; get; } = 22;
        public Brush Foreground_ { set; get; } = Brushes.Gray;

        public Brush Background_ { set; get; } = Brushes.Transparent;
        public FontFamily FontFamily_ { set; get; } = new FontFamily("新宋体");

        /// <summary>
        /// 是否加粗
        /// </summary>
        public FontWeight FontWeight_ { set; get; } = FontWeights.Normal;
        /// <summary>
        /// 是否斜体
        /// </summary>
        public FontStyle FontStyle_ { set; get; } = FontStyles.Normal;
        public Thickness Margin_ { set; get; } = new Thickness();
    }




}
