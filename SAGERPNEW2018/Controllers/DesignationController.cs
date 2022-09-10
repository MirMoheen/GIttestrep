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
    public class DesignationController : Controller
    {
        // GET: Designation
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblDesignation a = new tblDesignation();
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
        public ActionResult create(tblDesignation a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                a.DesignationCode = a.geneartedesginationCode(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs), Convert.ToInt32(a.DepartmentID));


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
                tblDesignation a = new tblDesignation();

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

                tblDesignation a = new tblDesignation();
                var obj = a.getAlldataByID(Convert.ToInt32(IDo[0]));
                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                obj.IsView = a.IsView;
                obj.DepID = obj.DepartmentID.ToString() ;
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }
        public JsonResult loadDepartments(int id)
        {
            var list = new tblDesignation().LoadDepartment(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [UserRightFilters]

        public ActionResult Save(tblDesignation model)
        {
            try
            {
                int check;
                model.ProjectID = Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                if (model.DesignationID > 0)
                {
                   
                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    check = check = model.UpdateData(model);
                }
                else
                {
                    model.DesignationCode = model.geneartedesginationCode(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs), Convert.ToInt32( model.DepartmentID));
                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    check = model.addata(model);
                }

                if (check > 0)
                {
                    return RedirectToAction("Index");

                }
                return RedirectToAction("create", model);

            }
            catch (Exception ex)
            {

                return View("create");
            }
        }

        public ActionResult Duplicate(string Name, string ID ,string dept)
        {
            try
            {
                string json = "";
                var list = new tblDesignation().checkDuplicate(Convert.ToInt32(ID), Name, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs), Convert.ToInt32( dept));
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
    }
}