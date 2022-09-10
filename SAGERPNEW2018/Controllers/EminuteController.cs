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
    public class EminuteController : Controller
    {
        // GET: Eminute
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
                //a.Initiatedby = ConvertTo_ProperCase(obj.Initiatedby);
                //a.InitiatedDepartment = ConvertTo_ProperCase(obj.DepartmentName);

                a.Initiatedby = obj.Initiatedby.ToUpper();
                a.InitiatedDepartment = obj.DepartmentName.ToUpper();
                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
                a.MinuteCode = a.geneartMinuteNo(1, "E");// Convert.ToInt32( new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs) );
                a.MinuteDate = DateTime.Now;
                var tmep = a.getTempMinute(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                a.detailistDoc = a.getdetailistDocumentData("-1");
                a.Type = "0";
               
                ///detail head load
           //     a.JsonHeadbudget = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().gethead(1).OrderBy(x => x.Text));
                a.JsonHeadFinancalYear = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getFinYear("01"));
                a.JsonProject = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getproject());





                ViewData["Editmode"] = false;
                if (tmep != null)
                {
                    a.HtmlBox = tmep.htmlbox;
                    a.Remarks = tmep.remarks;
                }
                Session["MinuteList"] = @"../../home/index";

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
              

                Session["MinuteList"] = id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }



        [UserRightFilters]

        public ActionResult ApprovalViewList(string id="0")
        {
            tblEminuteInfo a = new tblEminuteInfo();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;

                Session["MinuteList"] = id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }



        [UserRightFilters]

        public ActionResult ViewPendingList(int id)
        {
            tblEminuteInfo a = new tblEminuteInfo();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);


                a.listDayWise = a.getDayWisePending(id);
                TempData["St"] = id;
                Session["MinuteList"] = @"../../Eminute/" + @"ViewPendingList/" + id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        [UserRightFilters]
        public ActionResult pendminuteLoadEdit(string id)

        {
            try
            {
                //var aad = TempData["St"];

              tblEminuteInfo a = new tblEminuteInfo();

              var currentEminute=  a.getcurrentMinuteByRowbyMinutecode(id);


                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);



                var obj = a.getAlldataByID(id, Convert.ToInt32(currentEminute.EmployeeID), 1);


                var minowner = a.getMinuteowneridID(id, 1);
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
                a.LocationProject = new HRandPayrollSystemModel.DBModel.tblProject().getAlldataByID(Convert.ToInt32(obj[0].ProjectID)).ProjectName;
                var emplopeinfo = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);

                // var getbudgetdata = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheaddetails(Convert.ToInt32(obj[0].BudgetHead), Convert.ToInt32(obj[0].BudgetSubHead), obj[0].FinancialYear);
                var getbudgetdata = a.loaddetailbudget(obj[0].MinuteCode);


                if (getbudgetdata.Count > 0)
                {
                    ///  a.SupplierAccountNo = obj[0].SupplierAccountNo.Trim();
                    a.detailistbudget = getbudgetdata;
                    ViewData["Budgetmode"] = false;


                }

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
                a.Subtype = obj[0].Subtype;
                a.HtmlBox = obj[0].HtmlBox;
                a.fldStatus = obj[0].fldStatus;
                a.estimatedcost = obj[0].estimatedcost;
                a.BudgetAmount = obj[0].BudgetAmount;
                a.BillClear = obj[0].BillClear;
                a.estimatedcostRange = obj[0].estimatedcostRange;
                a.IspettyCash = obj[0].IspettyCash;
                a.CheckBudgetAmount = Convert.ToDecimal(obj[0].BudgetAmount);
                a.EntryID = obj[0].EntryID;
                a.Status = obj[0].Status;
                a.fldStatus = "0";
                var payableamount = obj[0].OtherPayable == null ? 0 : obj[0].OtherPayable;
                a.totalPayableamt = obj[0].BudgetAmount + payableamount;
                a.OtherPayable = obj[0].OtherPayable;


                if (obj[0].ModifiedID != null)
                {
                    a.ModifiedID = obj[0].ModifiedID;
                }
                else
                {
                    a.ModifiedID = a.EntryID;
                }


                a.ForwardTo = obj[0].ForwardTo;
                //a.Initiatedby = ConvertTo_ProperCase(obj[0].Initiatedby);
                //a.InitiatedDepartment = ConvertTo_ProperCase(obj[0].InitiatedDepartment);

                a.Initiatedby = obj[0].Initiatedby.ToUpper();
                a.InitiatedDepartment = obj[0].InitiatedDepartment.ToUpper();
                var FirstInituserdata = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByID(Convert.ToInt32(obj[0].EntryID));

                var Firstintdata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(Convert.ToInt32(FirstInituserdata.EmployeeID));

                a.Initiatedby = Firstintdata.Initiatedby;
                a.IsPo = obj[0].IsPo;
                a.IsBudget = obj[0].IsBudget;
                a.IsPayBeforeApprovel = obj[0].IsPayBeforeApprovel;


           ///     a.JsonHeadbudget = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().gethead(1).OrderBy(x => x.Text));
                a.JsonHeadFinancalYear = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getFinYear("01"));
                a.JsonProject = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getproject());


                ViewData["htmlboxdata"] = "";
                ViewData["RemarksDATA"] = "";



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




        [UserRightFilters]
    
        public ActionResult Edit(string id)

        
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Eminute");
            }
            try
            {
                var aad = TempData["St"];
                bool isviewof = true;
                var dataeminite = id.Split('~');
                id = dataeminite[0];
                try { isviewof = dataeminite[1] == "0" ? false : true; } catch (Exception) { isviewof = false; }
                tblEminuteInfo a = new tblEminuteInfo();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAlldataByID(id, Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID), 1);
                var minowner = a.getMinuteowneridID(id, 1);
                a.NEwForwardTo = -1;
                if (obj[0].Displayfornew == 1 && (obj[0].Status == 2 || obj[0].Status == 3))
                {
                    if (!isviewof)
                    {
                        a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
                        return RedirectToAction("ViewList", "Eminute", new { id = 0 });
                    }
                    else
                    {
                        a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
                     //   return RedirectToAction("ViewList", "Eminute", new { id = 0 });

                    }
                }
               
                else
                {
                    a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
                }
                //if (obj[0].Displayfornew == 1 && (obj[0].Status == 2 || obj[0].Status == 3))
                //{
                //    a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
                //    return RedirectToAction("ViewList", "Eminute", new { id = 0 });
                //}
                //else
                //{
                //    a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);

                //}




                /// var update = a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);
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
                a.LocationProject = new HRandPayrollSystemModel.DBModel.tblProject().getAlldataByID(Convert.ToInt32( obj[0].ProjectID)).ProjectName;
                var emplopeinfo = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);

               // var getbudgetdata = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheaddetails(Convert.ToInt32(obj[0].BudgetHead), Convert.ToInt32(obj[0].BudgetSubHead), obj[0].FinancialYear);
                var getbudgetdata =  a.loaddetailbudget(obj[0].MinuteCode);


                if (getbudgetdata.Count>0)
                {
                  ///  a.SupplierAccountNo = obj[0].SupplierAccountNo.Trim();
                    a.detailistbudget = getbudgetdata;
                    ViewData["Budgetmode"] = false;


                }



               
              
                

                //if (true)
                //{
                //    ViewData["Budgetmode"] = true;
                //}

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
                a.Subtype = obj[0].Subtype;
                a.HtmlBox = obj[0].HtmlBox;
                a.fldStatus = obj[0].fldStatus;
                a.estimatedcost = obj[0].estimatedcost;
                a.BudgetAmount = obj[0].BudgetAmount;
                a.BillClear= obj[0].BillClear;
                a.estimatedcostRange = obj[0].estimatedcostRange;
                a.IspettyCash = obj[0].IspettyCash;
                a.CheckBudgetAmount = Convert.ToDecimal( obj[0].BudgetAmount);
                a.EntryID = obj[0].EntryID;
                a.Status = obj[0].Status;
              var payableamount =  obj[0].OtherPayable == null ? 0 : obj[0].OtherPayable;
                a.totalPayableamt = obj[0].BudgetAmount + payableamount;
                a.OtherPayable = obj[0].OtherPayable;
                a.IsTech = obj[0].IsTech;
                a.IsHold = obj[0].IsHold;
                a.IsPayBeforeApprovel = obj[0].IsPayBeforeApprovel;




                if (obj[0].ModifiedID != null)
                {
                    a.ModifiedID = obj[0].ModifiedID;
                }
                else
                {
                    a.ModifiedID = a.EntryID;
                }


                a.ForwardTo = obj[0].ForwardTo;
                //a.Initiatedby = ConvertTo_ProperCase(obj[0].Initiatedby);
                //a.InitiatedDepartment = ConvertTo_ProperCase(obj[0].InitiatedDepartment);

                a.Initiatedby = obj[0].Initiatedby.ToUpper();
                a.InitiatedDepartment = obj[0].InitiatedDepartment.ToUpper();
                var FirstInituserdata = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByID(Convert.ToInt32(obj[0].EntryID));

                var Firstintdata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(Convert.ToInt32(FirstInituserdata.EmployeeID));

                a.Initiatedby = Firstintdata.Initiatedby;
                a.IsPo = obj[0].IsPo;
                a.IsBudget = obj[0].IsBudget;
                a.JsonProject = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getproject());

           //     a.JsonHeadbudget = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().gethead(1).OrderBy(x => x.Text));
                a.JsonHeadFinancalYear = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getFinYear("01"));


                ViewData["htmlboxdata"] = "";
                ViewData["RemarksDATA"] = "";



                var tmep = a.getTempMinute(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                if (tmep != null)
                {
                    // a.HtmlBox = tmep.htmlbox;
                    a.Remarks = tmep.remarks;
                }
                // a.Remarks = tmep.remarks;
               var result= a.gettechicalPerson(Convert.ToInt32( obj[0].Type));
                a.techicalpersonID = 0;
                if (result!=null && result.TechPerson!=null)
                {
                    a.techicalpersonID =(int)result.TechPerson;
                }

                ViewData["istechical"] = new SAGERPNEW2018.Models.SystemLogin().GetUser().IsTechicalPerson;


                if (new SAGERPNEW2018.Models.SystemLogin().GetUser().IsDirectApproval && isviewof && a.Status==1)
                {
                    a.NEwForwardTo = -1;
                    a.Remarks = "<p>Approved</p>";
                    a.Status = 2;
                    a.fldStatus = "Approved";
                    a.HtmlBox = null;
                    return RedirectToAction("SaveDirect", a );


                }


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
                            var Random =Guid.NewGuid().ToString("n").Substring(0, 8);

                            string imagename = model.MinuteCode+"m"+ Random;
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


        public int? checktransfer(int empid)
        {


            try
            {

                return new tblEminuteInfo().transfercheck(empid);




            }
            catch (Exception ex)
            {

                return null;
            }


        }
        [ValidateInput(false)]
        public ActionResult SaveDirect(tblEminuteInfo model)
        {
            int index = 0;
            try
            {

                model.detailistDoc = model.getdetailistDocumentData(model.MinuteCode);

                model.listdocpath = model.detailistDoc.Select(x => x.PathDoc).ToList();

                if (model.ReminderMinute == 1)
                {
                    model.NEwForwardTo = Convert.ToInt32(model.getMinuteReminder(model.MinuteCode).EmployeeID);
                    model.Remarks = "<p></p>";
                    model.IsReminder = true;

                }


                if (model.DiscussMinute == 1)
                {
                    model.NEwForwardTo = Convert.ToInt32(model.getMinuteReminder(model.MinuteCode).EmployeeID);
                    model.Remarks = "<p>This E-minute needs discussion</p>";
                    model.IsDiscuss = true;

                }

                if (model.HoldMinute == 1)
                {
                    model.NEwForwardTo = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    model.Remarks = "<p> This E-minute is on hold now. </p>";
                    model.IsHold = true;


                }


                if (model.Minuteopen == 1)
                {

                    model.Remarks = "<p> " + model.MinuteOpenStatus + " </p>";


                }



                model.Transferedfrom = 0;
                model.Subject = model.Subject.ToUpper();

                model.UserColor = new SAGERPNEW2018.Models.SystemLogin().GetUser().UserColor;
                var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;


                if (model.NEwForwardTo == -1)
                {

                    var employeeid = model.loadlastTowMinute(Convert.ToInt32(model.ModifiedID));
                    model.NEwForwardTo = employeeid;
                }



                model.MinuteType = 1;
                int check=0;
                if (model.SelectInfo != null)
                {
                    model.ForInfo = string.Join(",", model.SelectInfo);

                }
                var checkpath = CreatImagesPath(model);

                if (model.MinuteID > 0)
                {
                   // model.HtmlBox = collection["htmlboxValue"];

                    ///tranfer user///////////
                    var tranferemp = checktransfer(model.NEwForwardTo);
                    if (tranferemp != null)
                    {
                        model.Transferedfrom = Convert.ToInt32(model.NEwForwardTo);
                        model.NEwForwardTo = Convert.ToInt32(tranferemp);
                        model.isforward = false;
                    }
                    /////////////////////////

                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.currentUserEmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                   

                    model.eID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                
                    if (model.IsBudget && new SAGERPNEW2018.Models.SystemLogin().GetUser().ForPayment)
                    {
                        if (model.ThisExpense.Sum(x => Convert.ToDecimal(x)) > 0)
                        {
                            model.BudgetAmount = model.ThisExpense.Sum(x => Convert.ToDecimal(x));

                            if (model.CheckBudgetAmount == 0 || model.BudgetAmount != model.CheckBudgetAmount)
                            {
                                GetBudgetlist(model);
                            }


                        }

                    }


                    var resultofNextMinutes = new GLUser().loadnewMinutes(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID));
                    index = resultofNextMinutes.FindIndex(a => a.MinuteCode == model.MinuteCode);

                    check = check = model.UpdateData(model);


                    smssend(model);

                }


                return RedirectToAction("ViewList", "Eminute", new { id = 0 });


            }
            catch (Exception ex)
            {
                TempData["ActionMessage"] = false;

                return View("create");

            }
        }




        [UserRightFilters]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(tblEminuteInfo model, FormCollection collection)
        {
            int index = 0;
            try
            {

                if (model.ReminderMinute==1)
                {
                    model.NEwForwardTo = Convert.ToInt32( model.getMinuteReminder(model.MinuteCode).EmployeeID);
                    model.Remarks = "<p></p>";
                    model.IsReminder = true;
                   
                }


                if (model.DiscussMinute == 1)
                {
                    model.NEwForwardTo = Convert.ToInt32(model.getMinuteReminder(model.MinuteCode).EmployeeID);
                    model.Remarks = "<p>This E-minute needs discussion</p>";
                    model.IsDiscuss = true;

                }

                if (model.HoldMinute == 1)
                {
                    model.NEwForwardTo = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    model.Remarks = "<p> This E-minute is on hold now. </p>";
                    model.IsHold = true;


                }




                if (model.Minuteopen == 1)
                {
                  
                    model.Remarks = "<p> " + model.MinuteOpenStatus + " </p>";


                }



                if (!Convert.ToBoolean(new SAGERPNEW2018.Models.SystemLogin().GetUser().ForPayment))
                {
                    if (collection["payabelAmtRead"]!=null)
                    {
                        model.OtherPayable = Convert.ToDecimal(collection["payabelAmtRead"]);
                    }

                }

            
            

                model.Transferedfrom = 0;
                model.Subject = model.Subject.ToUpper();

                model.UserColor = new SAGERPNEW2018.Models.SystemLogin().GetUser().UserColor;
                var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;


                if (model.NEwForwardTo == -1)
                {

                    var employeeid = model.loadlastTowMinute(Convert.ToInt32(model.ModifiedID));
                    model.NEwForwardTo = employeeid;
                }



                model.MinuteType = 1;
                int check;
                if (model.SelectInfo != null)
                {
                    model.ForInfo = string.Join(",", model.SelectInfo);

                }
                var checkpath = CreatImagesPath(model);

                if (model.MinuteID > 0)
                {
                    model.HtmlBox = collection["htmlboxValue"];

                    ///tranfer user///////////
                    var tranferemp = checktransfer(model.NEwForwardTo);
                    if (tranferemp != null)
                    {
                        model.Transferedfrom = Convert.ToInt32(model.NEwForwardTo);
                        model.NEwForwardTo = Convert.ToInt32(tranferemp);
                        model.isforward = false;
                    }
                    /////////////////////////

                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.currentUserEmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
                    //   model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    //   model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
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
                    //model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    // model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    if (model.IsBudget && new SAGERPNEW2018.Models.SystemLogin().GetUser().ForPayment )
                    {
                        if (model.ThisExpense.Sum(x => Convert.ToDecimal(x))>0)
                        {
                            model.BudgetAmount = model.ThisExpense.Sum(x => Convert.ToDecimal(x));

                            if (model.CheckBudgetAmount==0 ||   model.BudgetAmount!= model.CheckBudgetAmount)
                            {
                                GetBudgetlist(model);
                            }

                           
                        }
                     
                    }


                    var resultofNextMinutes = new GLUser().loadnewMinutes(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID));
                    index = resultofNextMinutes.FindIndex(a => a.MinuteCode == model.MinuteCode);

                    check = check = model.UpdateData(model);


                    smssend(model);

                }
                else
                {

                    // tranfer user///////////
                    var tranferemp = checktransfer(Convert.ToInt32(model.ForwardTo));
                    if (tranferemp != null)
                    {
                        model.Transferedfrom = Convert.ToInt32(model.ForwardTo);
                        model.ForwardTo = Convert.ToInt32(tranferemp);
                        model.isforward = false;
                    }
                    ///////////////////////
                    model.MinuteCode = model.geneartMinuteNo(1, "E");// Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));

                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.ProjectID = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                    model.DepartmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;
                    model.EmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

                    //create stamp
                    if (model.IsBudget)
                    {
                        GetBudgetlist(model);
                    }
                    //
                 
                        check = model.addata(model);
                    Session["MinuteList"]=null;
                    model.ForwardTo = model.NEwForwardTo;
                    smssend(model);
                    //try
                    //{
                    //    if (model.Priority.Trim()=="3")
                    //    {
                    //        var result = new HRandPayrollSystemModel.DBModel.tblEmployee().getAlldataByID(Convert.ToInt32(model.ForwardTo));
                    //        if (result != null)
                    //        {

                    //            var userdataResult = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByEmployeID(Convert.ToInt32(result.EmployeeID));
                    //            if (Convert.ToBoolean(userdataResult.AllowSendSMS))
                    //            {
                    //                var Priority = "";
                    //                if (model.Priority == "1")
                    //                {
                    //                    Priority = "Normal";
                    //                }
                    //                //else if (model.Priority == "2")
                    //                //{
                    //                //    Priority = "High";
                    //                //}
                    //                else
                    //                {
                    //                    Priority = "Very High";
                    //                }



                    //                var smsBody = "EM No:" + ConvertTo_ProperCase(model.MinuteCode) + " \nPriority:" + ConvertTo_ProperCase(Priority)

                    //                    + "\nSubject:" + ConvertTo_ProperCase(model.Subject) + " \nInitiated by:" + model.Initiatedby.ToUpper();

                    //               // ConvertTo_ProperCase(model.Initiatedby)

                    //                var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, userdataResult.mobileNo);
                    //            }
                    //        }
                    //    }



                    //}
                    //catch (Exception ex)
                    //{


                    //}




                }



                if(Session["MinuteList"] != null && model.MinuteID > 0)
                {
                    var listId = Session["MinuteList"];
                    if (new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID == 1)
                    {
                        var resultofNextMinutes = new GLUser().loadnewMinutes(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID));


                        if (resultofNextMinutes != null && resultofNextMinutes.Count>0)
                        {
                            string emcode = "";
                            try
                            {
                                emcode = resultofNextMinutes.Where(x => x.MinuteCode != model.MinuteCode && x.Statusofminute == "In-Process").ToList()[index].MinuteCode;

                            }
                            catch (Exception)
                            {
                                emcode = resultofNextMinutes.Where(x => x.MinuteCode != model.MinuteCode && x.Statusofminute == "In-Process").ToList()[0].MinuteCode;


                            }



                            listId = emcode;

                            return RedirectToAction("Edit", "Eminute", new { id = listId });

                        }
                    }
                    return RedirectToAction("ViewList", "Eminute", new { id = listId });

                }



                //if (Session["MinuteList"] !=null&&  model.MinuteID > 0)
                //{
                //    var listId = Session["MinuteList"];
                //    return RedirectToAction("ViewList", "Eminute", new { id= listId });
                //}

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

                return RedirectToAction("index","home");

            }
        }



        public void GetBudgetlist(tblEminuteInfo model)
        {
            try
            {

                List<tblEminuteBudgetDetail> listbudget = new List<tblEminuteBudgetDetail>();

                int count = 0;
                foreach (var item in model.BudgetSubID)
                {

                    tblEminuteBudgetDetail objbudget = new tblEminuteBudgetDetail();

                    objbudget.BudgetHeadID = item;
                    objbudget.EminuteCode = model.MinuteCode;
                    objbudget.date = DateTime.Now;
                    objbudget.BudgetSubID = model.BudgetHeadID.ToArray()[count];
                    objbudget.FinancalYear = model.FinancalYear.ToArray()[count];
                    objbudget.BudgetPro = Convert.ToDecimal(model.BudgetPro.ToArray()[count]);
                    objbudget.BudgetProjectID = Convert.ToInt32(model.BudgetProjectlist.ToArray()[count]);

                    objbudget.AlreadyExpense = Convert.ToDecimal(model.AlreadyExpense.ToArray()[count]);
                    objbudget.ThisExpense = Convert.ToDecimal(model.ThisExpense.ToArray()[count]);
                    objbudget.TotalExpense = Convert.ToDecimal(model.TotalExpenses.ToArray()[count]);
                    objbudget.Balance = Convert.ToDecimal(model.Balance.ToArray()[count]);
                    objbudget.IP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    objbudget.UserID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;


                    listbudget.Add(objbudget);
                    count++;

                }
                model.detailistbudget = listbudget;

                //using (var context= new HRandPayrollDBEntities())
                //{

                //    var resultminutebudgetList = context.tblEminuteBudgetDetails.Where(c => c.EminuteCode == model.MinuteCode).ToList();

                //    if (resultminutebudgetList!=null && resultminutebudgetList.Count>0)
                //    {

                //        foreach (var item in resultminutebudgetList)
                //        {
                //            var resultCurrentbuget = model.detailistbudget.FirstOrDefault(q => q.BudgetHeadID == item.BudgetHeadID && q.BudgetSubID == item.BudgetSubID);
                //            if (resultCurrentbuget!=null)
                //            {
                //                if (resultCurrentbuget.AlreadyExpense>0)
                //                {
                //                    resultCurrentbuget.AlreadyExpense = resultCurrentbuget.AlreadyExpense - item.ThisExpense;
                //                }
                              
                //                resultCurrentbuget.TotalExpense = resultCurrentbuget.TotalExpense - item.ThisExpense;
                //                resultCurrentbuget.Balance = resultCurrentbuget.Balance + item.ThisExpense;
                //            }
                           
                //        }
                //    }

                //}

                
                string stamp = "</br>";
                 stamp+= "<div class='row'>";

                foreach (var item in listbudget) 
                {
                    stamp += "<div class='col-md-3' style='width:30%'>";
                   // stamp += " <table  > <tr> <th colspan='2' style='width:174px;'> Al-Shifa Hospital RWP </th> </tr></ table> <table border = 1 >";
                    stamp += "<table border = 1 class='table table-bordered mb - none table - hover'><tr>";
                    stamp += "<th colspan='2'  style='text-align:center'  > <strong style='font-size:larger;'>Al-Shifa Hospital RWP </strong></th>";
                   
                    stamp += "</tr>";
                    stamp += "<tr>";
                    stamp += "<th colspan='2'  style='text-align:center' ><strong>" + new HRandPayrollSystemModel.InventryModel.tbl_Head().getheadname(Convert.ToInt32(item. BudgetSubID)) + " </strong></th>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<th colspan='2'  style='text-align:center' > <strong>" + new HRandPayrollSystemModel.InventryModel.tbl_Head().getheadsabname(Convert.ToInt32(item.BudgetHeadID)) + "</strong></th>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<th> <strong>Budget Head</strong> </th>";
                    stamp += "<th> <strong> Amount </strong> </th>";
                    stamp += "</tr>";


                    stamp += "<tr>";
                    stamp += "<td><strong> Budget Provision</strong></td>";
                    stamp += "<td> <strong style='color: blueviolet;'>" + string.Format("{0:N0}", item.BudgetPro) + "</strong>   </td>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<td><strong> Already Expenses</strong>  </td>";
                    stamp += "<td> <strong style='color: brown;'>   " + string.Format("{0:N0}", item.AlreadyExpense) + "</strong> </td>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<td><strong> This Expense </strong></td>";
                    stamp += "<td> <strong style='color: green;'>  " + string.Format("{0:N0}", item.ThisExpense) + "</strong>   </td>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<td> <strong>Total Expense</strong></td>";
                    stamp += "<td> <strong>  " + string.Format("{0:N0}", item.TotalExpense) + "</strong>   </td>";
                    stamp += "</tr>";

                    stamp += "<tr>";
                    stamp += "<td> <strong>Balance </strong></td>";
                    stamp += "<td>  <strong> " + string.Format("{0:N0}", item.Balance) + "  </strong> </td>";
                    stamp += "</tr>";
                    stamp += "</table> </br>";
                    stamp += "</div>";

                }
                stamp += "</div>";
                model.HtmlStamp = stamp;
           


            }
            catch (Exception ex)
            {

            }

        }

        public void smssend(tblEminuteInfo model)
        {


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
                            //else if (model.Priority == "2")
                            //{
                            //    Priority = "High";
                            //}
                            else
                            {
                                Priority = "Very High";
                            }



                            var smsBody = "EM No:" + ConvertTo_ProperCase(model.MinuteCode) + " \nPriority:" + ConvertTo_ProperCase(Priority)

                                + "\nSubject:" + ConvertTo_ProperCase(model.Subject) + " \nInitiated by:" + model.Initiatedby.ToUpper();

                            // ConvertTo_ProperCase(model.Initiatedby)

                            var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, userdataResult.mobileNo);
                        }
                    }
                }



            }
            catch (Exception ex)
            {


            }

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
        public JsonResult loadFarwardUser(int id  , int typeid, string istechperson )
        {
            if (id > 0)
            {
                    int techicalpersonID = 0;
                    var result = new tblEminuteInfo().gettechicalPerson(Convert.ToInt32(typeid));
                 
                    if (result != null)
                    {
                        techicalpersonID = (int) result.TechPerson;
                    }


      


                if (techicalpersonID > 0 && new SAGERPNEW2018.Models.SystemLogin().GetUser().IsTechicalPerson && techicalpersonID != new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID && !Convert.ToBoolean( istechperson  ))
                {

                    //var list = new tblEminuteInfo().LoadForwardDesginationWiseSendBack(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)).Where(x => x.Value == techicalpersonID.ToString()).ToList();

                    //return Json(list.Where(x => x.Value != new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID.ToString()), JsonRequestBehavior.AllowGet);



                    var list = new tblEminuteInfo().LoadForwardDesginationWiseSendBack(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs)).Where(x=>x.Value== techicalpersonID.ToString()).ToList();

                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var list = new tblEminuteInfo().LoadForwardDesginationWiseSendBack(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid, Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));


                    return Json(list, JsonRequestBehavior.AllowGet);

                }

                  
            }
            else
            {
              

                    var list = new tblEminuteInfo().LoadDepartmentForwardUser(Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID));

                    return Json(list.Where(x => x.Value != new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID.ToString()), JsonRequestBehavior.AllowGet);
                
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
        [UserRightFilters]
        public ActionResult AllMinutesReport(string id)
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

        public JsonResult loadallMinuestatus(int QuryStatus, int Deptid, string fromdate, string todate, int type, int TypeSub)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            DateTime fudate = DateTime.Parse(fromdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(todate, new CultureInfo("en-US", true));
            var check = new tblEminuteInfo().sp_ReportForAllminutes(useid, QuryStatus, Deptid, fudate, tudate, type, TypeSub);
            return Json(check, JsonRequestBehavior.AllowGet);
        }



        [UserRightFilters]
        public ActionResult AllComplaintReport(string id)
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

        public JsonResult loadalComplainttatus(int QuryStatus, int Deptid, string fromdate, string todate)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            DateTime fudate = DateTime.Parse(fromdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(todate, new CultureInfo("en-US", true));
            var check = new tblEminuteInfo().sp_ReportForAllcomplaint(useid, QuryStatus, Deptid, fudate, tudate);
            return Json(check, JsonRequestBehavior.AllowGet);
        }




        [UserRightFilters]
        public ActionResult PendingReport(string id)
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

        public JsonResult LoadPendingReport(int departmentID)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

            var check = new tblEminuteInfo().getDepartmentDaysWisePendingMinutes(departmentID, 1);
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadFixedAssiListTypewise(int typeid)
        {
            var departmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;

            var check = new tblEminuteInfo().getfixassitTypelist(departmentID, typeid);
            return Json(check, JsonRequestBehavior.AllowGet);
        }



        public JsonResult LoadFixedAssiListTypeSubwise(int typeSubid)
        {
            var departmentID = new SAGERPNEW2018.Models.SystemLogin().GetUser().DepartmentID;

            var check = new tblEminuteInfo().getfixassitTypeSublist(departmentID, typeSubid);
            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public string sendsms(string bodysms, string phone)
        {


            try
            {
                string ApiPhoneNmber = ConfigurationManager.AppSettings["SMSApiNmber"];
                string ApiPhonePass = ConfigurationManager.AppSettings["SMSApiPass"];

                ServiceReference1.QuickSMSResquest QSMS = new ServiceReference1.QuickSMSResquest();
                CorporateCBSClient objsend = new CorporateCBSClient("BasicHttpBinding_ICorporateCBS");
                QSMS.loginId = ApiPhoneNmber;
                QSMS.loginPassword = ApiPhonePass;
                QSMS.Destination = phone;
                QSMS.Mask = "AL-SHIFA";
                QSMS.Message = bodysms;
                QSMS.UniCode = "1";
                QSMS.ShortCodePrefered = "n";
                return objsend.QuickSMS(QSMS);


            }
            catch (Exception)
            {

                return "Error:Message Sending Fail. Please Check Your internet connection,";
            }



        }



        public JsonResult getSubtype(int type)
        {

            var result = new tblEminuteInfo().loadSubtypebyTypeID(type).OrderBy(x => x.Text);

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadHeadCostCenter(int costcenter)
        {

            var result = new HRandPayrollSystemModel.InventryModel.tbl_Head().gethead(costcenter).OrderBy(x => x.Text).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadCostCenter( int projectId)
        {

            var result = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheadsab( projectId).OrderBy(x => x.Text).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getSubHead(int headid, int projectId)
        //{

        //    var result = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheadsab(headid, projectId).OrderBy(x => x.Text).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult getHeadDetail(string headid, string subhead, string fyear  )
        {

          var result= new HRandPayrollSystemModel.InventryModel.tbl_Head().getCostCenterwiseheaddetails(headid, subhead, fyear);




            //var result = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheaddetails(headid, subhead, fyear);
            //var resultexpense=   new HRandPayrollSystemModel.InventryModel.tbl_Head().getExpensebudget(headid.ToString(), subhead.ToString(), "01/01/2050", "01/01/2050", Convert.ToInt32(fyear) );
            //var Reallocation = new HRandPayrollSystemModel.InventryModel.tbl_Head().getProvion(headid, subhead, fyear);
            //decimal expenseAmount = 0;
            // result.BudgetPro = result.BudgetPro + (Reallocation*-1);

            //if (resultexpense!=null && resultexpense.Count>0)
            //{
            //    expenseAmount = (Convert.ToDecimal(resultexpense.Sum(x => x.credit)) - Convert.ToDecimal(resultexpense.Sum(x => x.debit)));

            //}

            ////    var Currentexpsnes  =   result.BudgetPro - expenseAmount ;

            //decimal Currentexpsnes;
            //if (expenseAmount != 0)
            //{
            //    Currentexpsnes = Convert.ToDecimal(result.BudgetPro) - expenseAmount;
            //}
            //else
            //{
            //    Currentexpsnes = 0;
            //}



            //result.Expense = Currentexpsnes;


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getHeadDetail(int headid, int subhead, string fyear  )
        //{

        //    var result = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheaddetails(headid, subhead, fyear);
        //    var resultexpense=   new HRandPayrollSystemModel.InventryModel.tbl_Head().getExpensebudget(headid.ToString(), subhead.ToString(), "01/01/2050", "01/01/2050", Convert.ToInt32(fyear) );
        //    var Reallocation = new HRandPayrollSystemModel.InventryModel.tbl_Head().getProvion(headid, subhead, fyear);
        //    decimal expenseAmount = 0;
        //     result.BudgetPro = result.BudgetPro + (Reallocation*-1);

        //    if (resultexpense!=null && resultexpense.Count>0)
        //    {
        //        expenseAmount = (Convert.ToDecimal(resultexpense.Sum(x => x.credit)) - Convert.ToDecimal(resultexpense.Sum(x => x.debit)));

        //    }

        //    //    var Currentexpsnes  =   result.BudgetPro - expenseAmount ;

        //    decimal Currentexpsnes;
        //    if (expenseAmount != 0)
        //    {
        //        Currentexpsnes = Convert.ToDecimal(result.BudgetPro) - expenseAmount;
        //    }
        //    else
        //    {
        //        Currentexpsnes = 0;
        //    }



        //    result.Expense = Currentexpsnes;


        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        
        public JsonResult loadfavlist(int id)
        {

          var favdata=  new tblFavorite().favlist(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid);

            return Json(favdata, JsonRequestBehavior.AllowGet);
        }


        public JsonResult favremovedata(int id)
        {

            var favdata = new tblFavorite().DeleteData(id);

            return Json(favdata, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadFavData(int id)
        {

            var favdata = new tblFavorite().getAlldata(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid).Where(x=>x.inactive==false).ToList();

            return Json(favdata, JsonRequestBehavior.AllowGet);
        }




        public JsonResult favouriteAdd( string text)
        {

            if (!string.IsNullOrWhiteSpace(text))
            {
                tblFavorite obj = new tblFavorite();

                obj.FavText = text;
                obj.FavRemarks = text;
                obj.UserID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                obj.EntryDate = DateTime.Now;
                obj.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                obj.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;

                return Json(obj.addata(obj), JsonRequestBehavior.AllowGet);



            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);


            }



        }

        public JsonResult LoadPresentAprovalNewMinutes(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getPresidentnewAllMinute(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }




        public ActionResult printPdfforPresidentApporvel(FormCollection data)
        {

            var Employeeid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var checkedMinutes = data["checkedMinuteIds"].ToString();

            var dt = new tblEminuteInfo().getPresidentnewMinuteData(Convert.ToInt32(Employeeid), checkedMinutes);

            var obj2 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(Employeeid);


            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptApprovelPresident.rpt"));



            rptH.SetDataSource(dt);
            rptH.SetParameterValue("username", obj2.Initiatedby);

            //rptH.SetDataSource();
            if (dt.Rows.Count>0)
            {
                var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "rptApprovelPresident.rpt" + DateTime.Now.Ticks + ".pdf");
            }




            return RedirectToAction("ApprovalViewList");

        }


        public ActionResult SearchMinuteforAttachment(tblEminuteInfo a)
        {
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);




                ViewData["Editmode"] = false;


                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }




        public ActionResult EminuteAttachments(string id)

        {
            try
            {
                //var aad = TempData["St"];

                tblEminuteInfo a = new tblEminuteInfo();

                var currentEminute = a.getcurrentMinuteByRowbyMinutecode(id);


                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);



                var obj = a.getAlldataByID(id, Convert.ToInt32(currentEminute.EmployeeID), 1);


                var minowner = a.getMinuteowneridID(id, 1);
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
                a.LocationProject = new HRandPayrollSystemModel.DBModel.tblProject().getAlldataByID(Convert.ToInt32(obj[0].ProjectID)).ProjectName;
                var emplopeinfo = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);

                // var getbudgetdata = new HRandPayrollSystemModel.InventryModel.tbl_Head().getheaddetails(Convert.ToInt32(obj[0].BudgetHead), Convert.ToInt32(obj[0].BudgetSubHead), obj[0].FinancialYear);
                var getbudgetdata = a.loaddetailbudget(obj[0].MinuteCode);


                if (getbudgetdata.Count > 0)
                {
                    ///  a.SupplierAccountNo = obj[0].SupplierAccountNo.Trim();
                    a.detailistbudget = getbudgetdata;
                    ViewData["Budgetmode"] = false;


                }

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
                a.Subtype = obj[0].Subtype;
                a.HtmlBox = obj[0].HtmlBox;
                a.fldStatus = obj[0].fldStatus;
                a.estimatedcost = obj[0].estimatedcost;
                a.BudgetAmount = obj[0].BudgetAmount;
                a.BillClear = obj[0].BillClear;
                a.estimatedcostRange = obj[0].estimatedcostRange;
                a.IspettyCash = obj[0].IspettyCash;
                a.CheckBudgetAmount = Convert.ToDecimal(obj[0].BudgetAmount);
                a.EntryID = obj[0].EntryID;
                a.Status = obj[0].Status;
                a.fldStatus = "0";



                if (obj[0].ModifiedID != null)
                {
                    a.ModifiedID = obj[0].ModifiedID;
                }
                else
                {
                    a.ModifiedID = a.EntryID;
                }


                a.ForwardTo = obj[0].ForwardTo;
                //a.Initiatedby = ConvertTo_ProperCase(obj[0].Initiatedby);
                //a.InitiatedDepartment = ConvertTo_ProperCase(obj[0].InitiatedDepartment);

                a.Initiatedby = obj[0].Initiatedby.ToUpper();
                a.InitiatedDepartment = obj[0].InitiatedDepartment.ToUpper();
                var FirstInituserdata = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByID(Convert.ToInt32(obj[0].EntryID));

                var Firstintdata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(Convert.ToInt32(FirstInituserdata.EmployeeID));

                a.Initiatedby = Firstintdata.Initiatedby;
                a.IsPo = obj[0].IsPo;
                a.IsBudget = obj[0].IsBudget;

            //    a.JsonHeadbudget = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().gethead(1).OrderBy(x => x.Text));
                a.JsonHeadFinancalYear = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getFinYear("01"));
                a.JsonProject = JsonConvert.SerializeObject(new HRandPayrollSystemModel.InventryModel.tbl_Head().getproject());


                ViewData["htmlboxdata"] = "";
                ViewData["RemarksDATA"] = "";



                var tmep = a.getTempMinute(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid.ToString());
                if (tmep != null)
                {
                    // a.HtmlBox = tmep.htmlbox;
                    a.Remarks = tmep.remarks;
                }
                // a.Remarks = tmep.remarks;

                return View("AttachmentView", a);

            }
            catch (Exception ex)
            {
                return View("AttachmentView");
            }

        }



        public ActionResult AttachmentView(tblEminuteInfo a)
        {
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                //a.Initiatedby = ConvertTo_ProperCase(obj.Initiatedby);
                //a.InitiatedDepartment = ConvertTo_ProperCase(obj.DepartmentName);

                a.Initiatedby = obj.Initiatedby.ToUpper();
                a.InitiatedDepartment = obj.DepartmentName.ToUpper();
                a.LocationProject = new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName;
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAttachment(tblEminuteInfo model, FormCollection collection)
        {
            bool check;


            model.ModifiedDate = DateTime.Now;
            model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
            model.eID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var checkpath = CreatImagesPath(model);

            check = model.UpdateAttachment(model);


            if (check)
            {
                TempData["ActionMessage"] = true;
                return RedirectToAction("EminuteAttachments/" + model.MinuteCode, "Eminute");

            }
            TempData["ActionMessage"] = false;
            return RedirectToAction("SearchMinuteforAttachment", model);


        }



        public JsonResult getTechnicalPersonComment(string code)
        {

            var result = new tblEminuteInfo().gettechicalPerson(code);

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult getminutestatusPoorclear(string code)
        {

            var resultclear = new tblEminuteInfo().getclearminute(code);
            var resultpo = new tblEminuteInfo().getminutepo(code);
            var dts = new tblEminuteInfo().GetWoTranctionMinute(code);

            var workcheck = Convert.ToInt32(dts.Tables[0].Rows[0][0]) == 0 ? false : true;
            var tranction = Convert.ToInt32(dts.Tables[1].Rows[0][0]) == 0 ? false : true;



            List<bool> myarray = new List<bool>();
            myarray.Add(resultclear);
            myarray.Add(resultpo);
            myarray.Add(workcheck);
            myarray.Add(tranction);


            return Json(myarray, JsonRequestBehavior.AllowGet);
        }





        public JsonResult removebudget(int project,int fy ,int budgethead,int subhead, string eminute)
        {

            
            var result = new tblEminuteInfo().removeEminuteBudget(project,fy, subhead, budgethead,  eminute);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult loadMinuteAge(String DaySearch)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

            if (string.IsNullOrEmpty(DaySearch))
            {

                return Json(new GLUser().loadnewMinutes(Convert.ToInt32(useid)).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new GLUser().loadnewMinutes(Convert.ToInt32(useid)).Where(x => x.totalDays == Convert.ToInt32(DaySearch)).ToList(), JsonRequestBehavior.AllowGet);

            }


        }


        [UserRightFilters]
        public ActionResult MinuteSearchForEdit(string id)

        {
            tblEminuteInfo a = new tblEminuteInfo();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;

                Session["MinuteList"] = @"../../Eminute/" + @"MinuteSearchForEdit/" + id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }


        [UserRightFilters]

        public ActionResult EditViewForMinute(string id)
        {
            try
            {
                //var aad = TempData["St"];

                tblEminuteInfo a = new tblEminuteInfo();

                var currentEminute = a.getcurrentMinuteByRowbyMinutecode(id);


                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);



                var obj = a.getAlldataByID(id, Convert.ToInt32(currentEminute.EmployeeID), 1);
                var minowner = a.getMinuteowneridID(id, 1);
                var update = a.UpdateCurrentMinuteData(obj[0].MinuteID, obj[0].MinuteType, obj[0].fldStatus);

                a.detailistDoc = a.getdetailistDocumentData(id);
                a.LocationProject = new HRandPayrollSystemModel.DBModel.tblProject().getAlldataByID(Convert.ToInt32(obj[0].ProjectID)).ProjectName;
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
                a.Subtype = obj[0].Subtype;
                a.HtmlBox = obj[0].HtmlBox;
                a.fldStatus = obj[0].fldStatus;


                a.ForwardTo = obj[0].ForwardTo;
                a.Initiatedby = obj[0].Initiatedby.ToUpper();
                a.InitiatedDepartment = obj[0].InitiatedDepartment.ToUpper();
                var FirstInituserdata = new HRandPayrollSystemModel.DBModel.GLUser().getAlldataByID(Convert.ToInt32(obj[0].EntryID));
                var Firstintdata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(Convert.ToInt32(FirstInituserdata.EmployeeID));
                a.Initiatedby = Firstintdata.Initiatedby;

                ViewData["htmlboxdata"] = "";


                //return View("create", a);
                return View("EditViewForMinute", a);

            }
            catch (Exception ex)
            {
                //return View("create");
                return View("EditViewForMinute");
            }
        }


        [UserRightFilters]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateHtmlBox(tblEminuteInfo model, FormCollection collection)
        {
            bool check;


            model.ModifiedDate = DateTime.Now;
            model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
            model.eID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;


            //check = model.UpdateAttachment(model);
            //check = model.UpdateEM_OLD(model);
            check = model.UpdateEM(model);


            if (check)
            {
                TempData["ActionMessage"] = true;
                //return RedirectToAction("EminuteAttachments/" + model.MinuteCode, "Eminute");
                return RedirectToAction("MinuteSearchForEdit", "Eminute");

            }

            TempData["ActionMessage"] = false;
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("MinuteSearchForEdit", "Eminute");

            //return RedirectToAction("SearchMinuteforAttachment", model);


        }





        public JsonResult checkpettycashlimite(decimal amount)
        {
          var userid= new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
          var check  =new tblEminuteInfo().pettylimitcheck(userid, amount);

            return Json(check, JsonRequestBehavior.AllowGet);
        }



    }
}