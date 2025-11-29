using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    public  class List分页_
    {
        /// <summary>
        /// 数据结构
        /// </summary>
        public class _info_
        {
            public int 每页行数 { set; get; } = 10;
            public int 总页数 { set; get; } = 0;
            public int 最后一页行数 { set; get; } = 0;
            public int 当前页码 { set; get; } = 0;



            public int 总行数 { set; get; } = 0;
            public int 在总数据第几行 { set; get; } = 0;
        }


        /// <summary>
        /// config分割后的数据:数组格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="config_"></param>
        /// <param name="config数据原始"></param>
        /// <param name="config分割后的数据"></param>
        public virtual  void 获取总页及分页好的数据<T>(List<T> lst数据原始, out List<T[]> lst分割后的数据, ref _info_ config_)
        {
            lst分割后的数据 = new List<T[]>();
            lst分割后的数据.Clear();
            config_.总行数 = lst数据原始.Count;
            config_.总页数 = 0;

            if (config_.总行数 > 0)
            {
                // 取总页数,总行数/每页行数 = 页数量,然后根据余数判断是否需要加1
                int a = (int)(config_.总行数 * 1.0 / config_.每页行数 * 1.0);

                //取余数
                int b = (int)(config_.总行数 * 1.0 % config_.每页行数 * 1.0);


                if (b == 0)
                {
                    config_.总页数 = a;
                }
                else if (b > 0)
                {
                    //有余数表示,后面还有数据,则+1
                    config_.总页数 = a + 1;
                    //最后一页的数据就是余数
                    config_.最后一页行数 = b;
                }




                //在无余数情况下分割的数据
                for (int i = 0; i < config_.总页数; i++)
                {
                    int count = config_.每页行数;
                    //b>0表示有余数,  i == config_.总行数 - 1 表示是最后一行了
                    if (b > 0 && i == config_.总页数 - 1)
                    {
                        count = config_.最后一页行数;
                    }


                    List<T> lst中转 = new List<T>();
                    for (int ia = 0; ia < count; ia++)
                    {
                        int mc = (i * config_.每页行数) + ia;
                        lst中转.Add(lst数据原始[mc]);
                    }
                    lst分割后的数据.Add(lst中转.ToArray());
                }

            }

        }


        /// <summary>
        /// 页码: 从1到总页数,,页码可以用 config.当前页码+1或 config.当前页码-1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="页码"></param>
        /// <param name="lst分页好的数据"></param>
        /// <param name="lst当前页码数据"></param>
        public virtual List<T> 选择页<T>(int 页码, List<T[]> lst分页好的数据)
        {
            List<T> lst = new List<T>();
            if (页码 <= 0 || 页码 > lst分页好的数据.Count)
            {
                return lst;
            }
            int a = 页码 - 1;

            lst = lst分页好的数据[a].ToList();

            return lst;
        }

        /// <summary>
        /// 在当前页中的行号,从0开始的
        /// </summary>
        /// <param name="页码"></param>
        /// <param name="在当前页中的行号"></param>
        /// <param name="config_"></param>
        public virtual void 当前行在总数据中的行号(int 页码, int 在当前页中的行号, _info_ config_)
        {
            if (页码 <= 0 || 页码 > config_.总页数)
            {
                return;
            }
            int a = 页码 - 1;
            int b = a * config_.每页行数;
            config_.在总数据第几行 = b + 在当前页中的行号;
        }

    }
}
