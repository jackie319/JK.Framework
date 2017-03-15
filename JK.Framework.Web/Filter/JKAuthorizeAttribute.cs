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
            bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (flag)
            {
                return;
            }
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthorizeException("用户未通过认证");
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }


    #region 自定义User(UserModel)实现UserBase（实现了IPrincipal）
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
    //[AllowAnonymous]
    //public ActionResult SubmitLogin(string userName, string nickName)
    //{
    //    string md5 = nickName.ToMd5();
    //    try
    //    {
    //        var account = _userAccount.Login(userName, md5);
    //        var menu = _authority.GetUserMenu(new Guid(), Guid.Empty);
    //        UserModel userModel = new UserModel(account.Guid, account.UserName, account.NickName, menu, true) { };
    //        HttpContext.User = userModel;
    //        Session["UserInfoModel"] = userModel;
    //    }
    //    catch (CommonException)
    //    {
    //        return this.ResultError("用户名或密码错误");
    //    }
    //    return this.ResultSuccess();
    //}

    #endregion
    #region Global中每次请求时把UserModel 赋值给 HttpContext.User 
    //public MvcApplication()
    //{
    //    PostAcquireRequestState += MvcApplicationPostAcquireRequestState;
    //}
    //protected void MvcApplicationPostAcquireRequestState(object sender, EventArgs e)
    //{
    //    var user = Session["UserInfoModel"];
    //    if (user is UserModel)
    //    {
    //        HttpContext.Current.User = user as UserModel;
    //    }
    //}
    #endregion
 
}
