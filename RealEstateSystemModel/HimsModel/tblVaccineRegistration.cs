//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.HimsModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblVaccineRegistration
    {
        public int VaccineID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relation { get; set; }
        public string RelativeName { get; set; }
        public bool Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Cnic { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<int> EntryUserID { get; set; }
        public string EntryIP { get; set; }
        public bool IsConfim { get; set; }
        public string VaccineType { get; set; }
        public bool isCancel { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Datevacc { get; set; }
        public Nullable<int> Slot { get; set; }
    }
}
