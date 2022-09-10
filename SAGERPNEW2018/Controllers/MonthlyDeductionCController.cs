using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class MonthlyDeductionCController : Controller
    {
        // GET: MonthlyDeductionC
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                MonthlyDeduction a = new MonthlyDeduction();
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

        public ActionResult Edit(string id)
        {
            try
            {
                string[] IDo = id.Split('|');

                MonthlyDeduction a = new MonthlyDeduction();
                var obj = a.getAlldataByID(Convert.ToInt32(IDo[0]));
   
                ViewData["Editmode"] = true;
                if (IDo[1] == "0")
                {
                    obj.IsView = true;
                }
               
                obj.DepartmentTableComboJson = JsonConvert.SerializeObject(a.LoadAllDepartment(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)));
                obj.DeductionTableComboJson = JsonConvert.SerializeObject(a.LoadAllDeduction(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)));


                obj.MonthlyDeductionDetaillist = a.getDetailData(Convert.ToInt32(IDo[0]));
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }
        [UserRightFilters]
        public ActionResult create(MonthlyDeduction a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                a.DeductionMonth = DateTime.Now;
                a.DepartmentTableComboJson = JsonConvert.SerializeObject(a.LoadAllDepartment(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)));
                a.DeductionTableComboJson = JsonConvert.SerializeObject(a.LoadAllDeduction(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)));
                a.MonthlyDeductionDetaillist = a.getDetailData(-1);
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }
        [UserRightFilters]

        public ActionResult Save(MonthlyDeduction model)
        {
            try
            {
                int check=0;

                if (model.MonthlyDeductionID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                   check = check = model.UpdateData(model);
                }
                else
                {
                    
                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.ProjectID = Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                    check = model.saveData(model);
                }

                if (check > 0)
                {
                    TempData["ActionMessage"] = true;

                    return RedirectToAction("Index");

                }
                return RedirectToAction("create", model);

            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("create");
            }
        }

        public ActionResult GetDDListData(int departmentid)
        {
            MonthlyDeduction a = new MonthlyDeduction();
            string JsonResult = "";
            JsonResult = JsonConvert.SerializeObject(a.LoadAllemployees(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs), departmentid));
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}