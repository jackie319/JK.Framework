using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    public static class SHA1
    {

        /// <summary>
        /// 对字符串进行SHA1加密（不可逆）
        /// </summary>
        /// <param name="Source_String">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string SHA1_Encrypt(this string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
    }
}
