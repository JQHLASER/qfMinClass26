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

        



        public virtual void 上移<T>(List<T> list, ref int index, int moveCount)
        {
            if (list == null || moveCount <= 0 || index <= 0) return;

            int newIndex = Math.Max(0, index - moveCount);
            var item = list[index];
            list.RemoveAt(index);
            list.Insert(newIndex, item); 
            index = newIndex;
        }
        public virtual void 下移<T>(List<T> list, ref int index, int moveCount)
        {
            if (list == null || moveCount <= 0 || index >= list.Count - 1) return;

            int newIndex = Math.Min(list.Count - 1, index + moveCount);

            var item = list[index];
            list.RemoveAt(index);
            list.Insert(newIndex, item);

            index = newIndex;
        }

        /// <summary>
        /// index : 当前索引
        ///<para>移动行数 : 小于0向上, 大于0向下</para>
        /// </summary> 
        public virtual int 移动<T>(List<T> list, int index, int 移动行数)
        {
            if (list == null || index < 0 || index >= list.Count) return index;

            int newIndex = index + 移动行数;
            if (newIndex < 0) newIndex = 0;
            if (newIndex > list.Count - 1) newIndex = list.Count - 1;

            if (newIndex == index) return index;

            var item = list[index];
            list.RemoveAt(index);

            if (newIndex > index)
                newIndex--; // ⭐ 修正索引

            list.Insert(newIndex, item);

            return newIndex;
        } 

        /// <summary>
        /// index : 当前索引
        /// <para>性能高,不卡UI</para>
        /// </summary> 
        public virtual int 移动Swap<T>(List<T> list, int index, int 移动行数)
        {
            if (list == null || index < 0 || index >= list.Count) return index;

            int newIndex = index + 移动行数;
            if (newIndex < 0) newIndex = 0;
            if (newIndex > list.Count - 1) newIndex = list.Count - 1;

            if (newIndex == index) return index;

            if (newIndex > index)
                for (int i = index; i < newIndex; i++)
                    (list[i], list[i + 1]) = (list[i + 1], list[i]);
            else
                for (int i = index; i > newIndex; i--)
                    (list[i], list[i - 1]) = (list[i - 1], list[i]);

            return newIndex;
        }
          
        public virtual void 在指定位置插入<T>(List<T> lst, T item, int index)
        {
            lst.Insert(index, item); 
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


        public virtual List<T> 合并_LINQ<T>(List<T> lst, List<T> lst新)
        {
            return lst.Concat(lst新).Distinct().ToList();
        }

        /// <summary>
        /// 大量数据更快,适合10万+
        /// </summary> 
        public virtual List<T> 合并_HashSet<T>(List<T> lst, List<T> lst新)
        {
            var set = new HashSet<T>(lst);
            set.UnionWith(lst新);
            return set.ToList();
        }

        /// <summary>
        /// 不创建新 List,保留重复元素,适合大量数据
        /// </summary> 
        public virtual void 合并_AddRange<T>(List<T> lst原, List<T> lst新)
        {
            lst原.AddRange(lst新);
        }

    }
}
