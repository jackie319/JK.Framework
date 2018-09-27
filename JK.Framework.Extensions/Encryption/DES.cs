using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Encryption
{
    public class DES
    {
        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key
        {
            get { return @"ab@cd(ng"; }
        }
        /// <summary>
        /// 获取向量
        /// </summary>
        private static string IV
        {
            get { return @"YK#23098"; }
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <returns>密文</returns>
        public static string DESEncrypt(string plainStr)
        {
            string encrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key);
                byte[] bIV = Encoding.UTF8.GetBytes(IV);
                byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }//TODO:错误处理
            des.Clear();
            return encrypt;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <returns>明文</returns>
        public static string DESDecrypt(string encryptStr)
        {
            string decrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key);
                byte[] bIV = Encoding.UTF8.GetBytes(IV);
                byte[] byteArray = Convert.FromBase64String(encryptStr);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }//TODO：错误处理
            des.Clear();
            return decrypt;
        }
    }
}
