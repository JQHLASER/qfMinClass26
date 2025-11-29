using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mainclassqf
{
    public class 正则表达式
    {

        public bool 字符串和数字(string value)
        {
            Regex rege = new Regex(@"^[a-zA-Z0-9]{1,20}$");
            //1表示最少一位,20表示最多20位

            // Regex rege = new Regex(@"^[\u4e00-\u9fa5@-a-zA-Z0-9]{2,20}$"); //表示为中文/英文/数字 @"^[\u4e00-\u9fa5@-a-zA-Z0-9]*$"


            if (rege.Match(value).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
