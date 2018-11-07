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
    
    public partial class FLINTEC_Whse__Item_Tracking_Line
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public string Item_No_ { get; set; }
        public string Location_Code { get; set; }
        public decimal Quantity__Base_ { get; set; }
        public string Description { get; set; }
        public int Source_Type { get; set; }
        public int Source_Subtype { get; set; }
        public string Source_ID { get; set; }
        public string Source_Batch_Name { get; set; }
        public int Source_Prod__Order_Line { get; set; }
        public int Source_Ref__No_ { get; set; }
        public string Serial_No_ { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public System.DateTime Warranty_Date { get; set; }
        public System.DateTime Expiration_Date { get; set; }
        public decimal Qty__to_Handle__Base_ { get; set; }
        public decimal Qty__to_Invoice__Base_ { get; set; }
        public decimal Quantity_Handled__Base_ { get; set; }
        public decimal Quantity_Invoiced__Base_ { get; set; }
        public decimal Qty__to_Handle { get; set; }
        public int Buffer_Status { get; set; }
        public int Buffer_Status2 { get; set; }
        public string New_Serial_No_ { get; set; }
        public string New_Lot_No_ { get; set; }
        public decimal Qty__Registered__Base_ { get; set; }
        public byte Created_by_Whse__Activity_Line { get; set; }
        public string Lot_No_ { get; set; }
        public string Variant_Code { get; set; }
        public System.DateTime New_Expiration_Date { get; set; }
    }
}