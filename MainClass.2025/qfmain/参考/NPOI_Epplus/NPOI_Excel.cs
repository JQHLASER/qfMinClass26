using System;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
 
using NPOI.HSSF.UserModel;
using System.Windows.Forms;
using ExcelDataReader;

using System.Collections.Generic;
using NPOI.XSSF.UserModel;

namespace qfmain
{
    public class NPOI_Excel
    {
        /// <summary>
        /// 将excel导入到datatable,默认第一个表
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <returns>返回datatable</returns>
        public DataTable ExcelToDataTable_(string filePath, bool 第一行是否是列名)
        {
            return ExcelToDataTable_(filePath, 第一行是否是列名);
        }



        /// <summary>
        /// 将excel导入到datatable
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <returns>返回datatable</returns>
        public DataTable ExcelToDataTable_(string filePath, bool 第一行是否是列名, int 表索引)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本
                    if (filePath.IndexOf(".xlsx") > 0)
                    {
                        workbook = new XSSFWorkbook(fs);
                    }

                    // 2003版本
                    else if (filePath.IndexOf(".xls") > 0)
                    {

                        workbook = new HSSFWorkbook(fs);
                    }



                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(表索引);//读取第一个sheet，当然也可以循环读取每个sheet
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行
                                int cellCount = firstRow.LastCellNum;//列数

