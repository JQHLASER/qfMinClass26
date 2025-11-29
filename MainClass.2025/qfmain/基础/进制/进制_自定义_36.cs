using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain 
{
    internal   class 进制_自定义_36
    {


        /// <summary>
        /// 十进制转换成36进制
        /// </summary>
        /// <param name="xx">待转换的值</param>
        /// <returns></returns>
        public string IntToiMy(long xx)

        {

            string a = "";
            if (xx == 0)
            {
                a = BaseMyCode[0];
            }
            while (xx >= 1)
            {

                int index = Convert.ToInt16(xx - (xx / 36) * 36);

                a = BaseMyCode[index] + a;

                xx = xx / 36;
            }

            return a;

        }


        /// <summary>
        /// 36进制转换成10进制
        /// </summary>
        /// <param name="xx">待转换的值</param>
        /// <returns></returns>
        public long MyToInt(string xx)

        {

            long a = 0;

            int power = xx.Length - 1;



            for (int i = 0; i <= power; i++)

            {

                a += _BaseMyCode[xx[power - i].ToString()] * Convert.ToInt64(Math.Pow(36, i));




            }



            return a;

        }










        /// <summary>
        /// 进制字典
        /// </summary>
        public Dictionary<int, string> BaseMyCode = new Dictionary<int, string>() {

            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},

            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"I"}, {   19  ,"J"},

            {   20  ,"K"}, {   21  ,"L"}, {   22  ,"M"}, {   23  ,"N"}, {   24  ,"O"}, {   25  ,"P"}, {   26  ,"Q"}, {   27  ,"R"}, {   28  ,"S"}, {   29  ,"T"},

            {   30  ,"U"}, {   31  ,"V"}, {   32  ,"W"}, {   33  ,"X"}, {   34  ,"Y"}, {   35  ,"Z"},





        };
        public Dictionary<string, int> _BaseMyCode
        {
            get
            {
                return Enumerable.Range(0, BaseMyCode.Count()).ToDictionary(i => BaseMyCode[i], i => i);
            }
        }




    }
}
