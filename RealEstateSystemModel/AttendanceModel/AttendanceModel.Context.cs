﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.AttendanceModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AttendenceSystemEntities : DbContext
    {
        public AttendenceSystemEntities()
            : base("name=AttendenceSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AttendanceDeviceConfiguration> AttendanceDeviceConfigurations { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
    
        public virtual ObjectResult<SP_ApiCheckInOutAttendance_Result> SP_ApiCheckInOutAttendance(string empcode)
        {
            var empcodeParameter = empcode != null ?
                new ObjectParameter("empcode", empcode) :
                new ObjectParameter("empcode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ApiCheckInOutAttendance_Result>("SP_ApiCheckInOutAttendance", empcodeParameter);
        }
    
        public virtual ObjectResult<GetNewEmployeesAttendanceDateWiseApi_Result> GetNewEmployeesAttendanceDateWiseApi(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string empId, string dept, string hospNo, Nullable<bool> status, Nullable<bool> isExt)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var empIdParameter = empId != null ?
                new ObjectParameter("EmpId", empId) :
                new ObjectParameter("EmpId", typeof(string));
    
            var deptParameter = dept != null ?
                new ObjectParameter("Dept", dept) :
                new ObjectParameter("Dept", typeof(string));
    
            var hospNoParameter = hospNo != null ?
                new ObjectParameter("HospNo", hospNo) :
                new ObjectParameter("HospNo", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var isExtParameter = isExt.HasValue ?
                new ObjectParameter("isExt", isExt) :
                new ObjectParameter("isExt", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetNewEmployeesAttendanceDateWiseApi_Result>("GetNewEmployeesAttendanceDateWiseApi", fromDateParameter, toDateParameter, empIdParameter, deptParameter, hospNoParameter, statusParameter, isExtParameter);
        }
    
        public virtual ObjectResult<GetEmployeesAttendanceCurrentDateStatusApi_Result> GetEmployeesAttendanceCurrentDateStatusApi(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, string empId, string dept, string hospNo, Nullable<bool> status, Nullable<bool> isExt)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var empIdParameter = empId != null ?
                new ObjectParameter("EmpId", empId) :
                new ObjectParameter("EmpId", typeof(string));
    
            var deptParameter = dept != null ?
                new ObjectParameter("Dept", dept) :
                new ObjectParameter("Dept", typeof(string));
    
            var hospNoParameter = hospNo != null ?
                new ObjectParameter("HospNo", hospNo) :
                new ObjectParameter("HospNo", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var isExtParameter = isExt.HasValue ?
                new ObjectParameter("isExt", isExt) :
                new ObjectParameter("isExt", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeesAttendanceCurrentDateStatusApi_Result>("GetEmployeesAttendanceCurrentDateStatusApi", fromDateParameter, toDateParameter, empIdParameter, deptParameter, hospNoParameter, statusParameter, isExtParameter);
        }
    
        public virtual ObjectResult<sp_getDepartmentforApi_Result> sp_getDepartmentforApi(string hospId)
        {
            var hospIdParameter = hospId != null ?
                new ObjectParameter("HospId", hospId) :
                new ObjectParameter("HospId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getDepartmentforApi_Result>("sp_getDepartmentforApi", hospIdParameter);
        }
    
        public virtual ObjectResult<sp_getemployeebyID_Result> sp_getemployeebyID(string deparmentid, string hospid)
        {
            var deparmentidParameter = deparmentid != null ?
                new ObjectParameter("deparmentid", deparmentid) :
                new ObjectParameter("deparmentid", typeof(string));
    
            var hospidParameter = hospid != null ?
                new ObjectParameter("hospid", hospid) :
                new ObjectParameter("hospid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getemployeebyID_Result>("sp_getemployeebyID", deparmentidParameter, hospidParameter);
        }
    
        public virtual ObjectResult<SP_Apitofetchdata_Result> SP_Apitofetchdata(Nullable<int> doctorID, string rosterDate, Nullable<int> city)
        {
            var doctorIDParameter = doctorID.HasValue ?
                new ObjectParameter("DoctorID", doctorID) :
                new ObjectParameter("DoctorID", typeof(int));
    
            var rosterDateParameter = rosterDate != null ?
                new ObjectParameter("RosterDate", rosterDate) :
                new ObjectParameter("RosterDate", typeof(string));
    
            var cityParameter = city.HasValue ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Apitofetchdata_Result>("SP_Apitofetchdata", doctorIDParameter, rosterDateParameter, cityParameter);
        }
    
        public virtual ObjectResult<SP_Apitofetchdays_Result> SP_Apitofetchdays(Nullable<int> doctorID, Nullable<int> cty)
        {
            var doctorIDParameter = doctorID.HasValue ?
                new ObjectParameter("DoctorID", doctorID) :
                new ObjectParameter("DoctorID", typeof(int));
    
            var ctyParameter = cty.HasValue ?
                new ObjectParameter("cty", cty) :
                new ObjectParameter("cty", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Apitofetchdays_Result>("SP_Apitofetchdays", doctorIDParameter, ctyParameter);
        }
    
        public virtual int SP_ApitoSubmit(string patient_no, string patientName, string phoneNo, string sourceq, string remarks, string entryid, string entryip, Nullable<int> detailid)
        {
            var patient_noParameter = patient_no != null ?
                new ObjectParameter("Patient_no", patient_no) :
                new ObjectParameter("Patient_no", typeof(string));
    
            var patientNameParameter = patientName != null ?
                new ObjectParameter("PatientName", patientName) :
                new ObjectParameter("PatientName", typeof(string));
    
            var phoneNoParameter = phoneNo != null ?
                new ObjectParameter("PhoneNo", phoneNo) :
                new ObjectParameter("PhoneNo", typeof(string));
    
            var sourceqParameter = sourceq != null ?
                new ObjectParameter("Sourceq", sourceq) :
                new ObjectParameter("Sourceq", typeof(string));
    
            var remarksParameter = remarks != null ?
                new ObjectParameter("Remarks", remarks) :
                new ObjectParameter("Remarks", typeof(string));
    
            var entryidParameter = entryid != null ?
                new ObjectParameter("entryid", entryid) :
                new ObjectParameter("entryid", typeof(string));
    
            var entryipParameter = entryip != null ?
                new ObjectParameter("entryip", entryip) :
                new ObjectParameter("entryip", typeof(string));
    
            var detailidParameter = detailid.HasValue ?
                new ObjectParameter("detailid", detailid) :
                new ObjectParameter("detailid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ApitoSubmit", patient_noParameter, patientNameParameter, phoneNoParameter, sourceqParameter, remarksParameter, entryidParameter, entryipParameter, detailidParameter);
        }
    
        public virtual ObjectResult<Sp_getallspeciltyApp_Result> Sp_getallspeciltyApp()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_getallspeciltyApp_Result>("Sp_getallspeciltyApp");
        }
    
        public virtual ObjectResult<Sp_getallspeciltyAppDoctor_Result> Sp_getallspeciltyAppDoctor(Nullable<int> finDeptID)
        {
            var finDeptIDParameter = finDeptID.HasValue ?
                new ObjectParameter("FinDeptID", finDeptID) :
                new ObjectParameter("FinDeptID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_getallspeciltyAppDoctor_Result>("Sp_getallspeciltyAppDoctor", finDeptIDParameter);
        }
    
        public virtual ObjectResult<Sp_getallspeciltyAppDoctorall_Result> Sp_getallspeciltyAppDoctorall()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_getallspeciltyAppDoctorall_Result>("Sp_getallspeciltyAppDoctorall");
        }
    
        public virtual ObjectResult<SP_Apihistory_Result> SP_Apihistory(string phoneNo)
        {
            var phoneNoParameter = phoneNo != null ?
                new ObjectParameter("PhoneNo", phoneNo) :
                new ObjectParameter("PhoneNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Apihistory_Result>("SP_Apihistory", phoneNoParameter);
        }
    }
}