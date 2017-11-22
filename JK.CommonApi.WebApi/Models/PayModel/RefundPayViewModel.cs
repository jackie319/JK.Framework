using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.PayModel
{
    public class RefundPayViewModel
    {
        /// <summary>
        /// 退款申请Guid
        /// </summary>
        [Required]
        public Guid OrderRefundGuid { get; set; }
    }
}