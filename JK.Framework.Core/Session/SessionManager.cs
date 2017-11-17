using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Core
{
    public class SessionManager
    {
        /// <summary>
        /// 获取sessionkey 每次登录返回不一样。
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GetSessionKey(Guid userGuid, string salt)
        {
            //TODO：sessionkey简单处理，每个用户只有一个session
            //并根据ip地址，登录设备等做复杂处理
            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string userId = userGuid.ToString() + datetime;
            return userId.ToMd5WithSalt(salt); ;
        }

    }

    /// <summary>
    /// 为了不引用JK.Framework.Extensions 
    /// 暂时冗余一个md5类
    /// </summary>
    internal static class CoreMd5
    {
        internal static string ToMd5(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var md5 = new MD5CryptoServiceProvider();
                var data = Encoding.ASCII.GetBytes(value);
                var hash = string.Empty;
                data = md5.ComputeHash(data);
                return data.Aggregate(hash, (current, item) => current + item.ToString("x2"));
            }
            return value;
        }

        internal static string ToMd5WithSalt(this string value, string salt)
        {
            return ToMd5(value + "Jackie[" + salt + "}");
        }
    }
}
