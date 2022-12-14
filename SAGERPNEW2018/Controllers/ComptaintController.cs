using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class ComptaintController : Controller
    {
        // GET: Comptaint
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblEminuteInfo a = new tblEminuteInfo();
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
        public ActionResult create(tblEminuteInfo a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
               

                a.Initiatedby = ConvertTo_ProperCase(obj.Initiatedby);
                a.InitiatedDepartment = ConvertTo_ProperCase(obj.DepartmentName);

                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                a.MinuteCode = a.geneartMinuteNo(2, "C");// Convert.ToInt32( new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs) );
                a.MinuteDate = DateTime.Now;
                var tmep = a.getTempMinute(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                a.detailistDoc = a.getdetailistDocumentData("-1");
                ViewData["Editmode"] = false;
                if (tmep != null)
                {
                    a.HtmlBox = tmep.htmlbox;
                    a.Remarks = tmep.remarks;
                }

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        [UserRightFilters]
        public ActionResult ViewList(string id)
        {
            tblEminuteInfo a = new tblEminuteInfo();
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

        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            try
            {


                tblEminuteInfo a = new tblEminuteInfo();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAlldataByID(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), 2);

                var minowner = a.getMinuteowneridID(id, 2);
                var update = a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
                var checkforinfo = a.getAlldataByforinfoID(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), obj[0].MinuteID);
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
                a.MinuteDate = obj[0].MinuteDate;
                a.MinuteID = obj[0].MinuteID;
                a.MinuteType = obj[0].MinuteType;
                a.MinuteCode = obj[0].MinuteCode;
                a.Priority = obj[0].Priority;
                a.ProjectID = obj[0].ProjectID;
                a.Subject = obj[0].Subject;
                a.Type = obj[0].Type;
                a.HtmlBox = obj[0].HtmlBox;
                a.ForwardTo = obj[0].ForwardTo;
             

                a.Initiatedby = ConvertTo_ProperCase(obj[0].Initiatedby);
                a.InitiatedDepartment = ConvertTo_ProperCase(obj[0].InitiatedDepartment);


                var tmep = a.getTempMinute(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                if (tmep != null)
                {
                    // a.HtmlBox = tmep.htmlbox;
                    a.Remarks = tmep.remarks;
                }
                // a.Remarks = tmep.remarks;

                return View("create", a);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }




        public bool CreatImagesPath(tblEminuteInfo model)
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
                            string imagename = model.MinuteCode + DateTime.Now.Ticks.ToString();
                            var fileName = Path.GetFileName(item.FileName);
                            string ext = Path.GetExtension(fileName);
                            var path = Path.Combine(Server.MapPath("~/AppFiles/MinuteAttachments/"), imagename + ext);
                            item.SaveAs(path);
                            string Savepath = "~/AppFiles/MinuteAttachments/" + imagename + ext;
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

        public static string ConvertTo_ProperCase(string text)
        {

            if (!string.IsNullOrWhiteSpace(text))
            {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                return myTI.ToTitleCase(text.ToLower());
            }
            else
            {
                return null;
            }

        }

        [UserRightFilters]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(tblEminuteInfo model, FormCollection collection)
        {
            try
            {


                model.Subject = ConvertTo_ProperCase(model.Subject);
                model.Remarks = ConvertTo_ProperCase(model.Remarks);
                model.HtmlBox = ConvertTo_ProperCase(model.HtmlBox);

                model.MinuteType = 2;
                int check;
                if (model.SelectInfo != null)
                {
                    model.ForInfo = string.Join(",", model.SelectInfo);

                }
                var checkpath = CreatImagesPath(model);
                if (model.MinuteID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    var app = collection["IsApprovec"];
                    var com = collection["IsCompletec"];
                    var ccan = collection["IsCancelc"];
                    model.Status = 0;
                    model.fldStatus = "In-Process";
                    if (app == "on")
                    {
                        //approve
                        model.Status = 2;
                        model.fldStatus = "Approved";
                    }
                    if (com == "on")
                    {
                        //Complete
                        model.Status = 3;
                        model.fldStatus = "Completed";
                    }
                    if (ccan == "on")
                    {
                        //Cancel
                        model.Status = 4;
                        model.fldStatus = "Cancelled";
                    }

                    // model.Status = Country;
                    model.eID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    check = check = model.UpdateData(model);

                }
                else
                {
                    model.MinuteCode = model.geneartMinuteNo(2, "C");// Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.ProjectID = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                    model.DepartmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;

                    model.EmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    check = model.addata(model);


                    try
                    {
                        if (model.Priority.Trim() == "3")
                        {
                            var result = new HRandPayrollSystemModel.DBModel.tblEmployee().getAlldataByID(Convert.ToInt32(model.ForwardTo));
                            if (result != null)
                            {

                                var userdataResult = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByEmployeID(Convert.ToInt32(result.EmployeeID));
                                if (Convert.ToBoolean(userdataResult.AllowSendSMS))
                                {
                                    var Priority = "";
                                    if (model.Priority == "1")
                                    {
                                        Priority = "Normal";
                                    }
                                    else if (model.Priority == "2")
                                    {
                                        Priority = "High";
                                    }
                                    else
                                    {
                                        Priority = "Very High";
                                    }


                                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;



                                    var smsBody = "Ticket No : " + textInfo.ToTitleCase(model.MinuteCode) + " \nPriority : " + textInfo.ToTitleCase(Priority)

                                        + "\nSubject : " + textInfo.ToTitleCase(model.Subject) + " \nInitiated by : " + textInfo.ToTitleCase(model.Initiatedby);

                                    var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, userdataResult.mobileNo);
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {


                    }



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


        public JsonResult loadFarwardUser(int id)
        {
            if (id > 0)
            {
                var list = new tblEminuteInfo().LoadForwardUser(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = new tblEminuteInfo().LoadDepartmentForwardUser(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID));

                return Json(list, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult loadFarwardUser1(int id)
        {

            var list = new tblEminuteInfo().LoadForwardUsercc(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID, id, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

            return Json(list, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveAuto(string Html, string Remarks)
        {
            tempEminuteInfo a = new tempEminuteInfo();
            a.htmlbox = Html;
            a.remarks = Remarks;
            a.IP = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString();

            var list = new tblEminuteInfo().adTempdata(a);
            return Json(list, JsonRequestBehavior.AllowGet);


        }
    }
}