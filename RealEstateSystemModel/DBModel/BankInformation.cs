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
    
    public partial class BankInformation
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public bool inactive { get; set; }
        public Nullable<int> EntryID { get; set; }
        public string EntryIP { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public string ModifiedIP { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
