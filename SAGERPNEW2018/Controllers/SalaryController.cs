using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblEmployeeSalary a = new tblEmployeeSalary();
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
        public ActionResult create(tblEmployeeSalary a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);


                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }
        [UserRightFilters]
        public ActionResult delete(int id)
        {
            try
            {
                tblEmployeeSalary a = new tblEmployeeSalary();

                bool c = a.DeleteData(id);
                if (c)
                {
                    return RedirectToAction("Index");

                }
                TempData["Dependancy"] = "This Record Using In Another Table";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }


        }
        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            try
            {
                string[] IDo = id.Split('|');

                tblEmployeeSalary a = new tblEmployeeSalary();
                var obj = a.getAlldataByID(Convert.ToInt32(IDo[0]));
              var datadept=  a.getAllSalarydatabyProc(" and SalaryID=" + IDo[0]).FirstOrDefault();
                obj.DepartmentID =Convert.ToInt32( datadept.DepartmentID);
                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                obj.IsView = a.IsView;
                obj.BankID = Convert.ToInt32(IDo[0]);
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }

        [UserRightFilters]

        public ActionResult Save(tblEmployeeSalary model)
        {
            try
            {
                int check;

                if (model.SalaryID > 0)
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
                    check = model.addata(model);

                }

                if (check > 0)
                {
                    TempData["ActionMessage"] = true;
                    return RedirectToAction("Index");

                }
                TempData["ActionMessage"] = false;
                return RedirectToAction("create", model);



            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("create");

            }
        }

        public ActionResult Duplicate(string Name, string ID)
        {
            try
            {
                string json = "";
                var list = new tblEmployeeSalary().checkDuplicate(Convert.ToInt32(ID), Convert.ToInt32( Name));
                if (list.Count() > 0)
                {
                    json = " Duplicate Record Found ";
                }
                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(JsonConvert.SerializeObject("Error"), JsonRequestBehavior.AllowGet);

            }


        }



        public JsonResult loadEmployeeBydepartment(int id)
        {

            var ProjectIDs = Convert.ToInt32( new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
            var list = new tblEmployeeSalary().loadEmployee(id, ProjectIDs);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}