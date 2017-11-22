using JK.CommonApi.WebApi.Models.PayModel;
using JK.WechatMP;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    /// <summary>
    /// 微信分享
    /// </summary>
    [RoutePrefix("WxShare")]
    public class WxShareController : ApiController
    {
        private IWxShare _WxShare;
        private ILog _log;
        public WxShareController(IWxShare wxshare)
        {
            _WxShare = wxshare;
            _log = LogManager.GetLogger(typeof(WxShareController));
        }

        [Route("")]
        [HttpPost]
        public WechatShareViewModel Index()
        {
            WechatShareViewModel model = new WechatShareViewModel();
            var url = "http://ynsufan.com/?";
            try
            {
                url = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch (Exception)
            {
            }

            _log.Info("分享地址：" + url.ToString());
            var jsPackage = _WxShare.GetJsSdkUiPackage(url);
            model = WechatShareViewModel.CopyFrom(jsPackage);
            return model;
        }
    }
}
