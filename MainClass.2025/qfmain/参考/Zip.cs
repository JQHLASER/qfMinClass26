using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace qfmain
{
    /// <summary>
    /// 需要到NuGet中下载ICSharpCode.SharpZipLib
    /// </summary>
    public class Zip
    {



        ///// <summary>
        ///// 目标文件后缀为.zip
        ///// </summary>
        ///// <param name="源文件path"></param>
        ///// <param name="目标文件path"></param>
        //public void 压缩1(string 源文件path, string 目标文件path)
        //{

        //    // 创建压缩文件
        //    System.IO.Compression.ZipFile.CreateFromDirectory(源文件path, 目标文件path);
        //}


        ///// <summary>
        ///// 压缩文件夹
        ///// </summary>
        ///// <param name="folderToZip">需要压缩的文件夹</param>
        ///// <param name="zipedFileName">压缩后的Zip完整文件名</param>
        //public void 压缩(string folderToZip, string zipedFileName)
        //{
        //    // 调用重载方法，使用默认参数
        //    压缩文件夹(folderToZip, zipedFileName, string.Empty, true, string.Empty, string.Empty, true);
        //}



        ///// <summary>
        ///// 压缩文件夹8
        ///// </summary>
        ///// <param name="folderToZip">需要压缩的文件夹</param>
        ///// <param name="zipedFileName">压缩后的Zip完整文件名（如D:\test.zip）</param>
        ///// <param name="isRecurse">如果文件夹下有子文件夹，是否递归压缩</param>
        ///// <param name="password">解压时需要提供的密码</param>
        ///// <param name="fileRegexFilter">文件过滤正则表达式</param>
        ///// <param name="directoryRegexFilter">文件夹过滤正则表达式</param>
        ///// <param name="isCreateEmptyDirectories">是否压缩文件中的空文件夹</param>
        //public   void 压缩文件夹(string folderToZip, string zipedFileName, string password, bool isRecurse, string fileRegexFilter, string directoryRegexFilter, bool isCreateEmptyDirectories)
        //{
        //    // 创建 FastZip 对象
        //    FastZip fastZip = new FastZip();
        //    // 设置是否压缩文件中的空文件夹
        //    fastZip.CreateEmptyDirectories = isCreateEmptyDirectories;
        //    // 设置解压时需要提供的密码
        //    fastZip.Password = password;
        //    // 创建 Zip 文件
        //    fastZip.CreateZip(zipedFileName, folderToZip, isRecurse, fileRegexFilter, directoryRegexFilter);
        //}




        ///// <summary>  
        ///// 功能：解压zip格式的文件。  
        ///// </summary>  
        ///// <param name="zipFilePath">压缩文件路径</param>  
        ///// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>  
        ///// <returns>解压是否成功</returns>  
        //public void 解压(string zipFilePath, string unZipDir)
        //{
        //    //判断压缩文件路径是否为空
        //    if (zipFilePath == string.Empty)
        //    {
        //       // 如果为空，则抛出异常
        //throw new Exception("压缩文件不能为空！");
        //    }

        //    //判断压缩文件是否存在
        //    if (!File.Exists(zipFilePath))
        //    {
        //        throw new FileNotFoundException("压缩文件不存在！");
        //    }
        //    //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹  
        //    if (unZipDir == string.Empty) unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));

        //    //如果解压文件夹路径不以“/”结尾，则添加“/”
        //    if (!unZipDir.EndsWith("/")) unZipDir += "/";

        //    //如果解压文件夹不存在，则创建该文件夹
        //    if (!Directory.Exists(unZipDir)) Directory.CreateDirectory(unZipDir);

        //    //用于读取zip文件中的数据
        //    using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
        //    {
        //        ZipEntry theEntry;
        //        //循环读取zip文件中的每一个文件
        //        while ((theEntry = s.GetNextEntry()) != null)
        //        {
        //            //获取文件所在的文件夹路径
        //            string directoryName = Path.GetDirectoryName(theEntry.Name);
        //            //获取文件名
        //            string fileName = Path.GetFileName(theEntry.Name);

        //            //获取文件名
        //            if (!string.IsNullOrEmpty(directoryName))
        //            {
        //                //创建该文件夹
        //                Directory.CreateDirectory(unZipDir + directoryName);
        //            }

        //            //如果文件所在的文件夹路径不为空且不以“/”结尾
        //            if (directoryName != null && !directoryName.EndsWith("/"))
        //            { }

        //            //如果文件名不为空
        //            if (fileName != String.Empty)
        //            {
        //                //创建文件流
        //                using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
        //                {
        //                    int size;
        //                    byte[] data = new byte[2048];
        //                    //循环读取文件流中的数据
        //                    while (true)
        //                    {
        //                        size = s.Read(data, 0, data.Length);
        //                        if (size > 0)
        //                        {
        //                            //将数据写入文件流
        //                            streamWriter.Write(data, 0, size);
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}





    }
}
