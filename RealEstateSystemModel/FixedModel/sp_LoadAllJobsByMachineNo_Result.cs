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
    
    public partial class sp_LoadAllJobsByMachineNo_Result
    {
        public int id { get; set; }
        public string JobNo { get; set; }
        public Nullable<System.DateTime> jobOpeningDate { get; set; }
        public string JobNature { get; set; }
        public string FaultReport { get; set; }
        public string RepairAction { get; set; }
        public string PartsReplace { get; set; }
        public Nullable<int> RepairExp { get; set; }
        public string jobStatus { get; set; }
        public string jobCompleteBy { get; set; }
        public Nullable<System.DateTime> jobClosingDate { get; set; }
        public string downTime { get; set; }
        public string FileUrl { get; set; }
        public Nullable<bool> status { get; set; }
        public string IP { get; set; }
        public string uid { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public string SerialNo { get; set; }
        public string type { get; set; }
        public Nullable<bool> checkAllProcedure { get; set; }
        public string EngName { get; set; }
    }
}
