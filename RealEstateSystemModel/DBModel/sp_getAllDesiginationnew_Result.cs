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
    
    public partial class sp_getAllDesiginationnew_Result
    {
        public int DesignationID { get; set; }
        public string DesignationCode { get; set; }
        public string DesignationTitle { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public bool inactive { get; set; }
        public Nullable<int> OrderNO { get; set; }
        public string ProjectName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}
