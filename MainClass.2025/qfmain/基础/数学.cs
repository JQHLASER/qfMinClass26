using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain.基础
{
    public  class 数学
    {

        public virtual decimal 余数一律加1(decimal x)
        {
            return Math.Ceiling(x);
        }

        public virtual decimal 带小数_不四舍五入(decimal x, int 小数位数)
        {
            return Math.Round(x, 小数位数);
        }

        public virtual decimal 四舍五入(decimal x)
        {
            return Math.Round(x, MidpointRounding.AwayFromZero);
        }

        public virtual decimal 四舍五入(decimal x, int 小数位数)
        {
            return Math.Round(x, 小数位数, MidpointRounding.AwayFromZero);
        }

        public virtual decimal 保留小数位数(decimal x, int 小数位数)
        {
            return Math.Round(x, 小数位数, MidpointRounding.AwayFromZero);
        }


        public virtual double 保留小数位数(double x, int 小数位数)
        {
            return Math.Round(x, 小数位数, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// x%y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual decimal 取余数(decimal x, decimal y)
        {
            return x % y;
        }




        /// <summary>
        /// 反回int,三种,int/double/byte[]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public virtual int 取随机数(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// 返回double,三种,int/double/byte[]
        /// </summary>
        /// <returns></returns>
        public virtual double 取随机数()
        {
            Random random = new Random();
            return random.NextDouble();
        }

        /// <summary>
        /// 取出byte[],三种,int/double/byte[]
        /// </summary>
        /// <returns></returns>
        public virtual void 取随机数(byte[] bytes)
        {
            Random random = new Random();
            random.NextBytes(bytes);
        }



    }
}
