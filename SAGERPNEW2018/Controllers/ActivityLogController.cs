using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class ActivityLogController : Controller
    {
        // GET: ActivityLog
        [UserRightFilters]
        public ActionResult Index()
        {
            ProjectActivityLog a = new ProjectActivityLog();
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }


        public ActionResult viewlist()
        {
            ProjectActivityLog a = new ProjectActivityLog();
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }


        //[UserRightFilters]
        public ActionResult create(ProjectActivityLog a)
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

        public ActionResult Save(ProjectActivityLog model, FormCollection collection)
        {
            long check=0;

            if (model.Idlog > 0)
            {
                model.Modifeddate = DateTime.Now;
                model.Modifedyid = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid; 
                model.Modifiedip = Request.UserHostAddress;
                check = model.update(model);

            }
            else
            {
                model.CreatedOn = DateTime.Now;
                model.CreatedIP = Request.UserHostAddress;
                model.CreatedBy = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                check = model.addata(model);

            }

          

            if (check > 0)
            {
                ViewData["Message"] = "Success";
                return RedirectToAction("Index");

            }
            return RedirectToAction("create", model);
        }

        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            try
            {
                string[] IDo = id.Split('|');

                ViewData["Editmode"] = true;
                ProjectActivityLog a = new ProjectActivityLog();
                var obj = a.getalldataedit(Convert.ToInt32(IDo[0]));
                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                a.Isedit = true;
                obj.IsView = a.IsView;
                obj.Idlog = Convert.ToInt32(IDo[0]);
                obj.DepartmentID = obj.DepartmentID;
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }

        public JsonResult getSubtype(string type)
        {

            var result = new ProjectActivityLog().LoadSericeTypes(type); // .OrderBy(x => x.Text)

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDepartment(int type)
        {

            var result = new ProjectActivityLog().loadDepartmentAll(type); // .OrderBy(x => x.Text)

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServiceReporting(string id)
        {
            ProjectActivityLog a = new ProjectActivityLog();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;
                
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        public JsonResult StatusCheckWise(string TypeSS, string fromdate, string todate, string deptid, string projectidd)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            DateTime fudate = DateTime.Parse(fromdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(todate, new CultureInfo("en-US", true));
            DataTable check = new ProjectActivityLog().GetReportData(TypeSS, fudate, tudate, deptid, projectidd);
            var JSONresult = JsonConvert.SerializeObject(check);

            var jsonResult = Json(JSONresult, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = 50000000;
            return jsonResult;

            // return Json(check, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadServiceChart(int ProjectChart, int DepartmentChart, string fdate, string tdate, string TypeStatus, string TypeRelated)
        {

            //DateTime fudate = DateTime.Parse(fdate, new CultureInfo("en-US", true));
            //DateTime tudate = DateTime.Parse(tdate, new CultureInfo("en-US", true));

            var check = new ProjectActivityLog().LoadChart(ProjectChart, DepartmentChart, fdate, tdate, TypeStatus, TypeRelated);

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DepartmentforChart(int type)
        {

            var result = new ProjectActivityLog().LoadChartDepartment(type); // .OrderBy(x => x.Text)

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}