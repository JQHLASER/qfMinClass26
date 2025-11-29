using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainclassqf
{
    public class ListView_T<T> : ListView
    {
        System.Windows.Forms.ListView listview_;

        public ListView_T(System.Windows.Forms.ListView listview) : base(listview)
        {
            listview_ = listview;
        }


        public void 添加数据(List<T> lst)
        {

            this.listview_.BeginUpdate();
            try
            {
                清空全部数据();
                On_添加(lst);
            }
            finally
            {
                this.listview_.EndUpdate();
            }
        }


        public Action<List<T>> Action_添加;
        void On_添加(List<T> m)
        {
            if (Action_添加 != null)
            {
                Action_添加(m);
            }
        }
    }
}
