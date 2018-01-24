using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Data.Model;
using JK.JKUserAccount.ServiceModel;

namespace JK.JKUserAccount.IServices
{
    public interface IUserAccount
    {
        UserAccount Login(string userName, string password);
        UserAccount FindUserAccount(Guid userAccountGuid);

        void ChangePwd(string userName, string oldPassowrdMd5, string newPasswordMd5);

        UserAccountWechat FindUserAccount(string sessionKey);
        void UpdateSessionKey(Guid userGuid, string sessionKey);
        void UserLoginRecord(UserLoginRecords record);
        Boolean IsUserNameExist(string userName);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        /// <exception cref="CommonException">CommonException</exception>
        void Register(string mobilePhone, string password, string smsCode);

        void GetBackPassword(string mobilePhone, string password, string smsCode);

        void UpdateUserInfo(UserAccount userAccount);
        void UpdateAvatar(Guid userGuid, string userName, string avatarUrl);
        IList<UserDeliveryAddress> GetList(Guid userGuid, int skip, int take, out int total);
        UserDeliveryAddress FinDeliveryAddress(Guid addressGuid);
        void AddDeliveryAddress(UserDeliveryAddress userDeliveryAddress);
        void UpdateDeliveryAddress(UserDeliveryAddress userDeliveryAddress);
        void SetDefaultDeliveryAddress(Guid addressGuid);
        void DeleteDeliveryAddress(Guid addressGuid);

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        UserAccount WechatLogin(string code, Guid userGuid);
        UserAccount WechatOpenLogin(string code, Guid userGuid);
        UserAccount WechatJscodeLogin(WxJscodeLoginModel model);
        UserAccountWechat FindUserAccountWechat(Guid userGuid);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userType"></param>
        /// <param name="isCertified"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<UserAccount> GetList(string userName, UserTypeEnum? userType, Boolean? isCertified, int skip, int take, out int total);
        IList<MyTeamServiceModel> GetMyTeam(string userName);
    }
}
