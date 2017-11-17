using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.UserAccount
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        public string MobilePhone { set; get; }
        /// <summary>
        /// MD5密码
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "密码不能少于6位")]
        [MaxLength(32, ErrorMessage = "密码最长为32位")]
        public string PasswordMd5 { set; get; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required(ErrorMessage = "请输入验证码")]
        [MinLength(2)]
        public string SmsCode { get; set; }
    }
}