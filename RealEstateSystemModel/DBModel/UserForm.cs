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
    
    public partial class UserForm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserForm()
        {
            this.GLUserGroupDetails = new HashSet<GLUserGroupDetail>();
        }
    
        public int Formid { get; set; }
        public string FormTitle { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string imageClass { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<bool> isParent { get; set; }
        public Nullable<int> parentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GLUserGroupDetail> GLUserGroupDetails { get; set; }
    }
}