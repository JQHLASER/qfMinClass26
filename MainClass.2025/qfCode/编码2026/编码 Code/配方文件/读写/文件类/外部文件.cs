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
            this._codeSys.On_初始化状态(qfmain._初始化状态_.已初始化 ,"");
        }

        /// <summary>
        ///  FileName : 无用,赋空值即可
        /// </summary> 
        public (bool s, string m) Save(string FileName, _配方文件_属性_ cfg)
        {
            return this._codeSys.On_保存(FileName,cfg);
        }

        public (bool s, string m, _配方文件_属性_ cfg) Read(string FileName)
        {
            return (true , Language_.Get语言("无此功能"), new _配方文件_属性_());
        }

        /// <summary>
        /// 导出全部时用
        /// </summary> 
        public (bool s, string m, qfNet.表.Code26[] cfg) ReadAll()
        {
            return (true, "无此功能", new 表.Code26[0]);
        }

        /// <summary>
        /// 导出全部时用
        /// </summary> 
        public (bool s, string m) SaveAll(qfNet.表.Code26[] cfg)
        {
            return (true, "无此功能");
        }

        public (bool s, string m) Delete(string FileName)
        {
            return (true , Language_.Get语言("无此功能") );
        }


        public (bool s, string m) 复制(string FileName, string NewFileName)
        {
            return (true , Language_.Get语言("无此功能"));
        }

        public (bool s, string m, string[] v) Get目录()
        {
            return (true , Language_.Get语言("无此功能"),new string[0]);
        }

    }

}
