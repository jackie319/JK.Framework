using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JK.Framework.Extensions
{
    public  class JsonStorage
    {
        public T Create<T>(SettingType type, T t) where T : class, ISetting
        {
            var json = JsonConvert.SerializeObject(t);
            //dosomething.
            return t;
        }

        public T Update<T>(SettingType type, T t) where T : class, ISetting
        {
            var json = JsonConvert.SerializeObject(t);
            //dosomething.
            return t;
        }

        public T Retrieve<T>(SettingType type) where T : class, ISetting
        {
            //dosomething.
            var json = string.Empty;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public interface ISetting
    {
    }

    public enum SettingType
    {
        Wechat,
        AliWin
    }
}
