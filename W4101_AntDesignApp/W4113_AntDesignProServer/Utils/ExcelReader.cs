using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;


using NPOI.SS.UserModel;

using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;


namespace W4113_AntDesignProServer.Utils
{
    public class ExcelReader
    {

        /// <summary>
        /// 获取Excel文件的所有sheet基本信息.
        /// <br/>
        /// 认为首行是标题行.
        /// </summary>
        /// <param name="excelFileName"></param>
        /// <returns></returns>
        public static List<ExcelSheetInfo> GetSheetInfo(string excelFileName)
        {
            List<ExcelSheetInfo> resultList = new List<ExcelSheetInfo>();


            using (FileStream fs = File.OpenRead(excelFileName))
            {
                IWorkbook? workbook = null;

                if (excelFileName.IndexOf(".xlsx") > 0)
                {
                    // 2007版本
                    workbook = new XSSFWorkbook(fs);
                }
                else if (excelFileName.IndexOf(".xls") > 0)
                {
                    // 2003版本
                    workbook = new HSSFWorkbook(fs);
                }

                if (workbook != null)
                {
                    // 遍历每一个 Sheet.
                    for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                    {
                        // 获取 Sheet.
                        var sheet = workbook.GetSheetAt(sheetIndex);
                        if (sheet != null)
                        {
                            var sheetInfo = new ExcelSheetInfo()
                            {
                                Name = sheet.SheetName,
                                RowCount = sheet.LastRowNum,
                                Columns = new List<string>()
                            };

                            // 第一行.
                            IRow firstRow = sheet.GetRow(0);
                            // 列数.
                            int cellCount = firstRow.LastCellNum;

                            // 遍历列.    
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    // 认为是自己有标题的.
                                    // 需要读取这一行的数据.
                                    if (cell.StringCellValue != null)
                                    {
                                        sheetInfo.Columns.Add(cell.StringCellValue);
                                    }
                                }
                            }

                            resultList.Add(sheetInfo);
                        }
                    }
                }
            }


            return resultList;
        }





        /// <summary>
        /// 获取Excel文件指定sheet的数据.
        /// </summary>
        /// <param name="excelFileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="startRow">起始行，默认值为 0.</param>
        /// <param name="hasTitle">是否首行是标题行，默认值为 true</param>
        /// <returns></returns>
        public static IEnumerable<Dictionary<string, object>> GetSheetData(string excelFileName, string sheetName, int startRow = 0, bool hasTitle = true)
        {

            // 标题列表.
            List<string> titleList = new List<string>();


            using (FileStream fs = File.OpenRead(excelFileName))
            {
                IWorkbook? workbook = null;

                if (excelFileName.IndexOf(".xlsx") > 0)
                {
                    // 2007版本
                    workbook = new XSSFWorkbook(fs);
                }
                else if (excelFileName.IndexOf(".xls") > 0)
                {
                    // 2003版本
                    workbook = new HSSFWorkbook(fs);
                }

                if (workbook != null)
                {

                    // 获取 Sheet.
                    var sheet = workbook.GetSheet(sheetName);
                    if (sheet != null)
                    {
                        // 第一行.
                        IRow firstRow = sheet.GetRow(startRow);
                        // 列数.
                        int cellCount = firstRow.LastCellNum;

                        // 遍历列.    
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            var cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                if (hasTitle)
                                {
                                    // 自己有标题的.
                                    // 需要读取这一行的数据.
                                    if (cell.StringCellValue != null)
                                    {
                                        titleList.Add(cell.StringCellValue);
                                    }
                                }
                                else
                                {
                                    // 自己没有标题的.
                                    // 直接用列号.
                                    string address = cell.Address.ToString();
                                    string rowNum = (startRow + 1).ToString();
                                    string columnName = address.Replace(rowNum, "");

                                    titleList.Add(columnName);
                                }
                            }
                        }

                        // 数据的起始行.
                        int dataStartRow = startRow;
                        if (hasTitle)
                        {
                            // 有标题，则数据从第二行开始.
                            dataStartRow = startRow + 1;
                        }

                        // 总行数.
                        int rowCount = sheet.LastRowNum;


                        for (int i = dataStartRow; i <= rowCount; ++i)
                        {
                            var row = sheet.GetRow(i);
                            if (row == null) continue;

                            // 当前行的数据.
                            Dictionary<string, object> rowData = new Dictionary<string, object>();

                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                var cell = row.GetCell(j);
                                if (cell == null)
                                {
                                    rowData.Add(titleList[j], "");
                                }
                                else
                                {
                                    //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                    switch (cell.CellType)
                                    {
                                        case CellType.Blank:
                                            rowData.Add(titleList[j], "");
                                            break;
                                        case CellType.Numeric:
                                            short format = cell.CellStyle.DataFormat;
                                            //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                            if (format == 14 || format == 31 || format == 57 || format == 58)
                                                rowData.Add(titleList[j], cell.DateCellValue);
                                            else
                                                rowData.Add(titleList[j], cell.NumericCellValue);
                                            break;
                                        case CellType.String:
                                            rowData.Add(titleList[j], cell.StringCellValue);
                                            break;
                                    }
                                }
                            }

                            yield return rowData;
                        }

                    }

                }
            }
        }



    }



    public class ExcelSheetInfo
    {

        /// <summary>
        /// 表格名称.
        /// </summary>
        public string? Name { get; set; }


        /// <summary>
        /// 列名.
        /// </summary>
        public List<string> Columns { get; set; } = new List<string>();


        /// <summary>
        /// 行数.
        /// </summary>
        public int RowCount { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"表格名称：{Name}");
            sb.Append("    列名：");
            sb.AppendLine(string.Join(",", Columns));
            sb.AppendLine($"    行数：{RowCount}");
            return sb.ToString();
        }
    }

}
