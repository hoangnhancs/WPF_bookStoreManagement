//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bookStoreManagetment.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class custommer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public custommer()
        {
            this.itemSummaries = new HashSet<itemSummary>();
            this.profitSummaries = new HashSet<profitSummary>();
        }
    
        public int idCustommer { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string custommerAddress { get; set; }
        public string custommerEmail { get; set; }
        public string sex { get; set; }
        public string citizenIdentification { get; set; }
        public System.DateTime dateOfBirth { get; set; }
        public string nameAccount { get; set; }
        public Nullable<int> accumulatedPoints { get; set; }
        public string custommerNote { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itemSummary> itemSummaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<profitSummary> profitSummaries { get; set; }
    }
}
