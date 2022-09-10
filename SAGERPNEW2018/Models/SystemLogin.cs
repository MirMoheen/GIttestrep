using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SAGERPNEW2018.Models
{
    public class SystemLogin : System.Web.HttpApplication
    {


        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool type { get; set; }
        public string SystemName { get; set; }
        public string IP{ get; set; }


    

        public string getCommentby()
        {
            GLUser user = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["cookiehtmlboxs"];

            if (authCookie != null)
            {
                user = new GLUser();

                return authCookie.Values["commentby"].ToString();

            }
            return "";
        }



        public void SetDataUser(GLUser user)
        {
            try
            {
                // HttpContext.Current.Session["user"] = user;
               
               HttpCookie aCookie = new HttpCookie("UserCookie");
                aCookie.Values["Userid"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.Userid.ToString())));
                aCookie.Values["UserName"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.UserName.ToString())));
                aCookie.Values["UserPassword"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.UserPassword.ToString()))); 
                aCookie.Values["mobileNo"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.mobileNo.ToString())));

                aCookie.Values["Type"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.Type.ToString())));
                aCookie.Values["system"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.system.ToString()))); 
                aCookie.Values["companyID"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.companyID.ToString())));
                aCookie.Values["ProjectIDs"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.ProjectIDs.ToString())));
                aCookie.Values["DepartmentID"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.DepartmentID.ToString()))); 
                aCookie.Values["EmployeeID"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.EmployeeID.ToString())));
                aCookie.Values["IsHRSystem"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsHRSystem.ToString())));
                aCookie.Values["UserInfo"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.UserInfo.ToString())));

                aCookie.Values["AllowSendSMS"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.AllowSendSMS.ToString())));

                aCookie.Values["AllowMarkPO"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.AllowMarkPO.ToString())));
                aCookie.Values["UserColor"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.UserColor.ToString())));
                aCookie.Values["ForPayment"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.ForPayment.ToString())));
                aCookie.Values["IspettyCash"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IspettyCash.ToString())));
                aCookie.Values["PaymentRange"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.PaymentRange==null?"0" : user.PaymentRange.ToString())));
                aCookie.Values["IsReminder"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsReminder.ToString())));
                aCookie.Values["IsDeleteAT"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsDeleteAT.ToString())));
                aCookie.Values["IsOpen"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsOpen.ToString())));
                aCookie.Values["IsTechicalPerson"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsTechicalPerson.ToString())));
                aCookie.Values["IsHold"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsHold.ToString())));
                aCookie.Values["IsDiscuss"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsDiscuss.ToString())));
                aCookie.Values["IsPaymentBeforeApprovel"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsPaymentBeforeApprovel.ToString())));
                aCookie.Values["IsDirectApproval"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsDirectApproval.ToString())));
                aCookie.Values["IsIP"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsIP.ToString())));
                aCookie.Values["pettycashlimit"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.pettycashlimit.ToString())));
                aCookie.Values["IsIT"] = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(user.IsIT.ToString())));






                //      aCookie.Expires = DateTime.Now.AddMinutes(30);


                //     aCookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            catch (Exception ex)
            { }
        }

        public GLUser GetUser()
        {
            GLUser user = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];
         
            if (authCookie != null)
            {
                user = new GLUser();


                user.Userid =    Convert.ToInt32((Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["Userid"])))));
                user.UserName = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["UserName"])));/// Convert.ToString(authCookie.Values["UserName"]);
                user.UserPassword = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["UserPassword"]))); //  Convert.ToString(authCookie.Values["UserPassword"]);
                user.Type =   Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["Type"]))));
                user.mobileNo = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["mobileNo"])));// authCookie.Values["mobileNo"];
                user.system = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["system"])));// Convert.ToString(authCookie.Values["system"]);
                user.companyID =Convert.ToInt32(  Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["companyID"]))));// string.IsNullOrWhiteSpace( authCookie.Values["companyID"])?0 : Convert.ToInt32(authCookie.Values["companyID"]);
                user.ProjectIDs = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["ProjectIDs"])));// authCookie.Values["ProjectIDs"];
                user.DepartmentID = Convert.ToInt32(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["DepartmentID"]))));// Convert.ToInt32( authCookie.Values["DepartmentID"]);
                user.EmployeeID = Convert.ToInt32(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["EmployeeID"]))));// Convert.ToInt32(authCookie.Values["EmployeeID"]);
                user.IsHRSystem = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsHRSystem"]))));// Convert.ToInt32(authCookie.Values["EmployeeID"]);
                user.UserInfo = Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["UserInfo"])));
                user.AllowMarkPO = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["AllowMarkPO"]))));
                user.AllowSendSMS = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["AllowSendSMS"]))));
                user.UserColor = string.IsNullOrWhiteSpace( authCookie.Values["UserColor"])?"":  Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["UserColor"])));
                user.ForPayment = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["ForPayment"]))));
                user.PaymentRange = string.IsNullOrWhiteSpace(authCookie.Values["PaymentRange"]) ? "" : Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["PaymentRange"])));
                user.IspettyCash = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IspettyCash"]))));
                user.IsReminder = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsReminder"]))));
                user.IsDeleteAT = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsDeleteAT"]))));
                user.IsOpen = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsOpen"]))));
                user.IsTechicalPerson = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsTechicalPerson"]))));
                user.IsHold = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsHold"]))));
                user.IsDiscuss = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsDiscuss"]))));
                user.IsPaymentBeforeApprovel = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsPaymentBeforeApprovel"]))));
                user.IsDirectApproval = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsDirectApproval"]))));
                user.IsIP = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsIP"]))));
                user.pettycashlimit = Convert.ToDecimal(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["pettycashlimit"]))));

                user.IsIT = Convert.ToBoolean(Encoding.UTF8.GetString(MachineKey.Unprotect(Convert.FromBase64String(authCookie.Values["IsIT"]))));


            }
            return user;
        }



        public void SetDataCompany(tblCompany compony)
        {
            try
            {

                HttpCookie aCookie = new HttpCookie("companyCookies");
                aCookie.Values["id"] = compony.id.ToString();
                aCookie.Values["CompanyName"] = compony.CompanyName;
                aCookie.Values["CompanyLogo"] = compony.CompanyLogo;
             ///
             /// 
             /// aCookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            catch (Exception)
            { }
        }

        public tblCompany Getcompany()
        {
            tblCompany company = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["companyCookies"];

            if (authCookie != null)
            {
                company = new tblCompany();
                company.id = Convert.ToInt32((authCookie.Values["id"]));
                company.CompanyLogo = Convert.ToString(authCookie.Values["CompanyLogo"]);
                company.CompanyName = Convert.ToString(authCookie.Values["CompanyName"]);



            }
            return company;
        }


        public void SetDataProject(tblProject project)
        {
            try
            {

                HttpCookie aCookie = new HttpCookie("ProjectCookies");
                aCookie.Values["ProjectID"] = project.ProjectID.ToString();
                aCookie.Values["ProjectCode"] = project.ProjectCode;
                aCookie.Values["ProjectName"] = project.ProjectName;
                ///
                /// 
                /// aCookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            catch (Exception)
            { }
        }
        public tblProject GetProject()
        {
            tblProject project = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["ProjectCookies"];

            if (authCookie != null)
            {
                project = new tblProject();
                project.ProjectID = Convert.ToInt32((authCookie.Values["ProjectID"]));
                project.ProjectCode = Convert.ToString(authCookie.Values["ProjectCode"]);
                project.ProjectName = Convert.ToString(authCookie.Values["ProjectName"]);



            }
            return project;
        }


        public void SetDataUserRights(GLUser user)
        {
            try
            {

                var ListOfRight = new SystemLogin().checkRightUser(user.Userid);
                string myObjectJson = JsonConvert.SerializeObject(ListOfRight,Formatting.Indented);
                HttpCookie cookie = new HttpCookie("RightCookie"); 
                cookie.Value = HttpContext.Current.Server.UrlEncode(myObjectJson);
                HttpContext.Current.Response.Cookies.Add(cookie);


                //                var cookie = new HttpCookie("RightCookie", myObjectJson)
                //{
                //    Expires = DateTime.Now.AddYears(1)
                //};
                //HttpContext.Current.Response.Cookies.Add(cookie);

             

                //HttpCookie aCookie = new HttpCookie("RightCookie");
                //var ListOfRight = new Login().checkRightUser(user.Userid);
                //aCookie.Values["Rights"] = JsonConvert.SerializeObject(ListOfRight);
                //aCookie.Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies.Add(aCookie);



                //if (HttpContext.Current.Request.Cookies["UserRightCookie"] == null)
                //{
                //   // var ListOfRight = new Login().checkRightUser(user.Userid);
                //    HttpContext.Current.Response.Cookies.Add(new HttpCookie("UserRightCookie", JsonConvert.SerializeObject(ListOfRight)));
                //}

            }
            catch (Exception)
            { }
        }
        
        public List<sp_GetUserRightByUser_Result> GetDataUserRights()
        {
            try
            {
                HttpContext authCookie = HttpContext.Current;
                if (authCookie != null)
                {

                    return JsonConvert.DeserializeObject<List<sp_GetUserRightByUser_Result>>(Convert.ToString(authCookie.Request.Cookies["RightCookie"]));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        
        public List<sp_GetUserRightByUser_Result> checkRightUser(int UserID )
        {
            using (var context = new HRandPayrollDBEntities())
            {
              return  context.sp_GetUserRightByUser(UserID).OrderBy(x=>x.Formid).ToList();
            }
        }
        public void ExpireUserCookiee()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }

            HttpCookie CompanyCookie = HttpContext.Current.Request.Cookies["ProjectCookies"];
            if (CompanyCookie != null)
            {
                CompanyCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(CompanyCookie);
            }
        }

        //public bool MenuExist(int UserRoleId, string ActionName, string ControllerName)
        //{
        //    try
        //    {
        //        using (var context = new SalesOperationsEntities())
        //        {
        //            var configurationId = context.MenusConfigurations.SingleOrDefault(c => c.ActionName.Equals(ActionName) && c.ControllerName.Equals(ControllerName)).ConfigurationId;

        //            if (context.MenusRights.Where(m => m.RoleId == UserRoleId && m.ConfigurationId == configurationId).ToList().Count() > 0)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new Logger().LogError(ex);
        //        return false;
        //    }
        //}


    }
}


//using SalesOperations.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SalesOperations.Filters
//{
//    public class CookieHandler : System.Web.HttpApplication
//    {
//        public void SetUser(User user)
//        {
//            try
//            {
//                HttpCookie aCookie = new HttpCookie("UserCookie");

//                aCookie.Values["UserId"] = new SalesOperations.Common.SecurityManager().EncodeString(user.UserId.ToString());
//                aCookie.Values["LoginName"] = user.LoginName;
//                aCookie.Values["LoginPassword"] = user.LoginPassword;
//                aCookie.Values["UserRoleId"] = user.UserRoleId.ToString();
//                aCookie.Values["FirstName"] = user.FirstName;
//                aCookie.Values["LastName"] = user.LastName;
//                aCookie.Expires = DateTime.Now.AddDays(1);
//                HttpContext.Current.Response.Cookies.Add(aCookie);
//            }
//            catch (Exception)
//            { }
//        }

//        public void SetDistributor(Distributor distributor)
//        {
//            try
//            {
//                HttpCookie aCookie = new HttpCookie("DistributorCookie");
//                aCookie.Values["DistributorId"] = distributor.Distributor_Id.ToString();
//                aCookie.Values["Name"] = distributor.Name;
//                aCookie.Expires = DateTime.Now.AddDays(1);
//                HttpContext.Current.Response.Cookies.Add(aCookie);
//            }
//            catch (Exception)
//            { }
//        }


//        public User GetUser()
//        {
//            User user = null;
//            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];

//            if (authCookie != null)
//            {
//                user = new User();
//                user.UserId = Convert.ToInt32(new SalesOperations.Common.SecurityManager().DecodeString(authCookie.Values["UserId"]));
//                user.LoginName = Convert.ToString(authCookie.Values["LoginName"]);
//                user.LoginPassword = Convert.ToString(authCookie.Values["LoginPassword"]);
//                user.UserRoleId = Convert.ToInt32(authCookie.Values["UserRoleId"]);
//                user.FirstName = Convert.ToString(authCookie.Values["FirstName"]);
//                user.LastName = Convert.ToString(authCookie.Values["LastName"]);
//            }
//            return user;
//        }

//        public Distributor GetDistributor()
//        {
//            Distributor distributor = null;
//            HttpCookie authCookie = HttpContext.Current.Request.Cookies["DistributorCookie"];

//            if (authCookie != null)
//            {
//                distributor = new Distributor();
//                distributor.Distributor_Id = Convert.ToInt32(authCookie.Values["DistributorId"]);
//                distributor.Name = Convert.ToString(authCookie.Values["Name"]);

//            }
//            return distributor;
//        }

//        public void ExpireUserCookiee()
//        {
//            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];
//            if (authCookie != null)
//            {
//                authCookie.Expires = DateTime.Now.AddDays(-1);
//                HttpContext.Current.Response.Cookies.Add(authCookie);
//            }
//        }

//        public void ExpireDistributorCookiee()
//        {
//            HttpCookie authCookie = HttpContext.Current.Request.Cookies["DistributorCookie"];
//            if (authCookie != null)
//            {
//                authCookie.Expires = DateTime.Now.AddDays(-1);
//                HttpContext.Current.Response.Cookies.Add(authCookie);
//            }
//        }
//    }
//}