                                //构建datatable的列
                                if (第一行是否是列名)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }


                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                return null;
            }
        }

















        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="表名">Sheet0</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DataTableToExcel_(string filePath, string 表名, DataTable dt, out string msgErr)
        {
            bool result = false;
            msgErr = string.Empty;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet(表名);//创建一个名称为Sheet0的表
                    int rowCount = dt.Rows.Count;//行数
                    int columnCount = dt.Columns.Count;//列数

                    //设置列头
                    row = sheet.CreateRow(0);//excel第一行设为列头
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(filePath))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                msgErr = ex.Message;
                return false;
            }
        }






        public void DataTableToExcel(DataTable dataTable, string 表名, string filePath)
        {
            IWorkbook workbook = new HSSFWorkbook(); // 创建Excel工作簿
            ISheet sheet = workbook.CreateSheet(表名); // 创建工作表

            // 将DataTable的数据写入Excel工作表
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                IRow row = sheet.CreateRow(rowIndex);
                for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                {
                    ICell cell = row.CreateCell(colIndex);
                    cell.SetCellValue(dataTable.Rows[rowIndex][colIndex].ToString());
                }
            }

            // 写入到文件
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(file);
            }

            // 释放资源
            workbook.Close();
        }



        public class info_Excel
        {
            public DataTable datatable { set; get; } = new DataTable();
            public string 表名 { set; get; } = string.Empty;
        }


        public bool DataTableToExcel(string path, info_Excel[] info, bool 第一行为标题, out string msgErr)
        {
            bool rt = true;
            msgErr = string.Empty;
            try
            {
                //创建workbook，说白了就是在内存中创建一个Excel文件
                //IWorkbook workbook = new HSSFWorkbook();//低版本
                IWorkbook workbook = new HSSFWorkbook();//高版本
                                                        // IWorkbook workbook = new SXSSFWorkbook();//高版本

                try
                {

                    foreach (var s in info)
                    {
                        //要添加至少一个sheet,没有sheet的excel是打不开的
                        ISheet sheet = workbook.CreateSheet(s.表名);
                        //ISheet sheet2 = workbook.CreateSheet("sheet2");

                        int rows = s.datatable.Rows.Count;
                        int columns = s.datatable.Columns.Count;
                        int a = 0;

                        if (第一行为标题)
                        {
                            IRow row = sheet.CreateRow(a);
                            if (第一行为标题)
                            {
                                for (int i = 0; i < columns; i++)
                                {
                                    //IRow row1 = sheet.CreateRow(a);//添加第1行,注意行列的索引都是从0开始的
                                    //ICell cell1 = row.CreateCell(i);//给第1行添加第1个单元格
                                    //cell1.SetCellValue(s.datatable.Columns[i].ColumnName.ToString());//给单元格赋值
                                    row.CreateCell(i).SetCellValue(s.datatable.Columns[i].ColumnName.ToString());
                                }
                                a++;
                            }


                            for (int i = 0; i < rows; i++)
                            {
                                row = sheet.CreateRow(a);
                                for (int b = 0; b < columns; b++)
                                {
                                    row.CreateCell(b).SetCellValue(s.datatable.Rows[i][b].ToString());
                                }
                                a++;
                            }


                        }




                        //
                        //
                        //
                        //上边3个步骤合在一起：sheet1.CreateRow(0).CreateCell(0).SetCellValue("hello npoi");

                        //获取第一行第一列的string值
                        //  Console.WriteLine(sheet.GetRow(0).GetCell(0).StringCellValue); //输出：hello npoi




                    }


                    //写入文件
                    using (FileStream file = new FileStream(path, FileMode.Create))
                    {
                        workbook.Write(file);
                    }




                }
                finally
                {
                    workbook.Close();
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }
            return rt;
        }



        //创建Sheet和给单元格添加数据【基础1】测试
        void CreateSheetAndCell_BaseOne(string filePathAndName)
        {
            if (string.IsNullOrEmpty(filePathAndName)) return;

            //创建workbook，说白了就是在内存中创建一个Excel文件
            // IWorkbook workbook = new HSSFWorkbook();//低版本

            IWorkbook workbook = new HSSFWorkbook();//高版本



            //要添加至少一个sheet,没有sheet的excel是打不开的
            ISheet sheet1 = workbook.CreateSheet("sheet1");
            ISheet sheet2 = workbook.CreateSheet("sheet2");

            IRow row1 = sheet1.CreateRow(0);//添加第1行,注意行列的索引都是从0开始的
            ICell cell1 = row1.CreateCell(0);//给第1行添加第1个单元格
            cell1.SetCellValue("hello npoi!");//给单元格赋值
                                              //上边3个步骤合在一起：sheet1.CreateRow(0).CreateCell(0).SetCellValue("hello npoi");

            //获取第一行第一列的string值
            Console.WriteLine(sheet1.GetRow(0).GetCell(0).StringCellValue); //输出：hello npoi

            //写入文件
            using (FileStream file = new FileStream(filePathAndName, FileMode.Create))
            {
                workbook.Write(file);
            }


        }



        /// <summary>
        /// 使用linq加载xlsx
        /// </summary>
        /// <param name="path"></param>
        /// <param name="第一行是否标题"></param>
        /// <returns></returns>       
        //public DataTable ExcelToDatatable(string path, bool 第一行是否标题)
        //{
        //    List<string> list = new List<string>();
        //    list.Add("使用Linq调用");
        //    list.Add("使用NPOI调用");
        //    bool rt = true;
        //    DataTable dt = new DataTable();
        //    foreach (string item in list)
        //    {
        //        if (item == "使用Linq调用")
        //        {
        //            FileStream fStream = File.Open(path, FileMode.Open, FileAccess.Read);
        //            try
        //            {
        //                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream, null);
        //                dt = excelDataReader.AsDataSet(null).Tables[0];
        //                excelDataReader.Close();
        //                excelDataReader.Dispose();
        //            }
        //            catch (Exception)
        //            {
        //                rt = false;
        //            }
        //            fStream.Close();
        //            fStream.Dispose();
        //        }
        //        else if (item == "使用NPOI调用" && !rt)
        //        {
        //            dt = this.ExcelToDataTable_(path, false);
        //        }
        //    }
        //    if (第一行是否标题)
        //    {
        //        int count = dt.Rows.Count;
        //        int columns = dt.Columns.Count;
        //        int i = 0;
        //        while (i < columns && count != 0)
        //        {
        //            string titlenew = dt.Rows[0][i].ToString();
        //            if (!string.IsNullOrEmpty(titlenew))
        //            {
        //                dt.Columns[i].ColumnName = titlenew;
        //            }
        //            i++;
        //        }
        //        if (count > 0)
        //        {
        //            dt.Rows[0].Delete();
        //            dt.AcceptChanges();
        //        }
        //    }
        //    return dt;
        //}




       

 



        #region 未测代码

        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new HSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }




        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public void DataTableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new HSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }







        #endregion



 










    }


}






