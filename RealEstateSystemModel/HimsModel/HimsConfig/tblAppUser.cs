using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.HimsModel
{
   public partial class tblAppUser
    {


        public tblAppUser registrateruser(tblAppUser obj)
        {
            try
            {
                using (var context = new webserverEntities())
                {
                    context.tblAppUsers.Add(obj);
                    context.SaveChanges();

                    return obj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public decimal  Insert_Camp_Record(Nullable<int> hid, Nullable<int> cid, Nullable<System.DateTime> ddate, Nullable<long> male, Nullable<long> female, Nullable<long> chlidmale, Nullable<long> chlidfemale, Nullable<long> operations, Nullable<long> medicines, Nullable<long> glasses, Nullable<long> uid, string notes, Nullable<long> operAF, Nullable<long> operCM, Nullable<long> operCF, Nullable<bool> isSchoolScreening, Nullable<int> provinceId, Nullable<int> districtId, Nullable<int> cityId, Nullable<long> maleCataract, Nullable<long> femaleCataract, Nullable<long> childMaleCataract, Nullable<long> childFemaleCataract, Nullable<long> maleOtherC, Nullable<long> femaleOtherC, Nullable<long> mChildOtherC, Nullable<long> fChildOtherC, string campSponsor, Nullable<long> regAMT, Nullable<long> donationAMT, Nullable<long> conjunctivitis, Nullable<long> opacity, Nullable<long> pterygium, Nullable<long> squint, Nullable<long> glaucoma, Nullable<long> others)
        {
            decimal a = 0;
            try
            {
                using (var context = new webserverEntities())
                {
                    a= Convert.ToDecimal( context.Insert_Patient_Record( hid, cid, ddate, male,  female, chlidmale,chlidfemale,operations, medicines, glasses, uid, notes, operAF,  operCM, operCF,isSchoolScreening, provinceId,districtId,  cityId,  maleCataract,  femaleCataract,  childMaleCataract,  childFemaleCataract, maleOtherC,  femaleOtherC,mChildOtherC, fChildOtherC, campSponsor,  regAMT,  donationAMT, conjunctivitis, opacity,  pterygium, squint, glaucoma,others).ToList());
                    return a;

                }

            }
            catch (Exception ex)
            {
                return a ;

            }
        }

        public bool alreadyexist(string Cnic)
        {
            try
            {
                using (var context = new webserverEntities())
                {
               var Results=    context.tblAppUsers.FirstOrDefault(x => x.CNIC == Cnic && x.Status == false);
                    if (Results != null)
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

                return false;
            }
        }

        public tblAppUser login(string Cnic, string pasword)
        {
            try
            {
                using (var context = new webserverEntities())
                {
                    var Results = context.tblAppUsers.FirstOrDefault(x => x.CNIC == Cnic && x.Status == false && x.UserPassword== pasword);
                    if (Results != null)
                    {
                        var Random = Guid.NewGuid().ToString("n").Substring(0, 8);
                        Results.Forgetcode = Random;

                        context.SaveChanges();

                        return Results;
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



        public static void Emailsend(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("FromMailAddress");
                message.To.Add(new MailAddress("ToMailAddress"));
                message.Subject = "verify Code";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }




        public tblAppUser  forgetpassword( string emailadress)
        {
            try
            {
                using (var context = new webserverEntities())
                {
                    var Results = context.tblAppUsers.FirstOrDefault(x => x.Email == emailadress && x.Status == false);
                    if (Results != null)
                    {
                        return Results;


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



    }
}
