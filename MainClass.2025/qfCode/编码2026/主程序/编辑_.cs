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
        internal qfmain._初始化状态_ _初始化状态 = qfmain._初始化状态_.未初始化;

        /// <summary>
        /// 在视图存储参数,本地方式计算时
        /// </summary>
        internal 编码_ _编码;


        /// <summary>
        /// 本地或通讯交互,
        /// <para>有的是计算在服务端的</para>
        /// </summary>
        internal type_编辑._交互类型_ _交互类型 = type_编辑._交互类型_.本地;
        internal string[] _变量对象名 = new string[0];


        /// <summary>
        /// 变量对象名 : 如激光模板中的对象名称,方便设置
        /// <para>在视图存储参数,本地方式计算时</para>
        /// </summary> 
        public 编辑_(编码_ 编码, _交互类型_ 交互类型, string[] 变量对象名)
        {
            初始化(编码, 交互类型, 变量对象名);
        }
        public 编辑_()
        {

        }

        /// <summary>
        /// 变量对象名 : 如激光模板中的对象名称,方便设置
        /// <para>在视图存储参数,本地方式计算时</para>
        /// </summary> 
        public void 初始化(编码_ 编码, _交互类型_ 交互类型, string[] 变量对象名)
        {
            this._功能 = 编码._功能;
            this._编码 = 编码;
            this._交互类型 = 交互类型;
            this._变量对象名 = 变量对象名;

        }


        /// <summary>
        /// 配方名称 : 进入窗体时要打开的配方,否则为空白的
        /// <para> _配方文件_属性_ cfg  : 外部文件时使用,如将编码配方信息存放在工件配方内</para>
        /// </summary> >
        public void Win_主窗体(string 配方名称, bool Is父窗体 = false, _配方文件_属性_ cfg = null)
        {
            using (Form_主窗体 forms = new Form_主窗体(配方名称, this, cfg))
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
