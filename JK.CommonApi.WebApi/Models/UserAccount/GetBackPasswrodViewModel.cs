using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.UserAccount
{
    public class GetBackPasswrodViewModel
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "密码不能少于6位")]
        [MaxLength(32, ErrorMessage = "密码最长为32位")]
        public string PasswordMd5 { set; get; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string SmsCode { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string MobilePhone { get; set; }
    }
}