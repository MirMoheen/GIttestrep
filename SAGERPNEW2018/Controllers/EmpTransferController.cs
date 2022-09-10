using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class EmpTransferController : Controller
    {
        // GET: EmpTransfer
        [UserRightFilters]
        public ActionResult Index()
        {
            tblEmployeeTransfer a = new  tblEmployeeTransfer();
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);


            return View(a);
        }



        public ActionResult Save(tblEmployeeTransfer model)
        {
            try
            {
                if (model.ProjectID==model.fromProjectID)
                {
                    TempData["ActionMessage"] = false;
                    return RedirectToAction("Index", model);
                }
              
                model.EmpCode= new tblEmployee().genearteEmployeeNo(model.ProjectID, model.DepartmentID);

                model.EntryDate = DateTime.Now;
                model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                var result=   model.updatedata(model);
                if (result)
                {
                    TempData["ActionMessage"] = true;
                }
                else
                {

                    TempData["ActionMessage"] = false;

                }


                return RedirectToAction("Index", model);

            }
            catch (Exception ex)
            {

                return View("Index");
            }
        }

        public JsonResult loaddepartmentbyProject(int id)
        {
            var list = new tblEmployee().loadDepartment(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadDesginationbyDepartment(int id )
        {
            var list = new tblEmployee().LoadDesignation(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadEmployeebyDesgination(int id)
        {
            var list = new tblEmployee().loadEmployee(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}