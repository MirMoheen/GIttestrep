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
    
    public partial class tblProject
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool Inactive { get; set; }
        public Nullable<int> EntryID { get; set; }
        public string EntryIP { get; set; }
        public Nullable<int> ModifiedID { get; set; }
        public string ModifiedIP { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<System.DateTime> EnrtyDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProjectCode { get; set; }
    }
}