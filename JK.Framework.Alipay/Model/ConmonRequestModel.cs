using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Alipay.Model
{
    public class ConmonRequestModel
    {
        /// <summary>
        /// 支付宝分配给开发者的应用ID
        /// </summary>
        public string AppId = "2016091500520768";

        public string Getway = "https://openapi.alipaydev.com/gateway.do";
        /// <summary>
        /// 同步返回地址，HTTP/HTTPS开头字符串
        /// </summary>
        public string ReturnUrl = "http://test.api.ynsufan.com/Alipay/AlipayReturn";

        public string NotifyUrl = "http://test.api.ynsufan.com/Alipay/AlipayNotify";

        /// <summary>
        ///仅支持JSON
        /// </summary>
        public string Format = "JSON";

        /// <summary>
        ///请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        public string Charset = "utf-8";
        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        public string SignType = "RSA2";
        /// <summary>
        /// 商户请求参数的签名串
        /// </summary>
        //public string Sign { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string TimesTanmp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string Version = "1.0";



        /// <summary>
        /// 商户私钥
        /// </summary>
        public string MerchantPrivateKey = "MIIEowIBAAKCAQEAuj4q9Dt1oaP0pVxQZIfEfmyb3mblDNui1U4Eezq5WoXsb5ftRsOh+BLJSC2++Rr91mSKqL8uzAiZkqJKct9fiqnsE0diN1btbxZkQLMW+0X4bKa4rh6afjvjqw5KYwubMwF6Gohb4Kj1o3R685+KnLn+FuE2Gy41CHty2Ir7Nc2o5c1p4jpG+iexDGwyYN8tODNTs+IXwPbqEyfnUfnP+FoQsJH30ndNUwA7sLpoOoePjyx0IXs0eTagG0nCRVeuzrDK4QZP6NVMCPk+r6HKBgdMycdu1OLwWeTb92HRxD+8/3Pe5320jCFRU4rXz2OXfQUy+8+wVhvfaDwxiuQDJQIDAQABAoIBADqErhpvVFalnYkXqGrt/d97YL5QtkeD+3XqPZ46pWK8Eb4+jl9duNapkHUNaqP6xydGEAtVhu61BQv90kalyO9Y1MR5+XJJ7fCpqHZrzxzEr3n0KLMNCp3/U1m788OLMgikvij4D2ZXsFbbkoZZ6sOz7RJjUQzZR/CSWVOQ173fhy8xlLPRvkuMK8R7WJrrCL/LqBWbb9+K3Yuc+iXlnm7IXX1S8PxgL/jK1NCFDy3NTkzGzo1wD/sgWr1MAxZ0x/u8nT/o2ovzbCfPrjD8PSnSoFgv/zTXJxKKscwoSbr+MgS7+5Q1orsPR5C3YrsWBV4Yken/L0jWsRMkKO/fs0ECgYEA7XBAO+doz1LdR1WTTWM/GWenZaxpKfPyj0ogaR7X4hgEBurgIFHbtK8QVB6jDvxPcY+v7gEZ6w81idT7dinSgVeHTmCbU5tvqgsbb56DAkKxdHiMGcebJpOVeKjo3LzrWrnrFecOpv1XpYwmSV/3UPw5uJml6ZPYiXxnSyzfbmcCgYEAyM1cmLp9TR608F6kBSL5vb/7a+zGPNTXCfJFrkz+eBDJQTKxdwnXtpCCIoGoW/4Dpx8/twGQ29+Oo7r7vnOKXmQ1TzFft2tCUxUw4SjcKvHiJo3lT2wVHctCzXDCSFUBeYIHShfj7VNJvOKoJMg6V8HdYUnh2ozhWPtIHmkMspMCgYByT9ic/ow3RG3EKi8A0wdN89lj2d3HlOrykX2JHpBRCb7mla3R4ZclJiN2XEmkrVSSF9tbeqw/vtLHyQJHoVr3amx6D4i4d90KwM40rtvj+rf0mxTYVIEd0W/k52002L92rou/v74OJ2pLCW5lZQ5srwIbHd4C1mspxfr09mWbgQKBgDLPO/ntXl1QQsp7uBcQpY7s8Cp77CFiXdNHeXhERTOOU85qjFspo2qvoC4pDrfQS45TKuUcwVHRfJ9TZfXn+IAv7QlP3f9kZZDuUML4etVnBSlqSDbTi3hRgWLIgHCbCdd8HmoSJ3ta0tK3q4I7JAAmruS6NCr/8Nl6I+KGBE9/AoGBANjtw89XeqJkfsBwdchCJ5Vf2EUi4ZHLER12WkrOXKmvE2Mjbu+6xY4vZsamPV7CeGeNIqXO5ZbYkMXpMjK9SYWVvp1cOoaRlvIPeWM6R+OQ7henBonPA7qe16tkY6U8IWZUzTUzpEjQDstC7mbtwm9i5aVqCV1hayOBE878QuOk";
        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtH4RtkygEeLeUtMaVfHpctb1i+3f7P9tdg6YbZ+kzBDT6z77trUtoeuJofd3+s2CeU+ZmHqHV9qxPE2HzCK9mBeQ7b1fd5SLTVhew4aXuTwXTtOOXldPnmmOqK22JLqe8yKYlamqE3FSqhqvfk25ULOLlrRpp268eS+3pO4DYkrX4bN87WcW0yUhjmf1/UR4H9ZiPvmZqsSoxi/LmQp/BixAm9fZHePtXTSU4TRKEjnS6FV5aruLmR/xdGfJ4tVKPU9r9GjEZT9l1e2Hzk1QwBtIdTsGfd1WdXtrMOLDkTKxCSIEUsuU2waxLCKe02wrnbes50uWhAJYtMVJHofbOwIDAQAB";


    }
}
