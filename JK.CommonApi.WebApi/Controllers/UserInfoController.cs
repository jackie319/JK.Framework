using JK.CommonApi.Domain;
using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.UserAccount;
using JK.Framework.API.Filter;
using JK.Framework.API.Model;
using JK.Framework.Core;
using JK.Framework.Core.Caching;
using JK.JKUserAccount.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("UserInfo")]
    [ApiSessionAuthorize]
    public class UserInfoController : ApiController
    {
        private IUserAccount _UserAccount;
        private ICacheManager _cache;
        private ISms _sms;
        public UserInfoController(IUserAccount userAccount, ICacheManager cache, ISms sms)
        {
            _UserAccount = userAccount;
            _cache = cache;
            _sms = sms;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public UserInfoViewModel UserInfo()
        {
            var userModel = (UserModel)HttpContext.Current.User;
            var userInfo = _UserAccount.FindUserAccount(userModel.UserGuid);
            var result = UserInfoViewModel.CopyFrom(userInfo);
            return result;

        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel Update([FromBody]UserInfoViewModel model)
        {
            var mmyUser = (UserModel)HttpContext.Current.User;
            var entity = model.CopyTo();
            entity.Guid = mmyUser.UserGuid;
            entity.UserName = mmyUser.UserName;
            _UserAccount.UpdateUserInfo(entity);
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        [Route("UpdateAvatar")]
        [HttpPost]
        [ApiSessionAuthorize]
        [ApiValidationFilter]
        public ApiResultModel UpdateAvatar(string picUrl)
        {
            var mmyUser = (UserModel)HttpContext.Current.User;
            _UserAccount.UpdateAvatar(mmyUser.UserGuid, mmyUser.UserName, picUrl);
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [Route("ChangePassword")]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (!model.NewPasswordMd5Confirm.Equals(model.NewPasswordMd5))
                return this.ResultApiError("俩次输入的密码不匹配");
            try
            {
                var mmyUser = (UserModel)HttpContext.Current.User;
                _UserAccount.ChangePwd(mmyUser.UserName, model.OldPasswordMd5, model.NewPasswordMd5);
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }
    }
}
