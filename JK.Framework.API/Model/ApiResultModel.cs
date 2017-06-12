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
        /// <summary>
        /// 条目总数
        /// </summary>
        public virtual int Total { set; get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public virtual Object Data { set; get; }

        public ApiResultModel(bool success, string erroMsg, int total, string errorUrl = "", JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", Object data = null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = errorUrl;
            Total = total;
            ExceptionType = exceptionType;
            RedirectUrl = redirectUrl;
            Data = data;
        }
    }
}
