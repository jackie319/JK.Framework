using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Model
{
    public class JsonResultModel : JsonResult
    {
        public JsonResultModel(JsonRequestBehavior jsonRequestBehavior, ResultModel data)
        {
            Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
        }

        //其余父类JsonResult的属性（Encoding ContentEncoding等）待扩展 

    }


}
