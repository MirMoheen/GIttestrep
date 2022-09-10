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
    public partial class tbl_BoardProceeding
    {


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
        public string ForwardUserColor { get; set; }

        public IEnumerable<tbl_BoardProceeding> getallids { get; set; }
        public IEnumerable<tbl_BoardProceedingDOC> detailistDoc { get; set; }
        public IEnumerable<string> listdocpath { get; set; }
        public IEnumerable<HttpPostedFileBase> DocFile { get; set; }

        public IEnumerable<string> SelectInfo { get; set; }

        //public IEnumerable<string> ChairPerson { get; set; }

        public int NEwForwardTo { get; set; }
        public int Transferedfrom { get; set; }

        public string UserColor { get; set; }

        public string CurrentLogEmployeedetail { get; set; }

        // detail doc

        public IEnumerable<string> lisdescription { get; set; }

        public IEnumerable<sp_EmployeeName_Board_Result> getids { get; set; }


        public bool CheckUserSign { get; set; }

        public IEnumerable<string> coun { get; set; }

        //---------------------------------------------------------------------------------------------------------------------------------


        public List<sp_EmployeeName_Board_Result> GetAllIDS(string empid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_EmployeeName_Board(empid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool SignatureStatus(string id, int em)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    //return Convert.ToBoolean(context.sp_CheckSign_BoardSheet(id,em).FirstOrDefault().Value);
                    var result =context.sp_CheckSign_BoardSheet(id,em).FirstOrDefault().Value;
                  

                       // context.AttendanceDeviceConfigurations.FirstOrDefault(x => x.Mac == code && x.Status == false);
                    if (result == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public tbl_BoardProceeding LoadEmployeeInfo(int employeeid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    tbl_BoardProceeding obj = new tbl_BoardProceeding();
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


        public string generateProceedingNo(int mintype, string prefix)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    var resultList = context.sp_NextProceedingNumber(mintype).ToList();
                    return prefix + "-" + resultList[0].Value.ToString("D4");


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tbl_BoardProceedingDOC> getdetailistDocumentData(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tbl_BoardProceedingDOC.Where(x => x.MinuteSheetCode == code).ToList();

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
                    //listobj.Add(new SelectListItem { Text = "--Select All--", Value = "0" });

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

        public IEnumerable<SelectListItem> ForwardUsersMultiple(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Select Board Members--", Value = "0" });

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

        public IEnumerable<SelectListItem> LoadBoardPresident(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Select Board Chairman--", Value = "0" });

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

        public int SignOfBoardUser(tbl_BoardProceeding obj, int obj11, int? type, string fldstatus)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tbl_BoardProceeding.SingleOrDefault(x => x.MinuteSheetID == obj11 && x.MinuteType == type && x.inactive == false);
                            if (result != null && fldstatus != null)
                            {
                                result.SignMinute = true;
                                result.SignDate = obj.SignDate;
                                result.TimeofSign = obj.TimeofSign;
                                result.DateofSign = obj.DateofSign;
                                result.Displayfornew = 0;
                                context.SaveChanges();
                            }
                           
                                context.SaveChanges();

                                dbContextTransaction.Commit();
                          
   

                            return result.MinuteSheetID;
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

        public int FurtherAddition(tbl_BoardProceeding obj, string selectedempl)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tbl_BoardProceeding.Where(x => x.MinuteSheetCode == obj.MinuteSheetCode && x.MinuteType == obj.MinuteType);
                           
                                
                                obj.Displayfornew = 0;
                                obj.HtmlBox = obj.HtmlBox.Replace("&Nbsp;", "");
                                int cofp = Regex.Matches(obj.HtmlBox, "<p>").Count;
                                obj.HtmlBox = obj.HtmlBox.Replace("<p>", " <p> ");

                           




                                obj.Status = 1;
                                var forinfo2 = obj.ForInfo;
                                obj.ForInfo = null;
                                obj.fldStatus = "In-Process";
                            obj.RoleInThisSheet= "Initiater";
                            var asasa = forinfo2.Split(',');
                               var chairper = obj.ChairPerson;

                           

                           // obj.HtmlBox = obj.HtmlBox + "<br/><br/><strong> Initiated By :  <span style='color:" + obj.UserColor + "'> " + obj.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + "   </strong><hr>  ";
                            obj.HtmlBox = obj.HtmlBox + " ";


                            context.tbl_BoardProceeding.Add(obj);
                                context.SaveChanges();


                                obj.Status = 0;


                                if (forinfo2 != "" && forinfo2 != null)
                                {
                                    //obj.Displayfornew = 0;
                                    string[] forinfo = forinfo2.Split(',');
                                    for (int i = 0; i < forinfo.Length; i++)
                                    {
                                        obj.ForInfo = forinfo[i];
                                    if(obj.ForInfo == chairper)
                                    {
                                        obj.RoleInThisSheet = "Chair-Person";
                                    }
                                    else
                                    {
                                        obj.RoleInThisSheet = "Committe Member";
                                    }
                                        obj.fldStatus = "For Info";
                                        obj.EmployeeID = Convert.ToInt32(obj.ForInfo);
                                        obj.Displayfornew = 1;
                                        context.tbl_BoardProceeding.Add(obj);
                                        context.SaveChanges();

                                    }

                                }



                                int a = 0;
                                foreach (var item in obj.listdocpath)
                                {

                                    tbl_BoardProceedingDOC objdss = new tbl_BoardProceedingDOC();

                                    objdss.Description = lisdescription.ToArray()[a] + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";

                                    objdss.MinuteSheetCode = obj.MinuteSheetCode;
                                    objdss.PathDoc = obj.listdocpath.ToArray()[a];
                                    objdss.Inative = false;
                                    objdss.Ip = obj.EntryIP;


                                    objdss.date = DateTime.Now;


                                    a++;
                                    context.tbl_BoardProceedingDOC.Add(objdss);
                                    context.SaveChanges();

                                }
                            





                            context.SaveChanges();
                            var rem = context.tempBoardSheetInfoes.SingleOrDefault(x => x.IP == obj.EntryID.ToString());
                            if (rem != null)
                            {
                                context.tempBoardSheetInfoes.Remove(rem);
                                context.SaveChanges();
                            }




                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.MinuteSheetID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int transfertoactualpresident(tbl_BoardProceeding obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tbl_BoardProceeding.Where(x => x.MinuteSheetCode == obj.MinuteSheetCode && x.MinuteType == obj.MinuteType);


                           
                                //obj.Displayfornew = 0;
                                string forinfo = "1";
                               
                                    obj.ForInfo = forinfo;
                                    obj.fldStatus = "For Info";
                            obj.RoleInThisSheet = "President";
                            obj.TimeofSign = null;
                            obj.DateofSign = null;
                                    obj.EmployeeID = Convert.ToInt32(obj.ForInfo);
                                    obj.Displayfornew = 1;
                            obj.HtmlBox = obj.HtmlBox;

                            context.tbl_BoardProceeding.Add(obj);
                                    context.SaveChanges();

                                

                            




                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.MinuteSheetID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<sp_getAllBoardToEdit_Result> getAllBoardDataByID(string code, int employeeID, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllBoardToEdit(employeeID, code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getBoardOwner_Result> GetIonOwnerID(string code, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getBoardOwner(code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_EditForBoard_Result> GetAllDataForinfoID(int employeeID, int mit)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_EditForBoard(employeeID, mit).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Nullable<int>> sp_getCount(string mn)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GetSignCount(mn).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_GetRelatedMinutePerson_Result> sp_getRelatedPerson(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_GetRelatedMinutePerson(code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_RelatedPerson_OrderWIse_Result> sp_RelatedPerson_OrderWise(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_RelatedPerson_OrderWIse(code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_AllBoardPerson_Result> sp_AllBoardPerson(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_AllBoardPerson(code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_GetBoardSheet_Result> sp_GetBoardSheet(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_GetBoardSheet(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_GetReadedBoardSheet_Result> sp_GetReadedBoardSheet(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_GetReadedBoardSheet(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}