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
                            var errorMsg = string.Empty;
                            if (!string.IsNullOrEmpty(error.ErrorMessage))
                            {
                                errorMsg = error.ErrorMessage;
                            }
                            else
                            {
                                if (error.Exception != null)
                                {
                                    errorMsg = error.Exception.Message;
                                }
                            }
                            var resultErrorMsg = $"模型验证错误：{errorMsg}";
                            filterContext.Result = JsonResultHelper.Result(false, resultErrorMsg);
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
