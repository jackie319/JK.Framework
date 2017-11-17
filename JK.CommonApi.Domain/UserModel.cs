using JK.Data.Model;
using JK.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.CommonApi.Domain
{
    public class UserModel : UserBase
    {
        public Guid UserGuid { set; get; }
        public string UserName { set; get; }

        public string OpenId { get; set; }

        public string MobilePhone { set; get; }
        public string NickName { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarImgUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public System.DateTime Birthday { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        public UserModel(UserAccount userAccount, string userName, Boolean isAuthenticated, string openId = "") : base(userName, "MMY", isAuthenticated)
        {
            UserGuid = userAccount.Guid;
            UserName = userAccount.UserName;
            MobilePhone = userAccount.MobilePhone;
            NickName = userAccount.NickName;
            AvatarImgUrl = userAccount.AvatarImgUrl;
            Gender = userAccount.Gender;
            Birthday = userAccount.Birthday;
            Email = userAccount.Email;
            OpenId = openId;
        }
    }
}
