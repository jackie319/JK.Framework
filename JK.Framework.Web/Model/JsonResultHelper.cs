using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JK.Framework.Core;

namespace JK.Framework.Web.Model
{
   public static class JsonResultHelper
    {
       public static JsonResult Result(this Controller left, bool success=true,string errorMsg="",int total=0,string url="",object returnData=null, JKExceptionType exceptionType=JKExceptionType.Common,string redirectUrl="", JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
       {
           return new ResultModel(success,errorMsg,total,url, exceptionType,redirectUrl,returnData).ToJsonResultModel(jsonRequestBehavior);
       }

        public static JsonResult ResultListModel(this Controller left, int total, object returnData, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", JsonRequestBehavior jsonRequestBehavior=JsonRequestBehavior.AllowGet)
        {
            return new ResultModel(true, "",total, "",exceptionType,redirectUrl, returnData).ToJsonResultModel(jsonRequestBehavior);
        }

        public static JsonResult ResultModel(this Controller left,  object returnData, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return new ResultModel(true, "", 1, "", exceptionType,redirectUrl,returnData).ToJsonResultModel(jsonRequestBehavior);
        }

        public static JsonResult ResultSuccess(this Controller left, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            return new ResultModel(true, "", 0, "",exceptionType,redirectUrl,null).ToJsonResultModel(jsonRequestBehavior);
        }
        public static JsonResult ResultError(this Controller left,string errorMsg,string errorUrl="", JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            return new ResultModel(false, errorMsg, 0, errorUrl, exceptionType,redirectUrl,null).ToJsonResultModel(jsonRequestBehavior);
        }

        public static JsonResult Result( bool success = true, string errorMsg = "",string url="",int total=0, JKExceptionType exceptionType = JKExceptionType.Common, string redirectUrl = "", object returnData = null)
        {
            return new ResultModel(success, errorMsg, total,url,exceptionType,redirectUrl,returnData).ToJsonResultModel();
        }

     
    }
}
