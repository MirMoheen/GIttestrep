using HRandPayrollSystemModel.AttendanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRandPayrollSystemModel.AttendanceModel
{ 
  public partial  class Attendance
    {


        public Registration getlogin(string name , string code, string macadrr )
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    var result= context.Registrations.FirstOrDefault(x => x.PFNo == code  && x.MacAddress== macadrr);
                    if (result != null)
                    {
                        return result;
                    }
                    else {
                        return new Registration();
                    }


                }
            }
            catch (Exception ex)
            {

                return new Registration();
            }
        }



        public Registration Checkregister(string name, string code, string mobile, string macadrr)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {




                    var result = context.Registrations.FirstOrDefault(x=> x.MacAddress == macadrr);

                    if (result != null)
                    {
                        return result;

                    }
                    else
                    {
                        return new Registration();


                    }

                }
            }
            catch (Exception ex)
            {
                return new Registration();
            }
        }




        public Registration registerlogin(string name, string code,string mobile, string macadrr)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {



                    var result= context.Registrations.FirstOrDefault(x => x.PFNo == code && x.MacAddress==null && x.IsApp==false );

                    if (result != null  )
                    {
                    
                            result.MacAddress = macadrr;
                            context.SaveChanges();
                            return result;

                        
                    }
                    else
                    {
                        return new Registration();

                    }

                }
            }
            catch (Exception ex)
            {
                return new Registration();
            }
        }




        public bool InAttendance ( string code, string macadrr)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {

                    Attendance obj = new Attendance();

                    obj.EmployeeNo = code;
                    obj.Status = "I";
                    obj.EntryTime = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    obj.HostName = macadrr;

                   context.Attendances.Add(obj);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }






        public bool OutAttendance(string code, string macadrr)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {

                    Attendance obj = new Attendance();

                    obj.EmployeeNo = code;
                    obj.Status = "O";
                    obj.EntryTime = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    obj.HostName = macadrr;

                    context.Attendances.Add(obj);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public List<SP_Apitofetchdata_Result> GetDoctorRoasterData(int doctid, string rosdate, int cty)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    return context.SP_Apitofetchdata(doctid, rosdate, cty).ToList();



                }
            }
            catch (Exception ex)
            {

                return new List<SP_Apitofetchdata_Result>();
            }
        }

        public List<SP_Apitofetchdays_Result> GetDoctordaysData(int doctid, int cty)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    return context.SP_Apitofetchdays(doctid, cty).ToList();



                }
            }
            catch (Exception ex)
            {

                return new List<SP_Apitofetchdays_Result>();
            }
        }


        public string  postappData(string patient_no, string patientName, string phoneNo, string  sourceq, string remarks, string entryid, string entryip, Int32 detailid)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    return  context.SP_ApitoSubmit(patient_no,patientName,phoneNo,sourceq,remarks,entryid,entryip, detailid).ToString();



                }
            }
            catch (Exception ex)
            {

                return "ERR::ON::API::TO__::SAVE?SUBMIT";
            }
        }


        public List< SP_ApiCheckInOutAttendance_Result> CheckInOutAttendance(string code)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                   return context.SP_ApiCheckInOutAttendance(code).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<SP_ApiCheckInOutAttendance_Result>() ;

            }
        }

        public List<Sp_getallspeciltyApp_Result> Sp_getallspecilt()
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                    return context.Sp_getallspeciltyApp().ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Sp_getallspeciltyApp_Result>();

            }
        }

        public List<Sp_getallspeciltyAppDoctor_Result> Sp_getallspeciltdoc( int dep)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                    return context.Sp_getallspeciltyAppDoctor(dep).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Sp_getallspeciltyAppDoctor_Result>();

            }
        }
        public List<SP_Apihistory_Result> sp_patientdata(string dep)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                    return context.SP_Apihistory(dep).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<SP_Apihistory_Result>();

            }
        }

        public List<Sp_getallspeciltyAppDoctorall_Result> Sp_getallspeciltdocall()
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                    return context.Sp_getallspeciltyAppDoctorall().ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Sp_getallspeciltyAppDoctorall_Result>();

            }
        }

        public Registration getEmployedata(string code)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    var result = context.Registrations.FirstOrDefault(x => x.PFNo == code);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        return new Registration();
                    }


                }
            }
            catch (Exception ex)
            {

                return new Registration();
            }
        }


        public List< Registration> getdepartmentbycode(string code)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {



                    var result = context.Registrations.FirstOrDefault(x => x.PFNo == code && x.StatusId==0);

                    if (result != null)
                    {
                        return context.Registrations.Where(x => x.Department == result.Department  && x.StatusId==0).ToList();

                    }
                    else
                    {
                        return new List<Registration>();
                    }

                }
            }
            catch (Exception ex)
            {
                return new List<Registration>();
            }
        }






        public List<GetNewEmployeesAttendanceDateWiseApi_Result> getmonthwiseAttendane(string code)
        {
            try
            {
              DateTime datefrom=  new DateTime( DateTime.Now.Year, DateTime.Now.Month,1);
                //     DateTime dateto = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                DateTime dateto = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);



                using (var context = new AttendenceSystemEntities())
                {
                    return context.GetNewEmployeesAttendanceDateWiseApi(datefrom, dateto, code+",z","0","01",true,false).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<GetNewEmployeesAttendanceDateWiseApi_Result>();

            }
        }






        public bool CheckAllowDevices(string code)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    var result = context.AttendanceDeviceConfigurations.FirstOrDefault(x => x.Mac == code  && x.Status==false) ;
                    if (result != null)
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






        public List<sp_getDepartmentforApi_Result> getdepartment(string Hosp)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                   return context.sp_getDepartmentforApi(Hosp).ToList();
                   


                }
            }
            catch (Exception ex)
            {

                return new List<sp_getDepartmentforApi_Result>();
            }
        }







        public List<GetEmployeesAttendanceCurrentDateStatusApi_Result> getgraphicdataofemplooye(string empcodeof)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {


                    DateTime dateto = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    var empdata =  context.Registrations.FirstOrDefault(x => x.PFNo == empcodeof);

                   var departmentid= empcodeof.Substring(2, 2);

                    if (empdata != null)
                    {

                        if (empdata.RoleID == 2)
                        {



                            return context.GetEmployeesAttendanceCurrentDateStatusApi(dateto, dateto, "+,z", departmentid, "01", true, false).ToList();


                        }
                        else if (empdata.RoleID == 3)
                        {
                            return context.GetEmployeesAttendanceCurrentDateStatusApi(dateto, dateto, "+,z", "+", "01", true, false).ToList();


                        }
                        else
                        { 
                        
                       return new List<GetEmployeesAttendanceCurrentDateStatusApi_Result>();


                        }

                    }
                    else
                    {

                        return new List<GetEmployeesAttendanceCurrentDateStatusApi_Result>();
                    }




                   

                }
            }
            catch (Exception ex)
            {

                return new List<GetEmployeesAttendanceCurrentDateStatusApi_Result>();
            }
        }






        public List<GetEmployeesAttendanceCurrentDateStatusApi_Result> getgraphicdataofdeparmtnet(string departmentid, string empcode)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {
                    var hosid = empcode.Substring(0, 2);

                    DateTime dateto = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                  

                    return context.GetEmployeesAttendanceCurrentDateStatusApi(dateto, dateto, "+,z", departmentid, hosid, true, false).ToList();




                }
            }
            catch (Exception ex)
            {

                return new List<GetEmployeesAttendanceCurrentDateStatusApi_Result>();
            }
        }



        public List<sp_getemployeebyID_Result> getdepartmentbyId(string code)
        {
            try
            {
                using (var context = new AttendenceSystemEntities())
                {

              
                        return context.sp_getemployeebyID(code,"01").ToList();

                }
            }
            catch (Exception ex)
            {
                return new List<sp_getemployeebyID_Result>();
            }
        }




    }



}
