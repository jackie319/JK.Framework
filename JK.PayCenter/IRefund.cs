using JK.Data.Model;
using JK.PayCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.PayCenter
{
    public interface IRefund
    {
        IList<OrderRefund> Query(string orderNo, string orderRefundNo, string status, int skip, int take, out int total);

        IList<OrderRefund> RefundList(Guid userGuid, int skip, int take, out int total);
        void Check(OrderRefund refund);
        OrderRefund Find(Guid guid);
        void WechatRefund(Guid orderRefundGuid);
        PayNotifyResult RefundNotify(HttpContext httpContext);
    }
}
