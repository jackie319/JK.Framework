using System;
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
        /// 证书私钥
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
            string return_code = GetXmlValue(res, "return_code"); 
  
            string return_msg= GetXmlValue(res, "return_msg");
            string result_code= GetXmlValue(res, "result_code"); 
            string err_code = GetXmlValue(res, "err_code");
            string err_code_des = GetXmlValue(res, "err_code_des");
            string appid = GetXmlValue(res, "appid");
            string mch_id = GetXmlValue(res, "mch_id");
            string nonce_str = GetXmlValue(res, "nonce_str");
            string ResultSign = GetXmlValue(res, "ResultSign");
            string transaction_id = GetXmlValue(res, "transaction_id");
            string out_trade_no = GetXmlValue(res, "out_trade_no");
            string out_refund_no = GetXmlValue(res, "out_refund_no");
            string refund_id = GetXmlValue(res, "refund_id");
            string refund_fee = GetXmlValue(res, "refund_fee");
            string settlement_refund_fee = GetXmlValue(res, "settlement_refund_fee");
            string total_fee = GetXmlValue(res, "total_fee");

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

        private  string GetXmlValue(XDocument res,string nodeName)
        {
            if (res == null || res.Element("xml") == null
                || res.Element("xml").Element(nodeName) == null)
            {
                return null;
            }
            return res.Element("xml").Element(nodeName).Value;
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

            result.TransactionId = string.Empty;
            result.OutRefundNo = string.Empty;
            result.RefundId = string.Empty;
            result.OutTradeNo = string.Empty;
            result.TotalFee = 0;
            result.RefundFee = 0;
            result.RefundStatus = string.Empty;
            result.SuccessTime = string.Empty;
            result.RefundRecvAccout = string.Empty;
            result.RefundAccount = string.Empty;
            result.RefundRequestSource = string.Empty;

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
    }
}
