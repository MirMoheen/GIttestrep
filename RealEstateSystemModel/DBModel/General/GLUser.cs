using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;


namespace HRandPayrollSystemModel.DBModel
{
 public partial class GLUser
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public string UserInfo { get; set; }

        public IEnumerable<string> SelectProjectIDs { get; set; }
        
        public IEnumerable<string> SelectEmployeesTransfer { get; set; }
        public IEnumerable<string> SelectEmployees { get; set; }

        public IEnumerable<string> TopUsers { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }


        public GLUser UserLogin(string Username ,string password)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GLUsers.SingleOrDefault(u=>u.UserName== Username && u.UserPassword== password && u.Active==false);
                }
            }
            catch (Exception ex)
            {
             
                return null;
            }
        }



        public bool ChangeUserPassword(int userid, string paswd, string currrent )
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result= context.GLUsers.SingleOrDefault(u => u.Userid == userid);
                    if (result != null)
                    {
                        if (result.UserPassword == currrent)
                        {
                            result.UserPassword = paswd;
                            context.SaveChanges();
                            return true;
                        }
                        else {
                            return false;
                        }
                       
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


        public tblCompany GetCompany(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblCompanies.SingleOrDefault(u => u.id == id);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List< sp_getGrphEmployeeData_Result> loadChart(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getGrphEmployeeData(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getGrphMinuteDatanew3_Result> loadMinuteChart(int? id ,DateTime ? Fdate,DateTime ? TDate,int empid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getGrphMinuteDatanew3(id,Fdate,TDate,empid).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getGrphComplaintDatanew3_Result> loadMinuteChartchart(int? id, DateTime? Fdate, DateTime? TDate, int empid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getGrphComplaintDatanew3(id, Fdate, TDate, empid).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getGrphCompDatanew3_Result> sp_getGrphCompDatanew2(int? id, DateTime? Fdate, DateTime? TDate)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getGrphCompDatanew3(id, Fdate, TDate).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_departmentWiseSalariesMontly_Result> loadDepartmentSalaryChart(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_departmentWiseSalariesMontly(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllNewMinute_Result> loadnewMinutes(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllNewMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getAllHoldMinute_Result> loadHoldMinutes(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllHoldMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_getPresidentnewAllMinute_Result> sp_getPresidentnewAllMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getPresidentnewAllMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllPendingMinute_Result> loadpendingMinutes(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var asdasd= context.sp_getnewAllPendingMinute(id).ToList();

                    return context.sp_getnewAllPendingMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getnewAllApprovedMinute_Result> sp_getAllApprovedMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllApprovedMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllPurchaseMinute_Result> sp_getnewAllPurchaseMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllPurchaseMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllCompletedMinute_Result> sp_getAllCompletedMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllCompletedMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllCancelMinute_Result> sp_getAllCancelMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllCancelMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getAllButtonnewStatus_Result> sp_getAllButtonnewStatus(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllButtonnewStatus(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getAllCompButtonnewStatus_Result> sp_getAllCompButtonnewStatus(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllCompButtonnewStatus(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getnewAllNewComptaint_Result> loadnewCompalint(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllNewComptaint(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getnewAllPendingComptaint_Result> sp_getAllCompPending(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllPendingComptaint(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getnewAllApprovedComptaint_Result> sp_getAllApprovedComaplint(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllApprovedComptaint(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllCompletedComptaint_Result> sp_getAllCompCompleted(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllCompletedComptaint(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllCancelComptaint_Result> sp_getAllCompCancel(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllCancelComptaint(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getnewAllForInfoMinute_Result> sp_getAllForInfoMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllForInfoMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<sp_getnewAllIONMinute_Result> sp_getAllIONMinute(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getnewAllIONMinute(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public tblProject Getprojects(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblProjects.SingleOrDefault(u => u.ProjectID == id);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int addata(GLUser obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                  //  obj.Userid = new Login().GetUser().Userid;
                    context.GLUsers.Add(obj);
                    context.SaveChanges();
                    return obj.Userid;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public List<sp_EminuteTodayactivitylog_Result> loadActivities(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //return context.sp_zain(id).ToList();
                    return context.sp_EminuteTodayactivitylog(id).ToList();
                }
            }
            catch (Exception ex)
            {


                return null;
            }
        }



        public int UpdateData(GLUser obj)
        {
            try
            {
            
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUsers.SingleOrDefault(x => x.Userid == obj.Userid);
                    if (result != null)
                    {
                        result.UserName = obj.UserName;
                        result.UserPassword = obj.UserPassword;
                        result.GroupID = obj.GroupID;
                        result.Type = obj.Type;
                        result.Userid = obj.Userid;
                        result.Entryby = obj.Entryby;
                        result.TimeStamp = DateTime.Now;
                        result.Active = obj.Active;
                        result.mobileNo = obj.mobileNo;
                        result.ProjectIDs = obj.ProjectIDs;
                        result.DepartmentID = obj.DepartmentID;
                        result.EmployeeID = obj.EmployeeID;
                        result.AllowFwdPresedent = obj.AllowFwdPresedent;
                        result.AllowMarkPO = obj.AllowMarkPO;
                        result.AllowSendSMS = obj.AllowSendSMS;
                        result.IsHRSystem = obj.IsHRSystem;
                        result.AllowFwdEmployee = obj.AllowFwdEmployee;
                        result.UserColor = obj.UserColor;
                       // result.TransferEmpID = obj.TransferEmpID;
                       // result.IsTransfer = obj.IsTransfer;
                        result.TransferEmplyeeIDs = obj.TransferEmplyeeIDs;
                        result.ForPayment = obj.ForPayment;
                        result.IspettyCash = obj.IspettyCash;
                        result.PaymentRange = obj.PaymentRange;
                        result.PhotoPath = obj.PhotoPath;
                        result.IsReminder = obj.IsReminder;
                        result.IsDeleteAT = obj.IsDeleteAT;
                        result.IsOpen = obj.IsOpen; // IsOpen
                        result.IsTechicalPerson = obj.IsTechicalPerson; 
                        result.IsHold = obj.IsHold;
                        result.IsDiscuss = obj.IsDiscuss;
                        result.IsPaymentBeforeApprovel = obj.IsPaymentBeforeApprovel;
                        result.IsDirectApproval = obj.IsDirectApproval;

                        result.IsIP = obj.IsIP;
                        result.ipCheck = obj.ipCheck;
                        result.IsIT = obj.IsIT;

                        result.pettycashlimit = obj.pettycashlimit;



                        context.SaveChanges();
                        return result.Userid;
                    }
                    return result.Userid;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int UpdateDataforTransfer(GLUser obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUsers.SingleOrDefault(x => x.Userid == obj.Userid);
                    if (result != null)
                    {
                        
                         result.TransferEmpID = obj.TransferEmpID;
                         result.IsTransfer = obj.IsTransfer;                       
                        context.SaveChanges();
                        return result.Userid;
                    }
                    return result.Userid;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public GLUser GetUser()
        {
            GLUser user = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];

            if (authCookie != null)
            {
                user = new GLUser();
                user.Userid = Convert.ToInt32((authCookie.Values["Userid"]));
                user.UserName = Convert.ToString(authCookie.Values["UserName"]);
                user.UserPassword = Convert.ToString(authCookie.Values["UserPassword"]);
                user.Type = Convert.ToBoolean(authCookie.Values["Type"]);
                user.system = Convert.ToString(authCookie.Values["system"]);



            }
            return user;
        }
        public IEnumerable<SelectListItem> loadUsergroup()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var list = context.GLUserGroups.ToList();
                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "--Select Part--", Value = "0" });
                    foreach (var item in list)
                    {
                        listobj.Add(new SelectListItem { Text = item.GroupTitle, Value = item.GroupID.ToString() });
                    }


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUsers.SingleOrDefault(x => x.Userid == id);
                    if (result != null)
                    {
                        result.Active = true;
                        
                        context.SaveChanges();


                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public GLUser getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GLUsers.SingleOrDefault(x => x.Userid == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }





        public GLUser getAlldataByEmployeID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GLUsers.SingleOrDefault(x => x.EmployeeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getAllUsersData_Result> getAllUserdata(string projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllUsersData().Where(x=>x.ProjectIDs== projectid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<GLUser> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.GLUsers.Where(x => x.UserName == title && x.Userid != id).ToList();

                    }
                    else
                    {
                        return context.GLUsers.Where(x => x.UserName == title).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string[] userinfofromCookie()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            string cookiePath = ticket.CookiePath;
            DateTime expiration = ticket.Expiration;
            bool expired = ticket.Expired;
            bool isPersistent = ticket.IsPersistent;
            DateTime issueDate = ticket.IssueDate;
            string name = ticket.Name;
            string userData = ticket.UserData;
            int version = ticket.Version;
            string[] a = name.Split('|');
            return a;
        }


        public IEnumerable<SelectListItem> loadEmployee(int departmentID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });


                    listobj.AddRange(context.tblEmployees.Where(x => x.DepartmentID == departmentID).Select(x => new SelectListItem { Text = x.EmployeeName + " - " + x.EmployeeNo, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

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

        public IEnumerable<SelectListItem> loadAllTransferEmployee(int IDemployee)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.AddRange(context.sp_getAllEmployeeforTransferSet(IDemployee).Select(x => new SelectListItem { Text = x.Empname, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public DataTable checkRightUser(string where = "")
        {
            using (var context = new HRandPayrollDBEntities())
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT     GLUser.type,  GLUser.Userid, GLUser.UserName, GLUser.UserPassword, GLUser.GroupID, GLUser.Active, GLUserGroup.GroupTitle, GLUserGroup.Description, GLUserGroup.Inactive, GLUserGroupDetail.Assign, GLUserGroupDetail.IsEdit, 
                         GLUserGroupDetail.IsDelete, GLUserGroupDetail.IsPrint, GLUserGroupDetail.Isnew, UserForms.FormTitle, UserForms.Formid
                            FROM            GLUser INNER JOIN
                         GLUserGroup ON GLUserGroup.GroupID = GLUser.GroupID INNER JOIN
                         GLUserGroupDetail ON GLUserGroupDetail.UserGroupID = GLUserGroup.GroupID INNER JOIN
                         UserForms ON UserForms.Formid = GLUserGroupDetail.FormsID " + where + "", context.Database.Connection.ConnectionString.ToString()
                             );
                da.Fill(dt);
                return dt;
            }
        }



        public List<userdataforSms> getuserforIonsendData()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_sendIonallemployee", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);

                        return (from DataRow dr in dt.Rows
                                select new userdataforSms()
                                {
                                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                    noof = dr["noof"].ToString(),
                                    mobileNo = dr["mobileNo"].ToString(),

                                }).ToList();


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List< userdataforSms> getuserforsendData()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_SendmsgForNewPending", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);

                        return (from DataRow dr in dt.Rows
                                       select new userdataforSms()
                                       {
                                           EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                           noof = dr["noof"].ToString(),
                                           mobileNo = dr["mobileNo"].ToString(),
                                       
                                       }).ToList();

                    
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<userdataforSms> getuserforsendDatamerged()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_sendIonallemployeeMerged", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);

                        return (from DataRow dr in dt.Rows
                                select new userdataforSms()
                                {
                                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                    noof = dr["nominute"].ToString(),
                                    noofion = dr["noofion"].ToString(),
                                    mobileNo = dr["mobileNo"].ToString(),

                                }).ToList();


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<sp_getRanking_Result> sp_getranking()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getRanking().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public int SaveTop(GLUser obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    var listall = context.GLUsers.Where(u => u.Active == false).ToList();
                    foreach (var item in listall)
                    {
                        item.ShowTop = false;
                        item.MonthlyScore = 0;

                    }
                    context.SaveChanges();

                    foreach (var itemp in obj.TopUsers)
                    {
                        var ListTopUsers = context.GLUsers.Where(u => u.EmployeeID.ToString() == itemp && u.Active == false).ToList();

                        if (TopUsers != null)
                        {

                            foreach (var itempsub in ListTopUsers)
                            {
                                itempsub.ShowTop = true;
                                itempsub.MonthlyScore = itempsub.UserScore;


                            }
                            context.SaveChanges();
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

        public List<sp_TopRanking10_Result> sp_TopRanking10()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_TopRanking10().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int ResetShowTop()
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    List<GLUser> Listpci = new List<GLUser>();
                    var ListTopUsers = context.GLUsers.ToList();


                    foreach (var itemp in ListTopUsers)
                    {


                        if (itemp != null && itemp.ShowTop == true)
                        {


                            itemp.ShowTop = false;


                            context.SaveChanges();
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

        public List<sp_getAllION_Result> sp_getAllION(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllION(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getAllReadedION_Result> sp_getAllReadedION(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllReadedION(id).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
      
    }



    public class userdataforSms {

        public int EmployeeID { get; set; }
        public string noof { get; set; }
        public string noofion { get; set; }

        public string mobileNo { get; set; }
        

    }

}
