using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JK.Framework.Core;

namespace JK.Framework.Web.Filter
{
    /// <summary>
    /// 身份认证filter
    /// </summary>
    public class JKAuthorizeAttribute : AuthorizeAttribute
    {
        #region 判断 IIdentity.IsAuthenticated 是否通过

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthorizeException("用户未通过认证");
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }


    #region 自定义User实现UserBase（实现了IPrincipal）
    //public class UserModel : UserBase
    //{
    //    public Guid UserGuid { set; get; }
    //    public string UserName { set; get; }
    //    public string NickName { set; get; }

    //    public IList<UserMenuModel> UserMenuModels { set; get; }

    //    public UserModel(Guid userGuid, string userName, string nickName, IList<UserMenuModel> menus, Boolean isAuthenticated) : base(userName, "MMY", isAuthenticated)
    //    {
    //        UserGuid = userGuid;
    //        UserName = userName;
    //        NickName = nickName;
    //        UserMenuModels = menus;
    //    }
    //}
    #endregion

    #region 登录成功后将自定义User 赋给 HttpContext.User（IPrincipal）,并设置IIdentity.IsAuthenticated 设置为true（认证通过）
    //public ActionResult Login()
    //{
    //    UserModel userModel = new UserModel(account.Guid, account.UserName, account.NickName, menu, true) { };
    //    HttpContext.User = UserModel;

    //    return this.ResultSuccess();
    //}

    #endregion

    #region
    //结合session
    #endregion
}
