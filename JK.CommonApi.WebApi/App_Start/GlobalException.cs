using JK.Framework.API.Model;
using JK.Framework.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace JK.CommonApi.WebApi
{
    public  class GlobalException
    {
        internal static ApiResultModel GlobalExceptionHandler(HttpActionExecutedContext exceptionfiltercontext)
        {
            var exception = exceptionfiltercontext.Exception;
            string errorMsg = exception.Message;
            var url = exceptionfiltercontext.Request.RequestUri.ToString();
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorMsg = exception.Message;
            }
            var logger = LogManager.GetLogger(typeof(GlobalException));
            logger.Error("出错了！错误信息：" + errorMsg + "访问路径：" + url + "堆栈：" + exception.StackTrace);
            var result = new ApiResultModel(false, errorMsg, url) { };
            if (exception is AuthorizeException)
            {
                result = new ApiResultModel(false, errorMsg, url, JKExceptionType.NoAuthorized) { };
            }
            return result;
        }
    }
}