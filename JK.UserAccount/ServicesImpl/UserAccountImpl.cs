using JK.Data.Model;
using JK.Frameword.Oauth.Wechat;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Extensions;
using JK.Framework.Oauth.Wechat;
using JK.JKUserAccount.IServices;
using JK.JKUserAccount.ServiceModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.ServicesImpl
{
    public class UserAccountImpl : IUserAccount
    {
        private IRepository<UserAccount> _userAccountRepository;
        private IRepository<UserAccountWechat> _userAccountWechatRepository;
        private IRepository<UserDeliveryAddress> _userDeliveryAddressRepository;
        private IRepository<UserLoginRecords> _userLoginRecordRepository;
        private WechatOauth _wechatOauth;
        private WechatOauth _wechatOpenOauth;
        private WechatJsCode _wechatJsCode;
        private UnifiedOrderSetting _setting;
        private readonly string _Salt = "_MMYPlatform";
        public UserAccountImpl(IRepository<UserAccount> useraccountRepository, IRepository<UserDeliveryAddress> userDeliveryAddressRepository, IRepository<UserAccountWechat> userAccountWechatRepository, IRepository<UserLoginRecords> userLoginRecordRepository)
        {
            _setting = new UnifiedOrderSetting();
            _userAccountRepository = useraccountRepository;
            _userDeliveryAddressRepository = userDeliveryAddressRepository;
            _userAccountWechatRepository = userAccountWechatRepository;
            _wechatOauth = new WechatOauth(_setting.AppId, _setting.AppSecret);
            _wechatOpenOauth = new WechatOauth("wxa8f539ab4579afe9", "1828eacb166348a5dbbfdf3e863aaac6");
            _wechatJsCode = new WechatJsCode(_setting.AppId, _setting.AppSecret);
            _userLoginRecordRepository = userLoginRecordRepository;
        }
        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Login(string userName, string password)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
            if (userAccount == null) throw new CommonException("用户不存在");
            var passwordSalt = password.ToMd5WithSalt(_Salt);
            if (!userAccount.Password.Equals(passwordSalt)) throw new CommonException("密码错误");
            userAccount.Password = "";
            return userAccount;
        }

        public UserAccountWechat FindUserAccountWechat(Guid userGuid)
        {
            return _userAccountWechatRepository.Table.FirstOrDefault(q => q.UserAccountGuid == userGuid);
        }
        /// <summary>
        /// 微信登录
        /// 获取微信信息
        ///对比信息，若存在，则更新UserAccountWechat
        ///若不存在，则新建UserAccount和UserAccountWechat
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public UserAccount WechatLogin(string code, Guid userGuid)
        {
            UserAccount account = new UserAccount();
            var codeResult = _wechatOauth.GetAccessToken(code);
            if (!string.IsNullOrEmpty(codeResult.errcode))
            {
                throw new CommonException($"获取AccesToken失败：errcode:{codeResult.errcode},errmsg:{codeResult.errmsg}");
            }
            var wechat = FindUserAccountWechat(codeResult.openid);
            if (wechat == null)
            {
                var userInfo = _wechatOauth.GetUserInfo(codeResult.access_token, codeResult.openid);
                if (!string.IsNullOrEmpty(userInfo.errcode))
                {
                    throw new CommonException($"获取wechat用户信息失败：errcode:{userInfo.errcode},errmsg:{userInfo.errmsg}");
                }
                account = CreateUserByWechat(userInfo, userGuid);
            }
            else
            {
                account = _userAccountRepository.Table.FirstOrDefault(q => q.Guid == wechat.UserAccountGuid && !q.IsDeleted);
                //TODO:更新
            }
            account.Password = "";
            return account;
        }

        public UserAccount WechatOpenLogin(string code, Guid userGuid)
        {
            UserAccount account = new UserAccount();
            var codeResult = _wechatOpenOauth.GetAccessToken(code);
            if (!string.IsNullOrEmpty(codeResult.errcode))
            {
                throw new CommonException($"获取AccesToken失败：errcode:{codeResult.errcode},errmsg:{codeResult.errmsg}");
            }
            var wechat = FindUserAccountWechat(codeResult.openid);
            if (wechat == null)
            {
                var userInfo = _wechatOpenOauth.GetUserInfo(codeResult.access_token, codeResult.openid);
                if (!string.IsNullOrEmpty(userInfo.errcode))
                {
                    throw new CommonException($"获取wechat用户信息失败：errcode:{userInfo.errcode},errmsg:{userInfo.errmsg}");
                }
                account = CreateUserByWechat(userInfo, userGuid);
            }
            else
            {
                account = _userAccountRepository.Table.FirstOrDefault(q => q.Guid == wechat.UserAccountGuid && !q.IsDeleted);
                //TODO:更新
            }
            account.Password = "";
            return account;
        }

        /// <summary>
        /// 微信小程序登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserAccount WechatJscodeLogin(WxJscodeLoginModel model)
        {
            UserAccount account = new UserAccount();
            SessionKeyResult result = _wechatJsCode.GetSessionKey(model.Code);
            if (!string.IsNullOrEmpty(result.errcode))
            {
                throw new CommonException($"获取session_key失败：errcode:{result.errcode},errmsg:{result.errmsg}");
            }
            var wechat = FindUserAccountWechat(result.openid);
            if (wechat == null)
            {
                UserInfoResult userInfo = new UserInfoResult();
                userInfo.city = model.City ?? string.Empty;
                userInfo.country = model.country ?? string.Empty;
                userInfo.headimgurl = model.AvatarUrl;
                userInfo.nickname = model.NickName;
                userInfo.openid = result.openid;
                userInfo.unionid = result.unionid ?? string.Empty;
                userInfo.sex = model.Gender;
                account = CreateUserByWechat(userInfo, model.UserGuid);
            }
            else
            {
                account = _userAccountRepository.Table.FirstOrDefault(q => q.Guid == wechat.UserAccountGuid && !q.IsDeleted);
                //TODO:更新
            }
            account.Password = "";
            return account;
        }

        /// <summary>
        /// 判断微信用户有没有登录过系统。TODO：此处应改用unionId判断，而不是openId
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public UserAccountWechat FindUserAccountWechat(string openId)
        {
            return _userAccountWechatRepository.Table.FirstOrDefault(q => q.WechatOpenId.Equals(openId));
        }
        private UserAccount CreateUserByWechat(UserInfoResult userinfo, Guid rUserGuid)
        {
            Guid userGuid = Guid.NewGuid();
            UserAccount account = new UserAccount();
            account.Guid = userGuid;
            account.AvatarImgUrl = userinfo.headimgurl;
            account.UserType = UserTypeEnum.Member.ToString();
            account.Birthday = DateTime.MinValue;
            account.CountVisited = 0;
            account.Email = string.Empty;
            switch (userinfo.sex)
            {
                case "1":
                    account.Gender = GenderEnum.Male.ToString();
                    break;
                case "2":
                    account.Gender = GenderEnum.FeMale.ToString();
                    break;
                default:
                    account.Gender = GenderEnum.NotSet.ToString();
                    break;
            }
            account.IPv4LastVisited = string.Empty;
            account.IsDeleted = false;
            account.IsEmailValidated = false;
            account.IsMobilePhoneValidated = false;
            account.UserName = CreateUserName();
            account.MobilePhone = string.Empty;
            account.NickName = userinfo.nickname;
            account.Password = "25d55ad283aa400af464c76d713c07ad".ToMd5WithSalt(_Salt);//12345678
            account.TimeCreated = DateTime.Now;
            account.TimeLastVisited = DateTime.Now;
            account.Status = UserStatusEnum.Default.ToString();
            _userAccountRepository.Insert(account);

            UserAccountWechat wechat = new UserAccountWechat();
            wechat.Guid = Guid.NewGuid();
            wechat.UserAccountGuid = userGuid;
            wechat.WechatOpenId = userinfo.openid;
            wechat.WechatNickName = userinfo.nickname;
            wechat.WechatAvatarImgUrl = userinfo.headimgurl;
            wechat.Unionid = userinfo.unionid ?? string.Empty;
            var privilege = JsonConvert.SerializeObject(userinfo.privilege);
            wechat.Privilege = privilege ?? string.Empty;
            wechat.Province = userinfo.province ?? string.Empty;
            wechat.City = userinfo.city ?? string.Empty;
            wechat.Country = userinfo.country ?? string.Empty;
            wechat.Sex = userinfo.sex ?? string.Empty;
            if (rUserGuid != null && rUserGuid != Guid.Empty)
            {
                wechat.RecommenderGuid = rUserGuid;
            }
            else
            {
                wechat.RecommenderGuid = Guid.Empty;
            }
            wechat.SessionKey = string.Empty;
            wechat.TimeCreated = DateTime.Now;
            _userAccountWechatRepository.Insert(wechat);
            return account;
        }

        private string CreateUserName()
        {
            string date = DateTime.Now.ToString("yyMMddHHmmss");
            //种子精确到百纳秒级别
            int ram = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).Next(100000, 999999);
            return string.Format("mmy_" + date + ram);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userAccount"></param>
        public void UpdateUserInfo(UserAccount userAccount)
        {
            var entity =
                _userAccountRepository.Table.FirstOrDefault(
                    q => q.UserName == userAccount.UserName && q.Guid == userAccount.Guid);
            if (entity == null) throw new CommonException("用户不存在");
            entity.NickName = userAccount.NickName;
            entity.Birthday = userAccount.Birthday;
            entity.Email = userAccount.Email ?? string.Empty;
            entity.Gender = userAccount.Gender;
            entity.AvatarImgUrl = userAccount.AvatarImgUrl;
            entity.MobilePhone = userAccount.MobilePhone;
            _userAccountRepository.Update(entity);
        }

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="userAccount"></param>
        public void UpdateAvatar(Guid userGuid, string userName, string avatarUrl)
        {
            var entity =
               _userAccountRepository.Table.FirstOrDefault(
                   q => q.UserName == userName && q.Guid == userGuid);
            if (entity == null) throw new CommonException("用户不存在");
            entity.AvatarImgUrl = avatarUrl;
            _userAccountRepository.Update(entity);
        }

        public UserAccount FindUserAccount(Guid userAccountGuid)
        {

            return _userAccountRepository.Table.FirstOrDefault(q => q.Guid == userAccountGuid && !q.IsDeleted);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassowrdMd5"></param>
        /// <param name="newPasswordMd5"></param>
        public void ChangePwd(string userName, string oldPassowrdMd5, string newPasswordMd5)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => !q.IsDeleted && q.UserName.Equals(userName));
            if (userAccount == null) throw new CommonException("用户不存在");
            var passwordSalt = oldPassowrdMd5.ToMd5WithSalt(_Salt);
            if (!userAccount.Password.Equals(passwordSalt)) throw new CommonException("原密码错误");
            var newPasswordSalt = newPasswordMd5.ToMd5WithSalt(_Salt);
            userAccount.Password = newPasswordSalt;
            _userAccountRepository.Update(userAccount);
        }

        /// <summary>
        /// 更新sessionKey
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="sessionKey"></param>
        public void UpdateSessionKey(Guid userGuid, string sessionKey)
        {
            var entity = _userAccountWechatRepository.Table.FirstOrDefault(q => q.UserAccountGuid == userGuid);
            entity.SessionKey = sessionKey;
            _userAccountWechatRepository.Update(entity);
        }

        /// <summary>
        /// 登录记录
        /// </summary>
        /// <param name="record"></param>
        public void UserLoginRecord(UserLoginRecords record)
        {
            record.Guid = Guid.NewGuid();
            record.TimeCreated = DateTime.Now;
            _userLoginRecordRepository.Insert(record);
        }

        public UserAccountWechat FindUserAccount(string sessionKey)
        {
            return _userAccountWechatRepository.Table.FirstOrDefault(q => q.SessionKey.Equals(sessionKey));
        }



        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Boolean IsUserNameExist(string userName)
        {
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => q.UserName.Equals(userName));
            if (userAccount == null) return false;
            return true;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        /// <exception cref="CommonException">CommonException</exception>
        public void Register(string mobilePhone, string password, string smsCode)
        {
            if (IsUserNameExist(mobilePhone)) throw new CommonException("用户已存在");
            //TODO:先调用匹配验证码方法
            UserAccount account = new UserAccount();
            account.Guid = Guid.NewGuid();
            account.AvatarImgUrl = string.Empty;
            account.UserType = UserTypeEnum.Member.ToString();
            account.Birthday = DateTime.MinValue;
            account.CountVisited = 0;
            account.Email = string.Empty;
            account.Gender = GenderEnum.NotSet.ToString();
            account.IPv4LastVisited = string.Empty;
            account.IsDeleted = false;
            account.IsEmailValidated = false;
            account.IsMobilePhoneValidated = true;
            account.UserName = mobilePhone;
            account.MobilePhone = mobilePhone;
            account.NickName = mobilePhone;
            account.Password = password.ToMd5WithSalt(_Salt);
            account.TimeCreated = DateTime.Now;
            account.TimeLastVisited = DateTime.Now;
            account.Status = UserStatusEnum.Default.ToString();
            _userAccountRepository.Insert(account);
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <param name="smsCode"></param>
        public void GetBackPassword(string mobilePhone, string password, string smsCode)
        {
            //TODO:先调用匹配验证码方法
            var userAccount = _userAccountRepository.Table.FirstOrDefault(q => q.UserName.Equals(mobilePhone));
            if (userAccount == null) throw new CommonException("用户不存在");
            userAccount.Password = password.ToMd5WithSalt(_Salt);
            _userAccountRepository.Update(userAccount);
        }

        /// <summary>
        /// 我的收货地址
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public IList<UserDeliveryAddress> GetList(Guid userGuid, int skip, int take, out int total)
        {
            var query = _userDeliveryAddressRepository.Table.Where(q => !q.IsDeleted && q.UserGuid == userGuid);
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public UserDeliveryAddress FinDeliveryAddress(Guid addressGuid)
        {
            return _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
        }

        public void AddDeliveryAddress(UserDeliveryAddress userDeliveryAddress)
        {
            userDeliveryAddress.TimeCreated = DateTime.Now;
            userDeliveryAddress.Guid = Guid.NewGuid();
            userDeliveryAddress.IsDeleted = false;
            if (userDeliveryAddress.IsDefault)
            {
                CancleDefaultDeliveryAddress();
            }
            _userDeliveryAddressRepository.Insert(userDeliveryAddress);
        }

        public void UpdateDeliveryAddress(UserDeliveryAddress userDeliveryAddress)
        {
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == userDeliveryAddress.Guid && !q.IsDeleted);
            if (entity == null) throw new CommonException("此收货地址不存在");
            entity.IsDefault = userDeliveryAddress.IsDefault;
            if (entity.IsDefault)
            {
                CancleDefaultDeliveryAddress();
            }
            entity.Address = userDeliveryAddress.Address;
            entity.Phone = userDeliveryAddress.Phone;
            entity.ReceiverName = userDeliveryAddress.ReceiverName;
            entity.Region = userDeliveryAddress.Region;
            entity.ZipCode = userDeliveryAddress.ZipCode;
            _userDeliveryAddressRepository.Update(entity);
        }

        /// <summary>
        /// 设置默认收货地址
        /// </summary>
        /// <param name="addressGuid"></param>
        public void SetDefaultDeliveryAddress(Guid addressGuid)
        {

            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
            if (entity == null) throw new CommonException("此收货地址不存在");
            CancleDefaultDeliveryAddress();
            entity.IsDefault = true;
            _userDeliveryAddressRepository.Update(entity);
        }


        private void CancleDefaultDeliveryAddress()
        {
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.IsDefault && !q.IsDeleted);
            if (entity == null) return;
            entity.IsDefault = false;
            _userDeliveryAddressRepository.Update(entity);
        }

        public void DeleteDeliveryAddress(Guid addressGuid)
        {
            var entity = _userDeliveryAddressRepository.Table.FirstOrDefault(q => q.Guid == addressGuid && !q.IsDeleted);
            if (entity == null) throw new CommonException("此收货地址不存在");
            entity.IsDeleted = true;
            _userDeliveryAddressRepository.Update(entity);
        }

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
        public IList<UserAccount> GetList(string userName, UserTypeEnum? userType, Boolean? isCertified, int skip, int take, out int total)
        {
            var query = _userAccountRepository.Table.Where(q => !q.IsDeleted);
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(q => q.UserName.Contains(userName) || q.MobilePhone.Contains(userName));
            }
            if (userType != null)
            {
                query = query.Where(q => q.UserType.Equals(userType.ToString()));
            }
            //if (isCertified != null)
            //{
            //    query = query.Where(q => q.IsCertified == isCertified);
            //}
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// 我的团队(树型结构，可控制层级)
        /// 无限级递归的分页处理??
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<MyTeamServiceModel> GetMyTeam(string userName)
        {
            IList<MyTeamServiceModel> models = new List<MyTeamServiceModel>();
            var childrenModels = GetUserListByRecommender(userName);
            if (childrenModels.Count() > 0)
            {
                foreach (var item in childrenModels)
                {
                    int grade = 1;
                    var model = MyTeamServiceModel.CopyFrom(item);
                    model.Grade = grade;
                    if (grade <= 2)//控制层级
                    {
                        model.ChildrenModels = GetMyTeam(item.UserName);
                        grade += 1;
                    }

                    models.Add(model);
                }
            }
            return models;
        }
        /// <summary>
        /// 某用户推荐的所有用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<UserAccount> GetUserListByRecommender(string userName)
        {
            var list = _userAccountRepository.Table.Where(q => q.Recommender.Equals(userName) && !q.IsDeleted).OrderByDescending(q => q.TimeCreated);
            return list.ToList();
        }

    }
}
