using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseManage.Container.Update
{
    public class VersionJsonUtility
    {

        public static string UpdateUrl
        {
            get { return Read("UpdateUrl",ConfigPath); }
        }
        public static string ZipName
        {
            get { return Read("ZipName",ConfigPath); }
        }
        public static string VersionNum
        {
            get { return Read("VersionNum",ConfigPath); }
        }
        /// <summary>
        /// 获取上一级目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentPath(string path)
        {
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            string parentPath = Directory.GetCurrentDirectory();
            return parentPath;
        }
        public static string DestinationFileName
        {
            get { return Read("DestinationFileName",ConfigPath); }
        }
        /// <summary>
        /// 配置文件放在主程序根目录
        /// </summary>
        public static string ConfigPath = @"VersionConfig.json";
        public static string Read(string key,string configPath)
        {
            StreamReader file = File.OpenText(ConfigPath);
            JsonTextReader reader = new JsonTextReader(file);
            JObject jsonObject = (JObject)JToken.ReadFrom(reader);
            var version = Convert.ToString(jsonObject[key]);
            file.Close();
            return version;
        }

        public static void Write(string versionNum)
        {
            try
            {
                string json = File.ReadAllText(ConfigPath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                jsonObj["VersionNum"] = versionNum;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(ConfigPath, output);
            }
            catch { }
        }
    }
}
