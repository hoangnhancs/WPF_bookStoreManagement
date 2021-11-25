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
    
    public partial class item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item()
        {
            this.bookInformations = new HashSet<bookInformation>();
            this.checkItems = new HashSet<checkItem>();
            this.importBills = new HashSet<importBill>();
            this.itemSummaries = new HashSet<itemSummary>();
            this.sellBills = new HashSet<sellBill>();
            this.studytoolsInformations = new HashSet<studytoolsInformation>();
        }
    
        public string idItem { get; set; }
        public string linkItem { get; set; }
        public string imageItem { get; set; }
        public string nameItem { get; set; }
        public int priceItem { get; set; }
        public string descriptionItem { get; set; }
        public string barcode { get; set; }
        public int quantity { get; set; }
        public string typeItem { get; set; }
        public string supplierItem { get; set; }
        public string unit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookInformation> bookInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<checkItem> checkItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<importBill> importBills { get; set; }
        public virtual supplier supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itemSummary> itemSummaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellBill> sellBills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<studytoolsInformation> studytoolsInformations { get; set; }
    }
}
