//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public long dbid { get; set; }
        public string EmployeeNo { get; set; }
        public string Status { get; set; }
        public Nullable<byte> FingerIndex { get; set; }
        public Nullable<byte> Score { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public string HostName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
