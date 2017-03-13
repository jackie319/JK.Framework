using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Model
{
    public class ResultModel
    {
        public virtual bool Success { set; get; }

        public virtual string ErrorUrl { get; set; }
        public virtual string ErrorMsg { set; get; }

        public virtual int Total { set; get; }
        public virtual Object Data { set; get; }
        public ResultModel(bool success, string erroMsg, int total, string errorUrl = "", Object data = null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = errorUrl;
            Total = total;
            Data = data;
        }
        internal JsonResultModel ToJsonResultModel(JsonRequestBehavior jsonRequestBehavior= JsonRequestBehavior.DenyGet)
        {
           return new JsonResultModel(jsonRequestBehavior,this);
        }


    }
}
