using JK.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JK.Framework.API.Filter
{
    /// <summary>
    /// /// <summary>
    /// Api模型验证filter
    /// </summary>
    /// </summary>
    public class ApiValidationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var modelState = filterContext.ModelState;
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
                            throw new CommonException(resultErrorMsg);//交给全局异常
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
