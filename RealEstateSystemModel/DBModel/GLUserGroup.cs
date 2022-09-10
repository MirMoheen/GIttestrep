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
    
    public partial class GLUserGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GLUserGroup()
        {
            this.GLUserGroupDetails = new HashSet<GLUserGroupDetail>();
            this.GLUsers = new HashSet<GLUser>();
        }
    
        public int GroupID { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }
        public bool Inactive { get; set; }
        public string Entryby { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public int UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GLUserGroupDetail> GLUserGroupDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GLUser> GLUsers { get; set; }
    }
}