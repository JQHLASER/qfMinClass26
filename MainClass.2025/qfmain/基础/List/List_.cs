using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{

    /*list排序...int
     *  List<int> lst=new List<int>();
     *  lst.Sort(); 
     *  
     *  
     *  
     * list排序...结构体
     *  List<m> lst = new List<m>
            {
                new m ("A1","A11"),
                new m ("A2","A22"),
                new m ("A3","A33"),
                new m ("A4","A44"),
            };
        lst = lst.OrderByDescending(p => p.name ).ToList();
     */
    public class List_
    {



        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int 统计指定值的数量(List<int> lst, int value)
        {
            return lst.Count(s => s == value);
        }
        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int 统计指定值的数量(List<string> lst, string value)
        {
            return lst.Count(s => s == value);
        }
        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int 统计指定值的数量(List<short> lst, short value)
        {
            return lst.Count(s => s == value);
        }
        /// <summary>
        /// 重载...int/string/short/byte
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int 统计指定值的数量(List<byte> lst, byte value)
        {
            return lst.Count(s => s == value);
        }

        public virtual void 上移指定行<T>(ref List<T> Strbeff, int 当前选中行, int 上移数量)
        {

            if (当前选中行 <= 0)
            {
                return;
            }
            else if (当前选中行 - 上移数量 < 0)
            {
                return;
            }

            var item = Strbeff[当前选中行];
            Strbeff.RemoveAt(当前选中行);
            Strbeff.Insert(当前选中行 - 上移数量, item);
            当前选中行 = 当前选中行 - 上移数量;
        }
        public virtual void 下移指定行<T>(ref List<T> Strbeff, int 当前选中行, int 下移数量)
        {
            int count = Strbeff.Count;
            if (当前选中行 >= count - 1 || 当前选中行 < 0)
            {
                return;
            }
            else if (当前选中行 + 下移数量 > count - 1)
            {
                return;
            }

            var item = Strbeff[当前选中行];

            Strbeff.RemoveAt(当前选中行);
            Strbeff.Insert(当前选中行 + 下移数量, item);
            当前选中行 = 当前选中行 + 下移数量;

        }


        /// <summary>
        /// 在添加数据后,调用此方法
        /// </summary>
        /// <param name="listbox_"></param>
        public virtual void 在指定位置插入<T>(ref List<T> lst, int 当选选中行索引)
        {
            int count = lst.Count;
            if (count < 2 || 当选选中行索引 >= count - 1 || 当选选中行索引 < 0)
            {
                return;
            }

            int index = count - 1;
            int 上移数量 = count - 当选选中行索引 - 2;
            上移指定行(ref lst, index, 上移数量);


        }
        public virtual void 查询重复的内容<T>(List<T> list, out List<T> lst重复的内容)
        {
            lst重复的内容 = new List<T>();

            var duplicates = list.GroupBy(item => item)
                                 .Where(group => group.Count() > 1)
                                 .Select(group => group.Key);

            foreach (var item in duplicates)
            {
                lst重复的内容.Add(item);
            }

        }
        public virtual void 分割成多页<T>(List<T> list, int 每页条数, out List<List<T>> lst分割后的)
        {
            lst分割后的 = new List<List<T>>();

            int 每组数据 = 每页条数; //每组数据100条
            for (int i = 0; i < list.Count; i += 每组数据)
            {
                //去除数据 其中Skip 表示跳过多少条数据  Take表示获取多少条数据
                lst分割后的.Add(list.Skip(i).Take(每组数据).ToList());
            }

        }

        public virtual void 分割成多页<T>(List<T> list, int 每页条数, out List<T[]> lst分割后的)
        {
            lst分割后的 = new List<T[]>();
            int 每组数据 = 每页条数; //每组数据100条
            for (int i = 0; i < list.Count; i += 每组数据)
            {
                //去除数据 其中Skip 表示跳过多少条数据  Take表示获取多少条数据
                lst分割后的.Add(list.Skip(i).Take(每组数据).ToArray());
            }

        }



        /// <summary>
        /// list string去重 
        /// </summary>
        /// <param name="lst"></param>
        public virtual List<T> 去重<T>(List<T> lst)
        {
            return lst.Distinct().ToList();
        }

    }
}
