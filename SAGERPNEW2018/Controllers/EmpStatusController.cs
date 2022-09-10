using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    [HandleExceptionFilter]
    public class EmpStatusController : Controller
    {
        // GET: EmpStatus

        [UserRightFilters]
        public ActionResult EmployeeStatus(tblEmployee a)
        {
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);

          //  a.ProbationEnd = DateTime.Now;


            return View(a);
        }
        public ActionResult Save(tblEmployee model)
        {
            try
            {
                bool check =false;

                if (model.EmployeeID > 0)
                {
                    model.ModifiedDatetime = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    check = model.UpdateconfirmData(model);

                }
              

                if (check)
                {
                    TempData["ActionMessage"] = true;
                    return RedirectToAction("EmployeeStatus");

                }
                TempData["ActionMessage"] = false;
                return RedirectToAction("EmployeeStatus");



            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("create");

            }
        }


        public JsonResult loadEmployeeBydepartment(int id)
        {

            var ProjectIDs = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
            var list = new tblEmployeeSalary().loadEmployee(id, ProjectIDs);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getemployeedata(int id)
        {
           

            var check = new tblEmployee().getEmployeeData(id );
            check.FormatLeftdate = check.LeftDate==null? null: check.LeftDate.Value.ToString("dd-MMM-yyyy");
            check.FormatProbationEnddate = check.ProbationEnd ==null?null: check.ProbationEnd.Value.ToString("dd-MMM-yyyy");
            check.FormatTerminatedate = check.TerminateDate==null ?null: check.TerminateDate.Value.ToString("dd-MMM-yyyy");




            return Json(check, JsonRequestBehavior.AllowGet);
        }
     
       
    }
}