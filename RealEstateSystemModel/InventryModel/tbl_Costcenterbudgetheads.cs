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
    
    public partial class tbl_Costcenterbudgetheads
    {
        public int ID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> Costcenterid { get; set; }
        public string HeadName { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Uid { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyUid { get; set; }
        public string ModifyIP { get; set; }
        public string Type { get; set; }
        public string ReportCostCenterShow { get; set; }
        public Nullable<int> FinanceCostCenterID { get; set; }
        public string FinanceCostCenterName { get; set; }
        public Nullable<int> SRNO { get; set; }
    }
}