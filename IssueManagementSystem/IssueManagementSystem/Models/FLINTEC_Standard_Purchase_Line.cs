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
    
    public partial class FLINTEC_Standard_Purchase_Line
    {
        public byte[] timestamp { get; set; }
        public string Standard_Purchase_Code { get; set; }
        public int Line_No_ { get; set; }
        public int Type { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount_Excl__VAT { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Variant_Code { get; set; }
        public int Dimension_Set_ID { get; set; }
    }
}