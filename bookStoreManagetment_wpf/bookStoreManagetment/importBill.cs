//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bookStoreManagetment
{
    using System;
    using System.Collections.Generic;
    
    public partial class importBill
    {
        public int idImport { get; set; }
        public string billCodeImport { get; set; }
        public int idEmployee { get; set; }
        public string nameEmployee { get; set; }
        public int number { get; set; }
        public System.DateTime importDate { get; set; }
        public string idItem { get; set; }
        public int unitPrice { get; set; }
        public string note { get; set; }
    
        public virtual bill bill { get; set; }
        public virtual item item { get; set; }
    }
}