//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_kplinfo
    {
        public int KPI_ID { get; set; }
        public string KPI_Code { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string KPIMMYY { get; set; }
        public string Initiatedby { get; set; }
        public string HtmlBox { get; set; }
        public Nullable<int> ForwardTo { get; set; }
        public Nullable<int> EntryID { get; set; }
        public string EntryIP { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public string ModifiedIP { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int Status { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string InitiatedDepartment { get; set; }
        public Nullable<int> ShowInNew { get; set; }
        public string fldStatus { get; set; }
        public bool inactive { get; set; }
        public string LocationProject { get; set; }
        public Nullable<int> Projectid { get; set; }
    }
}
