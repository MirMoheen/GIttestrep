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
    
    public partial class tbl_BoardProceedingDOC
    {
        public int MinuteSheetDocID { get; set; }
        public string MinuteSheetCode { get; set; }
        public string Description { get; set; }
        public string PathDoc { get; set; }
        public bool Inative { get; set; }
        public string Ip { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}