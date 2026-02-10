using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class CSV
    {

        /// <summary>
        /// append : =true:追加,=false:覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rows"></param>
        /// <param name="append"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual async Task<(bool state, string msgErr)> Write(string path, List<string[]> rows, bool append, Encoding encoding = null)
        {
            bool rt = true;
            string msgErr = string.Empty;
            if (encoding is null)
            {
                encoding = Encoding.Default;


            }

            char quotechar = ',';

            try
            {

                using (StreamWriter fileWriter = new StreamWriter(path, append, encoding))
                {
                    foreach (string[] cells in rows)
                    {
                        StringBuilder buffer = new StringBuilder();
                        for (int i = 0; i < cells.Length; ++i)
                        {
                            string str = cells[i].Replace("\"", "").Trim();
                            if (str == null)
                                str = "";
                            if (str.Contains(","))
                            {
                                str = "\"" + str + "\"";
                            }
                            buffer.Append(str);
                            if (i != cells.Length - 1)
                                buffer.Append(quotechar);
                        }
                        // fileWriter.WriteLine(buffer.ToString());

                        await fileWriter.WriteLineAsync(buffer.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                rt = false;
                msgErr = ex.Message;
            }


            return (rt, msgErr);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lstCsv"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual bool Read(string path, out List<string[]> lstCsv, out string msgErr, Encoding encoding = null)
        {
            bool rt = true;
            msgErr = string.Empty;
            lstCsv = new List<string[]>();

            if (encoding is null)
            {
                encoding = Encoding.Default;
            }

            try
            {
                using (StreamReader fileReader = new StreamReader(path, encoding))
                {
                    string rowStr = fileReader.ReadLine();
                    // "a,1",b,c     // "\"a,1\",\"b,1,2\",\"c,cc\",ddd"
                    List<string[]> rowList = new List<string[]>();
                    while (rowStr != null)
                    {

                        List<string> cellVals = getStrCellVal(rowStr);

                        string[] cells = new string[cellVals.Count];
                        for (int i = 0; i < cellVals.Count; i++)
                        {
                            cells[i] = cellVals[i];
                        }
                        rowList.Add(cells);
                        rowStr = fileReader.ReadLine();
                    }
                    //fileReader.Close();
                    //fileReader.Dispose();
                    lstCsv = rowList;
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
        /// 读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lstCsv"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public virtual async Task<List<string[]>> Read(string path, Encoding encoding = null)
        {
            bool rt = true;

            List<string[]> lstCsv = new List<string[]>();

            if (encoding is null)
            {
                encoding = Encoding.Default;
            }
            using (StreamReader fileReader = new StreamReader(path, encoding))
            {
                string rowStr = fileReader.ReadLine();
                // "a,1",b,c     // "\"a,1\",\"b,1,2\",\"c,cc\",ddd"
                List<string[]> rowList = new List<string[]>();
                while (rowStr != null)
                {

                    List<string> cellVals = getStrCellVal(rowStr);

                    string[] cells = new string[cellVals.Count];
                    for (int i = 0; i < cellVals.Count; i++)
                    {
                        cells[i] = cellVals[i];
                    }
                    rowList.Add(cells);
                    rowStr = await fileReader.ReadLineAsync();
                }
                //fileReader.Close();
                //fileReader.Dispose();
                lstCsv = rowList;
            }


            return lstCsv;
        }







        private List<string> getStrCellVal(string rowStr)
        {
            List<string> cellList = new List<string>();
            while (rowStr != null && rowStr.Length > 0)
            {
                string cellVal = "";
                if (rowStr.StartsWith("\""))
                {
                    rowStr = rowStr.Substring(1);
                    int i = rowStr.IndexOf("\",");
                    int j = rowStr.IndexOf("\" ,");
                    int k = rowStr.IndexOf("\"");
                    if (i < 0) i = j;
                    if (i < 0) i = k;
                    if (i > -1)
                    {
                        cellVal = rowStr.Substring(0, i);
                        if ((i + 2) < rowStr.Length)
                            rowStr = rowStr.Substring(i + 2).Trim();
                        else
                            rowStr = "";
                    }
                    else
                    {
                        cellVal = rowStr;
                        rowStr = "";
                    }
                }
                else
                {
                    int i = rowStr.IndexOf(",");
                    if (i > -1)
                    {
                        cellVal = rowStr.Substring(0, i);
                        if ((i + 1) < rowStr.Length)
                            rowStr = rowStr.Substring(i + 1).Trim();
                        else
                            rowStr = "";
                    }
                    else
                    {
                        cellVal = rowStr;
                        rowStr = "";
                    }
                }
                if (cellVal == "") cellVal = " ";
                cellList.Add(cellVal);
            }
            return cellList;
        }

        /// <summary>
        ///  append : =true:追加,=false:覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <param name="标题"></param>
        /// <param name="rows"></param>
        /// <param name="append"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public virtual async Task<(bool state, string msgErr)> Write(string path, string[] 标题, string[][] rows, bool append, Encoding encoding = null)
        {
            List<string> lstStr = new List<string>();
            List<string[]> lstCsv = new List<string[]>();
            if (!new 文件_文件夹().文件_是否存在(path))
            {
                lstStr = 标题.ToList();
                lstCsv.Add(lstStr.ToArray());
            }
            foreach (var s in rows)
            {
                lstCsv.Add(s);
            }
            return await Write(path, lstCsv, append, encoding);
        }


        /// <summary>
        ///  append : =true:追加,=false:覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <param name="标题"></param>
        /// <param name="row"></param>
        /// <param name="append"></param>
        /// <param name="msgErr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public virtual async Task<(bool state, string msgErr)> Write(string path, string[] 标题, string[] row, bool append, Encoding encoding = null)
        {
            List<string[]> lstCsv = new List<string[]>();
            lstCsv.Add(row);
            return await Write(path, 标题, lstCsv.ToArray(), append, encoding);
        }


        /// <summary>
        ///  获取表头
        /// </summary> 
        public static string[] Get_CsvHeader<T>()
        {
            var props = typeof(T).GetProperties();
            return props.Select(p => new class类_属性显示名工具().Get_DisplayName(p)).ToArray ();
        }



    }


}
