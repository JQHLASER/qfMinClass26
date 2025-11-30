using System.Windows;

namespace qfWPFmain
{
    /// <summary>
    /// 语言
    /// </summary>
    public class Language_ : qfmain.Language_
    {

        public Language_() : base()
        {
            Inistiall();
        }





        public new static string Get语言(string TyepValue)
        {
            (string value, qfLanguage._language_Value_[] beff) rt = qfmain.Language_.Get语言(TyepValue, qfLanguage.LanguageList.lst_Language);

            return rt.value;
        }


        public static void 窗体设置(Window d)
        {
            new Win_语言设置() { Owner = Window.GetWindow(d) }.ShowDialog();
        }


    }

}
