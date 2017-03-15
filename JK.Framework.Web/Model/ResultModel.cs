using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JK.Framework.Core;

namespace JK.Framework.Web.Model
{
    public class ResultModel
    {
        public virtual bool Success { set; get; }

        public virtual string ErrorUrl { get; set; }
        public virtual string ErrorMsg { set; get; }

        public virtual JKExceptionType ExceptionType { set; get; }
        public virtual int Total { set; get; }
        public virtual Object Data { set; get; }
        public ResultModel(bool success, string erroMsg, int total, string errorUrl = "", JKExceptionType exceptionType=JKExceptionType.Common,Object data = null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = errorUrl;
            Total = total;
            ExceptionType = exceptionType;
            Data = data;
        }
        internal JsonResultModel ToJsonResultModel(JsonRequestBehavior jsonRequestBehavior= JsonRequestBehavior.DenyGet)
        {
           return new JsonResultModel(jsonRequestBehavior,this);
        }


    }
}
