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
    
    public partial class sp_LoadAllMachineDetail_Result
    {
        public int id { get; set; }
        public string SerialNo { get; set; }
        public string EquipmentName { get; set; }
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }
        public Nullable<System.DateTime> WarrantyEndDate { get; set; }
        public string MachineAmp { get; set; }
        public string PowerCategory { get; set; }
        public Nullable<System.DateTime> MainStartDate { get; set; }
        public Nullable<System.DateTime> MainEndDate { get; set; }
        public string ServiceProvider { get; set; }
        public string EngContact { get; set; }
        public string EngEmail { get; set; }
        public Nullable<System.DateTime> CurrentMainDate { get; set; }
        public string SchedulePeriod { get; set; }
        public Nullable<System.DateTime> NextMainDueDate { get; set; }
    }
}