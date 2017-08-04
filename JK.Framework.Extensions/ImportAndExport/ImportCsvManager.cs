using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.Framework.Extensions
{
    /// <summary>
    /// ClassName:ImportCsvManager
    /// Version:1.0
    /// Date:2013/08/14
    /// Author:杨建宝(jackie)
    /// </summary>
    /// <remarks>
    /// csv文件导入管理类
    /// </remarks>

    public class ImportCsvManager
    {
        /// <summary>
        /// 根据文件流得到DataTable数据
        /// </summary>
        /// <param name="httpPostedFileBase">文件流</param>
        /// <param name="saveServerLocation">文件保存路径</param>
        /// <returns>DataTable数据</returns>
        /// <remarks>
        /// 经过测试，Bug点如下(重点是2)：
        /// 1，csv文件内容中不能有半角逗号（这个简单，因为csv文件就是靠半角逗号分隔，批量替换成别的字符就行）
        /// 2，\r或\n或\r\n  这个比较麻烦。因为这几个换行符在文本文档或Excel中均看不见。但是程序会解析，将之换行，造成数据混乱
        /// 解决办法当然也是替换这几个换行符，只是一般编辑器无法做到。
        /// Bug2终极解决方案：将报错的那行所有内容拷贝出来(注意是单独一行)，然后：
        /// 1,使用vs：可以在VS中新建文本用正则去掉
        /// 2,使用UE:在UE中去掉所有换行符
        /// 去掉之后在复制回去
        /// 以上BUG均无法在程序端替换，只能在进入程序前保证数据的格式正确。
        /// </remarks>
        public DataTable GetImportDataByCsv(HttpPostedFileBase httpPostedFileBase, string saveServerLocation)
        {
            if (httpPostedFileBase == null) throw new ImportCsvException(CsvExceptionType.FileNotFound);
            string extension = Path.GetExtension(httpPostedFileBase.FileName);
            if (extension == null || !extension.Equals(".csv")) throw new ImportCsvException(CsvExceptionType.ExtensionNotFormat);
            string fullPath = saveServerLocation + "/" + DateTimeHelper.Now.ToString("yyyyMMddhhmmsss") +
                               Path.GetExtension(httpPostedFileBase.FileName);
            //文件夹不存在则创建
            if (!Directory.Exists(saveServerLocation))
            {
                Directory.CreateDirectory(saveServerLocation);
            }
            httpPostedFileBase.SaveAs(fullPath);//保存上传的csv文件在服务器上
            //读取服务器上刚保存的csv文件
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            int errorid = 0;//出错时用于记录当前行数
            while ((strLine = sr.ReadLine()) != null)
            {
                errorid++;
                aryLine = strLine.Split(',');//英文字符逗号(半角字符)
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        try
                        {
                            DataColumn dc = new DataColumn(aryLine[i]);
                            dt.Columns.Add(dc);
                        }
                        catch (Exception e)
                        {
                            string message = string.Format("创建列异常！行数：{0}，列数：{1}；详细错误：{2}", errorid, (i + 1), e.Message);
                            throw new ImportCsvException(CsvExceptionType.OutOfRange, message);
                        }
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        try
                        {
                            dr[j] = aryLine[j];
                        }
                        catch (Exception e)
                        {
                            string message = string.Format("创建行异常！行数：{0}，列数：{1}；详细错误：{2}", errorid, (j + 1), e.Message);
                            throw new ImportCsvException(CsvExceptionType.OutOfRange, message);
                        }
                    }
                    dt.Rows.Add(dr);

                }
            }

            sr.Close();
            fs.Close();
            File.Delete(fullPath);//删除临时文件
            return dt;
        }
    }
    /// <summary>
    /// 异常枚举
    /// </summary>
    public enum CsvExceptionType
    {
        /// <summary>
        /// 获取不到文件流
        /// </summary>
        FileNotFound,
        /// <summary>
        /// 文件扩展名不匹配
        /// </summary>
        ExtensionNotFormat,
        /// <summary>
        /// 超出数组索引
        /// </summary>
        OutOfRange,


    }
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class ImportCsvException : Exception
    {
        public CsvExceptionType Type { private set; get; }

        public ImportCsvException(CsvExceptionType type)
        {
            Type = type;
        }

        public ImportCsvException(CsvExceptionType type, string message)
            : base(message)
        {
            Type = type;
        }
    }
}
