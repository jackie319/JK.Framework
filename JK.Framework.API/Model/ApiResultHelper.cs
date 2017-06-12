using JK.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JK.Framework.API.Model
{
    public static class ApiResultHelper
    {
        //public static ApiResultModel Result(this ApiController left, bool success = true, string errorMsg = "", int total = 0, string url = "", object returnData = null, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        //{
        //    return new ApiResultModel(success, errorMsg, total, url, exceptionType, redirectUrl, returnData);
        //}

        public static ApiResultModel ResultListModel(this ApiController left, int total, object returnData, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        {
            return new ApiResultModel(true, "", total, "", exceptionType, redirectUrl, returnData);
        }
        public static ApiResultModel ResultModel(this ApiController left, object returnData, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        {
            return new ApiResultModel(true, "", 1, "", exceptionType, redirectUrl, returnData);
        }

        public static ApiResultModel ResultSuccess(this ApiController left, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        { 
            return new ApiResultModel(true, "", 0, "", exceptionType, redirectUrl, null);
        }

        public static ApiResultModel ResultError(this ApiController left, string errorMsg, string errorUrl = "", JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        {
            return new ApiResultModel(false, errorMsg, 0, errorUrl, exceptionType, redirectUrl, null);
        }
    }
}
