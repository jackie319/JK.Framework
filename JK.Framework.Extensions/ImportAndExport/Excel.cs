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
        /// 转化为Excel内存流。TODO：有bug.浏览器下载后提升格式不匹配并且乱码
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
                if (attrs.Any(a => a is DisplayNameAttribute)) 
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
        /// 保存excel 在本地 .在浏览器中输出MemoryStream 有bug。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void SaveToExcel<T>(IList<T> list, string filePath)

        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            //创建表头

            var coloumIndex = 0;//当前表的列

            foreach (var memberInfo in typeof(T).GetProperties())

            {
                var columnName = memberInfo.Name;
                var attrs = memberInfo.GetCustomAttributes(false);   //反射成员的所有特性
                if (attrs.Any(a => a is DisplayNameAttribute))
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
            var file = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            workbook.Write(file);
            file.Close();
            workbook.Close();
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


        #region 导入示例
        //public class ImportPrincipalModel
        //{
        //    public HttpPostedFileBase Excel { get; set; }
        //    public Guid MerchantUid { get; set; }
        //}

        ///// <summary>
        ///// 导入商户负责人
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult ImportPrincipal(ImportPrincipalModel model)
        //{
        //    var user = UserHelper.GetCurrentUser<WebUser>().EmallMerchantDoamin;
        //    var file = model.Excel;
        //    if (file != null)
        //    {
        //        var path = SaveFile(file);

        //        user.ImportMerchantStaff(model.MerchantUid, path);

        //        return Json(new { Error = "" });

        //    }
        //    return Json(new { Error = "请上传Excel文件" });

        //}

        //[System.Web.Mvc.NonAction]
        //private string SaveFile(HttpPostedFileBase httpPostedFileBase)
        //{
        //    var saveLocation = System.Configuration.ConfigurationManager.AppSettings["MerchantStaffExcel"];
        //    var fullPath = Path.Combine(saveLocation, DateTime.Now.ToString("yyyyMMddhhmmsss") + Path.GetExtension(httpPostedFileBase.FileName));
        //    if (!Directory.Exists(saveLocation))
        //    {
        //        Directory.CreateDirectory(saveLocation);
        //    }
        //    httpPostedFileBase.SaveAs(fullPath);
        //    return fullPath;
        //}


        //public void ImportMerchantStaff(Guid merchantUid, string path)
        //{
        //    var dataTable = ExcelTool.ExcelToDataTable(path, true, 0);
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        MerchantStaff staff = new MerchantStaff();
        //        staff.MerchantUid = merchantUid;
        //        staff.StaffNo = row.ItemArray[0].ToString().Trim();
        //        staff.FullName = row.ItemArray[1].ToString().Trim();
        //        staff.Gender = row.ItemArray[2].ToString().Trim();
        //        staff.Mobile = row.ItemArray[3].ToString().Trim();
        //        EmallManager.CreateMerchantStaff(staff);
        //    }
        //}
        #endregion


        #region 导出示例


        //保存为excel文件。手动版
        //private void SaveToExcel(IList<TenderRecordsUserAccountListVModel> list, string filePath)
        //{
        //    //var list = dc.v_bs_dj_bbcdd1.Where(eps).ToList();
        //    HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        //    ISheet sheet1 = hssfworkbook.CreateSheet("投标记录");

        //    IRow rowHeader = sheet1.CreateRow(0);

        //    //生成excel标题
        //    rowHeader.CreateCell(0).SetCellValue("投标人");
        //    rowHeader.CreateCell(1).SetCellValue("投标人昵称");
        //    rowHeader.CreateCell(2).SetCellValue("投标项目编号");
        //    rowHeader.CreateCell(3).SetCellValue("投标项目标题");
        //    rowHeader.CreateCell(4).SetCellValue("投标金额");
        //    rowHeader.CreateCell(5).SetCellValue("投标时间");
        //    //生成excel内容
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
        //        rowtemp.CreateCell(0).SetCellValue(list[i].UserName);
        //        rowtemp.CreateCell(1).SetCellValue(list[i].NickName);
        //        rowtemp.CreateCell(2).SetCellValue(list[i].ProjectNO);
        //        rowtemp.CreateCell(3).SetCellValue(list[i].Title);
        //        rowtemp.CreateCell(4).SetCellValue(list[i].Amount);
        //        rowtemp.CreateCell(5).SetCellValue(list[i].TimeCreated);

        //    }

        //    for (int i = 0; i < 10; i++)
        //        sheet1.AutoSizeColumn(i);
        //    var file = new System.IO.FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
        //    hssfworkbook.Write(file);
        //    file.Close();
        //    hssfworkbook.Close();
        //}

        /// <summary>
        /// 导出投标记录的Excel
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("TenderRecordsToExcel")]
        ////   [ApiSessionAuthorize]
        //public ToExcelModel TenderRecordsToExcel([FromUri]TenderRecordsUserAccountIQueryModel query)
        //{
        //    int total;
        //    if (query == null) query = new TenderRecordsUserAccountIQueryModel();
        //    var resultList = new List<TenderRecordsUserAccountListVModel>();
        //    var list = _tenderRecords.GetTenderRecordsList(query.UserNameOrNikeName, query.ProjectNo, query.Title, query.Skip, query.Take, out total);
        //    if (list != null)
        //    {
        //        resultList = list.Select(TenderRecordsUserAccountListVModel.CopyFrom).ToList();
        //    }
        //    string tableName = DateTime.Now.ToString("yyyymmddHHmmss");

        //    ToExcelModel model = new ToExcelModel();
        //    string path = "D:\\" + tableName + ".xls";
        //    model.DownloadUrl = path;
        //    SaveToExcel(resultList, path);
        //    return model;
        //}

        #endregion

        #region 导出示例 泛型版
        ///// <summary>
        ///// 导出投标记录的Excel。保存为excel文件
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("TenderRecordsToExcel")]
        ////   [ApiSessionAuthorize]
        //public ToExcelModel TenderRecordsToExcel([FromUri]TenderRecordsUserAccountIQueryModel query)
        //{
        //    int total;
        //    if (query == null) query = new TenderRecordsUserAccountIQueryModel();
        //    var resultList = new List<TenderRecordsUserAccountListVModel>();
        //    var list = _tenderRecords.GetTenderRecordsList(query.UserNameOrNikeName, query.ProjectNo, query.Title, query.Skip, query.Take, out total);
        //    if (list != null)
        //    {
        //        resultList = list.Select(TenderRecordsUserAccountListVModel.CopyFrom).ToList();
        //    }
        //    string tableName = DateTime.Now.ToString("yyyymmddHHmmss");

        //    ToExcelModel model = new ToExcelModel();
        //    string path = "D:\\" + tableName + ".xls";
        //    model.DownloadUrl = path;
        //    Excel.SaveToExcel(resultList, path);
        //    return model;
        //}

        ///// <summary>
        ///// 导出投标记录的Excel
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("TenderRecordsToExcel")]
        //[ApiSessionAuthorize]
        //public ToExcelModel TenderRecordsToExcel([FromUri]TenderRecordsUserAccountIQueryModel query)
        //{
        //    int total;
        //    if (query == null) query = new TenderRecordsUserAccountIQueryModel();
        //    var resultList = new List<TenderRecordsUserAccountListVModel>();
        //    var list = _tenderRecords.GetTenderRecordsList(query.UserNameOrNikeName, query.ProjectNo, query.Title, 0, int.MaxValue, out total);
        //    if (list != null)
        //    {
        //        resultList = list.Select(TenderRecordsUserAccountListVModel.CopyFrom).ToList();
        //    }
        //    string tableName = DateTime.Now.ToString("yyyymmddHHmmss");

        //    ToExcelModel model = new ToExcelModel();
        //    var FileSaveDirectory = AppSetting.Instance().FileSaveDirectory;
        //    string path = $"{FileSaveDirectory}{tableName}.xls";
        //    var url = AppSetting.Instance().UploadUrl;
        //    model.DownloadUrl = $"{url}{tableName}.xls";
        //    Excel.SaveToExcel(resultList, path);
        //    return model;
        //}

        // excel 标题名称使用DisplayName 汉化
        //public class TenderRecordsUserAccountListVModel
        //{
        //    /// <summary>
        //    /// 投标人Guid
        //    /// </summary>
        //    public Guid UserGuid { get; set; }
        //    /// <summary>
        //    /// 投标人名称
        //    /// </summary>
        //    [DisplayName("投标人")]
        //    public string UserName { get; set; }
        //    /// <summary>
        //    /// 投标人昵称
        //    /// </summary>
        //    [DisplayName("投标人昵称")]
        //    public string NickName { get; set; }

        //    /// <summary>
        //    /// 投标Guid
        //    /// </summary>
        //    public Guid Guid { get; set; }
        //    /// <summary>
        //    /// 投标项目Guid
        //    /// </summary>
        //    public Guid ProjectGuid { get; set; }
        //    /// <summary>
        //    /// 投标项目编号
        //    /// </summary>
        //    [DisplayName("投标项目编号")]
        //    public string ProjectNO { get; set; }
        //    /// <summary>
        //    /// 投标标题
        //    /// </summary>
        //    [DisplayName("投标项目标题")]
        //    public string Title { get; set; }
        //    /// <summary>
        //    /// 金额
        //    /// </summary>
        //    [DisplayName("投标金额")]
        //    public int Amount { get; set; }

        //    /// <summary>
        //    /// /
        //    /// </summary>
        //    public bool IsAutomatic { get; set; }
        //    /// <summary>
        //    /// 债权转让
        //    /// </summary>
        //    public bool IsTransferred { get; set; }
        //    /// <summary>
        //    /// 创建时间
        //    /// </summary>
        //    [DisplayName("投标时间")]
        //    public string TimeCreated { get; set; }

        //    public static TenderRecordsUserAccountListVModel CopyFrom(TenderRecordsUserAccountListV recordsUserAccountListV)
        //    {
        //        TenderRecordsUserAccountListVModel model = new TenderRecordsUserAccountListVModel();

        //        model.UserGuid = recordsUserAccountListV.UserGuid;
        //        model.Guid = recordsUserAccountListV.Guid;
        //        model.UserName = recordsUserAccountListV.UserName;
        //        model.NickName = recordsUserAccountListV.NickName;
        //        model.ProjectGuid = recordsUserAccountListV.ProjectGuid;
        //        model.ProjectNO = recordsUserAccountListV.ProjectNO;
        //        model.Title = recordsUserAccountListV.Title;
        //        model.Amount = recordsUserAccountListV.Amount;
        //        model.IsAutomatic = recordsUserAccountListV.IsTransferred;
        //        model.IsAutomatic = recordsUserAccountListV.IsAutomatic;
        //        model.TimeCreated = recordsUserAccountListV.TimeCreated.ToString("yyyy-MM-dd HH:mm:ss");
        //        return model;
        //    }
        //}
        #endregion
    }
}
