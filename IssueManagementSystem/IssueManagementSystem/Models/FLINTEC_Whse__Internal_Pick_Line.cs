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
    
    public partial class FLINTEC_Whse__Internal_Pick_Line
    {
        public byte[] timestamp { get; set; }
        public string No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Location_Code { get; set; }
        public string Shelf_No_ { get; set; }
        public string To_Bin_Code { get; set; }
        public string To_Zone_Code { get; set; }
        public string Item_No_ { get; set; }
        public decimal Quantity { get; set; }
        public decimal Qty___Base_ { get; set; }
        public decimal Qty__Outstanding { get; set; }
        public decimal Qty__Outstanding__Base_ { get; set; }
        public decimal Qty__Picked { get; set; }
        public decimal Qty__Picked__Base_ { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public string Variant_Code { get; set; }
        public string Description { get; set; }
        public string Description_2 { get; set; }
        public int Status { get; set; }
        public int Sorting_Sequence_No_ { get; set; }
        public System.DateTime Due_Date { get; set; }
        public decimal Cubage { get; set; }
        public decimal Weight { get; set; }
    }
}
