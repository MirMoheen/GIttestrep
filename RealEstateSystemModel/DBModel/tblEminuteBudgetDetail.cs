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
    
    public partial class tblEminuteBudgetDetail
    {
        public int BudgetdetailID { get; set; }
        public string EminuteCode { get; set; }
        public Nullable<int> BudgetHeadID { get; set; }
        public Nullable<int> BudgetSubID { get; set; }
        public Nullable<int> FinancalYear { get; set; }
        public Nullable<decimal> BudgetPro { get; set; }
        public Nullable<decimal> AlreadyExpense { get; set; }
        public Nullable<decimal> ThisExpense { get; set; }
        public Nullable<decimal> TotalExpense { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public bool Inactive { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string IP { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> BudgetProjectID { get; set; }
    }
}
