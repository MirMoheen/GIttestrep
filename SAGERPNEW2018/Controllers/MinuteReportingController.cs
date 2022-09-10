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

  [NoCache]
   [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    [HandleExceptionFilter]
    public class MinuteReportingController : Controller
    {
        // GET: MinuteReporting

        [UserRightFilters]
        public ActionResult MinuteStateWiseDetail(string id)
        {
            ReportModel a = new ReportModel();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;
                Session["MinuteList"] = @"../../MinuteReporting/" + @"MinuteStateWiseDetail/" + id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        public JsonResult StatusCheckWise(string QuryStatus, string fromdate, string todate, string deptid, string typeid)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            DateTime fudate = DateTime.Parse(fromdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(todate, new CultureInfo("en-US", true));
            DataTable check = new ReportModel().GetEminuteData(QuryStatus, fudate, tudate, deptid, typeid);
            var JSONresult = JsonConvert.SerializeObject(check);

            var jsonResult = Json(JSONresult, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = 50000000;
            return jsonResult;

            // return Json(check, JsonRequestBehavior.AllowGet);
        }








        [UserRightFilters]
        public ActionResult BudgetReport()
        {

            ReportModel a = new ReportModel();
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



        public JsonResult LoadBudgetData(int project,int headid,int subheadid)
        {

            


                var check = new ReportModel().getbudgetreport(project, headid, subheadid);
                return Json(check, JsonRequestBehavior.AllowGet);
        
           
        }







        [UserRightFilters]
        public ActionResult FixedAssitList()
        {

            ReportModel a = new ReportModel();
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

        public JsonResult loaddepartmentbyProject(int id)
        {
            var list = new tblEmployee().loadDepartment(id).OrderBy(x=>x.Text);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadFixedAssiListTypewise(int typeid)
        {
            var departmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;

            var check = new tblEminuteInfo().getfixassitTypelist(departmentID, typeid);
            return Json(check, JsonRequestBehavior.AllowGet);
        }



        public JsonResult LoadFixedAssiList(int project, int department, int typeid, int subtypeid)
        {
         

            var check = new ReportModel().getlistofFixedAsset(project, department, typeid, subtypeid);
            return Json(check, JsonRequestBehavior.AllowGet);
        }



       // [UserRightFilters]
        public ActionResult MinuteSearching(string id)
        {
            tblEminuteInfo a = new tblEminuteInfo();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;


               Session["MinuteList"] = @"../../MinuteReporting/" + @"MinuteSearching/" + id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }




        public JsonResult loadSearchMinue(String search, int Deptid, int type, int TypeSub)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

            var check = new tblEminuteInfo().sp_searchMinuteReport(search, Deptid, type, TypeSub);
            return Json(check, JsonRequestBehavior.AllowGet);
        }




        [UserRightFilters]
        public ActionResult MinutePoAllow(string id)
        {
            tblEminuteInfo a = new tblEminuteInfo();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;


                Session["MinuteList"] = @"../../MinuteReporting/" + @"MinutePoAllow/" + id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }




        public JsonResult loadPoMinutes(String search, int Deptid, int type, int TypeSub)
        {
        
            var check = new tblEminuteInfo().sp_PoAllowedMinuteReport(search, Deptid, type, TypeSub);
            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public JsonResult updateMinutePodata(string minuteIds)
        {

            var minutes = minuteIds.Split(',').ToList();
         var check =   new tblEminuteInfo().udpatePoMinute(minutes);

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MinutesReadOnly(string id = "0")
        {
            try
            {
                tblEminuteInfo a = new tblEminuteInfo();
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

        public JsonResult LoadNewMinutesReadOnly(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadnewMinutes(1);

            return Json(check, JsonRequestBehavior.AllowGet);
        }




    }
}