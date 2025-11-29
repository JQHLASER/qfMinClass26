using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWork
{
    public class Language_ : qfmain.Language_
    {
        public Language_() : base()
        {
            
        }
          
        public new static string Get语言(string TyepValue)
        {
            return qfmain.Language_.Get语言(TyepValue, qfmain. LanguageList.lst_Language);
        }

 



    }
}
