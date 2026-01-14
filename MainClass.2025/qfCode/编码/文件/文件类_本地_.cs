using qfmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace qfCode
{
    internal class 文件类_本地_
    {





        /// <summary>
        /// 文件夹类
        /// </summary>
        Type._文件夹_ _files;
        internal 文件类_本地_(Type._文件夹_ files)
        {
            this._files = files;
            初始化();




        }


        void 初始化()
        {
            new qfmain.文件_文件夹().文件夹_新建(this._files.主文件夹, out string msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._files.参数, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._files.班次, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._files.日期时间, out msgErr);
            new qfmain.文件_文件夹().文件夹_新建(this._files.信息, out msgErr);
             

        }








    }
}
