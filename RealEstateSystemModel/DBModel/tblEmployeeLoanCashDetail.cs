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
    
    public partial class tblEmployeeLoanCashDetail
    {
        public int LoanDetailID { get; set; }
        public Nullable<int> LoanID { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string LoanDetailDate { get; set; }
        public Nullable<int> EntryID { get; set; }
        public string EntryIP { get; set; }
    }
}
