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
    public class ScoringController : Controller
    {

        public ActionResult Index(string id = "0")
        {
            try
            {
                GLUser a = new GLUser();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;
                return View(a);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public JsonResult loadRanking(String DaySearch)
        {

            return Json(new GLUser().sp_TopRanking10().ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Save(GLUser model, FormCollection collection)
        {
            try
            {
                int check;

                var checkedMinutes = collection["checkedUserScore"].ToString();
                model.TopUsers = checkedMinutes.Split(',');

                check = check = model.SaveTop(model);
                return RedirectToAction("Index", "Home");

            }

            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("create");

            }
        }

        public JsonResult Reset(String DaySearch)
        {

            return Json(new GLUser().ResetShowTop(), JsonRequestBehavior.AllowGet);

        }

    }
}
