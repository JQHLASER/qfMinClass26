using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    public class Language_ : qfmain.Language_
    {
        public Language_() : base()
        {

        }

        public new static string Get语言(string TyepValue)
        {
            (string value, qfLanguage._language_Value_[] beff) rt = qfmain.Language_.Get语言(TyepValue, qfLanguage.LanguageList.lst_Language);

            return rt.value;
        }





    }
}
