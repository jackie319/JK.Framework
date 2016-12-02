using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JK.Framework.Web.Model;

namespace JK.Framework.Web.Filter
{
    /// <summary>
    /// mvc模型验证filter
    /// </summary>
   public class ValidationFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                foreach (var item in modelState.Values)
                {
                    if (item.Errors.Count > 0)
                    {
                        foreach (var error in item.Errors)
                        {
                            var errorMsg = error.ErrorMessage;
                            filterContext.Result = JsonResultHelper.JsonOk(false,errorMsg);
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
