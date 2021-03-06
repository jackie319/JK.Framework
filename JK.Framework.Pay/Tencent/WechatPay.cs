﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin;
using Senparc.Weixin.MP.TenPayLibV3;
using System.Web;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml.Linq;
using JK.Framework.Extensions;
using JK.Framework.Extensions.Encryption;

namespace JK.Framework.Pay.Tencent
{
    public class WechatPay
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        private string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        private string MchId { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        private string Key { get; set; }
        /// <summary>
        /// 证书位置
        /// </summary>
        private string Cert { get; set; }

        /// <summary>
        /// 证书密码（默认为商户号）
        /// </summary>
        private string Password { get; set; }

        public WechatPay(string appId, string mchId, string key, string cert, string password)
        {
            AppId = appId;
            MchId = mchId;
            Key = key;
            Cert = cert;
            Password = password;
        }
        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public UnifiedorderResult Pay(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.Unifiedorder(dataInfo, timeOut);
        }
        /// <summary>
        /// 统一下单(异步调用)
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<UnifiedorderResult> UnifiedorderAsync(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return await TenPayV3.UnifiedorderAsync(dataInfo, timeOut);
        }

        /// <summary>
        /// 支付通知
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="PayException">PayException</exception>
        public PayNotifyResultModel PayNotify(HttpContext httpContext)
        {
            PayNotifyResultModel notify = new PayNotifyResultModel();
            ResponseHandler resHandler = new ResponseHandler(httpContext);
            resHandler.Init();
            resHandler.SetKey(Key);

            //判断签名
            if (!resHandler.IsTenpaySign()) { throw new PayException("支付通知签名错误"); }

            //商户在收到后台通知后根据通知ID向财付通发起验证确认，采用后台系统调用交互模式
            // string notify_id = resHandler.GetParameter("notify_id");
            //取结果参数做业务处理
            string out_trade_no = resHandler.GetParameter("out_trade_no");
            //财付通订单号
            string transaction_id = resHandler.GetParameter("transaction_id");
            //金额,以分为单位
            string total_fee = resHandler.GetParameter("total_fee");
            ////如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
            //string discount = resHandler.GetParameter("discount");
            ////支付结果
            string trade_state = resHandler.GetParameter("trade_state");

            //
            string return_code = resHandler.GetParameter("return_code");
            string return_msg = resHandler.GetParameter("return_msg");
            string appid = resHandler.GetParameter("appid");
            string mch_id = resHandler.GetParameter("mch_id");
            string device_info = resHandler.GetParameter("device_info");
            string nonce_str = resHandler.GetParameter("nonce_str");
            string sign = resHandler.GetParameter("sign");
            string sign_type = resHandler.GetParameter("sign_type");
            string result_code = resHandler.GetParameter("result_code");
            string err_code = resHandler.GetParameter("err_code");
            string err_code_des = resHandler.GetParameter("err_code_des");
            string openid = resHandler.GetParameter("openid");
            string is_subscribe = resHandler.GetParameter("is_subscribe");
            string trade_type = resHandler.GetParameter("trade_type");
            string bank_type = resHandler.GetParameter("bank_type");
            string settlement_total_fee = resHandler.GetParameter("settlement_total_fee");
            string attach = resHandler.GetParameter("attach");
            string time_end = resHandler.GetParameter("time_end");

            notify.ReturnCode = return_code;
            notify.ReturnMsg = return_msg ?? string.Empty;
            notify.AppId = appid ?? string.Empty;
            notify.MchId = mch_id ?? string.Empty;
            notify.DeviceInfo = device_info ?? string.Empty;
            notify.NonceStr = nonce_str ?? string.Empty;
            notify.Sign = sign ?? string.Empty;
            notify.SignType = sign_type ?? string.Empty;
            notify.ResultCode = result_code ?? string.Empty;
            notify.ErrCode = err_code ?? string.Empty;
            notify.ErrCodeDes = err_code_des ?? string.Empty;
            notify.OpenId = openid ?? string.Empty;
            notify.IsSubscribe = is_subscribe ?? string.Empty;
            notify.TradeType = trade_type ?? string.Empty;
            notify.BankType = bank_type ?? string.Empty;
            if (string.IsNullOrEmpty(total_fee))
            {
                notify.TotalFee = 0;
            }
            else
            {
                notify.TotalFee = Convert.ToInt32(total_fee);
            }

            if (string.IsNullOrEmpty(settlement_total_fee))
            {
                notify.SettlementTotalFee = 0;
            }
            else
            {
                notify.SettlementTotalFee = Convert.ToInt32(settlement_total_fee);
            }
            notify.TransactionId = transaction_id ?? string.Empty;
            notify.OutTradeNo = out_trade_no ?? string.Empty;
            notify.Attach = attach ?? string.Empty;
            notify.TimeEnd = time_end ?? string.Empty;
            return notify;
        }

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public OrderQueryResult OrderQuery(TenPayV3OrderQueryRequestData dataInfo)
        {
            return TenPayV3.OrderQuery(dataInfo);
        }


        /// <summary>
        /// 发起退款
        /// </summary>
        public RefundResultModel Refund(RefundModel model)
        {
            RefundResultModel result = new RefundResultModel();
            string nonceStr = TenPayV3Util.GetNoncestr();
            RequestHandler packageReqHandler = new RequestHandler();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", AppId);       //公众账号ID
            packageReqHandler.SetParameter("mch_id", MchId);          //商户号
            packageReqHandler.SetParameter("out_trade_no", model.OutTradeNo);                 //填入商家订单号
            packageReqHandler.SetParameter("out_refund_no", model.OutRefundNo);                //填入退款订单号
            packageReqHandler.SetParameter("total_fee", model.totalFee.ToString());               //填入总金额
            packageReqHandler.SetParameter("refund_fee", model.RefundFee.ToString());               //填入退款金额
          //  packageReqHandler.SetParameter("op_user_id", MchId);   //操作员Id，默认就是商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
            string sign = packageReqHandler.CreateMd5Sign("key", Key);
            packageReqHandler.SetParameter("sign", sign);                       //签名
                                                                                //退款需要post的数据
            string data = packageReqHandler.ParseXML();

            //退款接口地址
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            string cert = Cert;// @"F:\apiclient_cert.p12";
                               //私钥（在安装证书时设置）
            string password = Password;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //调用证书
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            #region 发起post请求
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.ClientCertificates.Add(cer);
            webrequest.Method = "post";

            byte[] postdatabyte = Encoding.UTF8.GetBytes(data);
            webrequest.ContentLength = postdatabyte.Length;
            Stream stream;
            stream = webrequest.GetRequestStream();
            stream.Write(postdatabyte, 0, postdatabyte.Length);
            stream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)webrequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            #endregion
            var res = XDocument.Parse(responseContent);
            string return_code = GetXmlValue(res, "xml","return_code"); 
  
            string return_msg= GetXmlValue(res, "xml", "return_msg");
            string result_code= GetXmlValue(res, "xml", "result_code"); 
            string err_code = GetXmlValue(res, "xml", "err_code");
            string err_code_des = GetXmlValue(res, "xml", "err_code_des");
            string appid = GetXmlValue(res, "xml", "appid");
            string mch_id = GetXmlValue(res, "xml", "mch_id");
            string nonce_str = GetXmlValue(res, "xml", "nonce_str");
            string ResultSign = GetXmlValue(res, "xml", "ResultSign");
            string transaction_id = GetXmlValue(res, "xml", "transaction_id");
            string out_trade_no = GetXmlValue(res, "xml", "out_trade_no");
            string out_refund_no = GetXmlValue(res, "xml", "out_refund_no");
            string refund_id = GetXmlValue(res, "xml", "refund_id");
            string refund_fee = GetXmlValue(res, "xml", "refund_fee");
            string settlement_refund_fee = GetXmlValue(res, "xml", "settlement_refund_fee");
            string total_fee = GetXmlValue(res, "xml", "total_fee");

            result.ReturnCode = return_code ?? string.Empty;
            result.ReturnMsg = return_msg ?? string.Empty;
            result.ResultCode = result_code ?? string.Empty;
            result.ErrCode = err_code ?? string.Empty;
            result.ErrCodeDes = err_code_des ?? string.Empty;
          
            return result;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        private  string GetXmlValue(XDocument res,string rootName,string nodeName)
        {
            if (res == null || res.Element(rootName) == null
                || res.Element(rootName).Element(nodeName) == null)
            {
                return null;
            }
            return res.Element(rootName).Element(nodeName).Value;
        }


        /// <summary>
        /// 退款通知（不需验证签名。需要解密）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public RefundNotifyModel RefundNotify(HttpContext httpContext)
        {
            RefundNotifyModel result = new RefundNotifyModel();
            ResponseHandler resHandler = new ResponseHandler(httpContext);
            resHandler.Init();
            resHandler.SetKey(Key);

            string return_code = resHandler.GetParameter("return_code");
            string return_msg = resHandler.GetParameter("return_msg");

            string appid = resHandler.GetParameter("appid");
            string mch_id = resHandler.GetParameter("mch_id");
            string nonce_str = resHandler.GetParameter("nonce_str");
            string req_info = resHandler.GetParameter("req_info");

            result.ReturnCode = return_code ?? string.Empty;
            result.ReturnMsg = return_msg ?? string.Empty;
            result.AppId = appid ?? string.Empty;
            result.MchId = mch_id ?? string.Empty;
            result.NonceStr = nonce_str ?? string.Empty;
            result.ReqInfo = req_info ?? string.Empty;

        
            string key = Key.ToMd5();
            string xmlResult = AES.AESDecrypt(req_info, key);
            var res=XDocument.Parse(xmlResult);

            string transaction_id = GetXmlValue(res,"root", "transaction_id");
            string out_trade_no = GetXmlValue(res, "root", "out_trade_no");
            string out_refund_no = GetXmlValue(res, "root", "out_refund_no");
            string refund_id = GetXmlValue(res, "root", "refund_id");
            string total_fee = GetXmlValue(res, "root", "total_fee");
            string settlement_total_fee = GetXmlValue(res, "root", "settlement_total_fee");
            string refund_fee = GetXmlValue(res, "root", "refund_fee");
            string settlement_refund_fee = GetXmlValue(res, "root", "settlement_refund_fee");
            string refund_status = GetXmlValue(res, "root", "refund_status");
            string success_time = GetXmlValue(res, "root", "success_time");
            string refund_recv_accout = GetXmlValue(res, "root", "refund_recv_accout");
            string refund_account = GetXmlValue(res, "root", "refund_account");
            string refund_request_source = GetXmlValue(res, "root", "refund_request_source");

            result.TransactionId = transaction_id??string.Empty;
            result.OutRefundNo = out_refund_no??string.Empty;
            result.RefundId = refund_id??string.Empty;
            result.OutTradeNo = out_trade_no??string.Empty;
            if (string.IsNullOrEmpty(refund_fee))
            {
                result.RefundFee = 0;
            }
            else
            {
                result.RefundFee = Convert.ToInt32(refund_fee);
            }

            if (string.IsNullOrEmpty(total_fee))
            {
                result.TotalFee = 0;
            }
            else
            {
                result.TotalFee = Convert.ToInt32(total_fee);
            }

            result.RefundStatus = refund_status??string.Empty;
            result.SuccessTime = success_time??string.Empty;
            result.RefundRecvAccout = refund_recv_accout??string.Empty;
            result.RefundAccount = refund_account??string.Empty;
            result.RefundRequestSource = refund_request_source??string.Empty;

            return result;
        }

        /// <summary>
        /// 退款查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public RefundQueryResult RefundQuery(TenPayV3RefundQueryRequestData dataInfo)
        {
            return TenPayV3.RefundQuery(dataInfo);
        }
        /// <summary>
        /// 对账单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public string DownloadBill(TenPayV3DownloadBillRequestData dataInfo)
        {
            return TenPayV3.DownloadBill(dataInfo);
        }

        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="cert">证书绝对路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public  TransfersResult Transfers(TenPayV3TransfersRequestData dataInfo,int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.Transfers(dataInfo,Cert,Password,timeOut);
        }

        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="dataInfo">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        public  CloseOrderResult CloseOrder(TenPayV3CloseOrderRequestData dataInfo)
        {
            return TenPayV3.CloseOrder(dataInfo);
        }

        /// <summary>
        /// 撤销订单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param> 
        /// <param name="certPassword">证书密码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public  ReverseResult Reverse(TenPayV3ReverseRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.Reverse(dataInfo,Cert,Password,timeOut);
        }

        /// <summary>
        /// 短链接转换接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public  ShortUrlResult ShortUrl(TenPayV3ShortUrlRequestData dataInfo)
        {
            return TenPayV3.ShortUrl(dataInfo);
        }

        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public  GetTransferInfoResult GetTransferInfo(TenPayV3GetTransferInfoRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.GetTransferInfo(dataInfo,timeOut);
        }

        /// <summary>
        /// 刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static MicropayResult MicroPay(TenPayV3MicroPayRequestData dataInfo)
        {
            return TenPayV3.MicroPay(dataInfo);
        }


#region 异步接口
        /// <summary>
        /// 【异步方法】统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public  async Task<UnifiedorderResult> PayAsync(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.UnifiedorderAsync(dataInfo,timeOut).Result;
        }
#endregion
    }
}
