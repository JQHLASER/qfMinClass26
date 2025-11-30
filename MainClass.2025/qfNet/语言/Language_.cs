using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfNet
{
    public class Language_
    { 
        public static string Get语言(string TyepValue)
        {
            return Get语言(TyepValue, qfLanguage.LanguageList.lst_Language);
        }

        public static string Get语言(string TyepValue, List<qfLanguage._language_Value_> lstLanguage)
        {
            (string value, qfLanguage._language_Value_[] beff) rt = qfmain.Language_.Get语言(TyepValue, qfLanguage.LanguageList.lst_Language);

            return rt.value;
        }

        public static void Win_设置()
        {
            using (Form_语言 forms = new Form_语言())
            {
                forms.ShowDialog();
            }
        }


    }
}
