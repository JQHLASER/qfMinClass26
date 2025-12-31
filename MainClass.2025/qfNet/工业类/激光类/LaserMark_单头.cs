
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qfNet
{

    /// <summary>
    /// 激光打标,综合体
    /// </summary>
    public class LaserMark_单头_All : Language_
    {
        public qfWork._激光参数_ _激光参数;
        public string _激光模板Path = string.Empty;
        public qfWork._初始化状态_ _初始化态 = qfWork._初始化状态_.未初始化;
        public qfWork._激光加工状态_ _激光加工状态 = qfWork._激光加工状态_.闲置;
        public _打标卡类型_ _打标卡类型 = _打标卡类型_.EzCad2;
        public string _打标软件名称 = "EzCad2.exe";


        /// <summary>
        /// IO输入输出的最大端口号
        /// </summary>
        public ushort _IO最大端口号 = 15;

        #region 打标卡

        /// <summary>
        /// 金橙子EzCad2.0
        /// </summary>
        public qfNet.MarkEzd _Markezd = new qfNet.MarkEzd();

        #endregion



        public enum _打标卡类型_
        {
            /// <summary>
            /// 不使用打标卡
            /// </summary>
            不使能,
            /// <summary>
            /// 金橙子2.0
            /// </summary>
            EzCad2,
        }



        public void 初始化(_打标卡类型_ 打标卡类型_)
        {
            this._打标卡类型 = 打标卡类型_;


            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region 初始化

                    this._Markezd.Event_IO_IN += On_IO_IN;
                    this._Markezd.Event_IO_OUT += On_IO_OUT;
                    this._Markezd.Event_初始化状态 += On_初始化状态;
                    this._Markezd.Event_Log += On_Log;
                    this._Markezd.Event_加工状态 += On_加工状态;
                    this._Markezd.Event_加载Ezd成功 += On_加载激光模板成功;
                    this._Markezd.Event_获取图像 += On_获取图像;
                    this._Markezd.Event_标题栏_初始化状态 += On_标题栏状态_初始化状态;
                    this._Markezd.Event_标题栏_加工状态 += On_标题栏状态_加工状态;

                    this._激光参数 = this._Markezd._参数;
                    this._打标软件名称 = this._激光参数.激光软件名称;
                    this._IO最大端口号 = 15;

                    this._Markezd.初始化();

                    #endregion

                    break;

            }
            读参数();

        }

        public void 释放()
        {
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region 释放

                    this._Markezd.Event_IO_IN -= On_IO_IN;
                    this._Markezd.Event_IO_OUT -= On_IO_OUT;
                    this._Markezd.Event_初始化状态 -= On_初始化状态;
                    this._Markezd.Event_Log -= On_Log;
                    this._Markezd.Event_加工状态 -= On_加工状态;
                    this._Markezd.Event_加载Ezd成功 -= On_加载激光模板成功;
                    this._Markezd.Event_获取图像 -= On_获取图像;
                    this._Markezd.Event_标题栏_初始化状态 -= On_标题栏状态_初始化状态;
                    this._Markezd.Event_标题栏_加工状态 -= On_标题栏状态_加工状态;


                    this._Markezd.释放();

                    #endregion

                    break;

            }
        }

        public async Task<bool> 打开激光软件()
        {
            bool rt = true;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    var mc = this._Markezd.EzCad2软件_打开();

                    break;

            }
            return true;
        }

        public bool 打开激光模板(string path, out string msgErr, bool 是否刷新图形 = true, bool 显示日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region EzCad2

                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.LoadEzdFile(path, 是否刷新图形);
                    rt = nErr != qfWork._Err_jczMarkEzd2_.成功 ? false : true;
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);

                    #endregion

                    break;

            }

            if (显示日志)
            {
                On_Log(rt, $"{Get语言("加载激光模板")},{msgErr},{path}");
            }

            return rt;
        }

        /// <summary>
        /// 弹窗选笃
        /// </summary>
        /// <param name="path"></param>
        /// <param name="是否刷新图形"></param>
        /// <param name="显示日志"></param>
        /// <returns></returns>
        public bool 打开激光模板(out string msgErr, bool 是否刷新图形 = true, bool 显示日志 = true)
        {
            bool rt = true;
            msgErr = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = qfmain.软件类.Files_Cfg.Files_Template;
            openFileDialog.Multiselect = false;//不充许选择多个文件

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region EzCad2

                    openFileDialog.DefaultExt = "ezd";
                    openFileDialog.Filter = "(*.ezd)|*.ezd";

                    DialogResult dt = openFileDialog.ShowDialog();
                    if (dt == DialogResult.OK)
                    {
                        string path = openFileDialog.FileName;
                        rt = 打开激光模板(path, out msgErr, 是否刷新图形, 显示日志);
                    }


                    #endregion

                    break;

            }

            return rt;
        }

        public bool 保存激光模板(string path, out string msgErr, bool 显示日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region EzCad2

                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.保存ezd文件(path);
                    rt = nErr != qfWork._Err_jczMarkEzd2_.成功 ? false : true;
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);

                    #endregion

                    break;

            }

            if (显示日志)
            {
                On_Log(rt, $"{Get语言("保存激光模板")},{msgErr},{path}");
            }

            return rt;
        }


        /// <summary>
        /// 显示图像控件
        /// </summary>
        /// <param name="control"></param>
        public void ui_图像控件(UserControl control)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    #region EzCad2

                    this._Markezd.UserColor_图像(control);

                    #endregion

                    break;

            }
        }

        public void 窗体调试()
        {
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    this._Markezd.窗体_调试();

                    break;

            }

            读参数();
        }

        public bool 红光指示(out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    qfWork._激光_红光指示_ red = this._Markezd._参数.红光指示轮廓 ? qfWork._激光_红光指示_.轮郭 : qfWork._激光_红光指示_.外框;
                    rt = this._Markezd.加工_红光指示(red, out msgErr);

                    break;

            }
            return rt;
        }

        public void 停止()
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.停止标刻和红光();
                    break;

            }

        }

        public bool 出激光标刻(bool bFlyMark, out string msgErr, bool is加工状态 = true)
        {
            msgErr = string.Empty;
            bool rt = true;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.标刻(bFlyMark, is加工状态);
                    rt = nErr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);
                    break;

            }
            return rt;
        }


        public void 输出_标刻中(bool NF)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.标刻中, NF);
                    break;

            }

        }
        public void 输出_红光(bool NF)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.红光, NF);
                    break;

            }

        }
        public void 输出_Ready(bool NF)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.软件准备好, NF);
                    break;

            }

        }
        public void 输出_报警(bool NF)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.报警, NF);
                    break;
            }

        }
        public void 输出_报警()
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.报警);
                    break;
            }

        }
        public void 输出_标刻完成()
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.输出(this._Markezd._参数.OUT.标刻完成);
                    break;
            }

        }

        /// <summary>
        /// 读写一下参数
        /// </summary>
        void 读参数()
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.读写参数(1);
                    this._激光参数 = this._Markezd._参数;
                    break;
            }

        }

        public void 刷新图形(qfWork._激光_获取图像_ state = qfWork._激光_获取图像_.获取)
        {

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    this._Markezd.获取_图像(state);
                    break;

            }

        }

        public bool 修改对象内容(string 对象名, string 内容, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.修改_指定对象的内容(对象名, 内容);
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);
                    rt = nErr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                    break;

            }
            return rt;
        }
        public bool 获取对象内容(string 对象名, out string 内容, out string msgErr, int lenght = 255)
        {
            内容 = string.Empty;
            msgErr = string.Empty;
            bool rt = true;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    string value = string.Empty;
                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.获取_对象内容(对象名, ref value, lenght);
                    内容 = value;
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);
                    rt = nErr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                    break;

            }
            return rt;
        }
        public bool 获取对象名称(int 对象索引, out string 对象名, out string msgErr, int lenght = 255)
        {
            对象名 = string.Empty;
            msgErr = string.Empty;
            bool rt = true;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    string value = string.Empty;
                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.获取_对象名称(对象索引, ref value, lenght);
                    对象名 = value;
                    this._Markezd.ErrToMsg((int)nErr, out msgErr);
                    rt = nErr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                    break;

            }
            return rt;
        }
        public int 获取对象总数()
        {
            int count = 0;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    string value = string.Empty;
                    count = this._Markezd.获取_对象总数();
                    break;

            }

            return count;
        }

        /// <summary>
        ///  a:角度,
        ///  <para>反馈结果有可能不准确</para>
        /// </summary> 
        public bool 设置绝对坐标(double x, double y, double xCenter, double yCenter, double a, out string msgErr)
        {

            msgErr = string.Empty;
            bool rt = true;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    string value = string.Empty;
                    qfWork._Err_jczMarkEzd2_ nErr = this._Markezd.旋转变换(x, y, xCenter, yCenter, a);

                    this._Markezd.ErrToMsg((int)nErr, out msgErr);
                    rt = nErr == qfWork._Err_jczMarkEzd2_.成功 ? true : false;
                    break;

            }
            return rt;
        }

        public class _变量信息_
        {
            public _变量信息_(string name, string value)
            {
                this.Name = name;
                this.Value = value;
            }

            /// <summary>
            /// 变量名
            /// </summary>
            public string Name { set; get; }
            /// <summary>
            /// 内容
            /// </summary>
            public string Value { set; get; }
        }

        public void 获取所有变量对象信息(out _变量信息_[] vBeff)
        {
            List<_变量信息_> lst = new List<_变量信息_>();
            int count = 获取对象总数();
            for (int i = 0; i < count; i++)
            {
                获取对象名称(i, out string 对象名, out string msgErr);
                if (!string.IsNullOrEmpty(对象名))
                {
                    获取对象内容(对象名, out string 内容, out msgErr);
                    _变量信息_ info = new _变量信息_(对象名, 内容);
                    lst.Add(info);
                }
            }
            vBeff = lst.ToArray();
        }


        public string 获取模板后缀名()
        {

            bool rt = true;
            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    return ".ezd";
                default:
                    return "";
            }

        }


        #region Err

        public bool Err_未初始化(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_未初始化(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_初始化中(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_初始化中(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }
        public bool Err_加载激光模板中(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_加载激光模板中(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_出激光标刻中(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_出激光标刻中(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_无可加工数据(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_无可加工数据(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_未加载激光模板(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_未加载激光模板(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_红光指示中(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:
                    rt = this._Markezd.Err_红光指示中(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }

        public bool Err_dll是否存在(out string msgErr, bool 是否日志 = true)
        {
            bool rt = false;
            msgErr = string.Empty;

            switch (this._打标卡类型)
            {
                case _打标卡类型_.EzCad2:

                    rt = this._Markezd.Err_dll是否存在(out msgErr, 是否日志);
                    break;

            }

            return rt;
        }





        #endregion





        #region 事件

        /// <summary>
        /// 输入端口
        /// </summary>
        public event Action<bool[]> Event_IO_IN;
        /// <summary>
        /// 输出端口
        /// </summary>
        public event Action<bool[]> Event_IO_OUT;

        void On_IO_IN(bool[] state)
        {
            Event_IO_IN?.Invoke(state);
        }
        void On_IO_OUT(bool[] state)
        {
            Event_IO_OUT?.Invoke(state);
        }


        public event Action<qfWork._初始化状态_> Event_初始化状态;
        void On_初始化状态(qfWork._初始化状态_ state)
        {
            this._初始化态 = state;
            Event_初始化状态?.Invoke(state);
        }


        public event Action<bool, string> Event_Log;
        void On_Log(bool state, string msg)
        {
            Event_Log?.Invoke(state, msg);
        }


        public event Action<qfWork._激光加工状态_> Event_加工状态;
        void On_加工状态(qfWork._激光加工状态_ state)
        {
            this._激光加工状态 = state;
            Event_加工状态?.Invoke(state);
        }


        public event Action<string> Event_加载激光模板成功;
        void On_加载激光模板成功(string Template)
        {
            this._激光模板Path = Template;
            Event_加载激光模板成功?.Invoke(Template);
        }


        public event Action<qfWork._激光_获取图像_> Event_获取图像;
        void On_获取图像(qfWork._激光_获取图像_ state)
        {
            Event_获取图像?.Invoke(state);
        }

        public event Action<_cfg_标题栏状态_[], qfWork._初始化状态_> Event_标题栏状态_初始化状态;
        void On_标题栏状态_初始化状态(_cfg_标题栏状态_[] info, qfWork._初始化状态_ state)
        {
            Event_标题栏状态_初始化状态?.Invoke(info, state);
        }

        public event Action<_cfg_标题栏状态_[], qfWork._激光加工状态_> Event_标题栏状态_加工状态;
        void On_标题栏状态_加工状态(_cfg_标题栏状态_[] info, qfWork._激光加工状态_ state)
        {
            Event_标题栏状态_加工状态?.Invoke(info, state);
        }



        #endregion

    }

}
