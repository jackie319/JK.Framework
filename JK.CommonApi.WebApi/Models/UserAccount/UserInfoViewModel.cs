using JK.JKUserAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.UserAccount
{
    public class UserInfoViewModel
    {

        public Guid UserGuid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "昵称必填")]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Required(ErrorMessage = "头像必填")]
        public string AvatarImgUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别必填")]
        public GenderEnum Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号必填")]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        public Data.Model.UserAccount CopyTo()
        {
            Data.Model.UserAccount account = new Data.Model.UserAccount();
            account.NickName = NickName;
            account.AvatarImgUrl = AvatarImgUrl;
            account.Gender = Gender.ToString();
            account.Birthday = Convert.ToDateTime(Birthday);
            account.MobilePhone = MobilePhone;
            account.Email = Email;
            return account;
        }

        public static UserInfoViewModel CopyFrom(Data.Model.UserAccount entity)
        {
            UserInfoViewModel model = new UserInfoViewModel();
            model.UserGuid = entity.Guid;
            model.NickName = entity.NickName;
            if (!string.IsNullOrEmpty(entity.AvatarImgUrl))
            {
                if (entity.AvatarImgUrl.Contains("http"))
                {
                    model.AvatarImgUrl = entity.AvatarImgUrl;
                }
                else
                {
                    model.AvatarImgUrl = AppSetting.Instance().PictureUrl+ entity.AvatarImgUrl;
                }
            }

            model.Gender = CovertToGenderEnum(entity.Gender);
            model.MobilePhone = entity.MobilePhone;
            model.Birthday = entity.Birthday.ToString("yyyy-MM-dd");
            model.Email = entity.Email;
            return model;
        }

        public static GenderEnum CovertToGenderEnum(string gender)
        {
            if (gender.Equals(GenderEnum.FeMale.ToString())) return GenderEnum.FeMale;
            if (gender.Equals(GenderEnum.Male.ToString())) return GenderEnum.Male;
            return GenderEnum.NotSet;
        }
    }
}