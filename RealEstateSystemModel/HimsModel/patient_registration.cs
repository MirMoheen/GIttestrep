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
    
    public partial class patient_registration
    {
        public long id { get; set; }
        public string Patient_no { get; set; }
        public string first_name { get; set; }
        public string mid_name { get; set; }
        public string last_name { get; set; }
        public string relative_name { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public Nullable<System.DateTime> date_of_registration { get; set; }
        public Nullable<System.DateTime> visit_date_time { get; set; }
        public string NIC { get; set; }
        public string Phone_No { get; set; }
        public string Mobile_No { get; set; }
        public Nullable<double> registration_fee { get; set; }
        public Nullable<double> consultation_fee { get; set; }
        public Nullable<double> total_balance { get; set; }
        public Nullable<long> Room_No { get; set; }
        public string relation_ship { get; set; }
        public Nullable<long> city { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<bool> ch_cat { get; set; }
        public Nullable<int> patient_type { get; set; }
        public Nullable<long> category_id { get; set; }
        public string LetterNo { get; set; }
        public Nullable<long> doctor_id { get; set; }
        public Nullable<bool> isb { get; set; }
        public int bit { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<long> UserCityId { get; set; }
        public Nullable<long> uid { get; set; }
        public Nullable<bool> change_cat { get; set; }
        public string ip { get; set; }
        public Nullable<int> Companyid { get; set; }
        public string Email { get; set; }
        public string Refund { get; set; }
        public Nullable<bool> is_fetched { get; set; }
        public Nullable<int> pMode { get; set; }
        public Nullable<double> bankCharges { get; set; }
        public Nullable<int> countryid { get; set; }
        public string passportno { get; set; }
        public Nullable<byte> VISIT_STATUS { get; set; }
        public Nullable<double> DiscountAMT { get; set; }
        public Nullable<int> tempCityId { get; set; }
        public Nullable<int> designation_id { get; set; }
        public string dGroup { get; set; }
        public string RefundType { get; set; }
        public string RelNic { get; set; }
        public string PanelNo { get; set; }
        public Nullable<int> PanelDesignationID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<long> PanelBillingID { get; set; }
        public Nullable<bool> IsPanelClear { get; set; }
        public Nullable<decimal> PanelReceviedAmount { get; set; }
        public Nullable<bool> IsConfirm { get; set; }
        public string IPDBillNo { get; set; }
        public string ImagePath { get; set; }
        public Nullable<bool> IsInfection { get; set; }
        public Nullable<bool> ISNewReg { get; set; }
        public Nullable<bool> IsCnic { get; set; }
        public string SecContactName { get; set; }
        public string SecContactPhone { get; set; }
        public Nullable<int> CDocID { get; set; }
        public Nullable<bool> IsBlackList { get; set; }
        public Nullable<int> Initcategory_id { get; set; }
        public Nullable<int> CClinicID { get; set; }
        public Nullable<bool> IsDead { get; set; }
    }
}
