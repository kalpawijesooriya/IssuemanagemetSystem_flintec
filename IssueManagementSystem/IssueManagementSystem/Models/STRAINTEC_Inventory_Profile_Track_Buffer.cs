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
    
    public partial class STRAINTEC_Inventory_Profile_Track_Buffer
    {
        public byte[] timestamp { get; set; }
        public int Line_No_ { get; set; }
        public int Priority { get; set; }
        public int Demand_Line_No_ { get; set; }
        public int Sequence_No_ { get; set; }
        public int Source_Type { get; set; }
        public string Source_ID { get; set; }
        public decimal Quantity_Tracked { get; set; }
        public int Surplus_Type { get; set; }
        public int Warning_Level { get; set; }
    }
}
