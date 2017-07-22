using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JK.Framework.Pay.Tencent
{
    public static class WechatPay
    {
        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UnifiedorderResult Pay(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.Unifiedorder(dataInfo, timeOut);
        }
        /// <summary>
        /// 统一下单(异步调用)
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UnifiedorderResult> UnifiedorderAsync(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return await TenPayV3.UnifiedorderAsync(dataInfo, timeOut);
        }

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static OrderQueryResult OrderQuery(TenPayV3OrderQueryRequestData dataInfo)
        {
            return TenPayV3.OrderQuery(dataInfo);
        }

        /// <summary>
        /// 退款查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static RefundQueryResult RefundQuery(TenPayV3RefundQueryRequestData dataInfo)
        {
            return TenPayV3.RefundQuery(dataInfo);
        }
        /// <summary>
        /// 对账单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static string DownloadBill(TenPayV3DownloadBillRequestData dataInfo)
        {
            return TenPayV3.DownloadBill(dataInfo);
        }
    }
}
