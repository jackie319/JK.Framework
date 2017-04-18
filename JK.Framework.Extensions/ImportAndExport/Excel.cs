using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace JK.Framework.Extensions
{

    /// <summary>
    /// 第一种方法：传统方法，采用OleDB读取EXCEL文件,
    ///优点：写法简单，缺点：服务器必须安有此组件才能用，不推荐使用
    /// 第二种方法：用第三方组件：NPOI组件,推荐使用此方法
    ///  NPOI 是 POI 项目的 .NET 版本。POI是一个开源的Java读写Excel、WORD等微软OLE2组件文档的项目。
    /// 使用 NPOI 你就可以在没有安装 Office 或者相应环境的机器上对 WORD/EXCEL 文档进行读写。
    /// </summary>
    public static class Excel

    {
        /// <summary>
        /// 转化为Excel内存流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>

        public static MemoryStream ToExcel(DataTable table)

        {
            var ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                // handling header.

                foreach (DataColumn column in table.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                    //If Caption not set, returns the ColumnName value

                }

                // handling value.

                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    rowIndex++;
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }


        /// <summary>
        /// 转化为Excel内存流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>

        public static MemoryStream ToExcel<T>(IList<T> list)

        {
            var ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            //创建表头

            var coloumIndex = 0;//当前表的列

            foreach (var memberInfo in typeof(T).GetProperties())

            {
                var columnName = memberInfo.Name;
                var attrs = memberInfo.GetCustomAttributes(false);   //反射成员的所有特性
                if (attrs.Any(a => a is DisplayAttribute)) //<=== 是否应改为 DisplayNameAttribute?
                {
                    columnName = ((DisplayNameAttribute)attrs.Single(a => a is DisplayNameAttribute)).DisplayName;

                } //不显示的就跳过
                headerRow.CreateCell(coloumIndex).SetCellValue(columnName);
                coloumIndex++;
            }

            //创建数据

            for (var rownum = 0; rownum < list.Count; rownum++)
            {
                IRow dataRow = sheet.CreateRow(rownum + 1);
                var row = list[rownum];
                coloumIndex = 0;
                for (var index = 0; index < row.GetType().GetProperties().Length; index++)
                {
                    var propertyInfo = row.GetType().GetProperties()[index];
                    var attrs = propertyInfo.GetCustomAttributes(false);    //反射成员的所有特性
                    if (attrs.Any(a => a is DisplayAttribute && !((DisplayAttribute)a).AutoGenerateField))
                    {
                        continue;//不显示的就跳过
                    }
                    var p = row.GetType().GetProperties()[index];           //将模型中的对应列取出
                    dataRow.CreateCell(coloumIndex).SetCellValue(p.GetValue(row, null).ToString());//If Caption not set, returns the ColumnName value
                    coloumIndex++;
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// 将List转化为Excel内容流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表头</param>
        /// <param name="list">列表</param>
        /// <returns></returns>

        public static MemoryStream ToExcel<T>(string tableName, IList<T> list)

        {
            var ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(1);
            //创建表头

            var coloumIndex = 0;//当前表的列

            foreach (var propertyInfo in typeof(T).GetProperties())

            {
                var attrs = propertyInfo.GetCustomAttributes(false);   //反射成员的所有特性
                if (attrs.Any(a => a is DisplayAttribute && !((DisplayAttribute)a).AutoGenerateField))
                {
                    continue; //不显示的就跳过
                }
                var dispalyName = (DisplayNameAttribute)attrs.Single(a => a is DisplayNameAttribute);
                headerRow.CreateCell(coloumIndex).SetCellValue(dispalyName.DisplayName);
                coloumIndex++;
            }

            //设置Title

            IRow titleRow = sheet.CreateRow(0);

            ICell cell = titleRow.CreateCell(0);
            IFont font = workbook.CreateFont();
            cell.SetCellValue(tableName);                   //设置单元格值
            cell.CellStyle.Alignment = (NPOI.SS.UserModel.HorizontalAlignment)HorizontalAlignment.Center;//垂直居中
            var cellRangeAddress = new CellRangeAddress(0, 0, 0, coloumIndex - 1);
            sheet.AddMergedRegion(cellRangeAddress);

            //创建数据

            for (var rownum = 0; rownum < list.Count; rownum++)

            {
                IRow dataRow = sheet.CreateRow(rownum + 2);
                var row = list[rownum];
                coloumIndex = 0;
                for (var index = 0; index < row.GetType().GetProperties().Length; index++)
                {
                    var propertyInfo = row.GetType().GetProperties()[index];
                    var attrs = propertyInfo.GetCustomAttributes(false);    //反射成员的所有特性
                    if (attrs.Any(a => a is DisplayAttribute && !((DisplayAttribute)a).AutoGenerateField))
                    {
                        continue;//不显示的就跳过
                    }
                    var p = row.GetType().GetProperties()[index];           //将模型中的对应列取出
                    var cellValue = p.GetValue(row, null);
                    if (cellValue == null)
                    {
                        dataRow.CreateCell(coloumIndex).SetCellValue("");
                    }
                    else
                    {
                        dataRow.CreateCell(coloumIndex).SetCellValue(cellValue.ToString());
                    }
                    //If Caption not set, returns the ColumnName value

                    coloumIndex++;

                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// 将List转化为Excel内容流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表头</param>
        /// <param name="audit">带审核人</param>
        /// <param name="list">列表</param>
        /// <returns></returns>
        public static MemoryStream ToExcel<T>(string tableName, bool audit, IList<T> list)

        {
            var ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(1);
            //创建表头

            var coloumIndex = 0;//当前表的列

            foreach (var propertyInfo in typeof(T).GetProperties())

            {
                var attrs = propertyInfo.GetCustomAttributes(false);   //反射成员的所有特性
                if (attrs.Any(a => a is DisplayAttribute && !((DisplayAttribute)a).AutoGenerateField))
                {
                    continue; //不显示的就跳过
                }
                var dispalyName = (DisplayNameAttribute)attrs.Single(a => a is DisplayNameAttribute);
                headerRow.CreateCell(coloumIndex).SetCellValue(dispalyName.DisplayName);
                coloumIndex++;
            }

            //设置Title

            IRow titleRow = sheet.CreateRow(0);

            ICell cell = titleRow.CreateCell(0);
            IFont font = workbook.CreateFont();
            cell.SetCellValue(tableName);                   //设置单元格值
            cell.CellStyle.Alignment = (NPOI.SS.UserModel.HorizontalAlignment)HorizontalAlignment.Center;//垂直居中
            var cellRangeAddress = new CellRangeAddress(0, 0, 0, coloumIndex - 1);
            sheet.AddMergedRegion(cellRangeAddress);

            //创建数据

            for (var rownum = 0; rownum < list.Count; rownum++)

            {
                IRow dataRow = sheet.CreateRow(rownum + 2);
                var row = list[rownum];
                coloumIndex = 0;
                for (var index = 0; index < row.GetType().GetProperties().Length; index++)
                {
                    var propertyInfo = row.GetType().GetProperties()[index];
                    var attrs = propertyInfo.GetCustomAttributes(false);    //反射成员的所有特性
                    if (attrs.Any(a => a is DisplayAttribute && !((DisplayAttribute)a).AutoGenerateField))
                    {
                        continue;//不显示的就跳过
                    }
                    var p = row.GetType().GetProperties()[index];           //将模型中的对应列取出
                    dataRow.CreateCell(coloumIndex).SetCellValue(Convert.ToString(p.GetValue(row, null)));//If Caption not set, returns the ColumnName value
                    coloumIndex++;
                }
            }

            //创建审核人操作员

            IRow auditRow = sheet.CreateRow(list.Count + 2);

            auditRow.CreateCell(0).SetCellValue("制表：");
            auditRow.CreateCell(2).SetCellValue("复核：");
            IRow auditRow2 = sheet.CreateRow(list.Count + 3);
            auditRow2.CreateCell(0).SetCellValue("部门负责人：");
            auditRow2.CreateCell(2).SetCellValue("分管领导：");

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }

        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <param name="sheetNumber">第几个工作表单第一个为0</param>
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName, int sheetNumber)
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
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(sheetNumber);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
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
                                        column = new DataColumn("column" + (i + 1));
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
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }

    }
}
