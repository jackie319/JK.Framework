using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Encryption
{
    public static class Base64
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64Str(this string value)
        {
            byte[] bytes = Encoding.Default.GetBytes(value);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static string FromBase64Str(this string value)
        {
            byte[] outputb = Convert.FromBase64String(value);
            string result = Encoding.Default.GetString(outputb);
            return result;
        }
    }
}
