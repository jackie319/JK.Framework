using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount
{
    public enum UserTypeEnum
    {
        /// <summary>
        /// 会员      
        /// </summary>
        Member,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin
    }

    public enum GenderEnum
    {
        /// <summary>
        ///0.未设置（保密）
        /// </summary>
        NotSet,
        /// <summary>
        /// 1.男
        /// </summary>
        Male,
        /// <summary>
        /// 2.女
        /// </summary>
        FeMale
    }

    public enum UserLoginSourceEnum
    {
        Wechat,
        MH5
    }
    public enum UserStatusEnum
    {

        Default,
    }
}
