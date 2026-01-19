using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qfCode
{
    public partial class Control_时间 : Sunny.UI.UITitlePanel
    {
        type_编辑._编辑类型_ _type;
        _元素_.时间 _cfg;
        public Control_时间(type_编辑._编辑类型_ type, _元素_.时间   cfg)
        {
            InitializeComponent();
            this._type = type;
            this._cfg = new _元素_.时间().Clone();

            this.Load += (s, e) =>
            {


            };
            

            show();
        }

        #region 对外方法

        /// <summary>
        /// 赋值
        /// </summary>
        public void GetCfg()
        {
           

        }

        #endregion


        #region 本地方法

        void show()
        {
            switch (this._type)
            {
                case type_编辑._编辑类型_.添加:
                    #region 添加 

                   
                    #endregion
                    break;
                case type_编辑._编辑类型_.修改:

                    #region 修改

                   

                    #endregion

                    break;
            }

        }
         
      
        void On_选中()
        { 
        }

        #endregion


    }
}
