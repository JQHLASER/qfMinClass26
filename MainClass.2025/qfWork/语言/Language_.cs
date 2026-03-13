using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class Language_ : qfmain.Language_
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
