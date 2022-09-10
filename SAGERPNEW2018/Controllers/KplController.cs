using CrystalDecisions.CrystalReports.Engine;
using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    [NoCache]
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    [HandleExceptionFilter]

    public class KplController : Controller 
    {
        // GET: Kpl
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult create(tbl_kplinfo a)
        {
            try
            {




                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                a.InitiatedDepartment = obj.DepartmentName.ToUpper();




                a.EntryDate = DateTime.Now;

                a.Initiatedby = obj.Initiatedby.ToUpper();
            
                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                a.KPI_Code = a.GenerateKplCode("K");
                a.EntryDate = DateTime.Now;
                ViewData["Editmode"] = false;
         
                Session["MinuteList"] = @"../../home/index";

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }


        public ActionResult createkpi(tbl_kplinfo a)
        {
            try
            {


                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                a.InitiatedDepartment = obj.DepartmentName.ToUpper();




                a.EntryDate = DateTime.Now.Date;

                a.Initiatedby = obj.Initiatedby.ToUpper();

                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                a.KPI_Code = a.GenerateKplCode("K");
                a.EntryDate = DateTime.Now;
                ViewData["Editmode"] = false;

                Session["MinuteList"] = @"../../home/index";

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        [ValidateInput(false)]
        public ActionResult Save(tbl_kplinfo model, FormCollection collection)
        {


            try
            {
              

                int check = 0;

                model.DepartmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;
                



                model.KPI_Code = model.GenerateKplCode("K");
                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;

                DateTime oDate = Convert.ToDateTime(model.KPIMMYY);
                model.KPIMMYY = oDate.ToString("MMMM yyyy");

                    model.EmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                   
                   
                    

                    model.UserColor = new SAGERPNEW2018.Models.SystemLogin().GetUser().UserColor;
                    var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                    model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;

                //var kpidata = new HRandPayrollSystemModel.DBModel.tbl_kplinfo().GetAll(new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID);
                //foreach (var bm in kpidata)
                //{
                //    if (bm.KPIMMYY == model.KPIMMYY)
                //    {
                //        Response.Write(@"<script language='javascript'>alert('Message: \n" + "Hi!" + " .');</script>");
                //        TempData["ActionMessage"] = true;
                //        return RedirectToAction("createkpi");
                //    }

                //}


                check = model.FurtherAddition(model); //Woring Fine



                

                if (check > 0)
                {
                    TempData["ActionMessage"] = true;

                    return RedirectToAction("Index", "Home");

                }
                TempData["ActionMessage"] = false;
                return RedirectToAction("createkpi", model);



            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("createkpi");

            }
        }
        public ActionResult ViewList(string id)
        {

            tbl_kplinfo a = new tbl_kplinfo();
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

        public JsonResult sp_getAllKpi(int projid, int id)
        {
            //var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new tbl_kplinfo().sp_getallkpl(Convert.ToInt32(id));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            try
            {

                tbl_kplinfo a = new tbl_kplinfo();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAlldataIonByID(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID));

                var minowner = a.GetIonOwnerID(id);
                //var update = a.UpdateCurrentIonData(obj[0].IonID, obj[0].MinuteType, obj[0].fldStatus, obj[0].ForInfo);
                //var checkforinfo = a.getAlldataIONforinfoID(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), obj[0].IonID);
                //if (checkforinfo.Count > 0)
                //{
                //    ViewData["forinfo"] = true;
                //}
                //else
                //{
                //    ViewData["forinfo"] = false;
                //    // var update2 = a.KeepNew(obj[0].MinuteID, obj[0].MinuteType);
                //}

                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                var emplopeinfo = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                ViewData["Editmode"] = true;


                a.EntryDate = obj[0].EntryDate;
                a.KPI_ID = obj[0].KPI_ID;

                a.KPI_Code = obj[0].KPI_Code;
                //a.Priority = obj[0].Priority;

                a.ShowInNew = obj[0].ShowInNew;
                a.LocationProject = obj[0].LocationProject;
                DateTime oDate1 = Convert.ToDateTime(obj[0].KPIMMYY);
                a.KPIMMYY = oDate1.ToString("yyyy-MM");
                //a.KPIMMYY = obj[0].KPIMMYY;
                a.HtmlBox = obj[0].HtmlBox;
                a.ForwardTo = obj[0].ForwardTo;
                a.Initiatedby = obj[0].Initiatedby;
                a.InitiatedDepartment = obj[0].InitiatedDepartment;

                a.EntryID = obj[0].EntryID;
                a.Status = obj[0].Status;

                return View("createkpi", a);

            }
            catch (Exception ex)
            {
                return View("createkpi");
            }

        }

        public ActionResult Duplicate(string date, string proj)
        {
            try
            {
                DateTime oDate = Convert.ToDateTime(date);
                date = oDate.ToString("MMMM yyyy");
                string json = "";
                var kpidata = new HRandPayrollSystemModel.DBModel.tbl_kplinfo().GetAll1(proj);
                foreach (var bm in kpidata)
                {
                   
                    if (bm.KPIMMYY ==date)
                    {
                        
                        json = " <strong>Error!</strong> Record Cannot Save, It's already saved 1- time in this month.";
                    }
                   
                }


                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

                //var list = new tblDepartment().checkDuplicate(Convert.ToInt32(ID), Name, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));
                //if (list.Count() > 0)
                //{
                //    json = " Duplicate Record Found ";
                //}
                //return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {


                return Json(JsonConvert.SerializeObject("Error"), JsonRequestBehavior.AllowGet);

            }


        }
        public JsonResult loaddepartmentbyProject(int id)
        {
            var list = new tbl_kplinfo().loadDepartment(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KpiList()
        {

            tbl_kplinfo a = new tbl_kplinfo();
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
    }

}