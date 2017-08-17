﻿using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Zip
{
    public class ZipHelper
    {
        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// 需要ICSharpCode.SharpZipLib.dl   NuGet 可下载
        /// </summary>
        /// <param name="dirPath"> 被压缩的文件夹夹路径 </param>
        /// <param name="zipFilePath"> 生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名 +.zip</param>
         public static void ZipFile(string dirPath, string zipFilePath)
        {
            if (dirPath == string.Empty)
            {
                throw new ArgumentException("要压缩的文件夹不能为空");
            }
            if (!Directory.Exists(dirPath))
            {
                throw new ArgumentException("要压缩的文件夹不存在");
            }
            //压缩文件名为空时使用文件夹名＋ zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("\\"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("压缩出现错误:"+ex.Message);
            }
        }


        /// <summary>
        /// 功能：解压 zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath"> 压缩文件路径 </param>
        /// <param name="unZipDir"> 解压文件存放路径 ,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹 </param>
         public static void UnZipFile(string zipFilePath, string unZipDir)
        {
            if (zipFilePath == string.Empty)
            {
                throw new ArgumentException("压缩文件不能为空");
            }
            if (!File.Exists(zipFilePath))
            {
                throw new ArgumentException("压缩文件不存在");
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("\\"))
                unZipDir += "\\";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("\\"))
                            directoryName += "\\";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    } //while
                }
            }
            catch (Exception ex)
            {
                throw new Exception("压缩出现错误:" + ex.Message);
            }
        } //解压结束
    }
}
