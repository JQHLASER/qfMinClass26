using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /*
     使用Epplus方式来操作Excel    
     */
    /// <summary>
    /// 安装 EPPlus
    /// </summary>
    public class xls_EPPlus
    {
        public void 初始化()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  //申明非商业用途不然用不了 

        }
        public void 加载工作薄(string path, out ExcelPackage package)
        {
            package = new ExcelPackage(path);//加载Excel工作簿
        }
        public void 加载工作表(ExcelPackage package, int 表索引, out ExcelWorksheet sheet1)
        {
            sheet1 = package.Workbook.Worksheets[表索引];//读取工作表
        }
        public void 加载工作表(ExcelPackage package, string 表名, out ExcelWorksheet sheet1)
        {
            sheet1 = package.Workbook.Worksheets[表名];//读取工作表
        }
        public void 保存(ExcelPackage package)
        {
            package.Save();//将更改保存到原文件             
        }
        public void 获取有效行数(ExcelWorksheet sheet1, out int rowCount)
        {
            rowCount = sheet1.Dimension.Rows;
        }
        public void 获取有效列数(ExcelWorksheet sheet1, out int colCount)
        {
            colCount = sheet1.Dimension.Columns;
        }
        public void 更新_单元格值(ExcelWorksheet sheet1, int 行索引, int 列索引, object 值)
        {
            sheet1.Cells[行索引, 列索引].Value = 值;
        }
        public void 获取_单元格值(ExcelWorksheet sheet1, string 单元格坐标, out object 值)
        {
            值 = sheet1.Cells[单元格坐标].Value;
        }

        public void 获取_单元格值(ExcelWorksheet sheet1, int 行索引, int 列索引,out  object 值)
        {
            值= sheet1.Cells[行索引, 列索引].Value ;
        }



        public void 添加_单元格(ExcelWorksheet sheet1,int 行索引,int 列索引,object 值)
        {
            sheet1.Cells[行索引, 列索引].Value = 值;
        }

        public void 删除_行(ExcelWorksheet sheet1, int 行索引 )
        { 
            sheet1.DeleteRow(行索引);
        }
        public void 删除_行(ExcelWorksheet sheet1, int 行索引, int 列索引 )
        {       
            sheet1.DeleteRow(行索引, 列索引);
        }

        public void 删除_列(ExcelWorksheet sheet1, int 列索引 )
        {
            sheet1.DeleteColumn(列索引);
        }
        public void 删除_列 (ExcelWorksheet sheet1, int 行索引, int 列索引 )
        {
            sheet1.DeleteColumn(行索引,列索引);
        }


        #region 例



        public string path = "SAP_production_order_data.xlsx";
        public void Update()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  //表明非商业用途不然用不了       

            ExcelPackage package = new ExcelPackage(path);//加载Excel工作簿
            ExcelWorksheet sheet1 = package.Workbook.Worksheets[0];//读取第一个工作表
            sheet1.Cells["D2"].Value = sheet1.Cells["D2"].Value + "1";
            package.Save();//将更改保存到原文件

            int rowCount = sheet1.Dimension.Rows;
            int colCount = sheet1.Dimension.Columns;
           

        }
        public void AddData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;//表明非商业用途不然用不了


            ExcelPackage package = new ExcelPackage(path);//加载Excel工作簿
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];//读取第一个工作表



            // 在第一行和第一列中添加数据
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Age";

            // 添加几行数据
            worksheet.Cells[2, 1].Value = "Alice";
            worksheet.Cells[2, 2].Value = 30;

            worksheet.Cells[3, 1].Value = "Bob";
            worksheet.Cells[3, 2].Value = 25;
            // 保存 Excel 文件
            package.Save();
        }

        public void DeleteRow()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;//表明非商业用途不然用不了


            ExcelPackage package = new ExcelPackage(path);//加载Excel工作簿
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];//读取第一个工作表
            // 删除第二行
            worksheet.DeleteRow(2);
            // 保存 Excel 文件
            package.Save();
        }






        #endregion


    }
}
