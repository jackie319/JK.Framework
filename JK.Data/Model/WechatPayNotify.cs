//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JK.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WechatPayNotify
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
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
        public System.DateTime TimeCreated { get; set; }
    }
}
