using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    public static class Md5
    {
        public static string ToMd5(this string value)
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

        public static string ToMd5Utf8(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var md5 = new MD5CryptoServiceProvider();
                var data = Encoding.UTF8.GetBytes(value);
                var hash = string.Empty;
                data = md5.ComputeHash(data);
                return data.Aggregate(hash, (current, item) => current + item.ToString("x2"));
            }
            return value;
        }
    }
}
