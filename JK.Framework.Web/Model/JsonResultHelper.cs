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
       public static JsonResult JsonOk(this Controller left, bool success=true,string errorMsg="",string url="",object returnData=null)
       {
           return new ResultModel(success,errorMsg,1,url,returnData).ToJsonResultModel();
       }

        public static JsonResult JsonOk(this Controller left, int total, object returnData)
        {
            return new ResultModel(true, "",total, "",returnData).ToJsonResultModel();
        }

        public static JsonResult JsonOk(this Controller left,  object returnData)
        {
            return new ResultModel(true, "", 1, "", returnData).ToJsonResultModel();
        }

        public static JsonResult JsonOk( bool success = true, string errorMsg = "",string url="", object returnData = null)
        {
            return new ResultModel(success, errorMsg, 1,url,returnData).ToJsonResultModel();
        }

        public static JsonResult JsonOk(object returnData)
        {
            return new ResultModel(true,"", 1,"",returnData).ToJsonResultModel();
        }
    }
}
