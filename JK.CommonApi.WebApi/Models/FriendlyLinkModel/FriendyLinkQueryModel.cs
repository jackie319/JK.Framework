using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.FriendlyLinkModel
{
    public class FriendyLinkQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean? Display { get; set; }
    }
}