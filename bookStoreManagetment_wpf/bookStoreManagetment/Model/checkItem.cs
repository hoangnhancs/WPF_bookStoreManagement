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
    
    public partial class checkItem
    {
        public int sttCheckItems { get; set; }
        public string idCheckItems { get; set; }
        public string idEmployee { get; set; }
        public System.DateTime dateCheckItems { get; set; }
        public string idItem { get; set; }
        public string note { get; set; }
        public Nullable<int> newQuantityItem { get; set; }
        public Nullable<int> oldQuantityItem { get; set; }
    }
}
