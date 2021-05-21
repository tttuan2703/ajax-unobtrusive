using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Insert()
        {
            return View();
        }
    }
}