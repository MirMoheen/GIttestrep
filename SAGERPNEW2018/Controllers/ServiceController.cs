using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;

namespace SAGERPNEW2018.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult ViewListServices()
        {
            tblProjectService a = new tblProjectService();
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }
        public ActionResult AddService(tblProjectService a)
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

        public ActionResult SaveService(tblProjectService model, FormCollection collection)
        {
            long check = 0;

            if (model.Service_ID > 0)
            {
                model.Modifed_Date = DateTime.Now;
                model.Modifed_ID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                model.Modified_IP = Request.UserHostAddress;
                check = model.UpdateService(model);

            }
            else
            {
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                model.CreatedIP = Request.UserHostAddress;
                check = model.AddServiceData(model);


            }


            if (check > 0)
            {
                ViewData["Message"] = "Success";
                return RedirectToAction("ViewListServices");

            }
            return RedirectToAction("AddService", model);
        }

        public ActionResult EditService(string id)
        {
            try
            {
                string[] IDo = id.Split('|');

                tblProjectService a = new tblProjectService();
                var obj = a.getalldataedit(Convert.ToInt32(IDo[0]));
                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                obj.IsView = a.IsView;
                obj.Service_ID = Convert.ToInt32(IDo[0]);
                return View("AddService", obj);

            }
            catch (Exception ex)
            {
                return View("ViewListServices");
            }

        }

        public ActionResult Duplicate(string Name, string ID)
        {
            try
            {
                string json = "";
                var list = new tblProjectService().checkDuplicate(Convert.ToInt32(ID), Name);
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