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
    
    public partial class FLINTEC_Service_Line_Price_Adjmt_
    {
        public byte[] timestamp { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public int Service_Item_Line_No_ { get; set; }
        public int Service_Line_No_ { get; set; }
        public string Service_Item_No_ { get; set; }
        public string Serv__Price_Adjmt__Gr__Code { get; set; }
        public int Type { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal New_Amount { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal New_Unit_Price { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Discount__ { get; set; }
        public decimal Discount_Amount { get; set; }
        public decimal Amount_incl__VAT { get; set; }
        public decimal New_Amount_incl__VAT { get; set; }
        public decimal Weight { get; set; }
        public int Adjustment_Type { get; set; }
        public string Service_Price_Group_Code { get; set; }
        public byte Manually_Adjusted { get; set; }
        public decimal Vat__ { get; set; }
        public decimal New_Amount_Excl__VAT { get; set; }
    }
}