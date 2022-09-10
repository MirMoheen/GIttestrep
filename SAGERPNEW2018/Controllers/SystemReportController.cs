using CrystalDecisions.CrystalReports.Engine;
using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace SAGERPNEW2018.Controllers
{
    public class SystemReportController : Controller
    {
        // GET: SystemReport
        [UserRightFilters]
        public ActionResult RecoveryReport()
        {
            ReportModel a = new ReportModel();
            a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
            a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
            a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }

        [UserRightFilters]

        public ActionResult GetRecoveryReport(string data)
        {
            ReportModel a = new ReportModel();

           
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "RecoveryReport.rpt"));
            var ids = data.Split('|');
            DataTable dt = a.GetRecoveryData(ids[1]);
            dt.Columns.Add("PaidOndate");
            dt.Columns.Add("Surcharges");
            dt.Columns.Add(new DataColumn("Barcode", typeof(byte[])));


            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(dt.Rows[0]["RegistrationNo"].ToString(), QRCodeGenerator.ECCLevel.H);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    var asdas = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    var t = asdas.Substring(22);
                    byte[] imageBytes = Convert.FromBase64String(t);
                    dt.Rows[0]["Barcode"] = imageBytes;

                }
            }



            DataSet ds = a.GetRecoveryDateData(Convert.ToInt32("0"+ ids[1]));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string b = dt.Rows[i]["Ptype"].ToString();
                if (b == "BOOKNG")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Rows[i]["PaidOndate"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Select("PaymentType='" + b + "'").Length > 0)
                    {
                        DataTable dt3 = ds.Tables[1].Select("PaymentType='" + b + "'").CopyToDataTable();
                        if (dt3.Rows.Count > 0)
                        {
                            dt.Rows[i]["PaidOndate"] = dt3.Rows[0]["entrydate"].ToString();
                        }
                    }
                }
                string[] c = b.Split('.');
                if (ds.Tables[2].Rows.Count > 0)
                {
                    if(c.Length> 1)
                    if (ds.Tables[2].Select("installmentNo='" + c[1] + "'").Length > 0)
                    {
                       DataTable dt3 = ds.Tables[2].Select("installmentNo='" + c[1] + "'").CopyToDataTable();
                        if (dt3.Rows.Count > 0)
                        {
                            dt.Rows[i]["PaidOndate"] = dt3.Rows[0]["EntryDate"].ToString();
                        }
                    }
                }

            }

            rptH.Database.Tables[0].SetDataSource(dt);
            //rptH.SetDataSource();
            if (dt.Rows.Count > 0)
            {
                var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);
                
                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "RecoveryReport.rpt"+ DateTime.Now.Ticks+".pdf");
            }


            return RedirectToAction("RecoveryReport", a);
        }


       [UserRightFilters]
        public ActionResult PaymentEntryReport()
        {
            ReportModel a = new ReportModel();
            a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
            a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
            a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }


        //public JsonResult LoadPaymentEntry( DateTime DateFrom, DateTime DateTo)
        //{

        //    using (var db = new HRandPayrollDBEntities())
        //    {
        //        var data = db.sp_GetPaymentEntryDetailReport(DateFrom, DateTo).ToList();
        //        if (data.Count > 0)
        //        {
        //            return Json(data, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            return Json("", JsonRequestBehavior.AllowGet);


        //        }


        //    }


        //}
        [UserRightFilters]

        public ActionResult GetPaymentEntryReport(string data)
        {
            ReportModel a = new ReportModel();


            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "PaymentEntryDatewiseReport.rpt"));
            var ids = data.Split('|');
            var dt = a.GetPaymmentDetailDatewise(Convert.ToDateTime(ids[0]), Convert.ToDateTime(ids[1]));
            rptH.Database.Tables[0].SetDataSource(dt);
            //rptH.SetDataSource();
            if (dt.Rows.Count > 0)
            {
                var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "PaymentEntryDatewiseReport.rpt" + DateTime.Now.Ticks + ".pdf");
            }


            return RedirectToAction("CashCollectionReport", a);
        }

        [UserRightFilters]
        public ActionResult CashCollection()
        {
            ReportModel a = new ReportModel();
            a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
            a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
            a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }
        [UserRightFilters]

        public ActionResult GetCashCollection(string data)
        {
            ReportModel a = new ReportModel();


            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "CashCollection.rpt"));
            var ids = data.Split('|');
            var dt = a.GetDataCashCollection(Convert.ToDateTime(ids[0]), Convert.ToDateTime(ids[1]));
            rptH.Database.Tables[0].SetDataSource(dt);
            //rptH.SetDataSource();
            if (dt.Rows.Count > 0)
            {
                var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "CashCollection.rpt" + DateTime.Now.Ticks + ".pdf");
            }


            return RedirectToAction("CashCollection", a);
        }

        //public JsonResult loadCashCollectionData(DateTime DateFrom,DateTime DateTo)
        //{

        //    using (var db= new HRandPayrollDBEntities())
        //    {
        //     var data=   db.sp_GetCashCollectionData(DateFrom, DateTo).ToList();
        //        if (data.Count>0)
        //        {
        //            return Json(data, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            return Json("", JsonRequestBehavior.AllowGet);


        //        }


        //    }


        //}

        [UserRightFilters]
        public ActionResult TrailbalanceReport()
        {
            ReportModel a = new ReportModel();
            a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
            a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
            a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }

        //public JsonResult LoadtrialBalance(DateTime DateFrom, DateTime DateTo)
        //{

        //    using (var db = new HRandPayrollDBEntities())
        //    {
        //        var data = db.sp_trailbalanceSheet(DateFrom, DateTo).ToList();
        //        if (data.Count > 0)
        //        {
        //            return Json(data, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            return Json("", JsonRequestBehavior.AllowGet);


        //        }


        //    }


        //}
        //public ActionResult GetTrialBalanceReport(string data)
        //{
        //    ReportModel a = new ReportModel();


        //    ReportDocument rptH = new ReportDocument();
        //    rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "TrialBalanceReport.rpt"));
        //    var ids = data.Split('|');
        //    var dt = a.GetTrialBalanceDatewise(Convert.ToDateTime(ids[0]), Convert.ToDateTime(ids[1]));

        //    for (int i = dt.Rows.Count - 1; i >= 0; i--)
        //    {
        //        if (Convert.ToInt64(dt.Rows[i]["ParentAccountID"]) != 0)
        //        {
        //            Int64 parentNode = Convert.ToInt64(dt.Rows[i]["ParentAccountID"]);
        //            Decimal value = Convert.ToDecimal(dt.Rows[i]["opdebit"]);
        //            Decimal value1 = Convert.ToDecimal(dt.Rows[i]["opcredit"]);
        //            Decimal value2 = Convert.ToDecimal(dt.Rows[i]["movdebit"]);
        //            Decimal value3 = Convert.ToDecimal(dt.Rows[i]["movcredit"]);
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                if (parentNode == Convert.ToInt64(row["AccountID"]))
        //                {
        //                    row["opDebit"] = Convert.ToDecimal(row["opdebit"]) + value;
        //                    row["opcredit"] = Convert.ToDecimal(row["opcredit"]) + value1;
        //                    row["movdebit"] = Convert.ToDecimal(row["movdebit"]) + value2;
        //                    row["movcredit"] = Convert.ToDecimal(row["movcredit"]) + value3;
        //                }
        //            }
        //        }
        //    }

        //    DataTable dtf = new DataTable();
        //    if (ids[2] != "0")
        //    {
        //        DataTable dtf1 = dt.Select(" ab <= " + Convert.ToInt32(ids[2])).CopyToDataTable();

        //        dtf1.DefaultView.Sort = "AccountNo,ParentAccountID  asc";
        //        dtf1 = dtf1.DefaultView.ToTable();
        //        dtf = dtf1;
        //    }
        //    else
        //    {
        //        dtf = dt;
        //    }

        //    rptH.Database.Tables[0].SetDataSource(dtf);
        //    //rptH.SetDataSource();
        //    if (dtf.Rows.Count > 0)
        //    {
        //       // var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

        //        Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        return File(stream, "application/pdf", "TrialBalanceReport.rpt" + DateTime.Now.Ticks + ".pdf");
        //    }


        //    return RedirectToAction("TrailbalanceReport", a);
        //}



        //[UserRightFilters]
        //public ActionResult LedagerReport()
        //{
        //    ReportModel a = new ReportModel();
        //    a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
        //    a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
        //    a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
        //    a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
        //    return View(a);
        //}

        //public JsonResult LoadLedger(int Id, DateTime DateFrom, DateTime DateTo)
        //{

        //    using (var db = new HRandPayrollDBEntities())
        //    {
        //        var data = db.sp_GeneralLegderReport(Id,DateFrom, DateTo).ToList();
        //        if (data.Count > 0)
        //        {
        //            return Json(data, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            return Json("", JsonRequestBehavior.AllowGet);


        //        }


        //    }


        //}
        public ActionResult GetLedagerReport(string data)
        {
            ReportModel a = new ReportModel();


            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "GeneralLedgerReport.rpt"));
            var ids = data.Split('|');
            var dt = a.GetAccountLedger(Convert.ToDateTime(ids[0]), Convert.ToDateTime(ids[1]), Convert.ToInt32(ids[2]));
            rptH.Database.Tables[0].SetDataSource(dt);
            //rptH.SetDataSource();
            if (dt.Rows.Count > 0)
            {
                // var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "GeneralLedgerReport.rpt" + DateTime.Now.Ticks + ".pdf");
            }


            return RedirectToAction("LedagerReport", a);
        }



        [UserRightFilters]
        public ActionResult employeeSalarySlip()
        {
            ReportModel a = new ReportModel();
            a.Isdelete = Convert.ToBoolean(TempData["IsNew"]);
            a.IsNew = Convert.ToBoolean(TempData["IsEdit"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsDelete"]);
            a.Isedit = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }

        public ActionResult PrintemployeeSalarySlip(ReportModel model)
        {
            ReportModel a = new ReportModel();


            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "EmoloyeeSalarySlip.rpt"));
            // var ids = data.Split('|');
            model.projectID = Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
            var dts = a.GetEmployeeSalarySlip(model);
          
            var dt1 = dts.Tables[2];
            var dt2 = dts.Tables[1];
            var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);
     

            //   dts.Tables[0].Columns.Add(new DataColumn("Barcode", typeof(byte[])));


            using (MemoryStream ms = new MemoryStream())
            {
                foreach (DataRow item in dts.Tables[0].Rows)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(item["EmployeeNo"].ToString(), QRCodeGenerator.ECCLevel.H);
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        var asdas = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                        var t = asdas.Substring(22);
                        byte[] imageBytes = Convert.FromBase64String(t);
                        item["Barcode"] = imageBytes;

                    }
                    item["EmployeePhotoPath"] = host + item["EmployeePhotoPath"].ToString().TrimStart('~');


                    item["AmountAllowance"] = dt1.Select("EmployeeID ="+item["EmployeeID"]).CopyToDataTable(). Compute("Sum(Amount)", string.Empty);
                    item["AmountDudection"] = dt2.Select("EmployeeID =" + item["EmployeeID"]).CopyToDataTable().Compute("Sum(Amount)", string.Empty);


                }
            }

        
        

           



            rptH.Database.Tables[0].SetDataSource(dts.Tables[0]);
            rptH.Subreports[0].SetDataSource(dts.Tables[2]);
            rptH.Subreports[1].SetDataSource(dts.Tables[1]);
            rptH.SetParameterValue("ProjectName", new SAGERPNEW2018.Models.SystemLogin().GetProject().ProjectName); 
            rptH.SetParameterValue("ReportTitle", "Employees Salary Slip Report "+ new DateTime(model.year,model.Month,1).ToString("MMM-yyyy") );
            

            //rptH.SetDataSource();
            if (rptH.Rows.Count > 0)
            {
                // var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf", "employeeSalarySlip" + DateTime.Now.Ticks + ".pdf");
            }

            TempData["ActionMessage"] = false;
            return RedirectToAction("employeeSalarySlip", a);
        }


    }
}