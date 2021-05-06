using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return PartialView();
        }
        public ActionResult OnGetPartial()
        {
            Thread.Sleep(2000);
            return new PartialViewResult
            {
                ViewName = "_HelloWorldPartial",
                ViewData = this.ViewData
            };
        }
    }
}