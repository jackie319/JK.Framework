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
       public static JsonResult JsonOk(this Controller left,bool success=true,string errorMsg="",string url="",object returnData=null)
       {
           return new JsonResultModel(success,errorMsg,url,returnData);
       }

        public static JsonResult JsonOk(this Controller left,object returnData)
        {
            return new JsonResultModel(true, "", "",returnData);
        }

        public static JsonResult JsonOk(bool success = true, string errorMsg = "",string url="", object returnData = null)
        {
            return new JsonResultModel(success, errorMsg, url,returnData);
        }

        public static JsonResult JsonOk(object returnData)
        {
            return new JsonResultModel(true,"", "",returnData);
        }
    }
}
