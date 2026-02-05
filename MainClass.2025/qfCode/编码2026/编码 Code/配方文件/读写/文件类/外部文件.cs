using Newtonsoft.Json;
using qfmain;
using qfNet;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfCode
{
    public class 外部文件 : Iwork_文件
    {
        编码_ _codeSys;
        public 外部文件(编码_ codeSys)
        {
            this._codeSys = codeSys;
        }

        /// <summary>
        ///  FileName : 无用,赋空值即可
        /// </summary> 
        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            return this._codeSys.On_保存(cfg);
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            return (false, Language_.Get语言("无此功能"), new _配方文件_属性_());
        }

        public (bool s, string m) Delete(string FileName)
        {
            return (false, Language_.Get语言("无此功能") );
        }


        public (bool s, string m) 复制(string FileName, string NewFileName)
        {
            return (false, Language_.Get语言("无此功能"));
        }

        public (bool s, string m, string[] v) Get目录()
        {
            return (false, Language_.Get语言("无此功能"),new string[0]);
        }

    }

}
