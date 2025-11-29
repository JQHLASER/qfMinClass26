using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfmain
{
    /// <summary>
    /// 自定义,将字母和数字错位表示
    /// <para>本加密算法为生成或解析注册码而定义</para>
    /// </summary>
    public class 加解密_注册码算法
    {
        string 分割符 = "-";
        public 加解密_注册码算法(string 分割符_ = "-")
        {
            分割符 = 分割符_;
        }



        #region 字典


        /// <summary>
        /// 进制字典
        /// </summary>
        public Dictionary<int, string> BaseMyCode = new Dictionary<int, string>() {

            {   0  ,"A"},
            {   1  ,"B"},
            {   2  ,"C"},
            {   3  ,"D"},
            {   4  ,"E"},
            {   5  ,"F"},
            {   6  ,"G"},
            {   7  ,"H"},
            {   8  ,"I"},
            {   9  ,"J"},

            {   10  ,"K"},
            {   11  ,"L"},
            {   12  ,"M"},
            {   13  ,"N"},
            {   14  ,"O"},
            {   15  ,"P"},
            {   16  ,"Q"},
            {   17  ,"R"},
            {   18  ,"S"},
            {   19  ,"T"},
            {   20  ,"U"},
            {   21  ,"V"},
            {   22  ,"W"},
            {   23  ,"X"},
            {   24  ,"Y"},
            {   25  ,"Z"},
            {   26  ,"1"},
            {   27  ,"2"},
            {   28  ,"3"},
            {   29  ,"4"},
            {   30  ,"5"},
            {   31  ,"6"},
            {   32  ,"7"},
            {   33  ,"8"},
            {   34  ,"9"},
            {   35  ,"0"},
        };


        /// <summary>
        /// 十进制转换成36进制
        /// </summary>
        /// <param name="xx">待转换的值</param>
        /// <returns></returns>
        string IntToiMy(long xx)

        {

            string a = "";
            if (xx == 0)
            {
                a = BaseMyCode[0];
            }
            while (xx >= 1)
            {

                int index = Convert.ToInt32(xx - (xx / 36) * 36);

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
        long MyToInt(string xx)

        {

            long a = 0;

            int power = xx.Length - 1;



            for (int i = 0; i <= power; i++)
            {
                a += _BaseMyCode[xx[power - i].ToString()] * Convert.ToInt64(Math.Pow(36, i));
            }



            return a;

        }

        Dictionary<string, int> _BaseMyCode
        {
            get
            {
                return Enumerable.Range(0, BaseMyCode.Count()).ToDictionary(i => BaseMyCode[i], i => i);
            }
        }

        #endregion

        /// <summary>
        /// 计算值
        /// </summary>
        int moths = 13;

        /// <summary>
        /// 不支持特殊符号
        /// </summary>
        /// <param name="value">格式:设备ID-用户ID-注册信息</param>
        /// <returns></returns>
        public virtual string 加密(string value)
        {
            string[] beff_Value = value.Split(new string[] { 分割符 },StringSplitOptions.None);

            List<string> lstStr = new List<string>();
            int a = 0;
            string xt = string.Empty;
            foreach (var s in beff_Value[0])
            {
                if (a <= 6)
                {
                    a++;
                    xt += new string(new char[] { s });
                }

                if (a == 6)
                {
                    lstStr.Add(xt);
                    xt = string.Empty;
                    a = 0;
                }
            }
            if (!string.IsNullOrEmpty(xt))
            {
                lstStr.Add(xt);
                xt = string.Empty;
            }

            if (beff_Value.Length >= 2)
            {
                lstStr.Add(beff_Value[1]);
            }
            if (beff_Value.Length >= 3)
            {
                lstStr.Add(beff_Value[2]);
            }

            string v0 = string.Empty;
            for (int i = 0; i < lstStr.Count; i++)
            {
                string s = lstStr[i];
                long b = MyToInt(s) * moths;

                string A = "";
                if (new 文本().获取(s, 0, 1) == "A")
                {
                    A = "A";
                }

                if (i == 0)
                {
                    v0 += $"{A}{IntToiMy(b)}";
                }
                else
                {
                    v0 += $"{分割符}{A}{IntToiMy(b)}";
                }
            }


            return v0;

        }


        /// <summary>
        /// 不支持特殊符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="使能分割符">不使能时会生成不含分割符的字符串</param>
        /// <returns>格式:设备ID-用户ID-注册信息</returns>
        public virtual string 解密(string value, bool 使能分割符 = true)
        {

            string[] a = value.Split(new string[] { 分割符 }, StringSplitOptions.None);
            string v0 = string.Empty;
            for (int i = 0; i < a.Length; i++)
            {
                string s = a[i];
                long b = MyToInt(s) / moths;

                string A = "";
                if (new 文本().获取(s, 0, 1) == "A")
                {
                    A = "A";
                }


                if (使能分割符 && (i >= a.Length - 2))
                {
                    v0 += $"{分割符}{A}{IntToiMy(b)}";
                }
                else
                {
                    v0 += $"{A}{IntToiMy(b)}";
                }

            }





            return v0;

        }




    }
}
