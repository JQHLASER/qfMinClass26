using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class Win_委托线程
    {
        /// <summary>
        /// 用法 this._uiContext.Post(delegate{ 处理方法}, "新状态信息");
        /// </summary>
        public System.Threading.SynchronizationContext _uiContext;


        public void 初始化()
        {
            this._uiContext = System.Threading.SynchronizationContext.Current;
        }


        public void UpdateUICallback(Action state,string msg= "新状态信息")
        {
            this._uiContext.Post(delegate
               {
                   state();
               }, msg);
        }


    }
}
