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
    
    public partial class FLINTEC_Put_away_Template_Line
    {
        public byte[] timestamp { get; set; }
        public string Put_away_Template_Code { get; set; }
        public int Line_No_ { get; set; }
        public string Description { get; set; }
        public byte Find_Fixed_Bin { get; set; }
        public byte Find_Floating_Bin { get; set; }
        public byte Find_Same_Item { get; set; }
        public byte Find_Unit_of_Measure_Match { get; set; }
        public byte Find_Bin_w__Less_than_Min__Qty { get; set; }
        public byte Find_Empty_Bin { get; set; }
    }
}