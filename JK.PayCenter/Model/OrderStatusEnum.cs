using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.PayCenter.Model
{
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 未支付 (待付款)
        /// </summary>
        Default,
        /// <summary>
        /// 已取消（交易关闭）
        /// </summary>
        Cancel,
        /// <summary>
        /// 已支付 （待发货）
        /// </summary>
        Paid,
        /// <summary>
        /// 支付失败
        /// </summary>
        PayFailure,
        /// <summary>
        /// 已发货（待收货）
        /// </summary>
        Delivered,
        /// <summary>
        /// 收货已完成（待评价）
        /// </summary>
        Finished,
        /// <summary>
        /// 已退款
        /// </summary>
        Refund,
        /// <summary>
        /// 仅用于查询
        /// </summary>
        All

    }

    public enum OrderRefundEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject,
        /// <summary>
        /// 同意退款
        /// </summary>
        Agree,
        /// <summary>
        /// 已退款
        /// </summary>
        Refund,
        /// <summary>
        /// 退款失败
        /// </summary>
        RefundFailure
    }
}
