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
    
    public partial class FLINTEC_Sales_Line_Discount
    {
        public byte[] timestamp { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public int Sales_Type { get; set; }
        public string Sales_Code { get; set; }
        public System.DateTime Starting_Date { get; set; }
        public string Currency_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Minimum_Quantity { get; set; }
        public decimal Line_Discount__ { get; set; }
        public System.DateTime Ending_Date { get; set; }
    }
}
