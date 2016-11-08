using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
 
        /// <summary>
        /// ClassName:LogTool
        /// Version:1.0
        /// Date:2013/08/14
        /// Author:杨建宝(jackie)
        /// </summary>
        /// <remarks>
        /// 简单日记记录
        /// </remarks>
        public static class LogTool
        {
            /// <summary>
            /// 是否开启日志记录
            /// </summary>
            private static bool DailyRecordDebug
            {
                get
                {
                    bool flag = false;
                    try
                    {
                        flag = Convert.ToBoolean(ConfigurationManager.AppSettings["DailyRecordDebug"]);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                    }

                    return flag;
                }
            }


            /// <summary>
            /// 文件路径
            /// </summary>
            /// <returns></returns>
            private static string GetFilePath(string file)
            {
                string dtime = DateTime.Now.ToString("yyyyMMdd");
                string directory = AppDomain.CurrentDomain.BaseDirectory + "/Daily";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                return directory + "/" + file + "_" + dtime + ".txt";
            }

            /// <summary>
            /// 简单记录日志 用于DEBUG
            /// </summary>
            /// <param name="type"></param>
            /// <param name="content"></param>
            public static void DailyRecord(string type, string content)
            {
                if (!DailyRecordDebug)
                {
                    return;
                }
                string path = GetFilePath("DailyRecord");
                var sw = new StreamWriter(path, true);//新建或追加
                sw.WriteLine("[{0} {1}]\r\n{2}\r\n", DateTime.Now.ToString("MM-dd HH:mm:ss"), type, content);
                sw.Close();
                sw.Dispose();
            }

            /// <summary>
            ///  简单记录错误日志
            /// </summary>
            /// <param name="message"></param>
            public static void ErrorRecord(string message)
            {
                if (!DailyRecordDebug)
                {
                    return;
                }
                string path = GetFilePath("DailyRecord");
                var sw = new StreamWriter(path, true);//新建或追加
                sw.WriteLine("[{0}]\r\n{1}\r\n", DateTime.Now.ToString("MM-dd HH:mm:ss"), message);
                sw.Close();
                sw.Dispose();
            }
            /// <summary>
            /// 简单记录错误日志
            /// </summary>
            /// <param name="type"></param>
            /// <param name="content"></param>
            /// <param name="url"></param>
            /// <param name="source"></param>
            /// <param name="stackTrace"></param>
            public static void ErrorRecord(string type, string content, string url, string source, string stackTrace)
            {
                var path = GetFilePath("ErrorRecord");
                var sw = new StreamWriter(path, true);//新建或追加
                sw.WriteLine("[{0} {1}]\r\n{2}\r\n", DateTime.Now.ToString("MM-dd HH:mm:ss"), type, content);
                sw.WriteLine("{0}\r\n{1}\r\n{2}\r\n", url, source, stackTrace);
                sw.Close();
            }
        }
    }


