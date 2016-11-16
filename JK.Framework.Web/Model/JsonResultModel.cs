using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Model
{
    internal class JsonResultModel:JsonResult
    {

        internal JsonResultModel(bool success,string erroMsg,Object data=null)
        {
            Success = success;
            ErrorMsg = erroMsg;
            Data = data;
        }
        //其余父类JsonResult的属性（Encoding ContentEncoding等）待扩展 
        public bool Success { set; get; }
        public string ErrorMsg { set; get; }
       
    }


}
