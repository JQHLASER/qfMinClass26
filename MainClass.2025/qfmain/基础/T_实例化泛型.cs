using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    public class T_实例化泛型
    {
        /// <summary>
        /// 使用方法, T t = FastNew(T).Create ();
        /// </summary>  
        public class FastNew<T>
        {
            public static readonly Func<T> Create = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
         

    }
}
