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
    
    public partial class WithdrawCashRecords
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.DateTime TimeCreated { get; set; }
        public string MchAppId { get; set; }
        public string MchId { get; set; }
        public string DeviceInfo { get; set; }
        public string NonceStr { get; set; }
        public string OutTradeNo { get; set; }
        public string OpenId { get; set; }
        public string CheckName { get; set; }
        public string ReUserName { get; set; }
        public int Amount { get; set; }
        public string PayDesc { get; set; }
        public string SpbillCreateIP { get; set; }
        public string PayKey { get; set; }
        public string ReturnCode { get; set; }
        public string ResultCode { get; set; }
        public string ErrCode { get; set; }
        public string ErrCodeDes { get; set; }
        public string PartnerTradeNo { get; set; }
        public string PaymentNo { get; set; }
        public string PaymentTime { get; set; }
    }
}
