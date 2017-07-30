using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Pay.Tencent
{
   public  class PayNotifyResultModel
    {
        public string ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
        public string AppId { get; set; }
        public string MchId { get; set; }
        public string DeviceInfo { get; set; }
        public string NonceStr { get; set; }
        public string Sign { get; set; }
        public string SignType { get; set; }
        public string ResultCode { get; set; }
        public string ErrCode { get; set; }
        public string ErrCodeDes { get; set; }
        public string OpenId { get; set; }
        public string IsSubscribe { get; set; }
        public string TradeType { get; set; }
        public string BankType { get; set; }
        public int TotalFee { get; set; }
        public int SettlementTotalFee { get; set; }
        public string TransactionId { get; set; }
        public string OutTradeNo { get; set; }
        public string Attach { get; set; }
        public string TimeEnd { get; set; }
    }
}
