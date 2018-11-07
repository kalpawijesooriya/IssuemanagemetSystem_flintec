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
    
    public partial class FLINTEC_Stockkeeping_Unit
    {
        public byte[] timestamp { get; set; }
        public string Location_Code { get; set; }
        public string Item_No_ { get; set; }
        public string Variant_Code { get; set; }
        public string Shelf_No_ { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Standard_Cost { get; set; }
        public decimal Last_Direct_Cost { get; set; }
        public string Vendor_No_ { get; set; }
        public string Vendor_Item_No_ { get; set; }
        public string Lead_Time_Calculation { get; set; }
        public decimal Reorder_Point { get; set; }
        public decimal Maximum_Inventory { get; set; }
        public decimal Reorder_Quantity { get; set; }
        public System.DateTime Last_Date_Modified { get; set; }
        public int Assembly_Policy { get; set; }
        public int Transfer_Level_Code { get; set; }
        public decimal Lot_Size { get; set; }
        public int Discrete_Order_Quantity { get; set; }
        public decimal Minimum_Order_Quantity { get; set; }
        public decimal Maximum_Order_Quantity { get; set; }
        public decimal Safety_Stock_Quantity { get; set; }
        public decimal Order_Multiple { get; set; }
        public string Safety_Lead_Time { get; set; }
        public string Components_at_Location { get; set; }
        public int Flushing_Method { get; set; }
        public int Replenishment_System { get; set; }
        public string Time_Bucket { get; set; }
        public int Reordering_Policy { get; set; }
        public byte Include_Inventory { get; set; }
        public int Manufacturing_Policy { get; set; }
        public string Rescheduling_Period { get; set; }
        public string Lot_Accumulation_Period { get; set; }
        public string Dampener_Period { get; set; }
        public decimal Dampener_Quantity { get; set; }
        public decimal Overflow_Level { get; set; }
        public string Transfer_from_Code { get; set; }
        public string Special_Equipment_Code { get; set; }
        public string Put_away_Template_Code { get; set; }
        public string Put_away_Unit_of_Measure_Code { get; set; }
        public string Phys_Invt_Counting_Period_Code { get; set; }
        public System.DateTime Last_Counting_Period_Update { get; set; }
        public string Next_Counting_Period { get; set; }
        public byte Use_Cross_Docking { get; set; }
        public decimal ROL { get; set; }
        public decimal ROQ { get; set; }
    }
}
