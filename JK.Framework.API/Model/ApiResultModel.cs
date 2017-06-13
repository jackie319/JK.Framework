using JK.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.API.Model
{
    /// <summary>
    /// 此类同JK.Framework.Web.Model 类似，应该再降一级
    /// </summary>
    public class ApiResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual bool Success { set; get; }

        /// <summary>
        /// 出错地址
        /// </summary>
        public virtual string ErrorUrl { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public virtual string ErrorMsg { set; get; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public virtual JKExceptionType ExceptionType { set; get; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUrl { set; get; }
  
     

        public ApiResultModel(bool success, string erroMsg,  string errorUrl = "", JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = errorUrl;
            ExceptionType = exceptionType;
            RedirectUrl = redirectUrl;
        }
    }
}
