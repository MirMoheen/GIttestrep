using HRandPayrollSystemModel.DBModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SAGERPNEW2018.Controllers
{
    public class SystemApiController : ApiController
    {
        private void urltofiles(string[] patharry, tblEminuteInfo model)
        {
            try
            {

                List<string> myCollection = new List<string>();
                List<string> listofDescription = new List<string>();

                //var path1 = "http://172.16.0.12:8010/" + patharry[0];
                //var path2 = "http://172.16.0.12:8010/" + patharry[1];

                foreach (var item in patharry)
                {
                    string url = "http://172.16.0.12:8010/" + item;



                    byte[] data;
                    using (var webClient = new WebClient())
                        data = webClient.DownloadData(url);
                    string enc = Convert.ToBase64String(data);

                    String pathof = HttpContext.Current.Server.MapPath("~/AppFiles/MinuteAttachments/"); //Path

                    //Check if directory exist
                    if (!System.IO.Directory.Exists(pathof))
                    {
                        System.IO.Directory.CreateDirectory(pathof); //Create directory if it doesn't exist
                    }
                    var Random = Guid.NewGuid().ToString("n").Substring(0, 8);
                    string imageName = model.MinuteCode + "Auto" + Random + ".pdf";
                    //   var t = enc;  // remove data:image/png;base64,
                    string Temppath = "~/AppFiles/MinuteAttachments/" + imageName;
                    //set the image path
                    string imgPath = Path.Combine(pathof, imageName);
                    //  byte[] imageBytes = Convert.FromBase64String(enc);

                    File.WriteAllBytes(imgPath, data);
                    myCollection.Add(Temppath);
                    listofDescription.Add("Attached File");
                }

                model.listdocpath = myCollection;
                model.lisdescription = listofDescription;


            }
            catch (Exception ex)
            {

                model.listdocpath = new List<string>();
            }

            
        }


        [HttpGet]
   
        public IHttpActionResult EminuteApi(string Token, int projectid, int departmentID, int ForwardTo, int entryuser, string priorrity, string mType, int Subtype, string Subject, string HtmlBox, string ip,string path)

        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "No Token supplied.",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = false,
                        Reason = "Invalid Token supplied.",
                    });
                


                var patharry = path.Split(',');
                
                var objUser = new GLUser().getAlldataByID(Convert.ToInt32(entryuser));
                var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(objUser.EmployeeID);

                tblEminuteInfo model = new tblEminuteInfo();

                model.MinuteDate = DateTime.Now;
                model.MinuteCode = model.geneartMinuteNo(1, "E");

                 urltofiles(patharry, model);

                model.EntryID = entryuser;
                model.Type = mType;
                model.Subtype = Subtype;
                model.HtmlBox = "<p>" + HtmlBox + "</p>";
                model.MinuteType = 1;
                model.Priority = priorrity;
                model.IsPo = true;
                model.ForwardTo = ForwardTo;
                model.Transferedfrom = 0;
                model.Subject = Subject;
                model.Initiatedby = employedata.Initiatedby.ToUpper();
                model.UserColor = objUser.UserColor;
                model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;
                model.InitiatedDepartment = employedata.DepartmentName.ToUpper();

                // tranfer user///////////
                var tranferemp = checktransfer(Convert.ToInt32(model.ForwardTo));
                if (tranferemp != null)
                {
                    model.Transferedfrom = Convert.ToInt32(model.ForwardTo);
                    model.ForwardTo = Convert.ToInt32(tranferemp);
                    model.isforward = false;
                }
                ///////////////////////

                model.EntryDate = DateTime.Now;
                model.EntryID = entryuser;
                model.EntryIP = ip;
                model.ProjectID = projectid;
                model.DepartmentID = objUser.DepartmentID;
                model.EmployeeID = objUser.EmployeeID;
             
                

                model.addata(model);
                return Json(model.MinuteCode);

            }
            catch (Exception ex)
            {

                return Json("Fail");
            }



        }

        public int? checktransfer(int empid)
        {


            try
            {

                return new tblEminuteInfo().transfercheck(empid);




            }
            catch (Exception ex)
            {

                return null;
            }


        }



        string tokenNumber = "Zulqarnain1122@1122";
        [HttpGet]
        public IHttpActionResult GetmintPrint(string Token, string MinuteNo)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "No Token supplied.",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = false,
                        Reason = "Invalid Token supplied.",
                    });
         
                var Printlist = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().minutePrint(MinuteNo);
                if (Printlist != null)
                {

                    return Json(Printlist);

                }

                else
                {
                    return Json(new
                    {
                        Success = false,
                        Reason = "Print Not found",
                    });

                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "Error occur in Api.",
                });
            }
        }


        //[HttpGet]
        //public IHttpActionResult SendSMSToUser(string Token, string stwich)
        //{

        //    if (Token == null)
        //        return Json(new
        //        {
        //            Success = false,
        //            Reason = "No Token supplied.",
        //        });
        //    if (Token != tokenNumber)
        //        return Json(new
        //        {
        //            Success = false,
        //            Reason = "Invalid Token supplied.",
        //        });


        //    string[] Arryphone = { "923127798518" ,"923018506709","923215226284"};


        //    if (Arryphone != null)
        //    {
        //        foreach (var item in Arryphone)
        //        {

        //            var smsBody = "Network switch down " + stwich;

        //            var reply = new SAGERPNEW2018.Controllers.EminuteController().sendsms(smsBody, item);


        //        }



        //    }

        //    return Json(new
        //    {
        //        Success = true,
        //        Reason = "sent.",
        //    });
        //}


    }
}
