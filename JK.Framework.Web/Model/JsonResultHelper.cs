using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Model
{
   public static class JsonResultHelper
    {
       public static JsonResult Result(this Controller left, bool success=true,string errorMsg="",int total=0,string url="",object returnData=null)
       {
           return new ResultModel(success,errorMsg,total,url,returnData).ToJsonResultModel();
       }

        public static JsonResult ResultListModel(this Controller left, int total, object returnData)
        {
            return new ResultModel(true, "",total, "",returnData).ToJsonResultModel();
        }

        public static JsonResult ResultModel(this Controller left,  object returnData)
        {
            return new ResultModel(true, "", 1, "", returnData).ToJsonResultModel();
        }

        public static JsonResult ResultSuccess(this Controller left)
        {
            return new ResultModel(true, "", 0, "",null).ToJsonResultModel();
        }
        public static JsonResult ResultError(this Controller left,string errorMsg,string errorUrl="")
        {
            return new ResultModel(false, errorMsg, 0, errorUrl, null).ToJsonResultModel();
        }

        public static JsonResult Result( bool success = true, string errorMsg = "",string url="",int total=0, object returnData = null)
        {
            return new ResultModel(success, errorMsg, total,url,returnData).ToJsonResultModel();
        }

     
    }
}
