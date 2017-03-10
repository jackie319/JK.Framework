using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Model
{
    public class JsonResultModel:JsonResult
    {

        public JsonResultModel(bool success,string erroMsg,int total, JsonRequestBehavior jsonRequestBehavior,string errorUrl="",Object data=null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = erroMsg;
            Total = total;
            Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
        }
        //其余父类JsonResult的属性（Encoding ContentEncoding等）待扩展 
        public virtual bool Success { set; get; }

        public virtual string ErrorUrl { get; set; }
        public virtual string ErrorMsg { set; get; }

        public virtual int Total { set; get; }

    }


}
