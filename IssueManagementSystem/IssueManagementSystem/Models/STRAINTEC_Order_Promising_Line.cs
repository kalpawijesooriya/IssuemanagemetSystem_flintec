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
    
    public partial class STRAINTEC_Order_Promising_Line
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public string Item_No_ { get; set; }
        public string Variant_Code { get; set; }
        public string Location_Code { get; set; }
        public decimal Quantity { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public decimal Unavailable_Quantity { get; set; }
        public decimal Quantity__Base_ { get; set; }
        public decimal Unavailable_Quantity__Base_ { get; set; }
        public decimal Required_Quantity__Base_ { get; set; }
        public int Source_Type { get; set; }
        public int Source_Subtype { get; set; }
        public string Source_ID { get; set; }
        public string Source_Batch_Name { get; set; }
        public int Source_Line_No_ { get; set; }
        public string Description { get; set; }
        public decimal Required_Quantity { get; set; }
        public System.DateTime Requested_Delivery_Date { get; set; }
        public System.DateTime Planned_Delivery_Date { get; set; }
        public System.DateTime Original_Shipment_Date { get; set; }
        public System.DateTime Earliest_Shipment_Date { get; set; }
        public System.DateTime Requested_Shipment_Date { get; set; }
        public System.DateTime Unavailability_Date { get; set; }
    }
}