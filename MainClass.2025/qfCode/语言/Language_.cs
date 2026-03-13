using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class Language_
    {
        public Language_()
        {
            qfmain.Language_.Set语言包(LanguageList.lst_Language);
        }

        public static string Get语言(string TyepValue)
        {
            return qfmain.Language_.Get语言(TyepValue);
        }





    }
}
