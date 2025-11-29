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
            return Get语言(TyepValue, qfmain.LanguageList.lst_Language);
        }

        public static string Get语言(string TyepValue, List<qfmain._language_Value_> lstLanguage)
        {
            return qfmain.Language_.Get语言(TyepValue, lstLanguage);
        }

        public static void Win_设置()
        {
            using (Form_语言 forms=new Form_语言())
            {
                forms.ShowDialog();
            }
        }


    }
}
