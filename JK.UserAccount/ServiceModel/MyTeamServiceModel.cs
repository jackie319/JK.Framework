using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.ServiceModel
{
    public class MyTeamServiceModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 直接推荐(二级推荐)
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public string TimeCreated { get; set; }
        public IList<MyTeamServiceModel> ChildrenModels { get; set; }

        public static MyTeamServiceModel CopyFrom(UserAccount user)
        {
            if (user == null) return null;
            return new MyTeamServiceModel
            {
                UserName = user.UserName,
                NickName = user.NickName,
                TimeCreated = user.TimeCreated.ToString("yyyy-MM-dd HH:mm:ss"),
                ChildrenModels = new List<MyTeamServiceModel>()
            };
        }
    }
}
