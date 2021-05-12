using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
            Models.MVCTutorialEntities db = new Models.MVCTutorialEntities();
        public ActionResult Insert()
        {//Control này em để hiển thị view chung cho 2 cái insert với loadAll(Bảng)
            //Session["employee"] = null;
            List<Department> list = db.Departments.ToList();
            var listEmp = db.Employees.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");
            //ViewBag.ListEmp = new SelectList(listEmp);
            return View();
        }

        //public ActionResult Insert()
        //{
        //    Models.MVCTutorialEntities db = new Models.MVCTutorialEntities();
        //    Session["employee"] = null;
        //    List<Department> list = db.Departments.ToList();
        //    var listEmp = db.Employees.ToList();
        //    ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");
        //    ViewBag.ListEmp = new SelectList(listEmp);
        //    return PartialView();
        //}

        [HttpPost]
        public ActionResult Insert(EmployeeModel model)
        {
                Employee emp = new Employee();
                List<Department> list = db.Departments.ToList();
                ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

                if (ModelState.IsValid)
                {   
                    emp.Address = model.Address;
                    emp.Name = model.Name;
                    emp.DepartmentId = model.DepartmentId;

                    db.Employees.Add(emp);
                    db.SaveChanges();

                    model.EmployeeId = emp.EmployeeId;

                    int latestEmpId = emp.EmployeeId;
                    var it = model;
                    Session["employee"] = it;
                    Site site = new Site();
                    site.SiteName = model.SiteName;
                    site.EmployeeId = latestEmpId;
                    db.Sites.Add(site);
                    db.SaveChanges();
                return RedirectToAction("About","Home",emp);
                //return Red("/Home/About");
            }
                else
                {
                    return View();
                }
                //return View();
        }
        public ActionResult About()
        {
            EmployeeModel emp = (EmployeeModel)Session["employee"];
            if (emp != null)
            {
                ViewBag.EmployeeId = emp.EmployeeId;
                ViewBag.DepartmentId = emp.DepartmentId;
                ViewBag.Name = emp.Name;
                ViewBag.Address = emp.Address;
            return View(emp);
            }
            return View();
        }
        [HttpPost]
        public ActionResult About(EmployeeModel emp)
        {
            if (emp.DepartmentId != null)
            {
                ViewBag.EmployeeId = emp.EmployeeId;
                ViewBag.DepartmentId = emp.DepartmentId;
                ViewBag.Name = emp.Name;
                ViewBag.Address = emp.Address;
            }
            //return View(emp);
            return View(emp);
        }

        public ActionResult LoadIndex()
        {
            return PartialView();
        }
        public ActionResult LoadAll()
        {
            return PartialView();
        }
    }
}