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
    
    public partial class STRAINTEC_Return_Receipt_Line
    {
        public byte[] timestamp { get; set; }
        public string Document_No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Sell_to_Customer_No_ { get; set; }
        public int Type { get; set; }
        public string No_ { get; set; }
        public string Location_Code { get; set; }
        public string Posting_Group { get; set; }
        public System.DateTime Shipment_Date { get; set; }
        public string Description { get; set; }
        public string Description_2 { get; set; }
        public string Unit_of_Measure { get; set; }
        public decimal Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Unit_Cost__LCY_ { get; set; }
        public decimal VAT__ { get; set; }
        public decimal Line_Discount__ { get; set; }
        public byte Allow_Invoice_Disc_ { get; set; }
        public decimal Gross_Weight { get; set; }
        public decimal Net_Weight { get; set; }
        public decimal Units_per_Parcel { get; set; }
        public decimal Unit_Volume { get; set; }
        public int Appl__to_Item_Entry { get; set; }
        public int Item_Rcpt__Entry_No_ { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Customer_Price_Group { get; set; }
        public string Job_No_ { get; set; }
        public string Work_Type_Code { get; set; }
        public decimal Quantity_Invoiced { get; set; }
        public string Bill_to_Customer_No_ { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public int VAT_Calculation_Type { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public int Attached_to_Line_No_ { get; set; }
        public string Exit_Point { get; set; }
        public string Area { get; set; }
        public string Transaction_Specification { get; set; }
        public string Tax_Area_Code { get; set; }
        public byte Tax_Liable { get; set; }
        public string Tax_Group_Code { get; set; }
        public string VAT_Bus__Posting_Group { get; set; }
        public string VAT_Prod__Posting_Group { get; set; }
        public string Blanket_Order_No_ { get; set; }
        public int Blanket_Order_Line_No_ { get; set; }
        public decimal VAT_Base_Amount { get; set; }
        public decimal Unit_Cost { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Variant_Code { get; set; }
        public string Bin_Code { get; set; }
        public decimal Qty__per_Unit_of_Measure { get; set; }
        public string Unit_of_Measure_Code { get; set; }
        public decimal Quantity__Base_ { get; set; }
        public decimal Qty__Invoiced__Base_ { get; set; }
        public System.DateTime FA_Posting_Date { get; set; }
        public string Depreciation_Book_Code { get; set; }
        public byte Depr__until_FA_Posting_Date { get; set; }
        public string Duplicate_in_Depreciation_Book { get; set; }
        public byte Use_Duplication_List { get; set; }
        public string Responsibility_Center { get; set; }
        public string Cross_Reference_No_ { get; set; }
        public string Unit_of_Measure__Cross_Ref__ { get; set; }
        public int Cross_Reference_Type { get; set; }
        public string Cross_Reference_Type_No_ { get; set; }
        public string Item_Category_Code { get; set; }
        public byte Nonstock { get; set; }
        public string Purchasing_Code { get; set; }
        public string Product_Group_Code { get; set; }
        public decimal Return_Qty__Rcd__Not_Invd_ { get; set; }
        public int Appl__from_Item_Entry { get; set; }
        public decimal Item_Charge_Base_Amount { get; set; }
        public byte Correction { get; set; }
        public string Return_Order_No_ { get; set; }
        public int Return_Order_Line_No_ { get; set; }
        public string Return_Reason_Code { get; set; }
        public byte Allow_Line_Disc_ { get; set; }
        public string Customer_Disc__Group { get; set; }
        public string Shipment_Method_Code { get; set; }
        public int GSP { get; set; }
        public string Sales_Reason_Code { get; set; }
        public string Sales_Comment_Line { get; set; }
        public byte Order_Changed { get; set; }
        public string Shipment_Status { get; set; }
        public string Customer_Order_No_ { get; set; }
    }
}