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
    
    public partial class FLINTEC_Availability_Calc__Overview
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public int Type { get; set; }
        public System.DateTime Date { get; set; }
        public string Item_No_ { get; set; }
        public string Location_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public int Attached_to_Entry_No_ { get; set; }
        public int Level { get; set; }
        public int Source_Type { get; set; }
        public int Source_Order_Status { get; set; }
        public string Source_ID { get; set; }
        public string Source_Batch_Name { get; set; }
        public int Source_Ref__No_ { get; set; }
        public int Source_Prod__Order_Line { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Reserved_Quantity { get; set; }
        public decimal Inventory_Running_Total { get; set; }
        public decimal Supply_Running_Total { get; set; }
        public decimal Demand_Running_Total { get; set; }
        public decimal Running_Total { get; set; }
        public byte Matches_Criteria { get; set; }
    }
}
