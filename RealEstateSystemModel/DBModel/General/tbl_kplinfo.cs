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
using System.Windows.Forms;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tbl_kplinfo
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public string UserColor { get; set; }
        public string CurrentLogEmployeedetail { get; set; }

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

        public string GenerateKplCode( string prefix)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    var resultList = context.sp_NextKplNumber().ToList();
                 
                    return prefix + "-" + resultList[0].Value.ToString("D4");


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int FurtherAddition(tbl_kplinfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tbl_kplinfo.Where(x => x.KPI_Code == obj.KPI_Code);
                           
                            
                                ////For Unread 
                                obj.ShowInNew = 0;
                                obj.HtmlBox = obj.HtmlBox.Replace("&Nbsp;", "");
                                int cofp = Regex.Matches(obj.HtmlBox, "<p>").Count;
                                //obj.HtmlBox = obj.HtmlBox.Replace("<p>", "  <p> ");


                                //obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By : " + obj.Initiatedby +" " on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong><hr>  ";
                                obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By :  <span style='color:" + obj.UserColor + "'> " + obj.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + "   </strong><hr>  ";


                                obj.Status = 1;
                               
                                obj.fldStatus = "KPL";
                         

                            
                                context.tbl_kplinfo.Add(obj);
                                context.SaveChanges();


                                obj.Status = 0;
                              
                                context.SaveChanges();



                       


                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.KPI_ID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List <sp_getAllkplData_Result> sp_getallkpl(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllkplData(id,1).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getAllkpitoEdit_Result> getAlldataIonByID(string code, int employeeID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllkpitoEdit(employeeID, code).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getkpiOwner_Result> GetIonOwnerID(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getkpiOwner(code).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<tbl_kplinfo> GetAll(int depid)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tbl_kplinfo.Where(x => x.DepartmentID == depid).ToList();

                    //return true;

                }


            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<tbl_kplinfo> GetAll1(string depid)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tbl_kplinfo.Where(x => x.LocationProject == depid).ToList();

                    //return true;

                }


            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadDepartment(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID == projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).OrderBy(x=>x.Text).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadProject()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Select Project --", Value = "0" });

                    listobj.AddRange(context.tblProjects.Where(x => x.Inactive == false).Select(x => new SelectListItem { Text = x.ProjectName, Value = x.ProjectID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

}
