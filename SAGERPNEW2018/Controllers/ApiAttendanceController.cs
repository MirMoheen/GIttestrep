using HRandPayrollSystemModel.AttendanceModel;
using HRandPayrollSystemModel.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAGERPNEW2018.Controllers
{
    public class ApiAttendanceController : ApiController
    {
        string tokenNumber = "Zulqarnain1122@1122";


        [Route("api/ApiAttendance/getroasterdata")]
        [HttpGet]
        public IHttpActionResult getroasterdata(string Token, int doctid, string rosdate, int cty)
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
                var result = obj.GetDoctorRoasterData(doctid, rosdate, cty);


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


        [Route("api/ApiAttendance/Sp_getallspecilt")]
        [HttpGet]
        public IHttpActionResult Sp_getallspecilt(string Token)
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
                var result = obj.Sp_getallspecilt();


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

        [Route("api/ApiAttendance/Sp_getallspeciltdoc")]
        [HttpGet]
        public IHttpActionResult Sp_getallspeciltdoc(string Token , int dep)
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
                var result = obj.Sp_getallspeciltdoc(dep );


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



        [Route("api/ApiAttendance/sp_patientdatahistory")]
        [HttpGet]
        public IHttpActionResult sp_patientdatahistory(string Token, string  dep)
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
                var result = obj.sp_patientdata(dep);


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


        [Route("api/ApiAttendance/Sp_getallspeciltdocall")]
        [HttpGet]
        public IHttpActionResult Sp_getallspeciltdocall(string Token)
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
                var result = obj.Sp_getallspeciltdocall();


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

        [Route("api/ApiAttendance/PostAppdata")]
        [HttpGet]
        public IHttpActionResult PostAppdata(string Token, string patient_no, string patientName, string phoneNo, string sourceq, string remarks, string entryid, string entryip, Int32 detailid)
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
                var result = obj.postappData(patient_no, patientName, phoneNo, sourceq, remarks, entryid, entryip, detailid);


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

        [Route("api/ApiAttendance/getdaysdata")]
        [HttpGet]
        public IHttpActionResult getdaysdata(string Token, int doctid, int cty)
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
                var result = obj.GetDoctordaysData(doctid, cty);


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
        public IHttpActionResult AddAttendance(string Token, string EmployeeCode, string Employestatus)
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
                var dataAttendance = obj.CheckInOutAttendance(EmployeeCode);

                if (dataAttendance != null && dataAttendance.Count>0)
                {

                    var Outdata = dataAttendance.FirstOrDefault(x => x.Status.Trim() == "O");

                    if (Outdata == null)
                    {
                        obj.OutAttendance(EmployeeCode, Employestatus);

                        return Json(new
                        {
                            Success = true,
                            Reason = "Attendance Out ",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = true,
                            Reason = "Already Attendance Out ",
                        });

                    }

                }
                else

                {

                    obj.InAttendance(EmployeeCode, Employestatus);

                    return Json(new
                    {
                        Success = true,
                        Reason = "Attendance In ",
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
        public IHttpActionResult getAttendanceInOut(string Token, string code, string mac)
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
                var result = obj.CheckInOutAttendance(code);
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
        public IHttpActionResult getEmployedata(string Token, string EmpCode )
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
                var result = obj.getEmployedata(EmpCode);
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
        public IHttpActionResult checkAllowDevices (string Token, string mac)
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
                var result = obj.CheckAllowDevices(mac);
                if (result)
                {
                    return Json(new
                    {
                        Success = true,
                        Reason = "success",
                    });

                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Reason = "fail",
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
        public IHttpActionResult getdepartmentbyEmployeeNo(string Token, string Codeemp)
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
                var result = obj.getdepartmentbycode(Codeemp);
                foreach (var item in result)
                {
                    item.Picture = null;
                }

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
        public IHttpActionResult getemployeedataforGraph(string Token, string empcodeof)
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
                var result = obj.getgraphicdataofemplooye(empcodeof);
             

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
        public IHttpActionResult getdepartment(string Token, string employeecodeof)
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


                var hostial = employeecodeof.Substring(0,2);

                Attendance obj = new Attendance();
                var result = obj.getdepartment(hostial);


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
        public IHttpActionResult getDepartmentdataforGraph(string Token, string deptid, string employeecodeof)
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
                var result = obj.getgraphicdataofdeparmtnet(deptid, employeecodeof);


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
        public IHttpActionResult getdepartmentbyID(string Token, string deparmtentid)
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
                var result = obj.getdepartmentbyId(deparmtentid);
               

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
        public IHttpActionResult getsericelog(string Token)
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



                ProjectActivityLog obj = new ProjectActivityLog();
                var result = obj.GetDataForGM();


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
