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
    public class MinuteTypeController : Controller
    {
        // GET: MinuteType
   

        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblEminuteType a = new tblEminuteType();
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
        public ActionResult create(tblEminuteType a)
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
                tblEminuteType a = new tblEminuteType();

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

                tblEminuteType a = new tblEminuteType();
                var obj = a.getAlldataByID(Convert.ToInt32(IDo[0]));
                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                obj.IsView = a.IsView;
                obj.MinuteTypeID = Convert.ToInt32(IDo[0]);
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }

        [UserRightFilters]

        public ActionResult Save(tblEminuteType model)
        {
            try
            {
                int check;

                if (model.MinuteTypeID > 0)
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
                var list = new tblEminuteType().checkDuplicate(Convert.ToInt32(ID), Name);
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