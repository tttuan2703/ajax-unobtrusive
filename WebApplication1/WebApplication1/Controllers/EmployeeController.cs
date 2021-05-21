using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        QL_NhanSuEntities db = new QL_NhanSuEntities();
        // GET: Employee
        public ActionResult Home()
        {
            List<bool> bl = new List<bool> { true, false };
            //return View(db.tbl_Employee.OrderByDescending(nv=>nv.Id).Take(5).ToList());
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            return View(db.tbl_Employee.OrderByDescending(nv => nv.Id).ToList());
        }
        
        public tbl_Employee timNhanVien(int? id)
        {
            tbl_Employee emp = new tbl_Employee();
            try
            {
                emp = db.tbl_Employee.Single(t => t.Id == id);
               return emp;
            }
            catch
            {
                return null;
            }
        }
        
        //public ActionResult Home(int length)
        //{
        //    List<bool> bl = new List<bool> { true, false };
        //    ViewBag.statusFlag = new SelectList(bl);
        //    return View(db.tbl_Employee.OrderByDescending(nv => nv.Id).Take(length).ToList());
        //}

        public ActionResult Insert()
        {
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            var a = ViewBag.DeptId;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(tbl_Employee emp)
        {
            try
            {
                var errors = new List<string>();
                ViewData["mess"] = errors;
                ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
                if (emp.Name == null)
                {
                    ViewBag.LoiTen = "Tên nhân viên còn thiếu! ";
                }
                else
                {
                    foreach (tbl_Employee e in db.tbl_Employee)
                    {
                        if (emp.Name.Equals(e.Name))
                            ViewBag.LoiTen = "Trùng tên nhân viên. ";
                    }
                }
                if (emp.DeptId == null)
                {
                    ViewBag.LoiDept = " Mã phòng ban còn thiếu! ";
                }
                if (emp.Gender == null)
                {
                    ViewBag.LoiGD = " Giới tính còn thiếu!";
                }
                if (emp.statusflag == null)
                {
                    ViewBag.LoiGD = " Trạng thái còn thiếu!";
                    IEnumerable<tbl_Deparment> tbl = db.tbl_Deparment.ToList();
                }
                bool flag = true;
                if (emp.Image != null)
                {
                    string[] kt = emp.Image.Split('.');
                    for (int i = 0; i < kt.Length; i++)
                    {
                        if (!kt[kt.Length - 1].Equals("jpg") && !kt[kt.Length - 1].Equals("png"))
                        {
                            flag = false;
                            ViewBag.LoiImage = " Image không phù hợp!";
                            return View();
                        }
                    }
                }
                //db.tbl_Employee.Add(emp);
                //db.SaveChanges();
                //return RedirectToAction("ShowDeTailEmp", "Employee", emp);
                if (emp.Name != null && emp.DeptId != null && emp.Gender != null && flag==true)
                {
                    db.tbl_Employee.Add(emp);
                    db.SaveChanges();
                    return RedirectToAction("AccessEmp", "Employee", emp);
                }
                return View(emp);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AccessEmp(tbl_Employee emp)
        {
            return View(emp);
        }
        public ActionResult EditEmp(int id)
        {
            tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            if (emp == null)
                return HttpNotFound();
            return PartialView(emp);
        }

        [HttpPost]
        public ActionResult EditEmp(tbl_Employee emp)
        {
            try
            {
                ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
                if (ModelState.IsValid)
                {
                    db.tbl_Employee.Attach(emp);
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AccessEmp", emp);
                }
                return PartialView(emp);
            }
            catch { 
                return PartialView(emp);
            }
        }
        public ActionResult AccessUpdate(int Id, string name, string gender, string city, string image, bool flag, int deptId)
        {
            tbl_Employee emp = new tbl_Employee();
            if (ModelState.IsValid)
            {
                emp.Id = Id;
                emp.Name = name;
                emp.Gender = gender;
                emp.City = city;
                emp.Image = image;
                emp.statusflag = flag;
                emp.DeptId = deptId;
                db.tbl_Employee.Attach(emp);
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
            return PartialView(emp);
        }
        //public ActionResult DeleteEmp(int id = 0)
        //{
        //    tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
        //    if (emp == null)
        //        return HttpNotFound();
        //    return View(emp);
        //}
        //[HttpPost, ActionName("DeleteEmployee")]
        public ActionResult DeleteEmployeeConfirm(int id = 0)
        {
            //Lấy nhân viên
            tbl_Employee emp = db.tbl_Employee.Single(d => d.Id == id);
            if (emp == null) {
                return HttpNotFound();
            }
            db.Entry(emp).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Home", "Employee");
        }
        //public ActionResult fullCollect()
        //{
        //    string search = null;
        //    int? i = null; 
        //    return View(db.tbl_Employee.Where(x => x.Name.StartsWith(search) || search == null ).ToList().ToPagedList(i ?? pageIndex, pageSize));
        //}
        [HttpGet]
        public ActionResult Index(string search, int? i)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = i.HasValue ? Convert.ToInt32(i) : 1;
            ViewBag.CurrentSort = search;
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            List<tbl_Employee> list = db.tbl_Employee.ToList();
            return View(db.tbl_Employee.Where(x => x.Name.Contains(search) || search == null && x.statusflag != false).ToList().ToPagedList(i ?? pageIndex, pageSize));
        }
        public ActionResult ShowALL(string search, int? i)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = i.HasValue ? Convert.ToInt32(i) : 1;
            ViewBag.CurrentSort = search;
            ViewBag.DeptId = new SelectList(db.tbl_Deparment, "DeptId", "Name");
            List<tbl_Employee> list = db.tbl_Employee.ToList();
            return View(db.tbl_Employee.Where(x => x.Name.Contains(search) || search == null).ToList().ToPagedList(i ?? pageIndex, pageSize));
        }
        //public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        //{
        //    int pageSize = 5;
        //    int pageIndex = 1;
        //    pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
        //    ViewBag.CurrentSort = sortOrder;
        //    sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
        //    IPagedList<tbl_Employee> employees = null;
        //    switch (sortOrder)
        //    {
        //        case "Id":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.Id).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.Id).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "Name":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.Name).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.Name).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "Gender":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.Gender).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.Gender).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "City":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.City).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.City).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "Image":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.Image).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.Image).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "DeptId":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.DeptId).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.DeptId).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "statusFlag":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.statusflag).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.statusflag).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "Default":
        //            if (sortOrder.Equals(CurrentSort))
        //                employees = db.tbl_Employee.OrderByDescending(m => m.Id).ToPagedList(pageIndex, pageSize);
        //            else
        //                employees = db.tbl_Employee.OrderBy(m => m.Id).ToPagedList(pageIndex, pageSize);
        //            break;
        //    }
        //    return View(employees);
        //}
    }
}