using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    [NoCache]
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    public class IONController : Controller
    {
        // GET: ION
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {

                tblIonInfo a = new tblIonInfo();
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



      ///  [UserRightFilters]
        public ActionResult create1(tblIonInfo a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                a.Initiatedby = obj.Initiatedby;
                a.InitiatedDepartment = obj.DepartmentName;
                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                a.IonCode = a.generateIonNo(3, "I");
                a.IonDate = DateTime.Now;
                ViewData["Editmode"] = false;
                var tmep = a.getTempIon(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                a.detailistDoc = a.getdetailistDocumentData("-1");
                if (tmep != null)
                {
                    a.HtmlBox = tmep.htmlbox;

                }

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }



        [UserRightFilters]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save1(tblIonInfo model, FormCollection collection)
        {
            try
            {
                model.MinuteType = 3;

                //////For Unread ION
                //model.Displayfornew = 1;
                int check=0;
            

                if (model.IonID > 0)
                {
                    model.UserColor = new SAGERPNEW2018.Models.SystemLogin().GetUser().UserColor;
                    model.eID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    check = model.canncelIon(model);

                }
                else
                {

                    var po = string.Join(",", model.SelectInfo);
                    if (po == "0")
                    {

                        var DataemployeeForinfo = model.loadAllEmp(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

                        if (DataemployeeForinfo != null && DataemployeeForinfo.Count() > 0)
                        {
                            model.SelectInfo = DataemployeeForinfo.Select(x => x.Value).ToList();
                        }



                    }



                    if (model.SelectInfo != null)
                    {
                        model.ForInfo = string.Join(",", model.SelectInfo);

                    }
                    var checkpath = CreateImagesPath(model);

                    model.IonCode = model.generateIonNo(3, "I");
                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.ProjectID = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                    model.DepartmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;

                    model.EmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    var employeeid = model.loadlastTowMinute(Convert.ToInt32(model.ModifiedID));
                    model.NEwForwardTo = employeeid;

                    var tranferemp = checktransfer(model.NEwForwardTo);
                    if (tranferemp != null)
                    {
                        model.Transferedfrom = Convert.ToInt32(model.NEwForwardTo);
                        model.NEwForwardTo = Convert.ToInt32(tranferemp);

                    }

                    model.UserColor = new SAGERPNEW2018.Models.SystemLogin().GetUser().UserColor;
                    var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                    model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;

                    if (model.NEwForwardTo == -1)
                    {

                        var employeeids = model.loadlastTowMinute(Convert.ToInt32(model.ModifiedID));
                        model.NEwForwardTo = employeeids;
                    }

                    check = model.FurtherAddition(model, po); //Woring Fine



                }

                if (check > 0)
                {
                    TempData["ActionMessage"] = true;

                    return RedirectToAction("Index", "Home");

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

     
        public ActionResult ViewList(string id)
        {

            tblIonInfo a = new tblIonInfo();
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

        //public List<sp_getAllMinutetoedit2_Result> getAlldataByID(string code, int employeeID, int type)
        //{
        //    try
        //    {
        //        using (var context = new HRandPayrollDBEntities())
        //        {
        //            return context.sp_getAllMinutetoedit2(employeeID, code, type).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}

     //   [UserRightFilters]

        public ActionResult Edit(string id)
        {
            try
            {

                tblIonInfo a = new tblIonInfo();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAlldataIonByID(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), 3);

                var minowner = a.GetIonOwnerID(id, 3);
                var update = a.UpdateCurrentIonData(obj[0].IonID, obj[0].MinuteType, obj[0].fldStatus, obj[0].ForInfo);
                var checkforinfo = a.getAlldataIONforinfoID(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), obj[0].IonID);
                if (checkforinfo.Count > 0)
                {
                    ViewData["forinfo"] = true;
                }
                else
                {
                    ViewData["forinfo"] = false;
                    // var update2 = a.KeepNew(obj[0].MinuteID, obj[0].MinuteType);
                }
                if (minowner[0].EmployeeID == Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID))
                {
                    ViewData["minInit"] = true;
                }
                else
                {
                    ViewData["minInit"] = false;

                }
                a.detailistDoc = a.getdetailistDocumentData(id);
                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                var emplopeinfo = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                ViewData["Editmode"] = true;
                ViewData["NumberAttachment"] = a.detailistDoc.Count();
                a.IsApprove = emplopeinfo.IsApprove;
                a.IsCancel = emplopeinfo.IsCancel;
                a.IsComplete = emplopeinfo.IsComplete;
                a.IsDepartment = emplopeinfo.IsDepartment;
                a.IonDate = obj[0].IonDate;
                a.IonID = obj[0].IonID;
                a.MinuteType = obj[0].MinuteType;
                a.IonCode = obj[0].IonCode;
                //a.Priority = obj[0].Priority;

                a.Displayfornew = obj[0].Displayfornew;
                a.ProjectID = obj[0].ProjectID;
                a.Subject = obj[0].Subject;
                a.Type = obj[0].Type;
                a.HtmlBox = obj[0].HtmlBox;
                a.ForwardTo = obj[0].ForwardTo;
                a.Initiatedby = obj[0].Initiatedby;
                a.InitiatedDepartment = obj[0].InitiatedDepartment;
                /// var tmep = a.getTempIon(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                //if (tmep != null)
                //{
                //     //a.HtmlBox = tmep.htmlbox;
                //    //a.Remarks = tmep.remarks;
                //}
                // a.Remarks = tmep.remarks;
                a.EntryID = obj[0].EntryID;
                a.Status = obj[0].Status;

                return View("create1", a);

            }
            catch (Exception ex)
            {
                return View("create1");
            }

        }




        public bool CreateImagesPath(tblIonInfo model)
        {
            try
            {
                List<string> myCollection = new List<string>();
                if (model.listdocpath != null)
                {
                    foreach (var item in model.listdocpath)
                    {
                        if (item != null)
                        {
                            myCollection.Add(item);
                        }
                    }
                }
                if (model.DocFile != null)
                {
                    foreach (var item in model.DocFile)  //3rd change
                    {
                        if (item != null)

                        {
                            var Random = Guid.NewGuid().ToString("n").Substring(0, 8);

                            string imagename = model.IonCode + "i" + Random;
                            var fileName = Path.GetFileName(item.FileName);
                            string ext = Path.GetExtension(fileName);
                            var path = Path.Combine(Server.MapPath("~/AppFiles/IonAttachments/"), imagename + ext);
                            item.SaveAs(path);
                            string Savepath = "~/AppFiles/IonAttachments/" + imagename + ext;
                            myCollection.Add(Savepath);
                        }
                    }
                }

                model.listdocpath = myCollection;
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }








        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveAuto(string Html, string Remarks)
        {
            tempIonInfo a = new tempIonInfo();
            a.htmlbox = Html;
            a.remarks = Remarks;
            a.IP = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString();

            var list = new tblIonInfo().adTempdataIon(a);
            return Json(list, JsonRequestBehavior.AllowGet);


        }

        public int? checktransfer(int empid)
        {


            try
            {

                return new tblIonInfo().transfercheck(empid);




            }
            catch (Exception ex)
            {

                return null;
            }


        }


        public JsonResult loadgetviewedEmployee(int Ionid)
        {
            var list = new tblIonInfo().getviewedEmployee(Ionid);

            return Json(list, JsonRequestBehavior.AllowGet);
        }



    }
}