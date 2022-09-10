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
using System.Windows.Forms;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tblEminuteInfo
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
        public int NEwForwardTo { get; set; }

        public int currentUserEmployeeID { get; set; }
        public int eID { get; set; }

        public int Transferedfrom { get; set; }

        public string UserColor { get; set; }
        public string ForwardUserColor { get; set; }
        public string CurrentLogEmployeedetail { get; set; }


        public decimal budgetprovence { get; set; }
        public decimal expense { get; set; }

        public string TotalExpense { get; set; }
        public string BudgetBalance { get; set; }
        public string HtmlStamp { get; set; }

        public int ReminderMinute { get; set; }
        public int DiscussMinute { get; set; }

        public int HoldMinute { get; set; }




        public int Minuteopen { get; set; }
        public decimal CheckBudgetAmount { get; set; }
        public Nullable<decimal> totalPayableamt { get; set; }



        public IEnumerable<string> SelectInfo { get; set; }


        public IEnumerable<tblEminuteDocDetail> detailistDoc { get; set; }


        // detail doc

        public IEnumerable<string> lisdescription { get; set; }

        public IEnumerable<string> listdocpath { get; set; }
        public IEnumerable<HttpPostedFileBase> DocFile { get; set; }



        public string JsonHeadbudget { get; set; }
        public string JsonHeadSubbudget { get; set; }
        public string JsonHeadFinancalYear { get; set; }
        public string JsonProject { get; set; }



        public IEnumerable<int> BudgetHeadID { get; set; }
        public IEnumerable<int> BudgetSubID { get; set; }
        public IEnumerable<int> FinancalYear { get; set; }
        public IEnumerable<string> AlreadyExpense { get; set; }
        public IEnumerable<string> ThisExpense { get; set; }
        public IEnumerable<string> TotalExpenses { get; set; }
        public IEnumerable<string> Balance { get; set; }
        public IEnumerable<string> BudgetPro { get; set; }
        public IEnumerable<string> BudgetProjectlist { get; set; }



        public IEnumerable<tblEminuteBudgetDetail> detailistbudget { get; set; }

        public IEnumerable<sp_getDayWisePendingMinute_Result> listDayWise { get; set; }

        public string finyear { get; set; }
        public int techicalpersonID { get; set; }


        public List<sp_getDayWisePendingMinute_Result> getDayWisePending(int empid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_getDayWisePendingMinute(empid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public tblEminuteInfo getcurrentMinuteByRowbyMinutecode(string Minutecode)
        {

            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tblEminuteInfoes.Where(x => x.MinuteCode == Minutecode && x.inactive == false).OrderBy(x => x.MinuteID).Take(1).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {

                return null;
            }


        }



        public List<sp_getAssetbyDepartmentID_Result> getfixassitlist(int department)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_getAssetbyDepartmentID(department).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public DataTable GetMinuteActivity(int projectid, int employeid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_eminuteActivity", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@projectid", projectid);
                        adapt.SelectCommand.Parameters.AddWithValue("@employeid", employeid);
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                return new DataTable();
            }
        }


        public IEnumerable<SelectListItem> LoadAllDesignationWise()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
                    var resultList = context.sp_EmployeeAllForward().ToList();

                    //var entities =
                    //   from emp in context.tblEmployees
                    //   join des in context.tblDesignations on emp.DesignationID equals des.DesignationID
                    //   where emp.DepartmentID == depertmentid 

                    //   select new SelectListItem { Text =des.DesignationTitle+ " - " +emp.EmployeeName, Value = emp.EmployeeID.ToString() };

                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }));

                    return listobj;

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
        public List<sp_getAssetbyMinuteTypeID_Result> getfixassitTypelist(int department, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_getAssetbyMinuteTypeID(department, type).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getAssetbyMinuteSubTypeID_Result> getfixassitTypeSublist(int department, int Subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    var fixedcategoerysubid = context.tblEminuteSubTypes.FirstOrDefault(x => x.MinuteSubTypeID == Subtype);
                    if (fixedcategoerysubid != null)
                    {
                        return context.sp_getAssetbyMinuteSubTypeID(department, fixedcategoerysubid.FixassetSubtypeId).ToList();

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


        public List<sp_getAssetbyMinuteTypeandSubwise_Result> getfixassittypeandSubtypewise(int typeid, int Subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (Subtype != null && Subtype > 0)
                    {
                        var fixedcategoerysubid = context.tblEminuteSubTypes.FirstOrDefault(x => x.MinuteSubTypeID == Subtype).FixassetSubtypeId;

                        return context.sp_getAssetbyMinuteTypeandSubwise(typeid, fixedcategoerysubid).ToList();
                    }
                    else
                    {

                        return new List<sp_getAssetbyMinuteTypeandSubwise_Result>();
                    }




                }
            }
            catch (Exception ex)
            {

                return new List<sp_getAssetbyMinuteTypeandSubwise_Result>();
            }
        }


        public List<tblEminuteDocDetail> getdetailistDocumentData(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tblEminuteDocDetails.Where(x => x.MinuteCode == code && x.Inative == false).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public sp_minutePrint_Result minutePrint(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_minutePrint(code).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<SelectListItem> loadSubtypebyTypeID(int typeId)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    List<SelectListItem> listobj = new List<SelectListItem>();
                    //  listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEminuteSubTypes.Where(x => x.inactive == false && x.MinuteTypeID == typeId).Select(x => new SelectListItem { Text = x.MinuteSubType, Value = x.MinuteSubTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public IEnumerable<SelectListItem> loadSubtype(int a)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    if (a == 0)
                    {
                        //  listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                        listobj.AddRange(context.tblEminuteSubTypes.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.MinuteSubType, Value = x.MinuteSubTypeID.ToString() }).ToList());



                    }
                    else
                    {
                        listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                        listobj.AddRange(context.tblEminuteSubTypes.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.MinuteSubType, Value = x.MinuteSubTypeID.ToString() }).ToList());


                    }

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
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

        public IEnumerable<SelectListItem> LoadMinuteType(int departmentID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.sp_getDepartmentWiseMinteType(departmentID).ToList().Select(x => new SelectListItem { Text = x.MinuteType, Value = x.MinuteTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadMinuteTypeEdit()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEminuteTypes.ToList().Select(x => new SelectListItem { Text = x.MinuteType, Value = x.MinuteTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadFixedAssitItem()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.sp_getFiedAssetitems().Select(x => new SelectListItem { Text = x.assetname, Value = x.assetid.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string geneartMinuteNo(int mintype, string prefix)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    var resultList = context.sp_NextNO(mintype).ToList();
                    //return prefix + "-" + DateTime.Now.Year + "-" + resultList[0].Value.ToString("D4");
                    return prefix + "-" + resultList[0].Value.ToString("D4");


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        //public string geneartMinuteNo(int projectID)
        //{

        //    try
        //    {


        //        using (var context = new HRandPayrollDBEntities())
        //        {
        //            string projectorderNo = context.tblProjects.FirstOrDefault(x => x.ProjectID == projectID).ProjectCode.ToString();

        //            var reuslts = context.tblEminuteInfoes.ToList();
        //            if (reuslts != null && reuslts.Count > 0)
        //            {
        //                int minuteNextID = Convert.ToInt32(context.tblEminuteInfoes.Max(x => x.MinuteID)) + 1;
        //                return projectorderNo  +"-"+ DateTime.Now.Year +"-"+  minuteNextID.ToString("D4");

        //            }
        //            else
        //            {
        //                return projectorderNo + "-" + DateTime.Now.Year + "-"  + 1.ToString("D4");
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }


        //}

        public IEnumerable<SelectListItem> LoadPriority()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Normal", Value = "1" });
                    //listobj.Add(new SelectListItem { Text = "High", Value = "2" });
                    listobj.Add(new SelectListItem { Text = "High", Value = "3" });




                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblEminuteBudgetDetail> loaddetailbudget(string minuteNo)
        {
            using (var context = new HRandPayrollDBEntities())
            {
                return context.tblEminuteBudgetDetails.Where(x => x.EminuteCode == minuteNo && x.Inactive == false).ToList();

            }


        }


        public IEnumerable<SelectListItem> LoadRange()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "1k to 100k", Value = "1" });
                    listobj.Add(new SelectListItem { Text = "100k to 200k", Value = "2" });
                    listobj.Add(new SelectListItem { Text = "200k to above", Value = "3" });




                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadForwardUser(int employeeID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
                    var resultList = context.sp_GetMinuteEmployee(employeeID, projectID).OrderBy(x => x.OrderNO).ToList();

                    //var entities =
                    //   from emp in context.tblEmployees
                    //   join des in context.tblDesignations on emp.DesignationID equals des.DesignationID
                    //   where emp.DepartmentID == depertmentid 

                    //   select new SelectListItem { Text =des.DesignationTitle+ " - " +emp.EmployeeName, Value = emp.EmployeeID.ToString() };

                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.DesignationTitle + " - " + x.EmployeeName, Value = x.EmployeeID.ToString() }));

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


        public IEnumerable<SelectListItem> LoadForwardDesginationWise(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
                    var resultList = context.sp_EmployeeForwardFormint(userID, projectID).ToList();

                    //var entities =
                    //   from emp in context.tblEmployees
                    //   join des in context.tblDesignations on emp.DesignationID equals des.DesignationID
                    //   where emp.DepartmentID == depertmentid 

                    //   select new SelectListItem { Text =des.DesignationTitle+ " - " +emp.EmployeeName, Value = emp.EmployeeID.ToString() };

                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }));

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadForwardDesginationWiseSendBack(int userID, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Send Back", Value = "-1" });
                    var resultList = context.sp_EmployeeForwardFormint(userID, projectID).ToList();

                    //var entities =
                    //   from emp in context.tblEmployees
                    //   join des in context.tblDesignations on emp.DesignationID equals des.DesignationID
                    //   where emp.DepartmentID == depertmentid 

                    //   select new SelectListItem { Text =des.DesignationTitle+ " - " +emp.EmployeeName, Value = emp.EmployeeID.ToString() };

                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }));

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadForwardUsercc(int employeeID, int employeeID2, int projectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
                    var resultList = context.sp_GetMinuteEmployeeCCC(employeeID, employeeID2, projectID).OrderBy(x => x.OrderNO).ToList();

                    listobj.AddRange(resultList.Select(x => new SelectListItem { Text = x.DesignationTitle + " - " + x.EmployeeName, Value = x.EmployeeID.ToString() }));

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadDepartmentForwardUser(int departmentID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });


                    var entities =
                       from emp in context.tblEmployees
                       join des in context.tblDesignations on emp.DesignationID equals des.DesignationID
                       where (emp.DepartmentID == departmentID && emp.inactive == false)
                       orderby des.OrderNO

                       select new SelectListItem { Text = des.DesignationTitle + " - " + emp.EmployeeName, Value = emp.EmployeeID.ToString() };

                    listobj.AddRange(entities);

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int addata(tblEminuteInfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int fid = obj.ForwardTo.Value;
                            var obj1 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(fid);
                            var result = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType);
                            if (result != null && result.Count() > 0)
                            {

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
                                obj.Remarks = null;

                                obj.HtmlBox = "1.]|[" + obj.HtmlBox + obj.HtmlStamp;
                                //var asasa =obj.HtmlBox.Split(new[] { "1.<P>" }, StringSplitOptions.None); 
                                //obj.HtmlBox =   "<p>1. " + asasa[1];
                                obj.HtmlBox = obj.HtmlBox.Replace("&Nbsp;", " ");
                                int cofp = Regex.Matches(obj.HtmlBox, "<p>").Count;
                                try
                                {
                                    var asasa = obj.HtmlBox.Split(new[] { "1.]|[<p>" }, StringSplitOptions.None);

                                    obj.HtmlBox = "<p>1." + asasa[1];
                                }
                                catch (Exception ex)
                                {

                                    var exasqa = obj.HtmlBox.Split(new[] { "1.]|[" }, StringSplitOptions.None);
                                    obj.HtmlBox = "<p>1. " + exasqa[1] + " </p>";
                                }


                                if (obj.Transferedfrom > 0)
                                {
                                    int fid2 = obj.Transferedfrom;
                                    var obj2 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(fid2);
                                    obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By :  <span style='color:" + obj.UserColor + "'> " + obj.Initiatedby + " </span>Forwarded to: <span style='color:" + obj2.ForwardUserColor + "'>   " + obj2.Initiatedby + "  </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong> Transfer to: <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1.Initiatedby + "  </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong><hr>  ";
                                }
                                else
                                {
                                    obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By :  <span style='color:" + obj.UserColor + "'> " + obj.Initiatedby + " </span>Forwarded to: <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1.Initiatedby + "  </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong><hr>  ";

                                }
                                obj.Initiatedby = obj.Initiatedby;
                                obj.Displayfornew = 0;
                                obj.Status = 1;
                                var forinfo2 = obj.ForInfo;
                                obj.ForInfo = null;
                                obj.fldStatus = "In-Process";
                                context.tblEminuteInfoes.Add(obj);
                                context.SaveChanges();

                                obj.EmployeeID = obj.ForwardTo;
                                obj.Status = 0;
                                // obj.ForwardTo = null;

                                obj.Remarks = null;

                                if (forinfo2 != "" && forinfo2 != null)
                                {
                                    obj.Displayfornew = 1;
                                    string[] forinfo = forinfo2.Split(',');
                                    for (int i = 0; i < forinfo.Length; i++)
                                    {
                                        obj.ForInfo = forinfo[i];
                                        obj.fldStatus = "For Info";
                                        obj.EmployeeID = null;
                                        context.tblEminuteInfoes.Add(obj);
                                        context.SaveChanges();

                                    }

                                }
                                obj.fldStatus = null;
                                obj.Displayfornew = 0;
                                obj.ForInfo = null;
                                obj.EmployeeID = obj.ForwardTo;


                                //      obj.ModifiedID = context.GLUsers.FirstOrDefault(x => x.EmployeeID == obj.ForwardTo).Userid;
                                context.tblEminuteInfoes.Add(obj);
                                context.SaveChanges();

                                if (obj.Transferedfrom > 0)
                                {
                                    obj.Status = 1;
                                    obj.isforward = true;
                                    obj.fldStatus = "In-Process";
                                    obj.EmployeeID = obj.Transferedfrom;
                                    obj.ForwardTo = obj.Transferedfrom;
                                    context.tblEminuteInfoes.Add(obj);
                                    context.SaveChanges();
                                }

                                List<tblEminuteDocDetail> Listobjdss = new List<tblEminuteDocDetail>();
                                int a = 0;
                                foreach (var item in obj.listdocpath)
                                {
                                    tblEminuteDocDetail objdss = new tblEminuteDocDetail();

                                    objdss.Description = lisdescription.ToArray()[a] + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";
                                    objdss.MinuteCode = obj.MinuteCode;
                                    objdss.Ip = obj.EntryIP;
                                    objdss.UserID = obj.EntryID;

                                    objdss.date = DateTime.Now;

                                    objdss.PathDoc = obj.listdocpath.ToArray()[a];
                                    Listobjdss.Add(objdss);
                                    a++;
                                }

                                context.tblEminuteDocDetails.AddRange(Listobjdss);
                                context.SaveChanges();
                            }






                            //  obj.CompID = new Login().GetUser().CompID;



                            context.SaveChanges();




                            //////////////////////////////////////////////////
                            var rem = context.tempEminuteInfoes.FirstOrDefault(x => x.IP == obj.EntryID.ToString());
                            if (rem != null)
                            {
                                context.tempEminuteInfoes.Remove(rem);
                                context.SaveChanges();
                            }

                            ////budget insert in detail and add in invertry

                            if (obj.IsBudget)
                            {
                                if (obj.detailistbudget != null)
                                {

                                    context.tblEminuteBudgetDetails.AddRange(obj.detailistbudget);

                                    context.SaveChanges();
                                    using (var dbinventry = new InventoryEntities())
                                    {

                                        foreach (var item in obj.detailistbudget)
                                        {
                                            var headid = Convert.ToInt32(item.BudgetHeadID);
                                            var headsubID = Convert.ToInt32(item.BudgetSubID);
                                            var Fyear = item.FinancalYear.ToString();

                                            var resultInventry = dbinventry.tbl_Costcenterbudgetheadamount.FirstOrDefault(x => x.HeadID == headid && x.FY == Fyear);
                                            if (resultInventry != null)
                                            {
                                                resultInventry.Expense = Convert.ToDecimal(resultInventry.Expense) + item.ThisExpense;
                                                dbinventry.SaveChanges();
                                            }

                                        }

                                    }

                                }


                            }


                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.MinuteID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int IONaddata(tblEminuteInfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var result = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType);
                            if (result != null && result.Count() > 0)
                            {

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
                                obj.Remarks = null;
                                obj.HtmlBox = obj.HtmlBox.Replace("&Nbsp;", "");
                                int cofp = Regex.Matches(obj.HtmlBox, "<p>").Count;
                                obj.HtmlBox = obj.HtmlBox.Replace("<p>", " <p> 1.");


                                obj.HtmlBox = obj.HtmlBox + "<strong> Initiated By : " + obj.Initiatedby + "on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong><hr>  ";

                                obj.Displayfornew = 0;
                                obj.Status = 1;
                                var forinfo2 = obj.ForInfo;
                                obj.ForInfo = null;
                                obj.fldStatus = "In-Process";
                                context.tblEminuteInfoes.Add(obj);
                                context.SaveChanges();


                                obj.Status = 0;
                                // obj.ForwardTo = null;

                                obj.Remarks = null;

                                if (forinfo2 != "" && forinfo2 != null)
                                {
                                    obj.Displayfornew = 0;
                                    string[] forinfo = forinfo2.Split(',');
                                    for (int i = 0; i < forinfo.Length; i++)
                                    {
                                        obj.ForInfo = forinfo[i];
                                        obj.fldStatus = "For Info";
                                        obj.EmployeeID = Convert.ToInt32(obj.ForInfo);
                                        context.tblEminuteInfoes.Add(obj);
                                        context.SaveChanges();

                                    }

                                }
                                obj.fldStatus = null;
                                obj.Displayfornew = 0;
                                obj.ForInfo = null;

                                context.tblEminuteInfoes.Add(obj);
                                context.SaveChanges();


                                //int a = 0;
                                //foreach (var item in obj.listdocpath)
                                //{
                                //    tblEminuteDocDetail objdss = new tblEminuteDocDetail();
                                //    objdss.Description = lisdescription.ToArray()[a];
                                //    objdss.MinuteCode = obj.MinuteCode;
                                //    objdss.PathDoc = obj.listdocpath.ToArray()[a];

                                //    a++;
                                //    context.tblEminuteDocDetails.Add(objdss);
                                //    context.SaveChanges();

                                //}
                            }






                            //  obj.CompID = new Login().GetUser().CompID;



                            context.SaveChanges();



                            //////////////////////////////////////////////////
                            var rem = context.tempEminuteInfoes.SingleOrDefault(x => x.IP == obj.EntryID.ToString());
                            if (rem != null)
                            {
                                context.tempEminuteInfoes.Remove(rem);
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




                    return obj.MinuteID;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int adTempdata(tempEminuteInfo obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tempEminuteInfoes.FirstOrDefault(x => x.IP == obj.IP);
                            if (result != null)
                            {
                                context.tempEminuteInfoes.Remove(result);
                                context.SaveChanges();


                            }
                            context.tempEminuteInfoes.Add(obj);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw;
                        }
                    }
                    return obj.tempMinuteid;
                }

            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateCurrentMinuteData(int obj, int? type, string fldstatus)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tblEminuteInfoes.SingleOrDefault(x => x.MinuteID == obj && x.MinuteType == type && x.inactive == false);
                            if (result != null && fldstatus != null)
                            {
                                result.Displayfornew = 0;
                                context.SaveChanges();
                            }

                            dbContextTransaction.Commit();

                            return result.MinuteID;
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

        public int KeepNew(int obj, int? type)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tblEminuteInfoes.SingleOrDefault(x => x.MinuteID == obj && x.MinuteType == type);
                            if (result != null)
                            {
                                result.Displayfornew = 1;
                                context.SaveChanges();
                            }

                            dbContextTransaction.Commit();

                            return result.MinuteID;
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


        public int UpdateData(tblEminuteInfo obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {




                            var obj1 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(obj.eID);
                            var result = context.tblEminuteInfoes.SingleOrDefault(x => x.MinuteID == obj.MinuteID && x.MinuteType == obj.MinuteType && x.inactive == false);
                            var Comentnumber = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.inactive == false && x.MinuteType == obj.MinuteType && x.inactive == false).Count();
                            if (result != null)
                            {
                                if (obj.NEwForwardTo > 0)
                                {

                                    var obj1for = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(obj.NEwForwardTo);
                                    result.Displayfornew = 0;
                                    if (obj.Status == 0)
                                    {
                                        result.Status = 1;
                                        if (obj.MinuteID != result.MinuteID)
                                            result.Displayfornew = 1;
                                    }
                                    else
                                    {
                                        result.Status = obj.Status;
                                        if (obj.MinuteID != result.MinuteID)
                                            result.Displayfornew = 1;
                                    }
                                    result.ForwardTo = obj.NEwForwardTo;


                                    result.IsHold = obj.IsHold;


                                    if (obj.IsTech && !result.IsTech)
                                    {

                                        result.TechicalPersonRemarks = "<strong> Technical Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong> ";
                                        obj.TechicalPersonRemarks = result.TechicalPersonRemarks;
                                    }

                                    if (!result.IsTech)
                                    {
                                        result.IsTech = obj.IsTech;

                                    }
                                    result.fldStatus = "In-Procecss";


                                    var checksourced = context.tblEminuteInfoes.Where(h => h.MinuteCode == obj.MinuteCode && h.EmployeeID == obj.currentUserEmployeeID).ToList();
                                    if (checksourced != null && checksourced.Count == 1)
                                    {
                                        RankCalculation(obj, result.EntryDate);

                                    }

                                    context.SaveChanges();


                                    //status
                                    //1 new 
                                    //2 pending
                                    //3 complete

                                    //minute type
                                    //1 minute
                                    //2 complain
                                    //3 ion

                                    result.Status = 0;
                                    result.EmployeeID = obj.NEwForwardTo;
                                    result.ForwardTo = obj.NEwForwardTo;
                                    result.IspettyCash = obj.IspettyCash;
                                    result.OtherPayable = obj.OtherPayable;
                                    result.IsTech = obj.IsTech;
                                    result.IsHold = obj.IsHold;





                                    obj.Remarks = obj.Remarks + obj.HtmlStamp;


                                    if (obj.Status == 0)
                                    {

                                        if (obj.Transferedfrom > 0)
                                        {
                                            int fid2 = obj.Transferedfrom;
                                            var obj2 = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(fid2);
                                            result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj2.ForwardUserColor + "'>   " + obj2.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong> <strong> Transfer to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";
                                        }
                                        else if (obj.ReminderMinute == 1) {

                                            var host = new UriBuilder(HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);
                                            result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong>" + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <img src='" + host + "/AppFiles/ReminderLogo.png' ><br/><br/> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";


                                        }

                                        else if (obj.DiscussMinute == 1)
                                        {


                                            result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";


                                        }


                                        else if (obj.HoldMinute == 1)
                                        {

                                            var host = new UriBuilder(HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);
                                            result.HtmlBox = result.HtmlBox + "<strong> Hold By :  <span style='color:#A833FF'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <hr> ";

                                            //   < strong > Forward to:  < span style = 'color:#A833FF' > " + obj1for.Initiatedby + " </ span > on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </ strong >
                                        }


                                        else if (obj.Minuteopen == 1)
                                        {


                                            result.HtmlBox = result.HtmlBox + "<strong> Open By :  <span style='color:blue'>  " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";


                                        }

                                        else if (obj.IsTech)
                                        {


                                            result.HtmlBox = result.HtmlBox + "<strong> Technical Comment By :  <span style='color:" + obj.UserColor + "'>  " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";


                                        }


                                        else
                                        {
                                            result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";

                                        }


                                        // result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>    " + obj1.DepartmentName + "-" + obj1.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p> <strong> Forward to  :  <span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> ";
                                        result.Initiatedby = obj1.Initiatedby;
                                    } if (obj.Status == 2)
                                    {
                                        result.HtmlBox = result.HtmlBox + "<strong> Approved By :    <span style='color:" + obj.UserColor + "'>   " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span>  on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  : <span style='color:" + obj1.ForwardUserColor + "'>  " + obj1for.Initiatedby + " </span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr> "; //  obj1for.Initiatedby
                                        result.Initiatedby = "Approved By " + obj1.Initiatedby;//obj1.Initiatedby;
                                    } if (obj.Status == 3)
                                    {
                                        result.HtmlBox = result.HtmlBox + "<strong> Completed By :  <span style='color:" + obj.UserColor + "'>  " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  : <span style='color:" + obj1.ForwardUserColor + "'>  " + obj1for.Initiatedby + "</span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong> <hr>";

                                        result.Initiatedby = "Completed By " + obj1.Initiatedby;
                                    } if (obj.Status == 4)
                                    {
                                        result.HtmlBox = result.HtmlBox + "<strong> Disapprove By :  <span style='color:" + obj.UserColor + "'>   " + obj1.Initiatedby + "-" + obj1.DepartmentName + " </span>  on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p><strong> " + Comentnumber + "</strong>. |-| " + obj.Remarks + "</p> <strong> Forward to  :<span style='color:" + obj1.ForwardUserColor + "'>   " + obj1for.Initiatedby + "</span> on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong><hr>";
                                        result.Initiatedby = " Disapproved By " + obj1.Initiatedby;


                                    }

                                    var asasa = result.HtmlBox.Split(new[] { "|-| <p>" }, StringSplitOptions.None);
                                    //   result.HtmlBox = addnonew(result.HtmlBox);
                                    try
                                    {
                                        result.HtmlBox = "";
                                        foreach (var item in asasa)
                                        {
                                            result.HtmlBox += item;
                                        }

                                        // result.HtmlBox = asasa[0] + asasa[1];
                                    }
                                    catch (Exception ex)
                                    {


                                    }


                                    string forinfo2 = "";
                                    if (obj.ForInfo != "" && obj.ForInfo != null)
                                    {
                                        forinfo2 = obj.ForInfo;
                                    }
                                    result.ForInfo = null;
                                    result.Remarks = obj.Remarks;
                                    var empid = result.EmployeeID;
                                    if (forinfo2 != "" && forinfo2 != null)
                                    {
                                        result.Displayfornew = 1;
                                        string[] forinfo = forinfo2.Split(',');
                                        for (int i = 0; i < forinfo.Length; i++)
                                        {
                                            result.ForInfo = forinfo[i];
                                            result.fldStatus = "For Info";
                                            result.EmployeeID = null;
                                            result.ForwardTo = null;
                                            context.tblEminuteInfoes.Add(result);
                                            context.SaveChanges();

                                        }

                                    }
                                    result.Displayfornew = 1;
                                    result.IsReminder = obj.IsReminder;
                                    result.IsDiscuss = obj.IsDiscuss;

                                    result.fldStatus = null;


                                    if (obj.Status == 4)
                                    {
                                        result.fldStatus = "Cancel";
                                        //budget canncel and update on disapprove

                                        if (result.IsBudget)
                                        {

                                            var budgetlist = context.tblEminuteBudgetDetails.Where(x => x.EminuteCode == result.MinuteCode).ToList();

                                            if (budgetlist != null)
                                            {

                                                //using (var dbinventry = new InventoryEntities())
                                                //{

                                                //    foreach (var item in budgetlist)
                                                //    {
                                                //        var headid = Convert.ToInt32(item.BudgetHeadID);
                                                //        var headsubID = Convert.ToInt32(item.BudgetSubID);
                                                //        var Fyear = item.FinancalYear.ToString();

                                                //        var resultInventry = dbinventry.tbl_Costcenterbudgetheadamount.FirstOrDefault(x => x.HeadID == headid && x.FY == result.FinancialYear);
                                                //        if (resultInventry != null)
                                                //        {
                                                //            resultInventry.Expense = Convert.ToDecimal(resultInventry.Expense) - item.ThisExpense;


                                                //            dbinventry.SaveChanges();
                                                //        }

                                                //        item.ThisExpense = -item.ThisExpense; 


                                                //    }

                                                //}
                                                context.tblEminuteBudgetDetails.AddRange(budgetlist);

                                            }


                                        }

                                    }
                                    if (obj.Status == 3)
                                    {
                                        result.fldStatus = "Complete";
                                    }
                                    if (obj.Status == 2)
                                    {
                                        result.fldStatus = "Approved";
                                    }




                                    result.ForInfo = null;
                                    result.EmployeeID = empid;
                                    result.ForwardTo = obj.NEwForwardTo;
                                    result.EntryID = obj.EntryID; ///////zulqi check
                                    result.Priority = obj.Priority;
                                    result.Initiatedby = result.Initiatedby;//zulqi
                                    result.ModifiedID = obj.ModifiedID;//zulqi
                                    result.ModifiedIP = obj.ModifiedIP;//zulqi
                                    result.IsBudget = obj.IsBudget;//zulqi
                                    result.IsPo = obj.IsPo;//zulqi
                                    result.IsPayBeforeApprovel = obj.IsPayBeforeApprovel;//zulqi

                                    result.SupplierAccountNo = obj.SupplierAccountNo;//zulqi
                                    result.EntryDate = DateTime.Now;//zulqi


                                    result.BudgetAmount = obj.BudgetAmount;//zulqi


                                    context.tblEminuteInfoes.Add(result);
                                    context.SaveChanges();
                                    //////////////////////////


                                    /// add budget 
                                    if (obj.IsBudget)
                                    {

                                        var resultminutebudgetList = context.tblEminuteBudgetDetails.Where(c => c.EminuteCode == obj.MinuteCode && c.Inactive == false).ToList();
                                        if (resultminutebudgetList.Count > 0 && resultminutebudgetList != null)
                                        {
                                            if (obj.CheckBudgetAmount == 0 || obj.BudgetAmount != obj.CheckBudgetAmount)
                                            {
                                                //    using (var dbinventry = new InventoryEntities())
                                                //{

                                                //    foreach (var itembuget in resultminutebudgetList)
                                                //    {

                                                //        var Invresult=   dbinventry.tbl_Costcenterbudgetheadamount.FirstOrDefault(x => x.HeadID == itembuget.BudgetHeadID );
                                                //        if (Invresult!=null)
                                                //        {

                                                //            Invresult.Expense = Invresult.Expense - itembuget.ThisExpense;
                                                //            dbinventry.SaveChanges();


                                                //        }
                                                //    }
                                                //}



                                                List<tblEminuteBudgetDetail> resultofbuget = context.tblEminuteBudgetDetails.Where(x => x.EminuteCode == obj.MinuteCode && x.Inactive == false).ToList();
                                                if (resultofbuget != null)
                                                {
                                                    foreach (var itembudget in resultofbuget)
                                                    {
                                                        itembudget.Inactive = true;
                                                    }

                                                    //  context.tblEminuteBudgetDetails.RemoveRange(context.tblEminuteBudgetDetails.Where(q => q.EminuteCode == obj.MinuteCode));

                                                }
                                                context.SaveChanges();
                                            }

                                        }

                                        if (obj.detailistbudget != null)
                                        {
                                            if (obj.CheckBudgetAmount == 0 || obj.BudgetAmount != obj.CheckBudgetAmount)
                                            {
                                                context.tblEminuteBudgetDetails.AddRange(obj.detailistbudget);

                                                context.SaveChanges();
                                                //using (var dbinventry = new InventoryEntities())
                                                //{

                                                //    foreach (var item in obj.detailistbudget)
                                                //    {
                                                //        var headid = Convert.ToInt32(item.BudgetHeadID);
                                                //        var headsubID = Convert.ToInt32(item.BudgetSubID);
                                                //        var Fyear = item.FinancalYear.ToString();

                                                //        var resultInventry = dbinventry.tbl_Costcenterbudgetheadamount.FirstOrDefault(x => x.HeadID == headid  && x.FY == Fyear);
                                                //        if (resultInventry != null)
                                                //        {
                                                //            resultInventry.Expense = Convert.ToDecimal(resultInventry.Expense) + item.ThisExpense;
                                                //            dbinventry.SaveChanges();
                                                //        }

                                                //    }

                                                //}
                                            }
                                        }

                                    }

                                    var Minuteresult = context.tblEminuteInfoes.SingleOrDefault(x => x.MinuteID == obj.MinuteID && x.inactive == false && x.MinuteType == obj.MinuteType);


                                    var result1 = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.inactive == false && x.MinuteType == obj.MinuteType).ToList();

                                    var intiateResult = result1.Min(x => x.MinuteID);
                                    foreach (var item in result1)
                                    {
                                        if (obj.Status == 0)
                                        {

                                            item.IsHold = obj.IsHold;
                                            item.IsTech = Minuteresult.IsTech;
                                            item.TechicalPersonRemarks = Minuteresult.TechicalPersonRemarks;



                                            item.Status = 1;
                                            item.HtmlBox = result.HtmlBox;
                                            item.Priority = obj.Priority;
                                            item.IspettyCash = obj.IspettyCash;

                                            item.Initiatedby = result.Initiatedby;
                                            if (obj.IsPo)
                                            {
                                                item.IsPo = result.IsPo;
                                            }


                                            if (obj.IsBudget)
                                            {

                                                item.IsBudget = obj.IsBudget;
                                                //     item.SupplierAccountNo = obj.SupplierAccountNo.Trim();
                                                item.BudgetAmount = obj.BudgetAmount;
                                                item.OtherPayable = obj.OtherPayable;



                                            }


                                            if (Minuteresult.IsPo)
                                            {
                                                item.IsPo = Minuteresult.IsPo;
                                            }




                                            if (Minuteresult.IsBudget)
                                            {


                                                var currentid = result1.Max(x => x.MinuteID);
                                                item.IsBudget = Minuteresult.IsBudget;

                                                //         item.BudgetAmount = Minuteresult.BudgetAmount;
                                                item.BudgetAmount = result1.FirstOrDefault(t => t.MinuteID == currentid).BudgetAmount;
                                                item.OtherPayable = obj.OtherPayable;



                                            }


                                        }
                                        else
                                        {
                                            //  item.TechicalPersonRemarks = obj.TechicalPersonRemarks;
                                            item.TechicalPersonRemarks = Minuteresult.TechicalPersonRemarks;

                                            item.IsTech = Minuteresult.IsTech;
                                            item.IsHold = obj.IsHold;

                                            item.Status = obj.Status;
                                            item.HtmlBox = result.HtmlBox;
                                            item.IspettyCash = obj.IspettyCash;
                                            item.Initiatedby = result.Initiatedby;
                                            if (obj.IsPo)
                                            {
                                                item.IsPo = obj.IsPo;
                                            }


                                            if (obj.IsBudget)
                                            {
                                                item.IsBudget = obj.IsBudget;
                                                //   item.SupplierAccountNo = obj.SupplierAccountNo.Trim();
                                                item.BudgetAmount = obj.BudgetAmount;
                                                item.OtherPayable = obj.OtherPayable;

                                            }

                                            if (Minuteresult.IsPo)
                                            {
                                                item.IsPo = Minuteresult.IsPo;
                                            }



                                            if (Minuteresult.IsBudget)
                                            {
                                                var currentid = result1.Max(x => x.MinuteID);
                                                item.IsBudget = Minuteresult.IsBudget;

                                                //         item.BudgetAmount = Minuteresult.BudgetAmount;
                                                item.BudgetAmount = result1.FirstOrDefault(t => t.MinuteID == currentid).BudgetAmount;
                                                item.OtherPayable = obj.OtherPayable;


                                                //item.IsBudget = Minuteresult.IsBudget;

                                                //item.BudgetAmount = Minuteresult.BudgetAmount;
                                                //item.OtherPayable = obj.OtherPayable;

                                            }

                                            if (obj.MinuteID != item.MinuteID)
                                                item.Displayfornew = 1;

                                            //if ( item.MinuteID> obj.MinuteID || intiateResult==item.MinuteID)
                                            //    item.Displayfornew = 1;

                                        }
                                        result.ForwardTo = obj.NEwForwardTo;
                                    }
                                    context.SaveChanges();



                                    if ((obj.Status == 1 || obj.Status == 0) && obj.Transferedfrom > 0)
                                    {
                                        result.Status = 1;
                                        result.Displayfornew = 0;
                                        result.isforward = true;
                                        result.fldStatus = "In-Process";
                                        result.EmployeeID = obj.Transferedfrom;
                                        result.ForwardTo = obj.Transferedfrom;
                                        context.tblEminuteInfoes.Add(result);
                                        context.SaveChanges();
                                    }


                                }
                                //else
                                //{
                                //    var resultlist = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType);

                                //    result.HtmlBox = removeoldNo(result.HtmlBox);
                                //    result.HtmlBox = removeoldNo(result.HtmlBox);
                                //    result.HtmlBox = removeoldNo(result.HtmlBox);

                                //    if (obj.Status == 0)
                                //    {
                                //        result.HtmlBox = result.HtmlBox + "<strong> Comment By :  <span style='color:" + obj.UserColor + "'>   " + obj1.DepartmentName + "-" + obj1.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p><hr>";
                                //        if(result.fldStatus!= "For Info")
                                //        result.Initiatedby = obj1.Initiatedby;
                                //    }
                                //    if (obj.Status == 2)
                                //    {
                                //        result.HtmlBox = result.HtmlBox + "<strong> Approved By :  <span style='color:" + obj.UserColor + "'>  " + obj1.DepartmentName + "-" + obj1.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p> <hr>";
                                //        result.Initiatedby = "Approved By " + obj1.Initiatedby;
                                //    }
                                //    if (obj.Status == 3)
                                //    {
                                //        result.HtmlBox = result.HtmlBox + "<strong> Completed By : <span style='color:" + obj.UserColor + "'>   " + obj1.DepartmentName + "-" + obj1.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p> <hr>";

                                //        result.Initiatedby = "Completed By " + obj1.Initiatedby;
                                //    }
                                //    if (obj.Status == 4)
                                //    {
                                //        result.HtmlBox = result.HtmlBox + "<strong> Disapprove By :  <span style='color:" + obj.UserColor + "'>  " + obj1.DepartmentName + "-" + obj1.Initiatedby + " </span> on " + DateTime.Now.ToString("dd / MM / yyyy hh: mm tt") + " </strong>  <p>" + obj.Remarks + "</p><hr>";
                                //        result.Initiatedby = " Disapproved By " + obj1.Initiatedby;
                                //    }

                                //   // result.HtmlBox = result.HtmlBox + "<br/><strong> Comment By : " + obj1.DepartmentName + "-" + obj1.Initiatedby + " on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " </strong> <p>" + obj.Remarks + "</p>  <br/>";
                                //    result.HtmlBox = addnonew(result.HtmlBox);// ahtml.Replace("<pp>", "<p>");
                                //    //result.fldStatus = "For Info";

                                //    foreach (var item in resultlist)
                                //    {
                                //        if (obj.Status > 0)
                                //        {

                                //            item.Status = obj.Status;
                                //            item.ModifiedIP = obj.ModifiedIP;
                                //            item.ModifiedDate = DateTime.Now;
                                //            item.ModifiedID = obj.ModifiedID;
                                //         item.Initiatedby = result.Initiatedby;

                                //        }
                                //        //if(result.fldStatus != "For Info")
                                //        if (obj.MinuteID != item.MinuteID )
                                //            item.Displayfornew = 1;
                                //        else
                                //        {
                                //            //result.fldStatus = "In-Procecss";
                                //            item.Displayfornew = 0;
                                //        }
                                //        if (item.fldStatus == "For Info")
                                //        {
                                //            item.Displayfornew = 0;
                                //        }

                                //        //// update to all minutes 

                                //        ///////
                                //        item.HtmlBox = result.HtmlBox;


                                //        context.SaveChanges();
                                //    }

                                //    ///////////////////// Start for info with  status chnage of minute////////////////////////
                                //    string forinfo2 = "";
                                //    if (obj.ForInfo != "" && obj.ForInfo != null)
                                //    {

                                //        forinfo2 = obj.ForInfo;
                                //        result.ForInfo = null;
                                //        if (forinfo2 != "" && forinfo2 != null)
                                //        {
                                //            result.Displayfornew = 1;
                                //            string[] forinfo = forinfo2.Split(',');
                                //            for (int i = 0; i < forinfo.Length; i++)
                                //            {
                                //                result.ForInfo = forinfo[i];
                                //                result.fldStatus = "For Info";
                                //                result.EmployeeID = null;
                                //                result.ForwardTo = null;
                                //                context.tblEminuteInfoes.Add(result);
                                //                context.SaveChanges();

                                //            }

                                //        }

                                //      //  result.EmployeeID = empid;
                                //    }/////////////////////End for info with  status chnage of minute////////////////////////


                                //}



                            }


                            List<tblEminuteDocDetail> resultofDOC = context.tblEminuteDocDetails.Where(x => x.MinuteCode == obj.MinuteCode).ToList();
                            if (resultofDOC != null)
                            {
                                foreach (var item in resultofDOC)
                                {
                                    item.Inative = true;
                                    item.Ip = obj.ModifiedIP;
                                    item.date = DateTime.Now;
                                    item.UserID = obj.ModifiedID;


                                }

                            }
                            context.SaveChanges();

                            //////////////////////////////////////////////////
                            var rem = context.tempEminuteInfoes.FirstOrDefault(x => x.IP == obj.EntryID.ToString());
                            if (rem != null)
                            {
                                context.tempEminuteInfoes.Remove(rem);
                                context.SaveChanges();
                            }
                            ////////////////////////////////////////////////
                            List<tblEminuteDocDetail> listobjdss = new List<tblEminuteDocDetail>();
                            int a = 0;

                            foreach (var item in obj.listdocpath)
                            {
                                tblEminuteDocDetail objdss = new tblEminuteDocDetail();

                                objdss.Description = lisdescription.ToArray()[a].Contains("Uploaded by :") ? lisdescription.ToArray()[a] : lisdescription.ToArray()[a] + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";

                                objdss.MinuteCode = obj.MinuteCode;
                                objdss.PathDoc = obj.listdocpath.ToArray()[a];
                                objdss.Ip = obj.ModifiedIP;
                                objdss.UserID = obj.ModifiedID;
                                objdss.date = DateTime.Now;
                                listobjdss.Add(objdss);
                                a++;


                            }
                            context.tblEminuteDocDetails.AddRange(listobjdss);
                            context.SaveChanges();


                            if (obj.ReminderMinute == 1)
                            {
                                var reminderMinuteID = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType).OrderByDescending(x => x.MinuteID).FirstOrDefault().MinuteID;

                                var reminder = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType && x.MinuteID != reminderMinuteID && x.fldStatus == null);

                                foreach (var item in reminder)
                                {
                                    item.fldStatus = "In-Procecss";
                                    item.Displayfornew = 0;
                                    item.IsReminder = false;


                                }
                                context.SaveChanges();

                            }



                            if (obj.DiscussMinute == 1)
                            {
                                var disussMinuteID = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType).OrderByDescending(x => x.MinuteID).FirstOrDefault().MinuteID;

                                var disuss = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType && x.MinuteID != disussMinuteID && x.fldStatus == null);

                                foreach (var item in disuss)
                                {
                                    item.fldStatus = "In-Procecss";
                                    item.Displayfornew = 0;
                                    item.IsDiscuss = false;


                                }
                                context.SaveChanges();

                            }


                            if (obj.HoldMinute == 1)
                            {
                                var Holdminuteid = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType).OrderByDescending(x => x.MinuteID).FirstOrDefault().MinuteID;

                                var holdminutes = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode && x.MinuteType == obj.MinuteType && x.MinuteID != Holdminuteid && x.fldStatus == null);

                                foreach (var item in holdminutes)
                                {
                                    item.fldStatus = "In-Procecss";
                                    //  item.Displayfornew = 0;
                                    ///     item.IsReminder = false;


                                }
                                context.SaveChanges();

                            }



                            if (obj.IsPayBeforeApprovel)
                            {

                                var BeforeApprovel = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode);

                                foreach (var item in BeforeApprovel)
                                {
                                    item.IsPayBeforeApprovel = true;
                                    //  item.Displayfornew = 0;
                                    ///     item.IsReminder = false;


                                }
                                context.SaveChanges();

                            }


                            if (obj.IspettyCash)
                            {

                                var applypettycasheminute = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode);

                                foreach (var item in applypettycasheminute)
                                {
                                    item.IspettyCash = true;
                                    item.IsBudget = true;
                                    item.BudgetAmount = obj.BudgetAmount;


                                    //  item.Displayfornew = 0;
                                    ///     item.IsReminder = false;


                                }
                                context.SaveChanges();



                                List<tblEminutePettyCashDetail> resultpetty = context.tblEminutePettyCashDetails.Where(x => x.EminuteCode == obj.MinuteCode).ToList();
                                if (resultpetty.Count() == 0)
                                {
                                    tblEminutePettyCashDetail pettycash = new tblEminutePettyCashDetail();

                                    pettycash.EminuteCode = obj.MinuteCode;
                                    pettycash.Amount = obj.BudgetAmount;
                                    pettycash.IP = obj.ModifiedIP;
                                    pettycash.date = DateTime.Now;
                                    pettycash.UserID = Convert.ToInt32(obj.ModifiedID);
                                    context.tblEminutePettyCashDetails.Add(pettycash);
                                    context.SaveChanges();

                                }

                            }

                            dbContextTransaction.Commit();

                            return result.MinuteID;
                        }
                        catch (Exception ex)
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
        public string removeoldNo(string a)
        {
            a = a.Replace("&Nbsp;", "");
            int cofp = Regex.Matches(a, "<p>").Count;
            for (int i = 0; i <= cofp; i++)
            {
                a = a.Replace("<p> " + i + ".", " <p>");
            }
            return a;
        }

        public string addnonew(string a)
        {
            a = a.Replace("&Nbsp;", "");
            int cofp2 = Regex.Matches(a, "<p>").Count;
            string[] ht = a.Split('/');
            int v = 2;
            for (int i = 0; i <= cofp2; i++)
            {
                for (int x = 0; x < ht.Length; x++)
                {
                    if (ht[x].Contains("<p>"))
                    {
                        ht[x] = ht[x].Replace("<p>", "<pp>" + v + " .");
                        v++;
                        break;
                    }
                }

                // result.HtmlBox = result.HtmlBox.Replace("<p>","<p> " + i + "." );
            }
            string ahtml = "";
            for (int i = 0; i < ht.Length; i++)
            {
                if (i < ht.Length - 1)
                    ahtml += ht[i] + "/";
                else
                    ahtml += ht[i];

            }
            a = ahtml.Replace("<pp>", "<p>");
            a = a.Replace(".<P>", ". ");

            return a;
        }

        public List<sp_CheckForEditForiNFo2_Result> getAlldataByforinfoID(int employeeID, int mit)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_CheckForEditForiNFo2(employeeID, mit).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getAllMinutetoedit2_Result> getAlldataByID(string code, int employeeID, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllMinutetoedit2(employeeID, code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getheckforMinOwner_Result> getMinuteowneridID(string code, int type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getheckforMinOwner(code, type).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public tempEminuteInfo getTempMinute(string uid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tempEminuteInfoes.SingleOrDefault(x => x.IP == uid);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<AllowancesDeduction> getAllowancesDeduction(int projectID, bool isdeduction)
        {
            try
            {
                AllowancesDeduction objAD = new AllowancesDeduction();
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.Where(x => x.Inactive == false && x.IsDeduction == isdeduction && x.ProjectID == projectID).ToList();
                    //    objAD.Rate = result.Sum(x => x.Rate);
                    //    objAD.AllowanceDeductionTitle = "Total";
                    //    objAD.OrderNo = 1000;

                    //    result.Add(objAD);
                    //return    result.OrderBy(x=>x.OrderNo).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public decimal GetTaxCalculaton(int deductioniD, decimal salary)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    decimal tax = 0;
                    var resultTaxConfigurations = context.TaxConfigurations.Where(x => x.AllowanceDeductionID == deductioniD).FirstOrDefault();
                    if (resultTaxConfigurations != null)
                    {
                        var resultTaxConfigurationDetail = context.TaxConfigurationDetails.Where(x => x.TaxConfiguration == resultTaxConfigurations.TaxConfigurationID && salary >= x.AnualSalaryFrom / 12 && salary <= x.AnualSalaryTo / 12).FirstOrDefault();

                        if (resultTaxConfigurationDetail != null)
                        {
                            var salaryslapTotal = (salary * 12) - (resultTaxConfigurationDetail.AnualSalaryFrom + resultTaxConfigurationDetail.FixedAmount);
                            var yearlyTax = ((salaryslapTotal / 100) * resultTaxConfigurationDetail.Percentage);
                            var MonthlyTax = yearlyTax / 12;
                            tax = Math.Round(Convert.ToDecimal(MonthlyTax), 0);

                        }

                    }

                    return tax;

                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public List<tblEminuteInfo> getAllSalarydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEminuteInfoes.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_getemployeePlacement_Result> sp_employeePlacement(string where)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getemployeePlacement(where).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getReportMinutes_Result> sp_ReportForAllminutes(int eMPID, int QuryStatus, int Deptid, DateTime fromdate, DateTime todate, int type, int subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getReportMinutes(eMPID, QuryStatus, Deptid, fromdate, todate, type, subtype).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getReportComplaint_Result> sp_ReportForAllcomplaint(int eMPID, int QuryStatus, int Deptid, DateTime fromdate, DateTime todate)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getReportComplaint(eMPID, QuryStatus, Deptid, fromdate, todate).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getDepartmentDaysWisePendingMinutes_Result> getDepartmentDaysWisePendingMinutes(int departmentiD, int Minutetype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getDepartmentDaysWisePendingMinutes(departmentiD, Minutetype).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }





        public tblEminuteInfo getMinuteReminder(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tblEminuteInfoes.Where(x => x.MinuteCode == code && x.inactive == false).OrderByDescending(x => x.MinuteID).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_searchMinuteReport_Result> sp_searchMinuteReport(string search, int Deptid, int type, int subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_searchMinuteReport(Deptid, type, subtype, search).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_PoAllowedMinuteReport_Result> sp_PoAllowedMinuteReport(string search, int Deptid, int type, int subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_PoAllowedMinuteReport(Deptid, type, subtype, search).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public bool udpatePoMinute(List<string> minutecodelist)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteInfoes.Where(x => minutecodelist.Contains(x.MinuteCode)).ToList();
                    if (result != null)
                    {

                        foreach (var item in result)
                        {
                            item.IsPo = true;
                        }

                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public DataTable getPresidentnewMinuteData(int emplid, string minutes)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_getPrintPresidentnewAllMinute", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@EMPID", emplid);
                        adapt.SelectCommand.Parameters.AddWithValue("@EminuteIds", minutes);




                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool UpdateAttachment(tblEminuteInfo obj)
        {
            using (var context = new HRandPayrollDBEntities())
            {

                List<tblEminuteDocDetail> resultofDOC = context.tblEminuteDocDetails.Where(x => x.MinuteCode == obj.MinuteCode).ToList();
                if (resultofDOC != null)
                {
                    foreach (var item in resultofDOC)
                    {
                        item.Inative = true;
                        item.Ip = obj.ModifiedIP;
                        item.date = DateTime.Now;
                        item.UserID = obj.ModifiedID;


                    }

                }
                context.SaveChanges();

                List<tblEminuteDocDetail> listobjdss = new List<tblEminuteDocDetail>();
                int a = 0;

                foreach (var item in obj.listdocpath)
                {
                    tblEminuteDocDetail objdss = new tblEminuteDocDetail();



                    objdss.Description = lisdescription.ToArray()[a].Contains("Uploaded by :") ? lisdescription.ToArray()[a] : lisdescription.ToArray()[a] + " Uploaded by : ADMIN IT ";


                    objdss.MinuteCode = obj.MinuteCode;
                    objdss.PathDoc = obj.listdocpath.ToArray()[a];
                    listobjdss.Add(objdss);
                    a++;


                }
                context.tblEminuteDocDetails.AddRange(listobjdss);
                context.SaveChanges();

                return true;
            }

        }


        public tblEminuteType gettechicalPerson(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEminuteTypes.FirstOrDefault(x => x.MinuteTypeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public bool removeEminuteBudget(int project, int fy, int budgethead, int subhead, string eminute)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteBudgetDetails.OrderByDescending(p => p.BudgetdetailID).FirstOrDefault(x => x.EminuteCode == eminute && x.BudgetProjectID == project && x.FinancalYear == fy && x.BudgetHeadID == budgethead && x.BudgetSubID == subhead);

                    if (result != null)
                    {
                        result.Inactive = true;
                        context.SaveChanges();
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




        public string gettechicalPerson(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteInfoes.FirstOrDefault(x => x.MinuteCode == code && x.IsTech == true && x.TechicalPersonRemarks != null);
                    if (result != null)
                    {
                        return result.TechicalPersonRemarks.ToString();
                    }
                    else
                    {

                        return "<strong> No Technical Comment Found. </strong>";
                    }

                }
            }
            catch (Exception ex)
            {

                return "<strong> No Technical Comment Found. </strong>";
            }
        }


        public bool RankCalculation(tblEminuteInfo obj, DateTime? EntryDate)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    GLUser GLobj = new GLUser();
                    GLobj = context.GLUsers.FirstOrDefault(x => x.EmployeeID == obj.currentUserEmployeeID);




                    // Scoring - HTML BOX wise
                    //var totalinput = obj.Remarks.Length;
                    var CountCharacte = Regex.Replace(obj.Remarks, "<.*?>", String.Empty);
                    var CountCharacter = CountCharacte.Length;

                    if (CountCharacter >= 100)
                    {
                        GLobj.UserScore += 0.5;
                    }

                    else if (CountCharacter > 50 && CountCharacter <= 99)
                    {
                        GLobj.UserScore += 0.25;
                    }

                    else if (CountCharacter >= 0 && CountCharacter <= 50)
                    {
                        GLobj.UserScore += 0.15;
                    }
                    else
                    {
                        GLobj.UserScore += 0;

                    }

                    context.SaveChanges();





                    var date1 = EntryDate;
                    DateTime dd1 = DateTime.Now;
                    int TDays = (int)(dd1 - date1).GetValueOrDefault().TotalDays;
                    // if (GLobj.UserScore == null)
                    //   GLobj.UserScore = 0;
                    if (GLobj != null && TDays == 1 || TDays == 0)
                    {
                        GLobj.UserScore += 0.1;

                    }

                    else if (GLobj != null && TDays == 2)
                    {
                        GLobj.UserScore += 0.09;

                    }
                    else if (GLobj != null && TDays == 3)
                    {
                        GLobj.UserScore += 0.08;

                    }
                    else if (GLobj != null && TDays == 4)
                    {
                        GLobj.UserScore += 0.07;

                    }
                    else if (GLobj != null && TDays == 5)
                    {
                        GLobj.UserScore += 0.06;

                    }
                    else if (GLobj != null && TDays == 6)
                    {
                        GLobj.UserScore += 0.05;

                    }
                    else if (GLobj != null && TDays == 7)
                    {
                        GLobj.UserScore += 0.04;

                    }
                    else if (GLobj != null && TDays == 8)
                    {
                        GLobj.UserScore += 0.03;

                    }
                    else if (GLobj != null && TDays == 9)
                    {
                        GLobj.UserScore += 0.02;

                    }
                    else if (GLobj != null && TDays == 10)
                    {
                        GLobj.UserScore += 0.01;

                    }

                    else
                    {
                        GLobj.UserScore += 0;

                    }

                    context.SaveChanges();

                    var resultadmin = context.GLUsers.FirstOrDefault(x => x.Userid == 8);
                    if (resultadmin != null)
                    {
                        resultadmin.UserScore = 0;
                        context.SaveChanges();
                    }


                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        public bool UpdateEM(tblEminuteInfo obj)
        {
            using (var context = new HRandPayrollDBEntities())
            {

                List<tblEminuteInfo> listobjdss = context.tblEminuteInfoes.Where(x => x.MinuteCode == obj.MinuteCode).ToList();

                tblEminuteEditlog editobj = new tblEminuteEditlog();

                editobj.BeforeEdit = listobjdss.FirstOrDefault().HtmlBox;

                editobj.AterEdit = obj.HtmlBox;
                editobj.Date = DateTime.Now;
                editobj.Userid = obj.ModifiedID;
                editobj.Ip = obj.ModifiedIP;
                editobj.Eminutecode = obj.MinuteCode;
                context.tblEminuteEditlogs.Add(editobj);

                foreach (var item in listobjdss)
                {

                    item.HtmlBox = obj.HtmlBox;
                    item.Subject = obj.Subject;



                }

                context.SaveChanges();

                return true;
            }

        }

        public bool getclearminute(string code)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteInfoes.OrderByDescending(x => x.MinuteID).FirstOrDefault(q => q.MinuteCode == code && q.BillClear);
                    if (result != null)
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

                return true;
            }
        }

        public bool getminutepo(string code)
        {
            try
            {
                using (var context = new InventoryEntities())
                {
                    var result = context.tblpoes.FirstOrDefault(x => x.EMinuteNo == code);
                    if (result != null)
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

                return true;
            }
        }




        public DataSet GetWoTranctionMinute(string code)
        {
            try
            {
                using (var context = new InventoryEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_getminuteopen", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@Ecode", code);

                        DataSet dt = new DataSet();

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


        public bool pettylimitcheck(int userid ,decimal amount)
        {
            using (var context = new HRandPayrollDBEntities())
            {

                List<tblEminutePettyCashDetail> listobjdss = context.tblEminutePettyCashDetails.Where(x => x.UserID == userid && x.isClear==false).ToList();

                var pettycash = context.GLUsers.FirstOrDefault(x => x.Userid == userid && x.Active == false).pettycashlimit;
                var limitedused = listobjdss.Sum(x => x.Amount);

                if (limitedused+ amount > pettycash)
                {
                    return true;
                }
                else
                {

                    return false;

                }

            }

        }


    }


}
