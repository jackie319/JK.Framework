using JK.Framework.Alipay;
using JK.Framework.Alipay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JK.CommonApi.WebApi.Controllers
{
    public class AlipayController : Controller
    {
        // GET: Alipay
        public ActionResult Index()
        {
            ViewBag.Title = "Alipay Test";
            return View();
        }
        /// <summary>
        /// 网站支付
        /// </summary>
        public void PageTest()
        {
            //TODO：提取到业务方法
            ConmonRequestModel requestModel = new ConmonRequestModel();
            Pay alipay = new Pay(requestModel);
            PagePayRequestModel pagePayRequestModel = new PagePayRequestModel();
            pagePayRequestModel.Boby = "Iphone6 16G";
            pagePayRequestModel.Subject = "Iphone6 16G";
            pagePayRequestModel.OutTradeNo = Guid.NewGuid().ToString();
            pagePayRequestModel.TotalAmount = 88.88;
            pagePayRequestModel.ProductCode = "FAST_INSTANT_TRADE_PAY";
            var result = alipay.DefaultPay(pagePayRequestModel);
            Response.Write(result.Body);
        }
        /// <summary>
        /// 手机网站支付
        /// </summary>
        public void H5Test()
        {
            //TODO：提取到业务方法
            ConmonRequestModel requestModel = new ConmonRequestModel();
            Pay alipay = new Pay(requestModel);
            H5PayRequestModel defaultPayModel = new H5PayRequestModel();
            defaultPayModel.Boby = "Iphone6 16G";
            defaultPayModel.Sbuject = "Iphone6 16G";
            defaultPayModel.OutTrabeNo = Guid.NewGuid().ToString();
            defaultPayModel.TimeoutExpress = "90m";
            defaultPayModel.TotalAmount = 9.99;
            defaultPayModel.ProductCode = "QUICK_WAP_WAY";
            defaultPayModel.QuitUrl = "http://wwww.baidu.com";
            defaultPayModel.SellerId = "2016091500520768";
            var result = alipay.H5DefulatPay(defaultPayModel);
            Response.Write(result.Body);

        }
    }
}