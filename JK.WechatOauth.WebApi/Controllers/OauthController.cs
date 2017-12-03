using JK.WechatOauth.WebApi.Models.Oauth;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace JK.WechatOauth.WebApi.Controllers
{
    /// <summary>
    /// 微信授权代理
    /// </summary>
    [RoutePrefix("Oauth")]
    public class OauthController : ApiController
    {
        private ILog _log;
        public OauthController()
        {
            _log = LogManager.GetLogger(typeof(OauthController));
        }

        /// <summary>
        /// 想要进行微信授权的项目跳转到此地址
        /// 传入返回的页面地址
        /// 公众号登录
        /// </summary>
        /// <returns></returns>
        [Route("GetCode")]
        [HttpGet]
        public IHttpActionResult GetCode([FromUri]OauthViewModel model)
        {
            string redirectUrl = string.Empty;
            string oauthUrl = string.Empty;
            string appId = string.Empty;
            string state = "STATE";
            if (!string.IsNullOrEmpty(model.RedirectUrl))
            {
                redirectUrl = model.RedirectUrl;
            }

            if (!string.IsNullOrEmpty(model.State))
            {
                state = model.State;
            }
            try
            {
                //进行微信授权代理的网站地址(本网站地址)
                oauthUrl = WebConfigurationManager.AppSettings["OauthUrl"];
                appId = WebConfigurationManager.AppSettings["AppId"];
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                throw new ArgumentException("请在web.config 配置OauthUrl和AppId");
            }

            string url = $"{oauthUrl}Oauth?RedirectUrl={redirectUrl}";
            if (!string.IsNullOrEmpty(model.AppId))
            {
                appId = model.AppId;
            }
            var myRedirecturl = System.Web.HttpUtility.HtmlEncode(url);

            string result = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={myRedirecturl}&response_type=code&scope=snsapi_userinfo&state={state}#wechat_redirect";
            //没有出现授权页面，而是白屏，基本上是地址result 拼写错误，大小写，空格等。
            //尤其注意：由于授权操作安全等级较高，所以在发起授权请求时，
            //微信会对授权链接做正则强匹配校验，如果链接的参数顺序不对，授权页面将无法正常访问
            return Redirect(result);
        }


        /// <summary>
        /// 想要进行微信授权的项目跳转到此地址
        /// 传入返回的页面地址
        /// PC登录
        /// </summary>
        /// <returns></returns>
        [Route("GetCodeQr")]
        [HttpGet]
        public IHttpActionResult GetCodeQr([FromUri]OauthViewModel model)
        {
            string redirectUrl = string.Empty;
            string oauthUrl = string.Empty;
            string appId = string.Empty;
            string state = "STATE";
            if (!string.IsNullOrEmpty(model.RedirectUrl))
            {
                redirectUrl = model.RedirectUrl;
            }

            if (!string.IsNullOrEmpty(model.State))
            {
                state = model.State;
            }
            try
            {
                //进行微信授权代理的网站地址(本网站地址)
                oauthUrl = WebConfigurationManager.AppSettings["OauthUrl"];
                appId = WebConfigurationManager.AppSettings["AppId"];
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                throw new ArgumentException("请在web.config 配置OauthUrl和AppId");
            }

            string url = $"{oauthUrl}Oauth?RedirectUrl={redirectUrl}";
            if (!string.IsNullOrEmpty(model.AppId))
            {
                appId = model.AppId;
            }
            var myRedirecturl = System.Web.HttpUtility.HtmlEncode(url);
            string result = $"https://open.weixin.qq.com/connect/qrconnect?appid={appId}&redirect_uri={myRedirecturl}&response_type=code&scope=snsapi_login&state={state}#wechat_redirect";
            //没有出现授权页面，而是白屏，基本上是地址result 拼写错误，大小写，空格等。
            //尤其注意：由于授权操作安全等级较高，所以在发起授权请求时，
            //微信会对授权链接做正则强匹配校验，如果链接的参数顺序不对，授权页面将无法正常访问
            return Redirect(result);
        }

        /// <summary>
        /// 收到code后跳转到上面Action收到的跳转地址并返回code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index([FromUri]CodeViewModel model)
        {
            //跳转到State带上code
            var redirectUrl = ""; ;
            if (model.RedirectUrl.Contains("?"))
            {
                redirectUrl = $"{model.RedirectUrl}&code={model.Code}&state={model.State}";
            }
            else
            {
                redirectUrl = $"{model.RedirectUrl}?code={model.Code}&state={model.State}";
            }
            _log.Info("Index的RedirectUrl=" + redirectUrl);
            return Redirect(redirectUrl);
        }
    }
}
