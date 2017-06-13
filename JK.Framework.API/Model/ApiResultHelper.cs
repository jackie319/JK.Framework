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


        public static ApiResultModel ResultApiSuccess(this ApiController left, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        { 
            return new ApiResultModel(true, "",  "", exceptionType, redirectUrl);
        }

        public static ApiResultModel ResultApiError(this ApiController left, string errorMsg, string errorUrl = "", JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "")
        {
            return new ApiResultModel(false, errorMsg,  errorUrl, exceptionType, redirectUrl);
        }
    }
}
