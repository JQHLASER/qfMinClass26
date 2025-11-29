 
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace qfmain
{

    /// <summary>
    /// 安装 NPOI
    /// </summary>
    public class NPOI_xls
    {

        public bool 打开xls文件(string filePath, out HSSFWorkbook workbook, out string msgErr, FileAccess fileaccess = FileAccess.Read)
        {
            bool rt = true;
            msgErr = string.Empty;
            workbook = null;
            try
            {
                // 打开 .xls 文件
                using (var fileStream = new FileStream(filePath, FileMode.Open, fileaccess))
                {
                    workbook = new HSSFWorkbook(fileStream);  // 读取 .xls 文件
                    //ISheet sheet = workbook.GetSheetAt(表索引); // 获取工作表

                    //// 获取总行数和列数
                    //int rowCount = sheet.PhysicalNumberOfRows;
                    //int colCount = sheet.GetRow(0).PhysicalNumberOfCells;
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;


        }

        public bool 获取工作表(HSSFWorkbook workbook, int 表索引, out ISheet sheet, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            sheet = null;
            try
            {
                sheet = workbook.GetSheetAt(表索引); // 获取工作表
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 获取总行数(ISheet sheet, out int rowCount, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            rowCount = 0;
            try
            {
                rowCount = sheet.PhysicalNumberOfRows;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 有时取出来不准,则可以直接指定列数,
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="colCount"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public bool 获取总列数(ISheet sheet, out int colCount, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            colCount = 0;
            try
            {
                colCount = sheet.GetRow(0).PhysicalNumberOfCells;
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }

        public bool 保存工作薄(string filePath, HSSFWorkbook workbook, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //保存修改后的工作簿
                using (var saveStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) { workbook.Write(saveStream); }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }

            return rt;
        }

        public bool 修改单元格内容(ISheet sheet, int 行索引, int 列索引, dynamic value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {

                IRow row = sheet.GetRow(行索引);
                if (row == null)
                {
                    row = sheet.CreateRow(行索引);
                }



                ICell cell = row.GetCell(列索引) ?? row.CreateCell(列索引);  // 获取或创建单元格

                cell.SetCellValue(value);//设置单元格数据
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;

            }

            return rt;
        }


        public bool 修改单元格内容(string path, int 表索引, int 行索引, int 列索引, dynamic value, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            msgErr = string.Empty;

            List<string> lstWork = new List<string>();
            lstWork.Add("读xls");
            lstWork.Add("获取表");
            lstWork.Add("修改数据");
            lstWork.Add("保存");
            HSSFWorkbook workbook = null;
            ISheet sheet = null;


            foreach (string s in lstWork)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "读xls")
                {
                    rt = 打开xls文件(path, out workbook, out msgErr, FileAccess.ReadWrite);

                }
                else if (s == "获取表")
                {
                    rt = 获取工作表(workbook, 表索引, out sheet, out msgErr);
                }

                else if (s == "修改数据")
                {
                    rt = 修改单元格内容(sheet, 行索引, 列索引, value, out msgErr);
                }
                else if (s == "保存")
                {
                    rt = 保存工作薄(path, workbook, out msgErr);
                }

            }

            return rt;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="总行数"></param>
        /// <param name="总列数"></param>
        /// <param name="listData"></param>
        /// <param name="msgErr"></param>
        /// <param name="开始行">从0开始</param>
        /// <returns></returns>
        public bool 获取数据(ISheet sheet, int 总行数, int 总列数, out List<string[]> listData, out string msgErr, int 开始行 = 0)
        {
            bool rt = true;
            msgErr = string.Empty;
            listData = new List<string[]>();
            try
            {

                // 从第开始行开始读取数据
                for (int row = 开始行; row < 总行数; row++) // 从第12行开始 (0-based index, 所以是11)
                {
                    IRow dataRow = sheet.GetRow(row);
                    if (dataRow == null) continue; // 如果行为空，跳过

                    string[] rowData = new string[总列数];

                    for (int col = 0; col < 总列数; col++)
                    {
                        var cellValue = dataRow.GetCell(col)?.ToString();
                        rowData[col] = cellValue ?? string.Empty; // 如果单元格为空，设置为字符串空值
                    }

                    listData.Add(rowData); // 将每一行数据添加到列表
                }
            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 读xls
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string[]> ReadXlsFile(string filePath, int 表索引)
        {
            List<string[]> dataList = new List<string[]>();

            // 打开 .xls 文件
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(fileStream);  // 读取 .xls 文件
                ISheet sheet = workbook.GetSheetAt(表索引); // 获取第一个工作表

                // 获取总行数和列数
                int rowCount = sheet.PhysicalNumberOfRows;
                int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                // 从第11行开始读取数据
                for (int row = 11; row < rowCount; row++) // 从第12行开始 (0-based index, 所以是11)
                {
                    IRow dataRow = sheet.GetRow(row);
                    if (dataRow == null) continue; // 如果行为空，跳过

                    string[] rowData = new string[colCount];

                    for (int col = 0; col < colCount; col++)
                    {
                        var cellValue = dataRow.GetCell(col)?.ToString();
                        rowData[col] = cellValue ?? string.Empty; // 如果单元格为空，设置为字符串空值
                    }

                    dataList.Add(rowData); // 将每一行数据添加到列表
                }
            }

            return dataList;
        }

        /// <summary>
        /// 定制
        /// </summary>
        /// <returns></returns>
        public bool 读xls(string path, int 表索引, int 开始行, int 总列数, out List<string[]> lst, out string msgErr)
        {
            msgErr = string.Empty;
            bool rt = true;
            lst = new List<string[]>();

            List<string> lstWork = new List<string>();
            lstWork.Add("读xls");
            lstWork.Add("获取表");
            lstWork.Add("获取总行数");
            lstWork.Add("获取总列数");
            lstWork.Add("获取数据");

            HSSFWorkbook workbook = null;
            ISheet sheet = null;
            int rowCount = 0;
            int colCount = 0;

            foreach (string s in lstWork)
            {
                if (!rt)
                {
                    break;
                }
                else if (s == "读xls")
                {
                    rt = 打开xls文件(path, out workbook, out msgErr);

                }
                else if (s == "获取表")
                {
                    rt = 获取工作表(workbook, 表索引, out sheet, out msgErr);
                }
                else if (s == "获取总行数")
                {
                    rt = 获取总行数(sheet, out rowCount, out msgErr);
                }
                else if (s == "获取总列数")
                {
                    //    rt = 获取总列数(sheet, out colCount, out msgErr);
                    colCount = 总列数;
                }
                else if (s == "获取数据")
                {
                    rt = 获取数据(sheet, rowCount, colCount, out lst, out msgErr, 开始行);
                }
            }

            return rt;

        }





    }
}
