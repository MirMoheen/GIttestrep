using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class SalaryAllowanceRewiseController : Controller
    {
        // GET: SalaryAllowanceRewise
        [UserRightFilters]
        public ActionResult AllowanceRewise()
        {
            try
            {
                EmployeePlacement a = new EmployeePlacement();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                return View(a);
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        [UserRightFilters]

        public ActionResult Save(EmployeePlacement model)
        {
            try
            {
                bool check= false;

                {
                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    
                    check = model.UpdateAllowancePlacement(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

                }

                if (check )
                {
                    TempData["ActionMessage"] = true;
                    return RedirectToAction("AllowanceRewise");

                }
                TempData["ActionMessage"] = false;
                return RedirectToAction("AllowanceRewise");



            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("AllowanceRewise");

            }
        }


    }
}