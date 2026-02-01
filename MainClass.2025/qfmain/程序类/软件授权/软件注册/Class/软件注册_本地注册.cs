using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace qfmain
{
    internal class 软件注册_本地注册
    {

        internal static string 客户ID_默认值 { get; } = "331FA1";


        /// <summary>
        /// 设备Sn,本地,加密狗时无效
        /// </summary>
        /// <returns></returns>
        static string Get_设备Sn()
        {
            string Files = new 电脑().获取_系统所在盘符() + "\\windowsoft";
            new 文件_文件夹().文件夹_新建(Files, out string msgErr);
            string path = Files + "\\System32f.dll";

            string sn = new 加解密_注册码算法("").加密($"{DateTime.Now.ToString("yyyyMMddHHmmss")}");
            sn = 软件注册_子程序.加密(sn);
            new 文件_文件夹().WriteReadText(path, 1, ref sn, out string smgErr, null, true);
            sn = 软件注册_子程序.解密(sn);
            return sn;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>反回机器码 设备Sn-客户Id</returns>
        internal static string Get机器码(out _软件授权_机器码信息_ info)
        {
            string[] Bf_Work = new string[]
            {
                "Get设备SN",
                "Get客户ID"
            };
            info = new _软件授权_机器码信息_();
            foreach (var s in Bf_Work)
            {
                if (s == "Get设备SN")
                {
                    info.Sn = Get_设备Sn();
                }
                else if (s == "Get客户ID")
                {
                    string id = 客户ID_默认值;
                    客户Id(1, ref id);
                    info.Uid = id;
                }
            }
            return 软件注册_子程序.Get机器码(info);

        }


        /// <summary>
        /// 读写客户ID,本地,加密狗时无效
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        internal static void 客户Id(ushort model, ref string Id)
        {
            string path = Path .Combine (AppDomain .CurrentDomain .BaseDirectory , "Guserf.dll");
            string sn = 软件注册_子程序.加密(Id);
            new 文件_文件夹().WriteReadText(path, model, ref sn, out string smgErr, null, true);
            Id = 软件注册_子程序.解密(sn);           
        }


        /// <summary>
        /// 读写注册码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        internal static void 注册码读写(ushort model, ref string 注册码)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gzcmf.dll");
            string sn = 软件注册_子程序.加密(注册码);
            new 文件_文件夹().WriteReadText(path, model, ref sn, out string smgErr, null, true);
            string vxt = 软件注册_子程序.解密(sn);
            注册码 = vxt;
        }


    }
}
