using System.Windows;

namespace qfWPFmain
{
    /// <summary>
    /// ”Ô—‘
    /// </summary>
    public class Language_ : qfmain.Language_
    {

        public Language_() : base()
        {
            Inistiall();
        }





        public new static string Get”Ô—‘(string TyepValue)
        {
            return qfmain.Language_.Get”Ô—‘(TyepValue, qfmain.LanguageList.lst_Language);
        }


        public static void ¥∞ÃÂ…Ë÷√(Window d)
        {
            new Win_”Ô—‘…Ë÷√() { Owner = Window.GetWindow(d) }.ShowDialog();
        }


    }

}
