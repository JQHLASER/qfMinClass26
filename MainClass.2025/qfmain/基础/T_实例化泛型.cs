using NPOI.SS.Formula.Functions;
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
            public static readonly Func<T> Create = () => Activator.CreateInstance<T>();
            //public static readonly Func<T> Create = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();

        }

        /// <summary>
        /// 方法2
        /// </summary> 
        public class FastNew2<T>
        {
            public static readonly Func<T> Create;
            static FastNew2()
            {
                try
                {
                    var ctor = typeof(T).GetConstructor(Type.EmptyTypes);
                    if (ctor != null)
                    {
                        Create = Expression
                            .Lambda<Func<T>>(Expression.New(ctor))
                            .Compile();
                        return;
                    }
                }
                catch
                {
                    // 忽略，走降级
                }

                // 兜底方案
                Create = () => Activator.CreateInstance<T>();
            }
        }


    }
}
