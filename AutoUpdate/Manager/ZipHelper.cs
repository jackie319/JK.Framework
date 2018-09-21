using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseManage.Container.Update
{
    public class ZipHelper
    {
        //public static string zipFileName = @"D:\HouseManage\Update.zip";
        //public static string DestinationFileName = @"D:\HouseManage\test\";

        /// <summary>
        /// 解压
        /// 打包的时候不要新建文件夹
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="destinationFileName"></param>
        public static void Unzip(string zipFileName,string destinationFileName)
        {
            
            (new FastZip()).ExtractZip(zipFileName, destinationFileName, "");
        }
    }
}
