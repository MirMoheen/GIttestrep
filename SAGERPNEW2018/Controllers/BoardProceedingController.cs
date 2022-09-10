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


    [HandleExceptionFilter]
    public class BoardProceedingController : Controller
    {
        // GET: BoardProceeding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewList(string id)
        {

            tbl_BoardProceeding a = new tbl_BoardProceeding();
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
        public ActionResult create(tbl_BoardProceeding a)
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
                a.MinuteSheetCode = a.generateProceedingNo(9, "B");
                a.MinuteSheetDate = DateTime.Now;
                ViewData["Editmode"] = false;
                //var tmep = a.getTempIon(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                a.detailistDoc = a.getdetailistDocumentData("-1");
                //if (tmep != null)
                //{
                //    a.HtmlBox = tmep.htmlbox;

                //}

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

  
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(tbl_BoardProceeding model, FormCollection collection)
        {
            var obj = model.getAllBoardDataByID(model.MinuteSheetCode, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), 9);
            

            if (model.MinuteSheetID > 0)
            {
                model.MinuteType = 9;
                model.EntryDate = DateTime.Now;
                model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                model.ProjectID = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                model.DepartmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;
                //DateTime dt = Convert.ToDateTime("11/23/2010");
                //string s2 = dt.ToString("dd-MM-yyyy");
                //DateTime dtnew = Convert.ToDateTime(s2);

                var ab = DateTime.Now.ToString("t");
                var bc = DateTime.Now.ToString("yyyy-MM-dd");

                model.TimeofSign = ab;
                model.DateofSign = bc;
                //model.SignDate = DateTime.Now;
                  
                model.ChairPerson = obj[0].ChairPerson;
                model.HtmlBox = obj[0].HtmlBox;

                model.SignOfBoardUser(model, obj[0].MinuteSheetID, obj[0].MinuteType, obj[0].fldStatus);
              
                var logeduser = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

                ////var obj1 = obj.ChairPerson;
                var resultk = new HRandPayrollSystemModel.DBModel.tbl_BoardProceeding().sp_getCount(model.MinuteSheetCode);
                var cn = resultk[0];
                if(cn==0 && logeduser != 1)
                {
                    int co;
                    co = model.transfertoactualpresident(model);
                }
                //if (Convert.ToInt32(model.ChairPerson) == obj11 && cn == 1)
                //{

                //}
                //else
                //{

                //}
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    model.MinuteType = 9;

                    //////For Unread ION
                    //model.Displayfornew = 1;
                    int check;
                    var po = string.Join(",", model.ChairPerson) + "," + string.Join(",", model.SelectInfo);
                    //var po = string.Join(",", model.SelectInfo);

                    var getperson = string.Join(",", model.ChairPerson);




                    if (model.SelectInfo != null)
                    {
                        model.ForInfo = string.Join(",", model.SelectInfo) + "," + string.Join(",", model.ChairPerson);

                    }
                    var checkpath = CreateImagesPath(model);

                    {
                        model.MinuteSheetCode = model.generateProceedingNo(9, "B");
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

            return RedirectToAction("Index", "Home");
        }


        public bool CreateImagesPath(tbl_BoardProceeding model)
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

                            string imagename = model.MinuteSheetCode + "i" + Random;
                            var fileName = Path.GetFileName(item.FileName);
                            string ext = Path.GetExtension(fileName);
                            var path = Path.Combine(Server.MapPath("~/AppFiles/BoardMinuteAttachments/"), imagename + ext);
                            item.SaveAs(path);
                            string Savepath = "~/AppFiles/BoardMinuteAttachments/" + imagename + ext;
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

        public int? checktransfer(int empid)
        {
            try
            {
                return new tbl_BoardProceeding().transfercheck(empid);
            }
            catch (Exception ex)
            {

                return null;
            }


        }

        public JsonResult sp_GetBoardSheet(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new tbl_BoardProceeding().sp_GetBoardSheet(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_GetReadedBoardSheet(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new tbl_BoardProceeding().sp_GetReadedBoardSheet(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            try
            {

                tbl_BoardProceeding a = new tbl_BoardProceeding();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAllBoardDataByID(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), 9);
                a.getids = a.GetAllIDS(id);
                a.CheckUserSign=a.SignatureStatus(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID));
              
                var minowner = a.GetIonOwnerID(id, 9);
                //var update = a.UpdateCurrentIonData(obj[0].IonID, obj[0].MinuteType, obj[0].fldStatus, obj[0].ForInfo);
                var checkforinfo = a.GetAllDataForinfoID(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), obj[0].MinuteSheetID);
                if (checkforinfo.Count > 0)
                {
                    ViewData["forinfo"] = true;
                }
                else
                {
                    ViewData["forinfo"] = false;
 
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
                a.MinuteSheetDate = obj[0].MinuteSheetDate;
                a.MinuteSheetID = obj[0].MinuteSheetID;
                a.MinuteType = obj[0].MinuteType;
                a.MinuteSheetCode = obj[0].MinuteSheetCode;
                a.ChairPerson = obj[0].ChairPerson;

                a.Displayfornew = obj[0].Displayfornew;
                a.ProjectID = obj[0].ProjectID;
                a.Subject = obj[0].Subject;
                a.Type = obj[0].Type;
                a.HtmlBox = obj[0].HtmlBox;
                a.ForwardTo = obj[0].ForwardTo;
                a.Initiatedby = obj[0].Initiatedby;
                a.InitiatedDepartment = obj[0].InitiatedDepartment;

                return View("create", a);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }
    }
}