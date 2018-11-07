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
    
    public partial class STRAINTEC_BOM_Buffer
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public int Type { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Location_Code { get; set; }
        public int Replenishment_System { get; set; }
        public int Indentation { get; set; }
        public byte Is_Leaf { get; set; }
        public byte Bottleneck { get; set; }
        public string Routing_No_ { get; set; }
        public string Production_BOM_No_ { get; set; }
        public decimal Lot_Size { get; set; }
        public int Low_Level_Code { get; set; }
        public decimal Rounding_Precision { get; set; }
        public decimal Qty__per_Parent { get; set; }
        public decimal Qty__per_Top_Item { get; set; }
        public decimal Able_to_Make_Top_Item { get; set; }
        public decimal Able_to_Make_Parent { get; set; }
        public decimal Available_Quantity { get; set; }
        public decimal Gross_Requirement { get; set; }
        public decimal Scheduled_Receipts { get; set; }
        public decimal Unused_Quantity { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public string Lead_Time_Offset { get; set; }
        public int Rolled_up_Lead_Time_Offset { get; set; }
        public System.DateTime Needed_by_Date { get; set; }
        public string Safety_Lead_Time { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Indirect_Cost__ { get; set; }
        public decimal Overhead_Rate { get; set; }
        public decimal Scrap__ { get; set; }
        public decimal Scrap_Qty__per_Parent { get; set; }
        public decimal Scrap_Qty__per_Top_Item { get; set; }
        public int Resource_Usage_Type { get; set; }
        public decimal Single_Level_Material_Cost { get; set; }
        public decimal Single_Level_Capacity_Cost { get; set; }
        public decimal Single_Level_Subcontrd__Cost { get; set; }
        public decimal Single_Level_Cap__Ovhd_Cost { get; set; }
        public decimal Single_Level_Mfg__Ovhd_Cost { get; set; }
        public decimal Single_Level_Scrap_Cost { get; set; }
        public decimal Rolled_up_Material_Cost { get; set; }
        public decimal Rolled_up_Capacity_Cost { get; set; }
        public decimal Rolled_up_Subcontracted_Cost { get; set; }
        public decimal Rolled_up_Capacity_Ovhd__Cost { get; set; }
        public decimal Rolled_up_Mfg__Ovhd_Cost { get; set; }
        public decimal Rolled_up_Scrap_Cost { get; set; }
        public decimal Total_Cost { get; set; }
        public string BOM_Unit_of_Measure_Code { get; set; }
        public decimal Qty__per_BOM_Line { get; set; }
    }
}
