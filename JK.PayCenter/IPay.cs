﻿using JK.PayCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.PayCenter
{
    public interface IPay
    {
        WechatPayResult WechatPay(Guid orderGuid, string orderNo, string openId, string spbillCreateIP, PaymentEnum payment);
        string Alipay(Guid orderGuid, string orderNo, Guid userGuid);
        PayNotifyResult PayNotify(HttpContext httpContext);
        void PayToUser(Guid userGuid, string openId, int money, string spbillCreateIP);
    }
}
