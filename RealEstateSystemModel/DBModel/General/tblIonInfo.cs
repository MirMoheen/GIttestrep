

using HRandPayrollSystemModel.InventryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tblIonInfo
    {
        //public int IonID { get; set; }
        //public string IonCode { get; set; }
        //public Nullable<int> ProjectID { get; set; }
        //public Nullable<int> DepartmentID { get; set; }
        //public Nullable<System.DateTime> IonDate { get; set; }
        //public string Initiatedby { get; set; }
        //public string Type { get; set; }
        //public string Subject { get; set; }
        //public string HtmlBox { get; set; }
        //public Nullable<int> ForwardTo { get; set; }
        //public string ForInfo { get; set; }
        //public bool inactive { get; set; }
        //public Nullable<int> EntryID { get; set; }
        //public string EntryIP { get; set; }
        //public Nullable<System.DateTime> EntryDate { get; set; }
        //public int Status { get; set; }
        //public Nullable<int> MinuteType { get; set; }
        //public Nullable<int> EmployeeID { get; set; }
        //public string InitiatedDepartment { get; set; }
        //public Nullable<int> Displayfornew { get; set; }
        //public string fldStatus { get; set; }
        //public Nullable<int> ModifiedID { get; set; }

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public string LocationProject { get; set; }
        public string DepartmentName { get; set; }
        public bool IsComplete { get; set; }
        public bool IsApprove { get; set; }
        public bool IsCancel { get; set; }
        public bool IsDepartment { get; set; }
        public int NEwForwardTo { get; set; }
        public int eID { get; set; }

        public int Transferedfrom { get; set; }

        public string UserColor { get; set; }
        public string ForwardUserColor { get; set; }
        public string CurrentLogEmployeedetail { get; set; }



        public int ReminderMinute { get; set; }
        public int HoldMinute { get; set; }




        public int Minuteopen { get; set; }



        public IEnumerable<string> SelectInfo { get; set; }


        public IEnumerable<tblIONDocDetail> detailistDoc { get; set; }


        // detail doc

        public IEnumerable<string> lisdescription { get; set; }

        public IEnumerable<string> listdocpath { get; set; }
        public IEnumerable<HttpPostedFileBase> DocFile { get; set; }






        public IEnumerable<sp_getDayWisePendingMinute_Result> listDayWise { get; set; }




        public string generateIonNo(int mintype, string prefix)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    var resultList = context.sp_NextNOION(mintype).ToList();
                    //return prefix + "-" + DateTime.Now.Year + "-" + resultList[0].Value.ToString("D4");
                    return prefix + "-" + resultList[0].Value.ToString("D4");


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int? transfercheck(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUsers.FirstOrDefault(x => x.EmployeeID == id);
                    if (result != null)
                    {
                        if (result.IsTransfer)
                        {
                            return result.TransferEmpID;
                        }
                        else
                        {
                            return null;
                        }


                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int loadlastTowMinute(int userID)
        {

            using (var db = new HRandPayrollDBEntities())
            {
                var result = db.GLUsers.FirstOrDefault(t => t.Userid == userID);
                if (result != null)
                {
                    return result.EmployeeID;
                }
                else
                {
                    return 0;
                }
            }


        }




        public tblEminuteInfo LoadEmployeeInfo(int employeeid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    tblEminuteInfo obj = new tblEminuteInfo();
                    var result = context.tblEmployees.FirstOrDefault(x => x.EmployeeID == employeeid);
                    if (result != null)
                    {
                        var resultdesignation = context.tblDesignations.FirstOrDefault(x => x.DesignationID == result.DesignationID);
                        var Department = context.tblDepartments.FirstOrDefault(x => x.DepartmentID == result.DepartmentID).DepartmentName;
                        obj.Initiatedby = result.EmployeeName + " - " + resultdesignation.DesignationTitle;
                        obj.DepartmentName = Department;
                        obj.IsApprove = resultdesignation.IsApprove;
                        obj.IsCancel = resultdesignation.IsCancel;
                        obj.IsComplete = resultdesignation.IsComplete;
                        obj.IsDepartment = resultdesignation.IsDepartment;
                        obj.ForwardUserColor = context.GLUsers.FirstOrDefault(x => x.EmployeeID == employeeid).UserColor;




                        return obj;

                    }
                    else { return null; }
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public tempIonInfo getTempIon(string uid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tempIonInfoes.SingleOrDefault(x => x.IP == uid);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int UpdateCurrentIonData(int obj, int? type, string fldstatus, string forin)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tblIonInfoes.SingleOrDefault(x => x.IonID == obj && x.MinuteType == type && x.ForInfo == forin && x.inactive == false);

                            //if (result != null && fldstatus != null)
                            if (result != null)
                            {
                                ////IF ION Read then set this to 0                       
                                result.Displayfornew = 0;
                                //context.SaveChanges();
                            }
                            //IF ION Read then set this to 0                       
                            //result.Displayfornew = 0;

                            dbContextTransaction.Commit();
                            context.SaveChanges();
                            return result.IonID;
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<sp_CheckForEditForION_Result> getAlldataIONforinfoID(int employeeID, int mit)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_CheckForEditForION(employeeID, mit).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_getIonOwner_Result> GetIonOwnerID(string code, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getIonOwner(code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getAllIonToEdit_Result> getAlldataIonByID(string code, int employeeID, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllIonToEdit(employeeID, code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblIONDocDetail> getdetailistDocumentData(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tblIONDocDetails.Where(x => x.IONCode == code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadForwardDesginationWise(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Select All--", Value = "0" });

                    var resultList = context.sp_EmployeeAllForward().ToList();



                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }));

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadAllEmp(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();


                    var resultList = context.sp_EmployeeAllForward().ToList();



                    listobj.AddRange(resultList.Select(x => new SelectListItem { Value = x.EmployeeID.ToString() }));

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public int canncelIon(tblIonInfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tblIonInfoes.Where(x => x.IonCode == obj.IonCode && x.MinuteType == obj.MinuteType);
                            if (result != null && result.Count() > 0)
                            {

                              var resultIOn=  result.FirstOrDefault(x=>x.inactive==false);

                                if (resultIOn!=null)
                                {
                                    var obj1 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(obj.eID);
                                    var host = new UriBuilder(HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);
                                    resultIOn.HtmlBox = resultIOn.HtmlBox + "<hr/> <strong>ION Canncel By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>  </p> <img src='" + host + "/AppFiles/cancelion.png' style='width:30%;' ><br/><hr> ";

                                }

                                foreach (var item in result)
                                {
                                    item.HtmlBox = resultIOn.HtmlBox;
                                    item.Status = 3;

                                }

                                context.SaveChanges();
                          
                            }
                           
                          

                            context.SaveChanges();



                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.IonID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public int FurtherAddition(tblIonInfo obj, string selectedempl)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tblIonInfoes.Where(x => x.IonCode == obj.IonCode && x.MinuteType == obj.MinuteType);
                            if (result != null && result.Count() > 0)
                            {
                                //For Unread ION
                                //obj.Displayfornew = 1;
                            }
                            else
                            {
                                //status
                                //1 new 
                                //2 pending
                                //3 complete

                                //minute type
                                //1 minute
                                //2 complain
                                //3 ion
                                //obj.Remarks = null;

                                ////For Unread ION
                                obj.Displayfornew = 0;
                                obj.HtmlBox = obj.HtmlBox.Replace("&Nbsp;", "");
                                int cofp = Regex.Matches(obj.HtmlBox, "<p>").Count;
                                //obj.HtmlBox = obj.HtmlBox.Replace("<p>", " <p> ");


                                //obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By : " + obj.Initiatedby +" " on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong><hr>  ";
                                obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By :  <span style='color:" + obj.UserColor + "'> " + obj.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + "   </strong><hr>  ";


                                obj.Status = 1;
                                var forinfo2 = obj.ForInfo;
                                obj.ForInfo = null;
                                obj.fldStatus = "In-Process";
                                var asasa = forinfo2.Split(',');

                                try
                                {

                                    if (selectedempl != "0")
                                    {
                                        obj.HtmlBox += "<strong>To: </strong><br/><br/>";
                                        int lop = 1;
                                        foreach (var item in asasa)
                                        {
                                            var obj11 = obj.LoadEmployeeInfo(Convert.ToInt16(item));
                                            //obj.HtmlBox += item;
                                            obj.HtmlBox = obj.HtmlBox + " <strong> " + lop++ + " - " + "<strong>  <span style ='color:" + obj.UserColor + "' >" + obj11.Initiatedby + " </span> </strong><br/>";
                                        }

                                    }
                                    else
                                    {
                                        obj.HtmlBox += "<strong>To: </strong><br/ ><strong>All the concerned Department and Employees.</strong>";
                                    }


                                }
                                catch (Exception ex)
                                {


                                }
                                context.tblIonInfoes.Add(obj);
                                context.SaveChanges();


                                obj.Status = 0;
                                // obj.ForwardTo = null;

                                //obj.Remarks = null;

                                if (forinfo2 != "" && forinfo2 != null)
                                {
                                    //obj.Displayfornew = 0;
                                    string[] forinfo = forinfo2.Split(',');
                                    for (int i = 0; i < forinfo.Length; i++)
                                    {
                                        obj.ForInfo = forinfo[i];
                                        obj.fldStatus = "For Info";
                                        obj.EmployeeID = Convert.ToInt32(obj.ForInfo);
                                        obj.Displayfornew = 1;
                                        context.tblIonInfoes.Add(obj);
                                        context.SaveChanges();

                                    }

                                }
                                //obj.fldStatus = null;
                                ////obj.Displayfornew = 0;
                                //obj.ForInfo = null;

                                //context.tblIonInfoes.Add(obj);
                                //context.SaveChanges();


                                int a = 0;
                                foreach (var item in obj.listdocpath)
                                {
                                    //tblEminuteDocDetail objdss = new tblEminuteDocDetail();
                                    tblIONDocDetail objdss = new tblIONDocDetail();
                                    ////objdss.Description = lisdescription.ToArray()[a];
                                    objdss.Description = lisdescription.ToArray()[a] + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";

                                    objdss.IONCode = obj.IonCode;
                                    objdss.PathDoc = obj.listdocpath.ToArray()[a];
                                    objdss.Inative = false;
                                    objdss.Ip = obj.EntryIP;


                                    objdss.date = DateTime.Now;


                                    a++;
                                    context.tblIONDocDetails.Add(objdss);
                                    context.SaveChanges();

                                }
                            }






                            //  obj.CompID = new Login().GetUser().CompID;



                            context.SaveChanges();



                            //////////////////////////////////////////////////
                            var rem = context.tempIonInfoes.SingleOrDefault(x => x.IP == obj.EntryID.ToString());
                            if (rem != null)
                            {
                                context.tempIonInfoes.Remove(rem);
                                context.SaveChanges();
                            }
                            ////////////////////////////////////////////////



                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.IonID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public void getemployeename(int jk)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployees.SingleOrDefault(x => x.EmployeeID == jk);
                    if (result != null)
                    {
                        result.EmployeeName.ToString();


                    }

                }
            }
            catch (Exception ex)
            {


            }
        }


        public int adTempdataIon(tempIonInfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tempIonInfoes.FirstOrDefault(x => x.IP == obj.IP);
                            if (result != null)
                            {
                                context.tempIonInfoes.Remove(result);
                                context.SaveChanges();


                            }
                            context.tempIonInfoes.Add(obj);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw;
                        }
                    }
                    return obj.tempIonid;
                }

            }
            catch (Exception ex)
            {

                return 0;
            }
        }




        public string getviewedEmployee(int ionid)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_viewedEmployees(ionid).FirstOrDefault();
                   

                }
            }
            catch (Exception ex)
            {
                return  "Viewed";

            }
        }

    }
}
