using HRandPayrollSystemModel.AttendanceModel;
using HRandPayrollSystemModel.DBModel;
using HRandPayrollSystemModel.FixedModel;
using HRandPayrollSystemModel.HimsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAGERPNEW2018.Controllers
{
    public class MobileApiController : ApiController
    {

        string tokenNumber = "Zulqarnain1122@1122";
        [HttpGet]
        public IHttpActionResult GetUser(string Token, string Name, string password)
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

                var logindata = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().UserLogin(Name, password);


                if (logindata != null)
                {

                    return Json(logindata );

                }

                else
                {
                    return Json(new tblUsersLogin());

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

        [HttpGet]
        public IHttpActionResult GetDataForAssetbyCode(string Token, string code)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().GetDataForAssetbyCode(code);


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return Json(new sp_getdataforandroid_Result());

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

        [HttpGet]
        public IHttpActionResult verifyAsset(string code, string userID, string status, string ip, string Token ,string details)
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

             var result=  new  HRandPayrollSystemModel.FixedModel.tblUsersLogin().updateverification(Convert.ToInt64(userID), code, Convert.ToBoolean( status), ip, details);

                if (result != null)
                {
                    return Json(result);
                }
                else
                {
                    return Json(new tblAsset());


                }


            }
            catch (Exception)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "Error occur in Api.",
                });
            }

        }




        [HttpGet]
        public IHttpActionResult GetDataForName(string Token, string name)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().GetDataRoomName(name);


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return Json(new Assetlist());

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





        [HttpGet]
        public IHttpActionResult loadprojectAPI(string Token, string data )
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().loadProject().ToList();


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return null;

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


        [HttpGet]
        public IHttpActionResult loadDepartmentAPI(string Token,string projectid)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().loadDepartment(projectid).OrderBy(x => x.Text);


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return null;

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




        [HttpGet]
        public IHttpActionResult loadTypeAPI(string Token)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().loadcategory().OrderBy(x => x.Text).ToList();


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return null;

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



        [HttpGet]
        public IHttpActionResult loadSubTypeAPI(string Token,int department,  int typeid)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().loadSubcategory( typeid);


                if (result != null)
                {

                    return Json(result);

                }

                else
                {
                    return null;

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



        [HttpGet]
        public IHttpActionResult loaddaataFixedListAPI(string Token,int projectID,  int department, int typeid,int subtype)
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

                var result = new HRandPayrollSystemModel.FixedModel.tblUsersLogin().loadfixedlist( projectID,  department,  typeid,  subtype);


                    return Json(result);

             

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




        [HttpGet]
        public IHttpActionResult VaccineApi(string Token, string FirstName , string lastname, string cnic, string mobile, string Passport, string DOB, string type )
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = "0",
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = "0",
                        Reason = "invalid Token supplied",
                    });
                

                tblVaccineRegistration obj = new tblVaccineRegistration();

                var resultcheck=    obj.CheckCnic(cnic);
                if (resultcheck!=null)
                {
                    return Json(new
                    {
                        Success = "0",
                        Reason = "cnic duplicate",
                    });
                }


                var c= Convert.ToInt32( cnic.Substring(cnic.Length - 1));
                if (c % 2 == 0)
                {
                    obj.Gender = true;
                  }
                else
                {
                    obj.Gender = false;
                }
                var datefromArr = DOB.Split('-');
                var dateofbirth = new DateTime(Convert.ToInt32(datefromArr[2]), Convert.ToInt32(datefromArr[1]), Convert.ToInt32(datefromArr[0]));
                obj.FirstName = FirstName;
                obj.LastName = lastname;
                obj.Cnic = cnic;
                obj.EntryDate = DateTime.Now;
                obj.DOB = dateofbirth;
                obj.Mobile = mobile;
                obj.VaccineType = type;

              var resultinsert=  obj.InsertVaccine(obj);


                

                if (resultinsert != null)
                {

                    return Json(new
                    {
                        Success = "1",
                        Reason = "done",
                    });

                }

                else
                {
                    return Json(new
                    {
                        Success = "0",
                        Reason = "fail try again",
                    });


                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = "0",
                    Reason = "Error occur in Api",
                });
            }
        }



        [HttpGet]
        public IHttpActionResult VaccineAvaible(string Token, int flag)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = "0",
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = "0",
                        Reason = "invalid Token supplied",
                    });


                tblVaccineRegistration obj = new tblVaccineRegistration();

                var parient = obj.getAvaliableforPatient();
               
                    return Json(new
                    {
                        
                        Success = "1",
                        Reason = parient,
                    });
               
                

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = "0",
                    Reason = "error occur in Api",
                });
            }
        }



        [HttpGet]
        public IHttpActionResult RegisterAttendance(string Token, string code, string name, string mac ,string mobile)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber && Token != "Alshifa1122@1122")
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });








                Attendance obj = new Attendance();

                var Checked = obj.Checkregister(name, code, mobile, mac);

            
                if (Checked!=null  && Checked.RegID>0)
                {
                    return Json(new
                    {
                        Success = false,
                        Reason = "Already Register",
                    });
                }



                var results = obj.registerlogin(name, code, mobile, mac);

                if (results.RegID > 0)
                {
                    return Json(new
                    {

                        Success = true,
                        Reason = "Register Success",
                    });

                }
                else
                {
                    return Json(new
                    {

                        Success = false,
                        Reason = "Register Fail",
                    });


                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "error occur in Api",
                });
            }
        }



        [HttpGet]
        public IHttpActionResult loginAttendance(string Token, string code, string name, string mac )
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber && Token != "Alshifa1122@1122")
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });



                Attendance obj = new Attendance();
                var result=   obj.getlogin(name, code, mac);
                result.Picture = null;

                return Json(result);





            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "error occur in Api",
                });
            }
        }







        [HttpGet]
       // [Route("api/getdataOnMacAdress/{Token}/{mac}")]
        
        public IHttpActionResult getdataOnMacAdress(string Token, string mac)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber && Token != "Alshifa1122@1122")
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });




                Attendance obj = new Attendance();

                var result = obj.Checkregister("", "", "", mac);

                result.Picture = null;

                return Json(result);





            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "error occur in Api",
                });
            }
        }



        [HttpGet]
      //  [Route("api/getmonthlyAttenace/{Token}/{Empcode}")]

        public IHttpActionResult getmonthlyAttenace(string Token, string Empcode)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber && Token != "Alshifa1122@1122")
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });




                Attendance obj = new Attendance();

                var result = obj.getmonthwiseAttendane(Empcode);

 

                return Json(result);





            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Reason = "error occur in Api",
                });
            }
        }







    }
}
