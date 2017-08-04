using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.Framework.Extensions
{

    /// <summary>
    /// ClassName:ExportCsvManager
    /// Version:1.0
    /// Date:2013/08/14
    /// Author:杨建宝(jackie)
    /// </summary>
    /// <remarks>
    /// csv文件导出管理类
    /// </remarks>

    public class ExportCsvManager
    {
        /// <summary>
        ///返回写入CSV的字符串
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private static string GetCsvData(DataSet src)
        {
            if (src == null || src.Tables.Count == 0) throw new Exception("dataset is null or has not table in dataset");
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < src.Tables.Count; i++)
            {
                DataTable dt = src.Tables[i];

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string colName = dt.Columns[j].ColumnName;

                    if (colName.IndexOf(',') > -1)
                        colName = colName.Insert(0, "\"").Insert(colName.Length + 1, "\"");

                    sb.Append(colName);

                    if (!colName.Equals(""))

                        if (j != dt.Columns.Count - 1)

                            sb.Append(",");

                }
                sb.Append("\n");

                string temp = "";

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    DataRow dr = dt.Rows[j];

                    for (int k = 0; k < dt.Columns.Count; k++)
                    {

                        object o = dr[k];

                        if (o != null)

                            temp = o.ToString();

                        if (temp.IndexOf(',') > -1)

                            temp = temp.Insert(0, "\"").Insert(temp.Length + 1, "\"");

                        sb.Append(temp);
                        if (k != dt.Columns.Count - 1)

                            sb.Append(",");
                    }
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 导出到CSV文件并且提示下载
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="fileName"></param>
        public static void ExportToCsv(DataSet ds, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = DateTimeHelper.Now.ToString("yyMMddHHmmssiii");
            string data = GetCsvData(ds);
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.csv", System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            HttpContext.Current.Response.ContentType = "text/h323;charset=gbk";
            HttpContext.Current.Response.Write(data);
            HttpContext.Current.Response.End();
        }


    }
}

