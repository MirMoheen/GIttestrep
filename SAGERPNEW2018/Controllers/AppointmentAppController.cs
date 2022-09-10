using HRandPayrollSystemModel.HimsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace SAGERPNEW2018.Controllers
{
    public class AppointmentAppController : ApiController
    {

        string tokenNumber = "(Alshifa@356trust@Pak!";

        [HttpGet]
        public IHttpActionResult UserRegistation(string Token, string password, string fname, string lname,string cnic, string phone,string email , string mac ,string ip)
        {
            try
            {

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber )
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });



                tblAppUser obj = new tblAppUser();
                var resultchek = obj.alreadyexist(cnic.Trim());
                if (resultchek)
                {
                    return Json(new
                    {
                        Success = false,
                        Reason = "User Already Exist",
                    });
                }

                obj.FirstName = fname;
                obj.LastName = lname;
                obj.UserPassword = password;
                obj.CNIC = cnic;
                obj.Phone = phone;
                obj.Email = email;
                obj.Mac = mac;
                obj.EntryIp = ip;
                obj.Entrydate = DateTime.Now;

                var regresuls=  obj.registrateruser(obj);
                if (regresuls != null)
                {
                    return Json(new
                    {
                        Success = true,
                        Reason = "Successfully Register",
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Reason = "User failed ",
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
       [Route("api/loginApp/{Token}/{name}/{password}")]
        public IHttpActionResult loginuser(string Token, string name, string password )
        {
            try
            {
                var asdasd = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(tokenNumber)));

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });


                tblAppUser obj = new tblAppUser();
                var resultchek = obj.login(name.Trim(), password.Trim());
                if (resultchek != null)
                {
                    return Json(resultchek);
                }
                else
                {

                    return Json(new tblAppUser()); 

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
        [Route("api/campdatasave")]
        public IHttpActionResult campdatasave(string Token, string hid, string cid, string ddate, string male, string female, string chlidmale, string chlidfemale, string operations, string medicines, string glasses, string uid, string notes, string operAF, string operCM, string operCF, string isSchoolScreening, string provinceId, string districtId, string cityId, string maleCataract, string femaleCataract, string childMaleCataract, string childFemaleCataract, string maleOtherC, string femaleOtherC, string mChildOtherC, string fChildOtherC, string campSponsor, string regAMT, string donationAMT, string conjunctivitis, string opacity, string pterygium, string squint, string glaucoma, string others)
        {
            try
            {
                var asdasd = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(tokenNumber)));

                if (Token == null)
                    return Json(new
                    {
                        Success = false,
                        Reason = "no Token supplied",
                    });
                if (Token != tokenNumber)
                    return Json(new
                    {
                        Success = false,
                        Reason = "invalid Token supplied",
                    });


                tblAppUser obj = new tblAppUser();
                var resultchek = obj.Insert_Camp_Record(Convert.ToInt32(hid), Convert.ToInt32(cid), Convert.ToDateTime(ddate), Convert.ToInt32(male), Convert.ToInt32(female)
                    , Convert.ToInt32(chlidmale), Convert.ToInt32(chlidfemale), Convert.ToInt32(operations), Convert.ToInt32(medicines), Convert.ToInt32(glasses), Convert.ToInt32(uid),
                    notes, Convert.ToInt32(operAF), Convert.ToInt32(operCM), Convert.ToInt32(operCF), Convert.ToBoolean(isSchoolScreening), Convert.ToInt32(provinceId), Convert.ToInt32(districtId),
                    Convert.ToInt32(cityId), Convert.ToInt32(maleCataract), Convert.ToInt32(femaleCataract), Convert.ToInt32(childMaleCataract), Convert.ToInt32(childFemaleCataract),
                   Convert.ToInt32(maleOtherC), Convert.ToInt32(femaleOtherC), Convert.ToInt32(mChildOtherC), Convert.ToInt32(fChildOtherC), campSponsor, Convert.ToInt32(regAMT), Convert.ToInt32(donationAMT),
                    Convert.ToInt32(conjunctivitis), Convert.ToInt32(opacity), Convert.ToInt32(pterygium), Convert.ToInt32(squint), Convert.ToInt32(glaucoma), Convert.ToInt32(others));
                if (resultchek != null)
                {
                    return Json(resultchek);
                }
                else
                {

                    return Json(new tblAppUser());

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



        //[HttpGet]
        //[Route("api/campdatasave")]
        //public IHttpActionResult campdatasave(string Token, string hid)
        //{
        //    try
        //    {
        //        var asdasd = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(tokenNumber)));

        //        if (Token == null)
        //            return Json(new
        //            {
        //                Success = false,
        //                Reason = "no Token supplied",
        //            });
        //        if (Token != tokenNumber)
        //            return Json(new
        //            {
        //                Success = false,
        //                Reason = "invalid Token supplied",
        //            });

        //        string resultchek = "ahsbjksbhlajbhflasn";

        //        if (resultchek != null)
        //        {
        //            return Json(resultchek);
        //        }
        //        else
        //        {

        //            return Json(new tblAppUser());

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new
        //        {
        //            Success = false,
        //            Reason = "error occur in Api",
        //        });
        //    }
        //}



    }
}
