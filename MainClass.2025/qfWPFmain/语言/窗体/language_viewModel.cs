using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfWPFmain
{
    internal partial class language_viewModel : ObservableObject
    {
        public class _语言_
        {
            public string 标题 { set; get; } = Language_.Get语言("语言");

        }




        public language_viewModel()
        {
            Language_.Inistiall();
            Language_.Get语言目录(out string[] FileName, out string msgErr);
            Items_语言包 = FileName;
            this.Selectedindex = FileName.ToList().IndexOf(Language_.Config.LangeuageCfg.LangeuageName);

            this.Label_提示 = Language_.Get语言("此改动在下次启动后生效");

        }



        [ObservableProperty]
        private _语言_  language_语言 = new _语言_();



        [ObservableProperty]
        private string[] items_语言包 = new string[0];

        /// <summary>
        /// 当前选中行
        /// </summary>
        [ObservableProperty]
        private int selectedindex = -1;

        /// <summary>
        /// 提示信息
        /// </summary>
        [ObservableProperty]
        private string label_提示 = "";


    }
}
