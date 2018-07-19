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
        public string trade_no { get; set; }
        public string out_trade_no { get; set; }

        public string trade_status { get; set; }
        public string total_amount { get; set; }

        public string subject { get; set; }

        public string gmt_create { get; set; }

        public string gmt_payment { get; set; }

        public string seller_id { get; set; }
    }
}