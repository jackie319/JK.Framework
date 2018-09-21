using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HouseManage.Container.Update
{
    public class UpdateManager
    {
        /// <summary>
        /// 版本检查地址
        /// </summary>
        private string UpdateUrl { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        private string VersionNum { get; set; }
        /// <summary>
        /// 服务器版本号
        /// </summary>
        private string ServerVersionNum { get; set; }
        /// <summary>
        /// 更新的zip地址
        /// 根据接口获取 exp:http://localhost:8002/DownLoad/Update.zip
        /// </summary>
        private string ZipUrl { get; set; }
        /// <summary>
        /// 下载保存后的zip文件名(包括路径)
        /// </summary>
        private string ZipName { get; set; }
        /// <summary>
        /// 解压到目标文件夹
        /// </summary>
        private string DestinationFileName { get; set; }

        public UpdateManager(string configPath)
        {
            DestinationFileName = "..\\";//自己调
            if (!string.IsNullOrEmpty(configPath))
            {
                VersionJsonUtility.ConfigPath = configPath;
                DestinationFileName = VersionJsonUtility.GetParentPath(configPath);//被调
            }
            UpdateUrl = VersionJsonUtility.UpdateUrl;
            VersionNum = VersionJsonUtility.VersionNum;
         
     
        }
        /// <summary>
        /// 是否需要更新
        /// </summary>
        public Boolean IsNeedUpdate
        {
            get { return CheckUpate(); }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (CheckUpate())
            {
                DownLoadZip();
                UnZip();
                ChangeVersionNum();
            }
        }


        //public static void StartUpdateExe()
        //{
        //    string fileName = Application.StartupPath + @"\update\HouseManage.Container.AutoUpdate.exe";
        //    Process p = new Process();
        //    p.StartInfo.UseShellExecute = false;
        //    p.StartInfo.RedirectStandardOutput = true;
        //    p.StartInfo.FileName = fileName;
        //    p.StartInfo.CreateNoWindow = true;
        //    p.StartInfo.Arguments = "";//参数以空格分隔，如果某个参数为空，可以传入””
        //    p.Start();
        //    System.Environment.Exit(System.Environment.ExitCode);
        //}

        public  void StartExe(string exePath)
        {
            string fileName = exePath;
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = fileName;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = "";//参数以空格分隔，如果某个参数为空，可以传入””
            p.Start();
            System.Environment.Exit(System.Environment.ExitCode);
        }

        public void DownLoadZip()
        {
            string path = "DownLoadZip";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
            ZipName = $"{path}\\{fileName}";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(new Uri(ZipUrl), ZipName);//要下载文件的路径,下载之后的命名
            }
        }
        public void UnZip()
        {
            ZipHelper.Unzip(ZipName, DestinationFileName);
        }
        public void DeleteZip()
        {
            
            if (File.Exists(ZipName))
            {
                FileInfo file = new FileInfo(ZipName);
                File.SetAttributes(ZipName, FileAttributes.Normal);
                File.Delete(ZipName);
            }
        }
        public void DownLoadZipAsync()
        {
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += DownloadProgressChanged;
            //wc.DownloadDataCompleted += DownloadFileCompleted;
            wc.DownloadFileAsync(new Uri(ZipUrl), ZipName);//要下载文件的路径,下载之后的命名
        }

        public void DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                UnZip();
                ChangeVersionNum();
            }
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns></returns>
        private Boolean CheckUpate()
        {
            var result = RequestUtility.Get(UpdateUrl);
            var versionModel = JsonConvert.DeserializeObject<ResultModel>(result);
            var flag = Compare(versionModel.Result.VersionNum, VersionNum);
            if (flag)
            {
                ServerVersionNum = versionModel.Result.VersionNum;
                ZipUrl = versionModel.Result.UpdateUrl;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更改本地版本号
        /// </summary>
        public void ChangeVersionNum()
        {
            VersionJsonUtility.Write(ServerVersionNum);
        }

        /// <summary>
        /// 比较版本号
        /// </summary>
        private Boolean Compare(string serverVersionStr, string clientVersionStr)
        {
            var serverVersion = serverVersionStr.Split('.');
            var clientVersion = clientVersionStr.Split('.');
            if (serverVersion.Count() != 3) throw new Exception("服务的版本号格式不正确");
            if (clientVersion.Count() != 3) throw new Exception("客户端版本号格式不正确");
            if (Convert.ToInt32(serverVersion[0]) > Convert.ToInt32(clientVersion[0]))
            {
                return true;
            }
            if (Convert.ToInt32(serverVersion[0]) == Convert.ToInt32(clientVersion[0]))
            {
                if (Convert.ToInt32(serverVersion[1]) > Convert.ToInt32(clientVersion[1]))
                {
                    return true;
                }
                if (Convert.ToInt32(serverVersion[1]) == Convert.ToInt32(clientVersion[1]))
                {
                    if (Convert.ToInt32(serverVersion[2]) > Convert.ToInt32(clientVersion[2]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
