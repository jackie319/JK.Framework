﻿using JK.Data.Model;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Pay.Tencent;
using JK.JKUserAccount.IServices;
using JK.JKUserAccount.ServiceModel;
using JK.PayCenter.Model;
using log4net;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.PayCenter
{
    /// <summary>
    ///  TODO:抽出公共部分到JK
    /// </summary>
    public class PayImpl : IPay
    {
        private IRepository<Order> _orderRepository;
        private IRepository<OrderProduct> _orderProductRepository;
        private IRepository<WechatPayRecords> _payRecordsRepository;
        private IRepository<Payment> _paymentRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductClassification> _productClassificationrRepository;
        private IRepository<WechatPayNotify> _orderPayNotifyRepository;
      //  private IProductCategory _productCategoryRepository;
        private UnifiedOrderSetting _setting;
        private ILog _log;
        private WechatPay _wechatPay;

        private ISms _sms;
        public PayImpl(IRepository<Order> ordeRepository, IRepository<WechatPayRecords> payRecordsRepository, IRepository<Payment> paymentRepository,
            IRepository<Product> productRepository, IRepository<OrderProduct> orderProductRepository,
            IRepository<WechatPayNotify> orderPayNotifyRepository, IRepository<ProductClassification> productClassificationrRepository, ISms sms)
        {
            _orderRepository = ordeRepository;
            _payRecordsRepository = payRecordsRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
            _orderPayNotifyRepository = orderPayNotifyRepository;
            _setting = new UnifiedOrderSetting();
            _productClassificationrRepository = productClassificationrRepository;
            _wechatPay = new WechatPay(_setting.AppId, _setting.MchId, _setting.Key, _setting.Cert, _setting.AppKey);
            _log = LogManager.GetLogger(typeof(PayImpl));
            _sms = sms;
        }
        /// <summary>
        /// 支付前检查商品库存，支付成功后（收到通知）扣减库存
        /// </summary>
        /// <param name="orderGuid"></param>
        public WechatPayResult WechatPay(Guid orderGuid, string orderNo, string openId, string spbillCreateIP, PaymentEnum payment)
        {
            WechatPayResult payResult = new WechatPayResult();
            var order = _orderRepository.Table.FirstOrDefault(q => q.Guid == orderGuid && q.OrderNo.Equals(orderNo));
            if (order == null) throw new CommonException("订单不存在");
            var products = _orderProductRepository.Table.Where(q => q.OrderGuid == orderGuid);
            foreach (var item in products)
            {
                var product = _productRepository.Table.FirstOrDefault(q => q.Guid == item.ProductGuid);
                var classification = product.ProductClassification.FirstOrDefault(q => q.Guid == item.ClassificationGuid);
                if (classification.Number < 1) throw new CommonException("商品库存不足");
            }

            var name = "";
               //GetCategoryName(order.OrderProduct.FirstOrDefault().ProductGuid);
            var body = $"分享精灵-{name}";
            string nonceStr = Guid.NewGuid().ToString("N");

            //record 字段待扩充
            WechatPayRecords record = new WechatPayRecords();
            record.Guid = Guid.NewGuid();
            record.OrderGuid = orderGuid;
            record.OrderNo = order.OrderNo;
            record.UserGuid = order.UserGuid;
            record.UserOpenId = openId;
            record.SpbillCreateIP = spbillCreateIP;
            record.TotalFee = order.OrderAmount;
            record.Body = body;
            record.ProductId = string.Empty;
            record.NonceStr = nonceStr;
            record.TradeType = payment.ToString();
            record.ReturnCode = string.Empty;
            record.ReturnMsg = string.Empty;
            record.ResultCode = string.Empty;
            record.ResultSign = string.Empty;
            record.ErrCode = string.Empty;
            record.ErrCodeDes = string.Empty;
            record.PrepayId = string.Empty;
            record.CodeUrl = string.Empty;
            record.TimeUpdate = DateTime.MinValue;
            record.TimeCreated = DateTime.Now;
            _payRecordsRepository.Insert(record);
            if (order != null)
            {
                order.PaymentType = payment.ToString();
                _orderRepository.Update(order);
            }
            TenPayV3Type tenPayType = TenPayV3Type.JSAPI;
            switch (payment)
            {
                case PaymentEnum.MWEB:
                    tenPayType = TenPayV3Type.MWEB;
                    break;
            }
            var result = UnifiedOrder(order, openId, spbillCreateIP, nonceStr, body, "", tenPayType);
            var entity = _payRecordsRepository.Table.FirstOrDefault(q => q.Guid == record.Guid);
            entity.ReturnCode = result.return_code;
            entity.ReturnMsg = result.return_msg ?? string.Empty;
            entity.ResultCode = result.result_code ?? string.Empty;
            entity.ResultSign = result.sign ?? string.Empty;
            entity.ErrCode = result.err_code ?? string.Empty;
            entity.ErrCodeDes = result.err_code_des ?? string.Empty;
            entity.PrepayId = result.prepay_id ?? string.Empty;
            entity.CodeUrl = result.code_url ?? string.Empty;
            _payRecordsRepository.Update(entity);
            if (!result.IsReturnCodeSuccess())
            {
                throw new CommonException("预下单失败：" + result.return_msg);
            }
            if (!result.IsResultCodeSuccess())
            {
                throw new CommonException("预下单失败：" + result.err_code_des);
            }
            payResult.PrepayId = result.prepay_id;
            payResult.MWebUrl = result.mweb_url;
            payResult.CodeUrl = result.code_url;
            return payResult;
        }

        private UnifiedorderResult UnifiedOrder(Order order, string openId, string spbillCreateIP, string nonceStr, string body, string productId, TenPayV3Type type)
        {

            _setting.DeviceInfo = "WEB";
            _setting.NonceStr = nonceStr;
            _setting.Body = body;
            _setting.Detail = _setting.Body;
            _setting.OutTradeNo = order.OrderNo;//商户订单号
            _setting.TotalFee = order.OrderAmount;//订单总金额
            _setting.SpbillCreateIP = spbillCreateIP;//用户端IP
            _setting.NotifyUrl = "http://api.m.maimaiyin.cn/WxNotify/Notify";
            _setting.TradeType = type;
            _setting.ProductId = productId;//扫码支付必填
            _setting.OpenId = openId;//trade_type=JSAPI时（即公众号支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识
            TenPayV3UnifiedorderRequestData data = new TenPayV3UnifiedorderRequestData(_setting.AppId, _setting.MchId, _setting.Body, _setting.OutTradeNo, _setting.TotalFee, _setting.SpbillCreateIP,
                _setting.NotifyUrl, _setting.TradeType, _setting.OpenId, _setting.Key, _setting.NonceStr);
            UnifiedorderResult result = _wechatPay.Pay(data);
            return result;
        }

        //private string GetCategoryName(Guid productGuid)
        //{
        //    string result = string.Empty;
        //    var entity = _productRepository.Table.FirstOrDefault(q => q.Guid == productGuid);
        //    var catetgory = _productCategoryRepository.FindCategory(entity.CategoryGuid);
        //    result = catetgory.CategoryName;
        //    return result;
        //}
        //private IList<OrderPayment> GetPayments()
        //{
        //    return _paymentRepository.Table.Where(q => !q.IsDeleted).ToList();
        //}

        //private OrderPayment GetJSAPIPay()
        //{
        //    string wechat = PaymentEnum.JSAPI.ToString();
        //    return _paymentRepository.Table.FirstOrDefault(q => q.PaymentType.Equals(wechat) && !q.IsDeleted);
        //}

        /// <summary>
        /// 支付成功后（收到通知）扣减库存, 增加销量,更改订单状态
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public PayNotifyResult PayNotify(HttpContext httpContext)
        {
            PayNotifyResultModel notifyResult = _wechatPay.PayNotify(httpContext);
            PayNotifyResult result = new PayNotifyResult();
            result.ReturnCode = "FAIL";
            result.ReturnMsg = "OK";

            WechatPayNotify notify = new WechatPayNotify();
            notify.Guid = Guid.NewGuid();
            notify.TimeCreated = DateTime.Now;
            notify.ReturnCode = notifyResult.ReturnCode;
            notify.ReturnMsg = notifyResult.ReturnMsg ?? string.Empty;
            notify.AppId = notifyResult.AppId ?? string.Empty;
            notify.MchId = notifyResult.MchId ?? string.Empty;
            notify.DeviceInfo = notifyResult.DeviceInfo ?? string.Empty;
            notify.NonceStr = notifyResult.NonceStr ?? string.Empty;
            notify.Sign = notifyResult.Sign ?? string.Empty;
            notify.SignType = notifyResult.SignType ?? string.Empty;
            notify.ResultCode = notifyResult.ResultCode ?? string.Empty;
            notify.ErrCode = notifyResult.ErrCode ?? string.Empty;
            notify.ErrCodeDes = notifyResult.ErrCodeDes ?? string.Empty;
            notify.OpenId = notifyResult.OpenId ?? string.Empty;
            notify.IsSubscribe = notifyResult.IsSubscribe ?? string.Empty;
            notify.TradeType = notifyResult.TradeType ?? string.Empty;
            notify.BankType = notifyResult.BankType ?? string.Empty;
            notify.TotalFee = notifyResult.TotalFee;
            notify.SettlementTotalFee = notifyResult.SettlementTotalFee;
            notify.TransactionId = notifyResult.TransactionId ?? string.Empty;
            notify.OutTradeNo = notifyResult.OutTradeNo ?? string.Empty;
            notify.Attach = notifyResult.Attach ?? string.Empty;
            notify.TimeEnd = notifyResult.TimeEnd ?? string.Empty;
            _orderPayNotifyRepository.Insert(notify);
            _log.Info("微信支付通知：接收通知记录保存成功");
            //更新订单状态
            //注意交易单不要重复处理
            if (notify.ReturnCode.Equals("SUCCESS"))
            {
                var order = _orderRepository.Table.FirstOrDefault(q => q.OrderNo.Equals(notify.OutTradeNo));
                //注意判断返回金额
                if (order.OrderAmount != notify.TotalFee)
                {
                    result.ReturnMsg = "订单金额不一致";
                    _log.Info("微信支付通知：订单金额不一致");
                    return result;
                }
                if (notify.ResultCode.Equals("SUCCESS"))
                {
                    if (order.OrderStatus.Equals(OrderStatusEnum.Default.ToString()) || order.OrderStatus.Equals(OrderStatusEnum.PayFailure.ToString()))
                    {
                        order.OrderStatus = OrderStatusEnum.Paid.ToString();
                        var amount = Convert.ToDecimal(order.OrderAmount) / 100;
                        var storeGuid = order.StoreGuid;
                        if (storeGuid != Guid.Empty)
                        {
                            order.OrderStatus = OrderStatusEnum.Finished.ToString();
                            //var store = _store.Find(storeGuid);
                            //if (store != null)
                            //{
                            //    //TODO:发送短信
                            //    IList<string> mobileList = new List<string>();
                            //    mobileList.Add(store.Phone);
                            //    mobileList.Add("18288215197");
                            //    IList<string> paramList = new List<string>();
                            //    paramList.Add("扫码");
                            //    var orderProduct = order.OrderProduct.FirstOrDefault();
                            //    paramList.Add(orderProduct.ProductName);
                            //    paramList.Add(amount.ToString());
                            //    _sms.PayNotify(mobileList, paramList);
                            //    _log.Info("订单短信通知发送成功");
                            //}
                        }
                        else
                        {
                            //TODO:发送短信
                            IList<string> mobileList = new List<string>();
                            mobileList.Add("18288215197");
                            IList<string> paramList = new List<string>();
                            paramList.Add("线上");
                            var orderProduct = order.OrderProduct.FirstOrDefault();
                            paramList.Add(orderProduct.ProductName);
                            paramList.Add(amount.ToString());
                            _sms.PayNotify(mobileList, paramList);
                            _log.Info("订单短信通知发送成功");
                        }
                        _log.Info("微信支付通知：Success 找到paying");
                    }
                }
                else
                {
                    if (order.OrderStatus.Equals(OrderStatusEnum.Default.ToString()))
                    {
                        order.OrderStatus = OrderStatusEnum.PayFailure.ToString();
                        _log.Info("微信支付通知：Failure 找到paying");
                    }
                }
                _orderRepository.Update(order);
                foreach (var item in order.OrderProduct)
                {

                    var product = _productRepository.Table.FirstOrDefault(q => q.Guid == item.ProductGuid);

                    var classification = _productClassificationrRepository.Table.FirstOrDefault(q => q.Guid == item.ClassificationGuid);
                    product.SoldTotal += item.ProductNumber;
                    product.ProductNumber -= item.ProductNumber;
                    _productRepository.Update(product);
                    classification.Number -= item.ProductNumber;
                    _productClassificationrRepository.Update(classification);
                }

            }

            //回复服务器处理成功
            result.ReturnCode = "SUCCESS";
            _log.Info("微信支付通知：成功处理完毕");
            return result;
        }
    }
}
