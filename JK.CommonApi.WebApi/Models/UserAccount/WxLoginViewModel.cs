using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.UserAccount
{
    public class WxLoginViewModel
    {
        public string Code { get; set; }

        /// <summary>
        /// 推荐人Guid
        /// </summary>
        public Guid UserGuid { get; set; }
    }
}