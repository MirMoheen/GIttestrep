using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class ProjectActivityLog
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }

        public IEnumerable<SelectListItem> LoadSericeTypes(string type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblProjectServices.Where(x => x.Status == false && x.RelatedTo==type).Select(x => new SelectListItem { Text = x.ServiceDetail, Value = x.Service_ID.ToString() }).OrderBy(x => x.Text).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> GetRelatedTo()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Please Select", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "Software", Value = "Software" });
                    listobj.Add(new SelectListItem { Text = "Support", Value = "Support" });
                   

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadDepartmentAll(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Please Select", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID == projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).OrderBy(x => x.Text).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<SelectListItem> DepartmentAll()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Please Select", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public long addata(ProjectActivityLog obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            
                            context.ProjectActivityLogs.Add(obj);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return 1;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public long update(ProjectActivityLog obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                           var result = context.ProjectActivityLogs.FirstOrDefault(x => x.Idlog == obj.Idlog);
                           if (result!=null)
                            {

                                result.ServiceActive = obj.ServiceActive;
                                result.Employeeid = obj.Employeeid;
                                result.Modifedyid = obj.Modifedyid;
                                result.Modifiedip = obj.Modifiedip;
                                result.Modifeddate = obj.Modifeddate;
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




                    return 1;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<sp_getSerivelogdetailviewlist_Result> GetDataForGM()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                   return context.sp_getSerivelogdetailviewlist().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getSerivelogdetail_Result> getalldata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getSerivelogdetail().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> loadAllActive()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Active", Value = "Active" });
                    listobj.Add(new SelectListItem { Text = "Inactive", Value = "Inactive" });
                    listobj.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
                    listobj.Add(new SelectListItem { Text = "No", Value = "No" });


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public ProjectActivityLog getalldataedit(int id )
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.ProjectActivityLogs.FirstOrDefault(x=>x.Idlog==id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadAllActiveEmployee()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.AddRange(context.sp_getAllEmployeewithDesignation().Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable GetReportData(string TypeSS, DateTime fromdate, DateTime todate, string deptid, string projectidd)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_ReportService", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@Status", TypeSS);
                        adapt.SelectCommand.Parameters.AddWithValue("@datefrom", fromdate);
                        adapt.SelectCommand.Parameters.AddWithValue("@dateto", todate);
                        adapt.SelectCommand.Parameters.AddWithValue("@deptid", deptid);
                        adapt.SelectCommand.Parameters.AddWithValue("@type", projectidd);



                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_GetServiceGraph_Result> LoadChart(int ProjectChart,int DepartmentChart,string Fdate, string TDate, string TypeChart, string TypeStatus)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_GetServiceGraph(DepartmentChart, Fdate, TDate, ProjectChart, TypeChart,TypeStatus).ToList();
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
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.tblProjects.Where(x => x.Inactive == false).Select(x => new SelectListItem { Text = x.ProjectName, Value = x.ProjectID.ToString() }).ToList());

                    return listobj;

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

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID == projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadChartDepartment(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "All", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID == projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).OrderBy(x => x.Text).ToList());

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