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
    
    public partial class STRAINTEC_Job_Journal_Line
    {
        public byte[] timestamp { get; set; }
        public string Journal_Template_Name { get; set; }
        public string Journal_Batch_Name { get; set; }
        public int Line_No_ { get; set; }
        public string Job_No_ { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public string Document_No_ { get; set; }
        public int Type { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Direct_Unit_Cost__LCY_ { get; set; }
        public decimal Unit_Cost__LCY_ { get; set; }
        public decimal Total_Cost__LCY_ { get; set; }
        public decimal Unit_Price__LCY_ { get; set; }
        public decimal Total_Price__LCY_ { get; set; }
        public string Resource_Group_No_ { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public string Location_Code { get; set; }
        public byte Chargeable { get; set; }
        public string Posting_Group { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Work_Type_Code { get; set; }
        public string Customer_Price_Group { get; set; }
        public int Applies_to_Entry { get; set; }
        public int Entry_Type { get; set; }
        public string Source_Code { get; set; }
        public string Reason_Code { get; set; }
        public int Recurring_Method { get; set; }
        public System.DateTime Expiration_Date { get; set; }
        public string Recurring_Frequency { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public string Country_Region_Code { get; set; }
        public string Entry_Exit_Point { get; set; }
        public System.DateTime Document_Date { get; set; }
        public string External_Document_No_ { get; set; }
        public string Area { get; set; }
        public string Transaction_Specification { get; set; }
        public string Serial_No_ { get; set; }
        public string Posting_No__Series { get; set; }
        public string Source_Currency_Code { get; set; }
        public decimal Source_Currency_Total_Cost { get; set; }
        public decimal Source_Currency_Total_Price { get; set; }
        public decimal Source_Currency_Line_Amount { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Time_Sheet_No_ { get; set; }
        public int Time_Sheet_Line_No_ { get; set; }
        public System.DateTime Time_Sheet_Date { get; set; }
        public string Job_Task_No_ { get; set; }
        public decimal Total_Cost { get; set; }
        public decimal Unit_Price { get; set; }
        public int Line_Type { get; set; }
        public int Applies_from_Entry { get; set; }
        public byte Job_Posting_Only { get; set; }
        public decimal Line_Discount__ { get; set; }
        public decimal Line_Discount_Amount { get; set; }
        public string Currency_Code { get; set; }
        public decimal Line_Amount { get; set; }
        public decimal Currency_Factor { get; set; }
        public decimal Unit_Cost { get; set; }
        public decimal Line_Amount__LCY_ { get; set; }
        public decimal Line_Discount_Amount__LCY_ { get; set; }
        public decimal Total_Price { get; set; }
        public decimal Cost_Factor { get; set; }
        public string Description_2 { get; set; }
        public int Ledger_Entry_Type { get; set; }
        public int Ledger_Entry_No_ { get; set; }
        public int Job_Planning_Line_No_ { get; set; }
        public decimal Remaining_Qty_ { get; set; }
        public decimal Remaining_Qty___Base_ { get; set; }
        public string Variant_Code { get; set; }
        public string Bin_Code { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public decimal Quantity__Base_ { get; set; }
        public string Service_Order_No_ { get; set; }
        public string Posted_Service_Shipment_No_ { get; set; }
        public string Lot_No_ { get; set; }
    }
}