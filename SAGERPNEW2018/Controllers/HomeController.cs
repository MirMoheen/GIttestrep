using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SAGERPNEW2018.Controllers
{
    [NoCache]
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]

    [HandleExceptionFilter]
    public class HomeController : Controller
    {
       // string constring = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;
        [UserRightFilters]
        public ActionResult Index()
        {

           

            TempData["Isminute"]= Convert.ToBoolean(   new SAGERPNEW2018.Models.SystemLogin().GetUser().Type);

            //var student = new HRandPayrollSystemModel.DBModel.AdmissionInformation().getStudentData();
            //ViewBag.part1 = student.Where(x => x.Partid == 1).Count();
            //ViewBag.part2 = student.Where(x => x.Partid == 2).Count();
            //ViewBag.part3 = student.Where(x => x.Partid == 3).Count();

            //ViewBag.Mgender1 = student.Where(x => x.gender == false && x.Partid == 1).Count();
            //ViewBag.Fgender1 = student.Where(x => x.gender == true && x.Partid == 1).Count();

            //ViewBag.Mgender2 = student.Where(x => x.gender == false && x.Partid == 2).Count();
            //ViewBag.Fgender2 = student.Where(x => x.gender == true && x.Partid == 2).Count();

            //ViewBag.Mgender3 = student.Where(x => x.gender == false && x.Partid == 3).Count();
            //ViewBag.Fgender3 = student.Where(x => x.gender == true && x.Partid == 3).Count();

            //var Package = new HRandPayrollSystemModel.DBModel.StudentPackageInfo().getAllPackages();
            //ViewBag.Packagepart1 = Package.Where(x => x.PartID == 1);
            //ViewBag.Packagepart2 = Package.Where(x => x.PartID == 2);
            //ViewBag.Packagepart3 = Package.Where(x => x.PartID == 3);
            return View();
        }

        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }



        public ActionResult deshboard()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            Loginform log = new Loginform();
            new SystemLogin().ExpireUserCookiee();
            FormsAuthentication.SignOut();
            return View(log);
        }
       
        private string GetSystemInfo()
        {
            try
            {
           
              //  string system = Environment.MachineName;
             //   string username= Environment.UserName  ;
                string ip = Request.UserHostAddress;
               // var user = HttpContext.User.Identity.Name;
                //IPAddress[] ipAddress = Dns.GetHostAddresses(Dns.GetHostName());
                //foreach (IPAddress item in ipAddress)
                //{
                //    if (item.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        ipsystem = "|" + item.ToString();
                //    }
                //}



                return  ip ;

            }
            catch (Exception ex)
            {
                return "";
            }
        }




        public ActionResult CheckLogin(Loginform a)

        {
            new SystemLogin().ExpireUserCookiee();

            var logindata = new GLUser().UserLogin(a.Username, a.Password);



            if (logindata != null)
            {

                logindata.ProjectIDs = logindata.ProjectIDs;
                logindata.system = GetSystemInfo();

                var resultemp = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(logindata.EmployeeID);
                if (resultemp != null)
                {
                    logindata.UserInfo = resultemp.Initiatedby;
                }
                else
                {
                    logindata.UserInfo = "Guest";

                }


                //    logindata.UserInfo = new HRandPayrollSystemModel.DBModel.tblEminuteInfo() .LoadEmployeeInfo(logindata.EmployeeID).Initiatedby;

                new SystemLogin().SetDataUser(logindata);

                if (new SAGERPNEW2018.Models.SystemLogin().GetUser().IsIP)
                {

                    if (logindata.ipCheck.Trim() == Request.UserHostAddress)
                    {


                        new SystemLogin().SetDataCompany(new GLUser().GetCompany(logindata.companyID));
                        new SystemLogin().SetDataProject(new GLUser().Getprojects(Convert.ToInt32(logindata.ProjectIDs)));

                        return RedirectToAction("Index");


                    }
                    else
                    {

                        TempData["LoginFailed"] = "You Can't Login from another Device";
                        return View("Login", a);
                    }

                }
                else
                {
                    new SystemLogin().SetDataCompany(new GLUser().GetCompany(logindata.companyID));
                    new SystemLogin().SetDataProject(new GLUser().Getprojects(Convert.ToInt32(logindata.ProjectIDs)));

                    return RedirectToAction("Index");


                }


            }
            else
            {
                TempData["LoginFailed"] = "Try Again Incorrect username or password";
                return View("Login", a);

            }


        }



        //public ActionResult CheckLogin(Loginform a)

        //{
        //    new SystemLogin().ExpireUserCookiee();

        //    var logindata = new GLUser().UserLogin(a.Username, a.Password);

        //    if (logindata != null)
        //    {
               
        //        logindata.ProjectIDs = logindata.ProjectIDs;
        //        logindata.system = GetSystemInfo();

        //        var resultemp = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(logindata.EmployeeID);
        //        if (resultemp != null)
        //        {
        //            logindata.UserInfo = resultemp.Initiatedby;
        //        }
        //        else
        //        {
        //            logindata.UserInfo = "Guest";

        //        }


        //        //    logindata.UserInfo = new HRandPayrollSystemModel.DBModel.tblEminuteInfo() .LoadEmployeeInfo(logindata.EmployeeID).Initiatedby;

        //        new SystemLogin().SetDataUser(logindata);
        //        new SystemLogin().SetDataCompany(new GLUser().GetCompany(logindata.companyID));
        //        new SystemLogin().SetDataProject(new GLUser().Getprojects(Convert.ToInt32(logindata.ProjectIDs)));

        //        //    FormsAuthentication.SetAuthCookie(a.Username + "|" + logindata.Userid.ToString() + "|" + logindata.Type.ToString() +"|" + logindata.system, false);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["LoginFailed"] = "Try Again Incorrect username or password";
        //        return View("Login", a);

        //    }


        //    //DataTable dt = new DataTable();
        //    //SqlDataAdapter da = new SqlDataAdapter(@" select * from gluser where GLUser.UserName='" + a.Username + "' and  GLUser.UserPassword='" + a.Password + "'", con);
        //    //da.Fill(dt);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    FormsAuthentication.SetAuthCookie(a.Username +"|"+ dt.Rows[0]["userid"].ToString() +"|"+ dt.Rows[0]["type"].ToString() + 
        //    //        "|"+ a.FiscalID+"|"+ a.Companyid+"|"+ system , false);

        //    //    Session["rightdt"] = dt;
        //    //    Session["UserRight"] = dt.Rows[0]["type"].ToString();
        //    //    Session["userid"] = dt.Rows[0]["userid"].ToString();
        //    //    Session["FiscalID"] = a.FiscalID;
        //    //    Session["Companyid"] = a.Companyid;
        //    //    Session["system"] = system;
        //    //    return View("Index",a);
        //    //}
        //    //else
        //    //{
        //    //    TempData["LoginFailed"] = "Try Again Incorrect username or password";
        //    //    return View("Login",a);

        //    //}
        //}
        //public ActionResult CheckLogin(Loginform a)

        //{

        //    var logindata=new GLUser().UserLogin(a.Username, a.Password);

        //    if (logindata!=null )
        //    {
        //        if (!logindata.ProjectIDs.Contains(a.ProjectID))
        //        {

        //            TempData["LoginFailed"] = "This User has no rights of selected project.!";
        //            return View("Login", a);
        //        }
        //        logindata.ProjectIDs = a.ProjectID;
        //        logindata.system = GetSystemInfo();

        //        var resultemp = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(logindata.EmployeeID);
        //        if (resultemp != null)
        //        {
        //            logindata.UserInfo = resultemp.Initiatedby;
        //        }
        //        else
        //        {
        //            logindata.UserInfo = "Guest";

        //        }


        //    //    logindata.UserInfo = new HRandPayrollSystemModel.DBModel.tblEminuteInfo() .LoadEmployeeInfo(logindata.EmployeeID).Initiatedby;

        //        new SystemLogin().SetDataUser(logindata);
        //        new SystemLogin().SetDataCompany(new GLUser().GetCompany(logindata.companyID));
        //        new SystemLogin().SetDataProject(new GLUser().Getprojects(Convert.ToInt32(a.ProjectID)));

        //        //    FormsAuthentication.SetAuthCookie(a.Username + "|" + logindata.Userid.ToString() + "|" + logindata.Type.ToString() +"|" + logindata.system, false);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["LoginFailed"] = "Try Again Incorrect username or password";
        //        return View("Login", a);

        //    }


        //    //DataTable dt = new DataTable();
        //    //SqlDataAdapter da = new SqlDataAdapter(@" select * from gluser where GLUser.UserName='" + a.Username + "' and  GLUser.UserPassword='" + a.Password + "'", con);
        //    //da.Fill(dt);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    FormsAuthentication.SetAuthCookie(a.Username +"|"+ dt.Rows[0]["userid"].ToString() +"|"+ dt.Rows[0]["type"].ToString() + 
        //    //        "|"+ a.FiscalID+"|"+ a.Companyid+"|"+ system , false);

        //    //    Session["rightdt"] = dt;
        //    //    Session["UserRight"] = dt.Rows[0]["type"].ToString();
        //    //    Session["userid"] = dt.Rows[0]["userid"].ToString();
        //    //    Session["FiscalID"] = a.FiscalID;
        //    //    Session["Companyid"] = a.Companyid;
        //    //    Session["system"] = system;
        //    //    return View("Index",a);
        //    //}
        //    //else
        //    //{
        //    //    TempData["LoginFailed"] = "Try Again Incorrect username or password";
        //    //    return View("Login",a);

        //    //}
        //}

        public JsonResult ChangePassword( string currentpas, string paswrd)
        {
            var userid= new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            var check = new GLUser().ChangeUserPassword(userid, paswrd, currentpas);

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadChart(int id)
        {
            var ProjectIDs = new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs;
            var check = new GLUser().loadChart( Convert.ToInt32(ProjectIDs));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadMinuteChart(int id,string fdate,string tdate)
        {

            DateTime fudate = DateTime.Parse(fdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(tdate, new CultureInfo("en-US", true)); 
           // id = 3;
            var empid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadMinuteChart(id,fudate,tudate, empid);

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadMinuteChartChart(int id, string fdate, string tdate)
        {

            DateTime fudate = DateTime.Parse(fdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(tdate, new CultureInfo("en-US", true));
            // id = 3;
            var empid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadMinuteChartchart(id, fudate, tudate, empid);

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataTransferEmployyee(int id)
        {
            GLUser model = new GLUser();
            if (id > 0)
            {
                model.TransferEmpID = id;
                model.IsTransfer = true;
            }
            else
            {
                model.TransferEmpID = null;
                model.IsTransfer = false;
            }
            var useridt = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            model.Userid = useridt;
            var check = new GLUser().UpdateDataforTransfer(model);

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadCompChart(int id, string fdate, string tdate)
        {

            DateTime fudate = DateTime.Parse(fdate, new CultureInfo("en-US", true));
            DateTime tudate = DateTime.Parse(tdate, new CultureInfo("en-US", true));
            // id = 3;
            var ProjectIDs = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getGrphCompDatanew2(Convert.ToInt32(ProjectIDs), fudate, tudate);

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadChartDepartmentSalry(int id)
        {
            var ProjectIDs = new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs;
            var check = new GLUser().loadDepartmentSalaryChart(Convert.ToInt32(ProjectIDs));

            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadNewMinutes(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadnewMinutes(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadholdMinutes(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadHoldMinutes(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }



        public JsonResult loadPendingMinutes(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadpendingMinutes(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult sp_getAllApprovedMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllApprovedMinute(Convert.ToInt32(useid));//.Skip(0).Take(10000);

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllPurchasedMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getnewAllPurchaseMinute(Convert.ToInt32(useid));//.Skip(0).Take(10000);

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
        public JsonResult sp_getAllCompletedMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCompletedMinute(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllCancelMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCancelMinute(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadNewComp(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadnewCompalint(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadPendingComp(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCompPending(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public JsonResult sp_getAllApprovedComp(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllApprovedComaplint(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllCompletedComp(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCompCompleted(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllCancelComp(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCompCancel(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllButtonStatus(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllButtonnewStatus(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllCompButtonnewStatus(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllCompButtonnewStatus(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

      

        public JsonResult sp_getAllForInfoMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllForInfoMinute(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllIONMinute(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllIONMinute(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadActivity(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().loadActivities(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }



        public JsonResult sp_getAllION(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllION(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult sp_getAllReadedION(int id)
        {
            var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
            var check = new GLUser().sp_getAllReadedION(Convert.ToInt32(useid));

            return Json(check, JsonRequestBehavior.AllowGet);
        }


    }
}