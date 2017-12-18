using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JK.PictureCenter.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";

            //return View();

            return Redirect("~/apis/index");
        }


        public ActionResult test()
        {
            return View();
        }
    }
}
