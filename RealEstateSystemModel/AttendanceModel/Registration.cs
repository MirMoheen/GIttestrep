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
    
    public partial class Registration
    {
        public long RegID { get; set; }
        public string PFNo { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Type { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public byte[] Picture { get; set; }
        public Nullable<int> PictureLength { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string UserId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string hName { get; set; }
        public Nullable<int> CLeave { get; set; }
        public Nullable<int> ELeave { get; set; }
        public Nullable<bool> Isstd { get; set; }
        public Nullable<int> totalCLeave { get; set; }
        public Nullable<int> totalELeave { get; set; }
        public Nullable<int> doctorid { get; set; }
        public Nullable<int> MachineCode { get; set; }
        public string MacAddress { get; set; }
        public bool IsApp { get; set; }
        public Nullable<int> RoleID { get; set; }
        public bool IsUpdate { get; set; }
        public string MOBILENO { get; set; }
        public string ExtensionNo { get; set; }
        public Nullable<int> DocID { get; set; }
    }
}
