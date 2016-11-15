using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.validation
{
    /// <summary>
    ///已废弃
    /// </summary>
    public class ValidationController:Controller
    {
        private bool isValid;
        private string Result { set; get; }
        protected bool IsValid
        {
            get
            {
                isValid = true;
                if (!ModelState.IsValid)
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors.Count > 0)
                        {
                            foreach (var error in item.Errors)
                            {
                                isValid = false;
                                Result = error.ErrorMessage;
                                return isValid;
                            }
                        }
                    }
                }
                return isValid;
            }
        }

        protected ActionResult ValidateJsonResult()
        {
            return Json(new { success = false, message = "验证失败！" + Result });
        }

        //protected ActionResult ValidateJsonResultNew()
        //{
        //    return this.JsonError("验证失败!" + Result); ;
        //}

        protected string ValidateStringResult()
        {
            return Result;
        }
    }
}
