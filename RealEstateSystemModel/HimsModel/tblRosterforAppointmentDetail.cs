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
    
    public partial class tblRosterforAppointmentDetail
    {
        public int detailid { get; set; }
        public Nullable<int> Masterid { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> RosterDate { get; set; }
        public string Rostertime { get; set; }
        public bool Iscancel { get; set; }
        public bool isBooked { get; set; }
        public string PatientName { get; set; }
        public string PhoneNo { get; set; }
        public string Sourceq { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Cancelid { get; set; }
        public string Cancelip { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> entryid { get; set; }
        public string entryip { get; set; }
        public string Patient_no { get; set; }
        public Nullable<int> CancelidRe { get; set; }
        public string CancelipRe { get; set; }
        public Nullable<System.DateTime> CancelDateRe { get; set; }
    }
}
