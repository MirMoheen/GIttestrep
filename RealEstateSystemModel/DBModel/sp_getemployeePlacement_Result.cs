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
    
    public partial class sp_getemployeePlacement_Result
    {
        public int PlacementID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> DesiginationID { get; set; }
        public Nullable<System.DateTime> PlacemantDate { get; set; }
        public Nullable<decimal> GrossSalary { get; set; }
        public Nullable<int> BankID { get; set; }
        public string BankAccountTitle { get; set; }
        public string BankAccountNo { get; set; }
        public Nullable<decimal> TaxRebait { get; set; }
        public bool Inactive { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCNIC { get; set; }
        public string EmployeeTypeName { get; set; }
        public string Color { get; set; }
    }
}