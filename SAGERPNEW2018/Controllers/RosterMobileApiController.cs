using HRandPayrollSystemModel.HimsModel;
using SAGERPNEW2018.CustomClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAGERPNEW2018.Controllers
{
    public class RosterMobileApiController : ApiController
    {
        webserverEntities db = new webserverEntities();

        [HttpPost]
        public HttpResponseMessage AddRoaster(AddRoasterModel Rm)
        {
            var data = db.tblRosterforAppointmentDetails.Where(x => x.RosterDate == Convert.ToDateTime(Rm.ScDate) && x.DoctorID == Rm.Docid).FirstOrDefault();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "AlreadyCreated");
            }
            try
            {
                int cityNum = 0;
                tblRosterforAppointment tblRosterfor = new tblRosterforAppointment();
                tblRosterfor.Docid = Rm.Docid;
                tblRosterfor.StartTime = Rm.startTime;
                tblRosterfor.MinutePerPatient = Rm.MinutesPerPatient;
                tblRosterfor.TotalPatient = Rm.TotalPatient;
                tblRosterfor.EntryDate = DateTime.Now;
                tblRosterfor.Entryid = null;
                if (Rm.city == "Rawalpindi")
                {
                    cityNum = 1;
                }
                else if (Rm.city == "Westridge Clinic")
                {
                    cityNum = 693;
                }
                else
                {
                    cityNum = 396;
                }
                tblRosterfor.Hospno = "1";
                tblRosterfor.City = cityNum;
                var doctinfo = db.Doctors.Where(x => x.id == Rm.Docid).FirstOrDefault();
                var clinicid = db.cl_ClinicName.Where(x => x.FinDeptID == doctinfo.FinDeptID).FirstOrDefault();
                tblRosterfor.Clinicid = clinicid.Clid;

                db.tblRosterforAppointments.Add(tblRosterfor);
                db.SaveChanges();
                var masterid = db.tblRosterforAppointments.OrderByDescending(x => x.ID).Take(1).FirstOrDefault();

                DateTime roasterdate = Convert.ToDateTime(Rm.ScDate);
                DateTime dt = DateTime.Now;
                TimeSpan intialtime = TimeSpan.Parse(Rm.startTime);
                int indextime = Rm.MinutesPerPatient;
                List<tblRosterforAppointmentDetail> detaillist = new List<tblRosterforAppointmentDetail>();
                for (int i = 0; i < Rm.TotalPatient; i++)
                {
                    tblRosterforAppointmentDetail tbDetail = new tblRosterforAppointmentDetail();
                    tbDetail.DoctorID = Rm.Docid;
                    tbDetail.RosterDate = roasterdate;
                    tbDetail.EntryDate = dt;

                    TimeSpan newtime = new TimeSpan(intialtime.Hours, intialtime.Minutes + indextime, 00);
                    indextime = indextime + Rm.MinutesPerPatient;
                    tbDetail.Rostertime = newtime.Hours.ToString() + ":" + newtime.Minutes.ToString();
                    tbDetail.Iscancel = false;
                    tbDetail.isBooked = false;
                    tbDetail.City = cityNum;
                    tbDetail.Masterid = masterid.ID;
                    db.AddDoctorRoasterDetail(masterid.ID, dt, tbDetail.Rostertime, roasterdate, 0, "172", 0, null, null, cityNum, Rm.Docid);
                    db.SaveChanges();


                }



                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            catch (Exception)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Fail");

            }


        }
        [HttpGet]
        public HttpResponseMessage GetDoctorRoster(int Docid)
        {
            var data = db.tblRosterforAppointmentDetails.Where(x => x.DoctorID == Docid).Select(x => x.RosterDate).Distinct().ToList();
            List<string> datelist = new List<string>();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    datelist.Add(data[i].ToString());
                }
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Fail");

        }
        [HttpGet]
        public HttpResponseMessage GetDoctorSchedule(int Did, string Rosterdate)
        {
            DateTime roasterdate = Convert.ToDateTime(Rosterdate);
            List<tblRosterforAppointmentDetail> tbl = new List<tblRosterforAppointmentDetail>();
            var data = db.tblRosterforAppointmentDetails.Where(x => x.DoctorID == Did && x.RosterDate == roasterdate).ToList();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    tblRosterforAppointmentDetail obj = new tblRosterforAppointmentDetail();
                    DateTime d = DateTime.Parse(data[i].Rostertime);
                    obj.Rostertime = d.ToString("hh:mm tt");
                    obj.RosterDate = data[i].RosterDate;
                    obj.Patient_no = data[i].Patient_no;
                    obj.isBooked = data[i].isBooked;
                    tbl.Add(obj);

                }
                return Request.CreateResponse(HttpStatusCode.OK, tbl);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Nodate");

        }
        [HttpGet]
        public HttpResponseMessage getPatietDetails(string patinetNO)
        {
            var data = db.patient_registration.Where(x => x.Patient_no == patinetNO).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        public HttpResponseMessage getPrivatePractice(int Docid, string Startdate, string Enddate)
        {
            DateTime sdate = Convert.ToDateTime(Startdate);
            DateTime edate = Convert.ToDateTime(Enddate);
            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString.ToString()))
            using (SqlCommand cmd = new SqlCommand("getDoctorPrivatePractice_Api", conn))
            {
                cmd.CommandTimeout = 1500;
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapt.SelectCommand.Parameters.AddWithValue("@from", sdate);
                adapt.SelectCommand.Parameters.AddWithValue("@to", edate);
                adapt.SelectCommand.Parameters.AddWithValue("@DID", Docid.ToString());
                DataTable dt = new DataTable();
                List<Privatepracticedata> ptdatalist = new List<Privatepracticedata>();
                adapt.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Privatepracticedata obj = new Privatepracticedata();
                    obj.Neww = Convert.ToInt32(dt.Rows[i]["New"]);
                    obj.Followup = Convert.ToInt32(dt.Rows[i]["F/U"]);
                    obj.Ptotal = Convert.ToDouble((dt.Rows[i]["P. Tot"])).ToString("N").Remove(Convert.ToDouble((dt.Rows[i]["P. Tot"])).ToString("N").Length - 3);
                    obj.NewAmount = Convert.ToDouble((dt.Rows[i]["New Amt"])).ToString("N").Remove(Convert.ToDouble((dt.Rows[i]["New Amt"])).ToString("N").Length - 3);
                    obj.Followamount = Convert.ToDouble((dt.Rows[i]["F/U Amt"])).ToString("N").Remove(Convert.ToDouble((dt.Rows[i]["F/U Amt"])).ToString("N").Length - 3);
                    obj.AmtTOtal = Convert.ToDouble((dt.Rows[i]["Amt Tot"])).ToString("N").Remove(Convert.ToDouble((dt.Rows[i]["Amt Tot"])).ToString("N").Length - 3);
                    ptdatalist.Add(obj);
                    return Request.CreateResponse(HttpStatusCode.OK, ptdatalist);

                }
            }


            return Request.CreateResponse(HttpStatusCode.OK, "NoRecord");
        }
    }
}
