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
    
    public partial class STRAINTEC_Sales_Price
    {
        public byte[] timestamp { get; set; }
        public string Item_No_ { get; set; }
        public int Sales_Type { get; set; }
        public string Sales_Code { get; set; }
        public System.DateTime Starting_Date { get; set; }
        public string Currency_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Minimum_Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public byte Price_Includes_VAT { get; set; }
        public byte Allow_Invoice_Disc_ { get; set; }
        public string VAT_Bus__Posting_Gr___Price_ { get; set; }
        public System.DateTime Ending_Date { get; set; }
        public byte Allow_Line_Disc_ { get; set; }
    }
}
