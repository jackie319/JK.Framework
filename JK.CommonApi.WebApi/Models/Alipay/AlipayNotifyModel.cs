using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.Alipay
{
    public class AlipayNotifyModel
    {
        public string sign { get; set; }

        public string app_id { get; set; }
        public string out_trade_no { get; set; }
    }
}