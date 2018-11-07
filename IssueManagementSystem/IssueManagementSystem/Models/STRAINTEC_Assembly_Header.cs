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
    
    public partial class STRAINTEC_Assembly_Header
    {
        public byte[] timestamp { get; set; }
        public int Document_Type { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public string Search_Description { get; set; }
        public string Description_2 { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public System.DateTime Last_Date_Modified { get; set; }
        public string Item_No_ { get; set; }
        public string Variant_Code { get; set; }
        public string Inventory_Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public string Location_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Due_Date { get; set; }
        public System.DateTime Starting_Date { get; set; }
        public System.DateTime Ending_Date { get; set; }
        public string Bin_Code { get; set; }
        public decimal Quantity { get; set; }
        public decimal Quantity__Base_ { get; set; }
        public decimal Remaining_Quantity { get; set; }
        public decimal Remaining_Quantity__Base_ { get; set; }
        public decimal Assembled_Quantity { get; set; }
        public decimal Assembled_Quantity__Base_ { get; set; }
        public decimal Quantity_to_Assemble { get; set; }
        public decimal Quantity_to_Assemble__Base_ { get; set; }
        public int Planning_Flexibility { get; set; }
        public byte MPS_Order { get; set; }
        public string Posting_No_ { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Cost_Amount { get; set; }
        public decimal Indirect_Cost__ { get; set; }
        public decimal Overhead_Rate { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public string No__Series { get; set; }
        public string Posting_No__Series { get; set; }
        public int Status { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Assigned_User_ID { get; set; }
    }
}
