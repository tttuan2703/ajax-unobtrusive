using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {//Control này em để hiển thị view chung cho 2 cái insert với loadAll(Bảng)
            Models.MVCTutorialEntities db = new Models.MVCTutorialEntities();
            Session["employee"] = null;
            List<Department> list = db.Departments.ToList();
            var listEmp = db.Employees.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");
            ViewBag.ListEmp = new SelectList(listEmp);
            return View();
        }

        public ActionResult Insert()
        {
            Models.MVCTutorialEntities db = new Models.MVCTutorialEntities();
            //Session["employee"] = null;
            List<Department> list = db.Departments.ToList();
            var listEmp = db.Employees.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");
            ViewBag.ListEmp = new SelectList(listEmp);
            return View();
        }

        [HttpPost]
        public ActionResult Insert(EmployeeModel model)
        {
            try
            {
                Employee emp = new Employee();
                Models.MVCTutorialEntities db = new Models.MVCTutorialEntities();
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
                    return RedirectToAction("About", "Home",model);
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult About(Employee emp)
        {
            //EmployeeModel emp = (EmployeeModel)Session["employee"];
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

        //public ActionResult About(EmployeeModel emp)
        //{
        //        return View(emp);
        //}

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