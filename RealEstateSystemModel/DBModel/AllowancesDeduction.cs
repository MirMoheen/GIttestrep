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
    
    public partial class AllowancesDeduction
    {
        public int AllowanceDeductionID { get; set; }
        public string AllowanceDeductionTitle { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public bool Inactive { get; set; }
        public Nullable<int> EntryID { get; set; }
        public string EntryIP { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public string ModifiedIP { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeduction { get; set; }
        public string Remarks { get; set; }
        public bool IsBasicSalary { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<int> ProjectID { get; set; }
    }
}
