//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.InventryModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Costcenterbudgetheadamount
    {
        public int ID { get; set; }
        public Nullable<int> CostcenterID { get; set; }
        public Nullable<int> HeadID { get; set; }
        public Nullable<int> projectid { get; set; }
        public string FY { get; set; }
        public Nullable<decimal> BudgetPro { get; set; }
        public Nullable<decimal> Expense { get; set; }
        public Nullable<bool> IsClose { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Uid { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyIP { get; set; }
        public string ModifyUid { get; set; }
        public Nullable<decimal> Expenseopening { get; set; }
    }
}