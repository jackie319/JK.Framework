using JK.Authority.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Authority.IServices
{
    public interface IAuthority
    {
        /// <summary>
        /// 用户所属角色的所有权限
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        IList<UserMenuModel> GetUserMenu(Guid roleGuid, Guid parentGuid);

        IList<UserMenuModel> GetUserMenuForAdmin(Guid roleGuid, Guid parentGuid);
    }
}
