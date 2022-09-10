using HRandPayrollSystemModel.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class SMSServiceController : Controller
    {
        // GET: SMSService
        public ActionResult SendSMSToUser()
        {
          

            if (DateTime.Today.DayOfWeek.ToString().ToLower()=="sunday".ToLower())
            {
                return Content("Today is Sunday, Message will send you tomorrow !");

            }
            GLUser userobj = new GLUser();
         //   var result=  userobj.getuserforsendData();
            var result = userobj.getuserforsendDatamerged();

            if (result!=null)
            {


                foreach (var item in result)
                {
                    if (item.EmployeeID == 1)
                    {
                        string ionst = "";
                        if (item.noofion.Trim()!="0")
                        {
                            ionst = "and ( " + item.noofion + " ) new ion ";
                        }

                        var smsBody = "Respected President,  \nYou have ( "+item.noof+ " ) new e-minute(s) "+ ionst + " in your e-minute portal, please check it. \nRegards,";

                        //   var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, item.mobileNo);
                        var number = item.mobileNo.Substring(2);
                        var reply = SendSMS("0" + number, smsBody);


                    }
                    else
                    {

                        string ionst = "";
                        if (item.noofion.Trim() != "0")
                        {
                            ionst = "and ( " + item.noofion + " ) new ion ";
                        }



                        var smsBody = "Dear Sir /Mam ,  \nYou have ( " + item.noof + " ) new e-minute(s) "+ ionst +" in your e-minute portal, please check it. \nRegards,";

                        //  var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, item.mobileNo);
                        var number = item.mobileNo.Substring(2);
                        var reply = SendSMS("0" + number, smsBody);

                    }
                   
                }
               
               // string joined = string.Join(",", result.Select(x=>x.mobileNo));

               // var smsBody = "Dear Sir /Mam ,  \nYou have new e-minute arrived in your eminute portal requested to please check them. \nRegards,";

               //var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, joined);

            }
            return Content("Correct"); 
        }


        public ActionResult SendIonSMSToUser()
        {


            if (DateTime.Today.DayOfWeek.ToString().ToLower() == "sunday".ToLower())
            {
                return Content("Today is Sunday, Message will send you tomorrow !");

            }
            GLUser userobj = new GLUser();
            var result = userobj.getuserforIonsendData();
            if (result != null)
            {


                foreach (var item in result)
                {


                    var smsBody = "Dear Sir /Mam ,  \nYou have ( " + item.noof + " ) new ION(s) in your eminute portal, please check it. \nRegards,";

                    //  var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, item.mobileNo);
                    var number = item.mobileNo.Substring(2);
                    var reply = SendSMS("0" + number, smsBody);

                    //if (item.EmployeeID == 1)
                    //{
                    //    var smsBody = "Respected President,  \nYou have ( " + item.noof + " ) new e-minute(s) in your e-minute portal, please check it. \nRegards,";

                    //    //   var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, item.mobileNo);
                    //    var number = item.mobileNo.Substring(2);
                    //    var reply = SendSMS("0" + number, smsBody);


                    //}
                    //else
                    //{


                    //}

                }

                // string joined = string.Join(",", result.Select(x=>x.mobileNo));

                // var smsBody = "Dear Sir /Mam ,  \nYou have new e-minute arrived in your eminute portal requested to please check them. \nRegards,";

                //var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, joined);

            }
            return Content("Correct");
        }



        //https://connect.jazzcmt.com/sendsms_url.html?Username=03051772899&Password=Jazz@123&From=Al-Shifa&To=03013857575&Message=asdgajdgas


        public string SendSMS( string toNumber="03013857575", string MessageText = "", string Masking = "Al-Shifa")
        {

            //  "03051772899" 
           //"Jazz@123"

            string MyUsername = ConfigurationManager.AppSettings["SMSApiNmber"];
            string MyPassword = ConfigurationManager.AppSettings["SMSApiPass"];

            String URI = "https://connect.jazzcmt.com" +
            "/sendsms_url.html?" +
            "Username=" + MyUsername +
            "&Password=" + MyPassword +
            "&From=" + Masking +
            "&To=" + toNumber +
            "&Message=" + Uri.UnescapeDataString(MessageText); // Visual Studio 10-15 
            ///  "//&message=" + System.Net.WebUtility.UrlEncode(MessageText);// Visual Studio 12 
            try
            {
                WebRequest req = WebRequest.Create(URI);
                WebResponse resp = req.GetResponse();
                var sr = new System.IO.StreamReader(resp.GetResponseStream());
                return sr.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                var httpWebResponse = ex.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    switch (httpWebResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return "404:URL not found :" + URI;
                            break;
                        case HttpStatusCode.BadRequest:
                            return "400:Bad Request";
                            break;
                        default:
                            return httpWebResponse.StatusCode.ToString();
                    }
                }
            }
            return null;
        }


    }
}