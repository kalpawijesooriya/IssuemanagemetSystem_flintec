//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IssueManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FLINTEC_Bin_Content
    {
        public byte[] timestamp { get; set; }
        public string Location_Code { get; set; }
        public string Bin_Code { get; set; }
        public string Item_No_ { get; set; }
        public string Variant_Code { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public string Zone_Code { get; set; }
        public string Bin_Type_Code { get; set; }
        public string Warehouse_Class_Code { get; set; }
        public int Block_Movement { get; set; }
        public decimal Min__Qty_ { get; set; }
        public decimal Max__Qty_ { get; set; }
        public int Bin_Ranking { get; set; }
        public byte Fixed { get; set; }
        public byte Cross_Dock_Bin { get; set; }
        public byte Default { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public byte Dedicated { get; set; }
    }
}
