using JK.PayCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.PayModel
{
    public class PayViewModel
    {
        /// <summary>
        /// 订单Guid
        /// </summary>
        [Required]
        public Guid OrderGuid { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付方式 JSAPI = 0, MWEB=1,
        /// </summary>
        [Required]
        public PaymentEnum Payment { get; set; }
    }
}