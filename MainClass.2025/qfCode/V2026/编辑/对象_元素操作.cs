using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Windows.Forms;

namespace qfCode
{
    internal class 对象_元素操作
    {

        internal void 上移一行<T>(List<T> lst)
        {
            int index = 1;
            try
            {
                new qfmain.List_().上移(lst, ref index, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void 下移一行<T>(List<T> lst)
        {
            int index = 1;
            try
            {
                new qfmain.List_().下移(lst, ref index, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void 上移一行(Sunny.UI.UIListBox listbox)
        {
            try
            {
                new qfNet.listbox_().上移(listbox, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void 下移一行(Sunny.UI.UIListBox listbox)
        {
            try
            {
                new qfNet.listbox_().下移(listbox, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); MessageBox.Show(ex.Message);
            }
        }

        internal void 在指定处插入<T>(List<T> lst, T item, int index)
        {
            try
            {
                new qfmain.List_().在指定位置插入(lst, item, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void 在指定处插入<T>(Sunny.UI.UIListBox listbox, T item, int index)
        {
            try
            {
                new qfNet.listbox_().在指定位置插入(listbox, item, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         
     
        internal void 上移一行<T>(BindingList<T> lstBind, int index,DataGridView datagrid)
        {
            int index1 =  index;
            try
            {
                new qfmain.List_().上移(lstBind, ref index1, 1);
                new qfNet.DataGridview_(datagrid).选中指定行(0,index1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void 下移一行<T>(BindingList<T> lstBind,int index, DataGridView datagrid)
        {
            int index1 =index ;
            try
            { 
                new qfmain.List_().下移(lstBind, ref index1, 1);
                new qfNet.DataGridview_(datagrid).选中指定行(0, index1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal void 在指定处插入<T>(BindingList<T> lstBind, T item, int index, DataGridView datagrid)
        {
            try
            {
                
                new qfmain.List_().在指定位置插入(lstBind, item, index);
                new qfNet.DataGridview_(datagrid).选中指定行(0, index);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
