using JK.Data.Model;
using JK.Framework.Core.Data;
using JK.Framework.Pay.Tencent;
using JK.JKUserAccount.ServiceModel;
using JK.PayCenter.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.PayCenter
{
    public class RefundImpl:IRefund
    {
        private IRepository<Order> _OrderRepository;
        private IRepository<OrderRefund> _OrderRefundRepository;
        private IRepository<WechatPayRefundNotify> _OrderRefundNotifyRepository;
        private IRepository<WechatPayRefundRecords> _OrderRefundRecordsRepository;
        private UnifiedOrderSetting _setting;
        private WechatPay _wechatPay;
        private ILog _log;
        public RefundImpl(IRepository<OrderRefund> orderRefundRepository, IRepository<WechatPayRefundNotify> orderRefundNotifyRepository, IRepository<WechatPayRefundRecords> orderRefundRecordsRepository, IRepository<Order> orderRepository)
        {
            _OrderRefundRepository = orderRefundRepository;
            _OrderRefundNotifyRepository = orderRefundNotifyRepository;
            _OrderRefundRecordsRepository = orderRefundRecordsRepository;
            _OrderRepository = orderRepository;
            _setting = new UnifiedOrderSetting();
            _wechatPay = new WechatPay(_setting.AppId, _setting.MchId, _setting.Key, _setting.Cert, _setting.MchId);
            _log = LogManager.GetLogger(typeof(RefundImpl));
        }

        public IList<OrderRefund> Query(string orderNo, string orderRefundNo, string status, int skip, int take, out int total)
        {
            var query = _OrderRefundRepository.Table;
            if (!string.IsNullOrEmpty(orderNo))
            {
                query = query.Where(q => q.Order.OrderNo.Contains(orderNo));
            }
            if (!string.IsNullOrEmpty(orderRefundNo))
            {
                query = query.Where(q => q.RefundNo.Contains(orderRefundNo));
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(q => q.Status.Equals(status));
            }

            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public IList<OrderRefund> RefundList(Guid userGuid, int skip, int take, out int total)
        {
            var query = _OrderRefundRepository.Table.Where(q => q.UserGuid == userGuid);

            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public OrderRefund Find(Guid guid)
        {
            var entity = _OrderRefundRepository.Table.FirstOrDefault(q => q.Guid == guid);
            return entity;
        }


        /// <summary>
        /// 退款审核
        /// </summary>
        /// <param name="refund"></param>
        public void Check(OrderRefund refund)
        {
            var entity = _OrderRefundRepository.Table.FirstOrDefault(q => q.Guid == refund.Guid);
            entity.Status = refund.Status;
            entity.CheckRefundRemark = refund.CheckRefundRemark;
            entity.CheckRefundTime = DateTime.Now;
            _OrderRefundRepository.Update(entity);
        }

        /// <summary>
        /// 退款
        /// </summary>
        public void WechatRefund(Guid orderRefundGuid)
        {
            var refund = _OrderRefundRepository.Table.FirstOrDefault(q => q.Guid == orderRefundGuid);
            if (refund == null) throw new ArgumentException("找不到该退款申请");
            var order = _OrderRepository.Table.FirstOrDefault(q => q.Guid == refund.OrderGuid);
            if (order == null) throw new ArgumentException("找不到该退款申请的订单");
            WechatPayRefundRecords record = new WechatPayRefundRecords();
            var recordGuid = Guid.NewGuid();
            record.Guid = recordGuid;
            record.NonceStr = string.Empty;
            record.Sign = string.Empty;
            record.SignType = "MD5";
            record.TransactionId = string.Empty;
            record.OutTradeNo = order.OrderNo;
            record.OutRefundNo = refund.RefundNo;
            record.TotalFee = order.OrderAmount;
            record.RefundFee = refund.RefundAmount;
            record.RefundFeeType = "CNY";
            record.RefundDesc = string.Empty;
            record.ReturnCode = string.Empty;
            record.ReturnMsg = string.Empty;
            record.ResultCode = string.Empty;
            record.ErrCode = string.Empty;
            record.ErrCodeDes = string.Empty;
            record.Appid = string.Empty;
            record.MchId = string.Empty;
            record.RefundId = string.Empty;
            record.TimeCreated = DateTime.Now;
            _OrderRefundRecordsRepository.Insert(record);

            RefundModel model = new RefundModel();
            model.OutTradeNo = order.OrderNo;
            model.OutRefundNo = refund.RefundNo;
            model.totalFee = order.OrderAmount;
            model.RefundFee = refund.RefundAmount;
            RefundResultModel refundResultModel = _wechatPay.Refund(model);

            var recordEntity = _OrderRefundRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            if (recordEntity == null) throw new ArgumentException("找不到该退款记录（OrderRefundRecords）");
            _log.Info("找到退款记录（OrderRefundRecords）");
            recordEntity.ReturnCode = refundResultModel.ReturnCode;
            recordEntity.ReturnMsg = refundResultModel.ReturnMsg;
            recordEntity.ResultCode = refundResultModel.ResultCode;
            recordEntity.ErrCode = refundResultModel.ErrCode;
            recordEntity.ErrCodeDes = refundResultModel.ErrCodeDes;
            _OrderRefundRecordsRepository.Update(recordEntity);
        }


        /// <summary>
        /// 退款通知(更改退款订单的状态和订单的状态)
        /// </summary>
        public PayNotifyResult RefundNotify(HttpContext httpContext)
        {
            PayNotifyResult result = new PayNotifyResult();
            RefundNotifyModel refundNotifyResult = _wechatPay.RefundNotify(httpContext);
            WechatPayRefundNotify notify = new WechatPayRefundNotify();
            notify.Guid = Guid.NewGuid();
            notify.ReturnCode = refundNotifyResult.ReturnCode;
            notify.ReturnMsg = refundNotifyResult.ReturnMsg;
            notify.Appid = refundNotifyResult.AppId;
            notify.MchId = refundNotifyResult.MchId;
            notify.NonceStr = refundNotifyResult.NonceStr;
            notify.ReqInfo = refundNotifyResult.ReqInfo;
            notify.TransactionId = refundNotifyResult.TransactionId;
            notify.OutTradeNo = refundNotifyResult.OutTradeNo;
            notify.OutRefundNo = refundNotifyResult.OutRefundNo;
            notify.RefundId = refundNotifyResult.RefundId;
            notify.TotalFee = refundNotifyResult.TotalFee;
            notify.RefundFee = refundNotifyResult.RefundFee;
            notify.RefundStatus = refundNotifyResult.RefundStatus;
            notify.SuccessTime = refundNotifyResult.SuccessTime;
            notify.RefundRecvAccout = refundNotifyResult.RefundRecvAccout;
            notify.RefundAccount = refundNotifyResult.RefundAccount;
            notify.RefundRequestSource = refundNotifyResult.RefundRequestSource;
            notify.TimeCreated = DateTime.Now;
            _OrderRefundNotifyRepository.Insert(notify);
            _log.Info("微信退款通知：接收通知记录保存成功");

            if (refundNotifyResult.ReturnCode.Equals("SUCCESS"))
            {
                var entity = _OrderRefundRepository.Table.FirstOrDefault(q => q.RefundNo.Equals(refundNotifyResult.OutRefundNo));
                if (entity.Status.Equals(OrderRefundEnum.Agree.ToString()) || entity.Status.Equals(OrderRefundEnum.RefundFailure.ToString()))
                {
                    if (refundNotifyResult.RefundStatus.Equals("SUCCESS"))
                    {
                        entity.Status = OrderRefundEnum.Refund.ToString();
                        var order = _OrderRepository.Table.FirstOrDefault(q => q.Guid == entity.OrderGuid);
                        order.OrderStatus = OrderStatusEnum.Refund.ToString();
                        _OrderRepository.Update(order);
                    }
                    else
                    {
                        entity.Status = OrderRefundEnum.RefundFailure.ToString();
                    }
                    entity.RefundSuccessTime = DateTime.Now;
                    _OrderRefundRepository.Update(entity);
                }

                _log.Info("微信退款通知：处理完毕");
            }
            result.ReturnMsg = "OK";
            result.ReturnCode = "SUCCESS";
            return result;
        }
    }
}
