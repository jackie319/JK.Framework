using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.PayCenter
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PaymentEnum
    {
        /// <summary>
        /// 公众号支付
        /// </summary>
        JSAPI = 0,
        /// <summary>
        /// H5支付
        /// </summary>
        MWEB = 1,
        /// <summary>
        /// 扫码支付
        /// </summary>
        NATIVE = 2,
    }
}
