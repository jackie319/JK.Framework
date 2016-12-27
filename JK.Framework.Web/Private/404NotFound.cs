namespace JK.Framework.Web.Private
{
    internal class NotFound
    {
        //Global
        //protected void Application_error(object sender, EventArgs e)
        //{
        //    HttpException error = (HttpException)Server.GetLastError();
        //    if (error.GetHttpCode() == 404)
        //    {
        //        if (Session[Domain.Server._SessionName_] != null)
        //        {
        //            Response.Redirect("/Error/notFound");
        //        }
        //        else
        //        {
        //            //请求不存在的页面之前会话已过期
        //            //（触发新的错误，左侧菜单不能正常显示,解决：跳转到登录页面，让用户登录恢复会话）
        //            Response.Redirect("/Account/Login");
        //        }

        //    }
        //}
    }
}
