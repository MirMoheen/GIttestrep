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
    
    public partial class sp_GetUserRightByUser_Result
    {
        public bool type { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }
        public string UserPassword { get; set; }
        public Nullable<int> GroupID { get; set; }
        public bool Active { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }
        public bool Inactive { get; set; }
        public Nullable<bool> Assign { get; set; }
        public Nullable<bool> IsEdit { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsPrint { get; set; }
        public Nullable<bool> Isnew { get; set; }
        public int Formid { get; set; }
        public string FormTitle { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string imageClass { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<bool> isParent { get; set; }
        public Nullable<int> parentId { get; set; }
    }
}
