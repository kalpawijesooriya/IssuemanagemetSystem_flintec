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
    
    public partial class FLINTEC_Standard_Cost_Breakdown
    {
        public byte[] timestamp { get; set; }
        public string No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Item_No_ { get; set; }
        public System.DateTime Last_Unit_Cost_Calc__Date { get; set; }
        public int Version { get; set; }
        public decimal Quantity_Per { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Amount { get; set; }
        public string Comp__Unit_of_Measure { get; set; }
        public string Base_Unit_of_Measure { get; set; }
        public decimal Quantity_Per__Base_ { get; set; }
        public System.DateTime Created_Date { get; set; }
    }
}