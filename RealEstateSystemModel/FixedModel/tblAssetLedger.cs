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
    using System.Collections.Generic;
    
    public partial class tblAssetLedger
    {
        public long id { get; set; }
        public Nullable<long> AssetId { get; set; }
        public string AssetNo { get; set; }
        public Nullable<int> FinancialYear { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<decimal> depriciatedAMT { get; set; }
        public string EntryType { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> enterBy { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public string entryIP { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
