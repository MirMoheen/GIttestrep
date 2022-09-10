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
    public class LoanController : Controller
    {
        // GET: Loan
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblEmployeeLoan a = new tblEmployeeLoan();
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
        public ActionResult create(tblEmployeeLoan a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);

                a.LoanDate = DateTime.Now;
                a.PaymentStartDate = DateTime.Now;
                a.PaymentEndDate = DateTime.Now;
                a.RequestedAmount = 0;
                a.PaymentAmount = 0;





                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }

        [UserRightFilters]
        public ActionResult Payment(int id)
        {
           
            tblEmployeeLoan a = new tblEmployeeLoan();
            var obj = a.getAlldataByID(id);
            var alldata = a.getAllloandata(" where  alldata.LoanID  =" + Convert.ToInt32(id) + "");
            obj.fromDepartmentID = Convert.ToInt32(alldata.Rows[0]["DepartmentID"]);
            obj.fromDesignation = Convert.ToInt32(alldata.Rows[0]["DesignationID"]);
            obj.detailistDetail=   a.GetLoanDetail(id);
            if (obj.detailistDetail!=null)
            {
                obj.Total = obj.detailistDetail.Sum(x => x.Amount).ToString();

            }



            return View(obj);
          

        }

        [UserRightFilters]
        public ActionResult delete(int id)
        {
            try
            {
                tblEmployeeLoan a = new tblEmployeeLoan();

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

                tblEmployeeLoan a = new tblEmployeeLoan();
                var obj = a.getAlldataByID(Convert.ToInt32(IDo[0]));
                var alldata = a.getAllloandata(" where  alldata.LoanID  =" + Convert.ToInt32(IDo[0]) + "" );
                obj.fromDepartmentID= Convert.ToInt32( alldata.Rows[0]["DepartmentID"]);
                obj.fromDesignation = Convert.ToInt32(alldata.Rows[0]["DesignationID"]);
                obj. GrossSalry = Convert.ToDecimal( new tblEmployeeLoan().loadEmployeeData(Convert.ToInt32( alldata.Rows[0]["EmployeeId"])).GrossSalary);


                if (IDo[1] == "0")
                {
                    a.IsView = true;
                }
                obj.IsView = a.IsView;
                obj.LoanID = Convert.ToInt32(IDo[0]);
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }

        [UserRightFilters]

        public ActionResult Save(tblEmployeeLoan model)
        {
            try
            {
                int check;

                if (model.LoanID > 0)
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
                    model.ProjectID = Convert.ToInt32( new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
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


        public ActionResult SavePayment(tblEmployeeLoan model)
        {
            try
            {
               

                    model.EntryDate = DateTime.Now;
                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    var  check = model.insertPayment(model);

                
            

                if (check)
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


        public ActionResult Duplicate(int Name, string ID)
        {
            try
            {
                string json = "";
                var list = new tblEmployeeLoan().checkDuplicate(Convert.ToInt32(ID), Name);
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


        public ActionResult EmployeData(int ID)
        {
          
                var Objdata = new tblEmployeeLoan().loadEmployeeData(Convert.ToInt32(ID));
              
                    return Json(Objdata, JsonRequestBehavior.AllowGet);
              

        }


        public ActionResult LoanCheckEmployee(int ID)
        {
            string json = "";

            var LoanRemiangCHeck = new HRandPayrollSystemModel.DBModel.tblSalaryGeneration(). getAllRemaingLoanAmount(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));


            if (LoanRemiangCHeck.FirstOrDefault(q => q.EmployeeId == ID)!=null && LoanRemiangCHeck.FirstOrDefault(q => q.EmployeeId == ID).remaningAmount > 0)
            {
                json = "false";
            }
            else
            {
                json = "true";

            }

            return Json(json, JsonRequestBehavior.AllowGet);


        }



    }
}