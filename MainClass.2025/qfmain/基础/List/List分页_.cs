using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static qfmain.List分页_;

namespace qfmain
{
    public class List分页_
    {
        /// <summary>
        /// 页信息
        /// </summary>
        public class _PageInfo_
        {
            /// <summary>每页行数</summary>
            public int 每页行数 { get; set; }

            /// <summary>总行数</summary>
            public int 总行数 { get; internal set; }

            /// <summary>总页数</summary>
            public int 总页数 { get; internal set; }

            /// <summary>最后一页行数</summary>
            public int 最后一页行数 { get; internal set; }

            /// <summary>当前页索引（从 0 开始）</summary>
            public int 当前页 { get; set; }
        }

        /// <summary>
        /// 分页结果模型
        /// </summary> 
        public class PageResult<T>
        {
            /// <summary>
            /// 页信息
            /// </summary>
            public _PageInfo_ PageInfo { get; set; }
            /// <summary>
            /// 分页后的数据
            /// </summary>
            public List<T[]> Pages { get; set; }
        }


        /// <summary>
        /// 一次性分页（适合数据量不大 / 配置数据）
        /// <para>pageSize :  每面行数</para>
        /// </summary>
        public PageResult<T> 分页_小数据量<T>(
            IList<T> source,
            int pageSize)
        {
            var pageInfo = new _PageInfo_
            {
                每页行数 = pageSize
            };

            var pages = new List<T[]>();

            if (source == null || source.Count == 0 || pageSize <= 0)
            {
                pageInfo.总行数 = 0;
                pageInfo.总页数 = 0;
                pageInfo.最后一页行数 = 0;

                return new PageResult<T>
                {
                    PageInfo = pageInfo,
                    Pages = pages
                };
            }

            pageInfo.总行数 = source.Count;
            pageInfo.总页数 = (pageInfo.总行数 + pageSize - 1) / pageSize;

            for (int i = 0; i < pageInfo.总页数; i++)
            {
                int startIndex = i * pageSize;

                int count = Math.Min(
                    pageSize,
                    pageInfo.总行数 - startIndex);

                if (i == pageInfo.总页数 - 1)
                    pageInfo.最后一页行数 = count;

                T[] page = new T[count];
                for (int j = 0; j < count; j++)
                    page[j] = source[startIndex + j];

                pages.Add(page);
            }

            return new PageResult<T>
            {
                PageInfo = pageInfo,
                Pages = pages
            };
        }

        /// <summary>
        /// 仅获取指定页（适合 UI 翻页 / 大数据）
        /// <para>pageIndex : 页索引,从0开始</para>
        /// <para>pageSize : 每页行数</para>
        /// <para>pageInfo : 页信息</para>
        /// </summary>
        public T[] 分页_仅获取指定页_大数据量<T>(
            IList<T> source,
            int pageIndex,
            int pageSize,
            out _PageInfo_ pageInfo)
        {
            pageInfo = new _PageInfo_
            {
                每页行数 = pageSize,
                当前页 = pageIndex
            };

            if (source == null || source.Count == 0 || pageSize <= 0)
            {
                pageInfo.总行数 = 0;
                pageInfo.总页数 = 0;
                pageInfo.最后一页行数 = 0;
                return Array.Empty<T>();
            }

            pageInfo.总行数 = source.Count;
            pageInfo.总页数 = (pageInfo.总行数 + pageSize - 1) / pageSize;

            if (pageIndex < 0 || pageIndex >= pageInfo.总页数)
                return Array.Empty<T>();

            int startIndex = pageIndex * pageSize;

            int count = Math.Min(
                pageSize,
                pageInfo.总行数 - startIndex);

            if (pageIndex == pageInfo.总页数 - 1)
                pageInfo.最后一页行数 = count;

            T[] result = new T[count];
            for (int i = 0; i < count; i++)
                result[i] = source[startIndex + i];

            return result;
        }
    }




}


