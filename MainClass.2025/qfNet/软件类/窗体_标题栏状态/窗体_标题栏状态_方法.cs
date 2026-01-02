
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class 窗体_标题栏状态_方法
    {
        /// <summary>
        /// <para>con : 标题栏状态控件</para>
        /// <para>Name : 变量名称</para>
        /// <para>Title : 标题,如PLC,读码器等</para>
        /// </summary> >
        public void 标题栏状态(窗体_标题栏状态 con, string Name, string Title, qfmain._连接状态_ state)
        {
            _cfg_标题栏状态_[] info = new _cfg_标题栏状态_[]
             {
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("已连接")}",(int)qfmain._连接状态_.已连接 ),
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("连接中")}",(int)qfmain._连接状态_.连接中  ),
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("未连接")}",(int)qfmain._连接状态_.未连接  ),
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("无响应")}",(int)qfmain._连接状态_.无响应   ),
             };
            con.Add(info, (int)state);
        }


        public void 标题栏状态(窗体_标题栏状态 con, string Name, string Title, qfWork._初始化状态_ state)
        {
            _cfg_标题栏状态_[] info = new _cfg_标题栏状态_[]
             {
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("未初始化")}",(int)qfWork._初始化状态_.未初始化),
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("初始化中")}",(int)qfWork._初始化状态_.初始化中),
                new _cfg_标题栏状态_ (Name,$"{Title }{Language_ .Get语言 ("已初始化")}",(int)qfWork._初始化状态_.已初始化),

             };
            con.Add(info, (int)state);
        }




    }
}
