using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Pay.Tencent
{
    public class RefundNotifyModel
    {

        public string ReturnCode { set; get; }

        public string ReturnMsg { get; set; }

        public string AppId { get; set; }

        public string MchId { get; set; }

        public string NonceStr { get; set; }
        /// <summary>
        /// 加密信息
        /// </summary>
        public string ReqInfo { get; set; }

        public string TransactionId { get; set; }

        public string OutTradeNo { get; set; }

        public string RefundId { get; set; }

        public string OutRefundNo { get; set; }

        public int TotalFee { get; set; }

        public int RefundFee { get; set; }

        public string RefundStatus { get; set; }

        /// <summary>
        /// 格式:20160725152626
        /// </summary>
        public string SuccessTime { get; set; }

        public string RefundRecvAccout { get; set; }

        public string RefundAccount { get; set; }

        public string RefundRequestSource { get; set; }
    }
}
