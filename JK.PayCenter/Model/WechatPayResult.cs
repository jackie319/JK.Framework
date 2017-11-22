using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.PayCenter.Model
{
    public class WechatPayResult
    {
        /// <summary>
        /// 预支付id
        /// </summary>
        public string PrepayId { get; set; }

        /// <summary>
        /// H5支付跳转链接
        /// </summary>
        public string MWebUrl { get; set; }
        /// <summary>
        /// 扫码支付
        /// </summary>
        public string CodeUrl { get; set; }
    }
}
