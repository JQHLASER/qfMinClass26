using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfCode.type_编辑;

namespace qfCode
{
    public class 编辑_
    {
        internal _功能_ _功能;
        internal _初始化状态_ _初始化状态 = _初始化状态_.未初始化;
        internal 编码_ _编码;

        /// <summary>
        /// 本地或通讯交互,
        /// <para>有的是计算在服务端的</para>
        /// </summary>
        internal type_编辑._交互类型_ _交互类型 = type_编辑._交互类型_.本地;


        public 编辑_(编码_ 编码, _交互类型_ 交互类型)
        {
            this._功能 = 编码._功能;
            this._编码 = 编码;
            this._交互类型 = 交互类型;
        }


        /// <summary>
        /// 配方名称 : 进入窗体时要打开的配方,否则为空白的
        /// </summary> >
        public void Win_主窗体( string 配方名称,bool Is父窗体 = false)
        {

            using (Form_主窗体 forms = new Form_主窗体(配方名称,this))
            {
                forms.MaximizeBox = false;
                if (!Is父窗体)
                {
                    forms.ShowInTaskbar = false;
                    forms.MinimizeBox = false;
                }
                forms.ShowDialog();
            }
        }




    }
}
