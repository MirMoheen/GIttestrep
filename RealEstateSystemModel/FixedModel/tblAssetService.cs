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
    
    public partial class tblAssetService
    {
        public long ServiceID { get; set; }
        public string ServiceCode { get; set; }
        public Nullable<int> AssetTypeID { get; set; }
        public Nullable<int> AssetSubTypeID { get; set; }
        public Nullable<int> AssetTagNoID { get; set; }
        public Nullable<System.DateTime> MaintenanceDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceRemarks { get; set; }
        public bool Status { get; set; }
        public string IP { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string ModifiedIP { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}
