//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.FixedModel
{
    using System;
    
    public partial class verify_logins_Result
    {
        public int ID { get; set; }
        public string fldLoginName { get; set; }
        public string fldLoginPassword { get; set; }
        public Nullable<System.DateTime> fldLoginCreationDate { get; set; }
        public Nullable<System.DateTime> fldPasswordChangeDate { get; set; }
        public Nullable<int> fldPasswordDuration { get; set; }
        public string fldUserCategoryNo { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> rightId { get; set; }
        public Nullable<int> dept_id { get; set; }
        public Nullable<int> Hosp_id { get; set; }
        public Nullable<int> EmpID { get; set; }
    }
}
