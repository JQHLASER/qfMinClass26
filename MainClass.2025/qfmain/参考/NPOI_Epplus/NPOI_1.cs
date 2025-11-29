using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace qfmain
{
    public class NPOI_1
    {

        /// <summary>
        /// .xlsx
        /// </summary>
        /// <param name="path"></param>
        public FileStream Open(string path)
        {
            //把文件内容导入到工作薄当中，然后关闭文件
            FileStream fs = File.OpenRead(path);
            return fs;

        }
        public void Close(FileStream fs)
        {
            fs.Close();
        }

        /// <summary>
        /// .xlsx
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fx"></param>
        /// <param name="path"></param>
        public void 保存(IWorkbook workbook, string path)
        {
            FileStream fs2 = File.Create(path);
            workbook.Write(fs2);
            fs2.Close();
        }

        public IWorkbook workBook(FileStream fs)
        {
            //  IWorkbook workbook = new XSSFWorkbook(fs);
            IWorkbook workbook = new XSSFWorkbook(fs);



            return workbook;
        }

        /// <summary>
        /// 获取一个表
        /// </summary>
        /// <param name="索引"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public ISheet 读取Sheet(int 索引, IWorkbook workbook)
        {
            ISheet sheet = workbook.GetSheetAt(索引);
            return sheet;
        }

        public int 获取行数(ISheet sheet)
        {
            return sheet.LastRowNum;
        }

        public int 获取列数(ISheet sheet)
        {
            return sheet.GetRow(0).LastCellNum;
        }

        /// <summary>
        /// 流程,open...close....workBook...读取sheet...设置指定单元格数据.....保存;
        /// </summary>
        /// <param name="行索引"></param>
        /// <param name="列索引"></param>
        /// <param name="值"></param>
        /// <param name="sheet"></param>
        public void 设置指定单元格的数据(int 行索引, int 列索引, string 值, ISheet sheet)
        {
            /*
               * Excel数据Cell有不同的类型，当我们试图从一个数字类型的Cell读取出一个字符串并写入数据库时，就会出现Cannot get a text value from a numeric cell的异常错误。
                     * 解决办法：先设置Cell的类型，然后就可以把纯数字作为String类型读进来了
             */
            sheet.GetRow(行索引).GetCell(列索引).SetCellType(CellType.String);
            sheet.GetRow(行索引).GetCell(列索引).SetCellValue(值);
        }




        public void 定制_设置指定单元格数据(string path, int 表索引, int 行索引, int 列索引, string 值)
        {
            FileStream fs = Open(path);
            IWorkbook iworkbook = workBook(fs);
            Close(fs);
            ISheet sheet = 读取Sheet(表索引, iworkbook);
            //设置指定单元格的数据(行索引, 列索引, 值, sheet);
            //保存(iworkbook, path);





        }






















    }
}