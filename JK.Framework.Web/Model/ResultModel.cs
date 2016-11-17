using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Web.Model
{
    public class ResultModel
    {
        public virtual bool Success { set; get; }

        public virtual string ErrorUrl { get; set; }
        public virtual string ErrorMsg { set; get; }

        public virtual Object Data { set; get; }
        public ResultModel(bool success, string erroMsg, string errorUrl = "", Object data = null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            ErrorUrl = erroMsg;
            Data = data;
        }
        internal JsonResultModel ToJsonResultModel()
        {
            return new JsonResultModel(Success,ErrorMsg,ErrorUrl,Data);
        }
    }
}
