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
    
    public partial class STRAINTEC_Gen__Journal_Line
    {
        public byte[] timestamp { get; set; }
        public string Journal_Template_Name { get; set; }
        public string Journal_Batch_Name { get; set; }
        public int Line_No_ { get; set; }
        public int Account_Type { get; set; }
        public string Account_No_ { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public string Description { get; set; }
        public decimal VAT__ { get; set; }
        public string Bal__Account_No_ { get; set; }
        public string Currency_Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit_Amount { get; set; }
        public decimal Credit_Amount { get; set; }
        public decimal Amount__LCY_ { get; set; }
        public decimal Balance__LCY_ { get; set; }
        public decimal Currency_Factor { get; set; }
        public decimal Sales_Purch___LCY_ { get; set; }
        public decimal Profit__LCY_ { get; set; }
        public decimal Inv__Discount__LCY_ { get; set; }
        public string Bill_to_Pay_to_No_ { get; set; }
        public string Posting_Group { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Salespers__Purch__Code { get; set; }
        public string Source_Code { get; set; }
        public byte System_Created_Entry { get; set; }
        public string On_Hold { get; set; }
        public int Applies_to_Doc__Type { get; set; }
        public string Applies_to_Doc__No_ { get; set; }
        public System.DateTime Due_Date { get; set; }
        public System.DateTime Pmt__Discount_Date { get; set; }
        public decimal Payment_Discount__ { get; set; }
        public string Job_No_ { get; set; }
        public decimal Quantity { get; set; }
        public decimal VAT_Amount { get; set; }
        public int VAT_Posting { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Applies_to_ID { get; set; }
        public string Business_Unit_Code { get; set; }
        public string Reason_Code { get; set; }
        public int Recurring_Method { get; set; }
        public System.DateTime Expiration_Date { get; set; }
        public string Recurring_Frequency { get; set; }
        public int Gen__Posting_Type { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public int VAT_Calculation_Type { get; set; }
        public byte EU_3_Party_Trade { get; set; }
        public byte Allow_Application { get; set; }
        public int Bal__Account_Type { get; set; }
        public int Bal__Gen__Posting_Type { get; set; }
        public string Bal__Gen__Bus__Posting_Group { get; set; }
        public string Bal__Gen__Prod__Posting_Group { get; set; }
        public int Bal__VAT_Calculation_Type { get; set; }
        public decimal Bal__VAT__ { get; set; }
        public decimal Bal__VAT_Amount { get; set; }
        public int Bank_Payment_Type { get; set; }
        public decimal VAT_Base_Amount { get; set; }
        public decimal Bal__VAT_Base_Amount { get; set; }
        public byte Correction { get; set; }
        public byte Check_Printed { get; set; }
        public System.DateTime Document_Date { get; set; }
        public string External_Document_No_ { get; set; }
        public int Source_Type { get; set; }
        public string Source_No_ { get; set; }
        public string Posting_No__Series { get; set; }
        public string Tax_Area_Code { get; set; }
        public byte Tax_Liable { get; set; }
        public string Tax_Group_Code { get; set; }
        public byte Use_Tax { get; set; }
        public string Bal__Tax_Area_Code { get; set; }
        public byte Bal__Tax_Liable { get; set; }
        public string Bal__Tax_Group_Code { get; set; }
        public byte Bal__Use_Tax { get; set; }
        public string VAT_Bus__Posting_Group { get; set; }
        public string VAT_Prod__Posting_Group { get; set; }
        public string Bal__VAT_Bus__Posting_Group { get; set; }
        public string Bal__VAT_Prod__Posting_Group { get; set; }
        public int Additional_Currency_Posting { get; set; }
        public decimal FA_Add__Currency_Factor { get; set; }
        public string Source_Currency_Code { get; set; }
        public decimal Source_Currency_Amount { get; set; }
        public decimal Source_Curr__VAT_Base_Amount { get; set; }
        public decimal Source_Curr__VAT_Amount { get; set; }
        public decimal VAT_Base_Discount__ { get; set; }
        public decimal VAT_Amount__LCY_ { get; set; }
        public decimal VAT_Base_Amount__LCY_ { get; set; }
        public decimal Bal__VAT_Amount__LCY_ { get; set; }
        public decimal Bal__VAT_Base_Amount__LCY_ { get; set; }
        public byte Reversing_Entry { get; set; }
        public byte Allow_Zero_Amount_Posting { get; set; }
        public string Ship_to_Order_Address_Code { get; set; }
        public decimal VAT_Difference { get; set; }
        public decimal Bal__VAT_Difference { get; set; }
        public string IC_Partner_Code { get; set; }
        public int IC_Direction { get; set; }
        public string IC_Partner_G_L_Acc__No_ { get; set; }
        public int IC_Partner_Transaction_No_ { get; set; }
        public string Sell_to_Buy_from_No_ { get; set; }
        public string VAT_Registration_No_ { get; set; }
        public string Country_Region_Code { get; set; }
        public byte Prepayment { get; set; }
        public byte Financial_Void { get; set; }
        public int Incoming_Document_Entry_No_ { get; set; }
        public string Creditor_No_ { get; set; }
        public string Payment_Reference { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Applies_to_Ext__Doc__No_ { get; set; }
        public string Recipient_Bank_Account { get; set; }
        public string Message_to_Recipient { get; set; }
        public byte Exported_to_Payment_File { get; set; }
        public int Dimension_Set_ID { get; set; }
        public string Credit_Card_No_ { get; set; }
        public string Job_Task_No_ { get; set; }
        public decimal Job_Unit_Price__LCY_ { get; set; }
        public decimal Job_Total_Price__LCY_ { get; set; }
        public decimal Job_Quantity { get; set; }
        public decimal Job_Unit_Cost__LCY_ { get; set; }
        public decimal Job_Line_Discount__ { get; set; }
        public decimal Job_Line_Disc__Amount__LCY_ { get; set; }
        public string Job_Unit_Of_Measure_Code { get; set; }
        public int Job_Line_Type { get; set; }
        public decimal Job_Unit_Price { get; set; }
        public decimal Job_Total_Price { get; set; }
        public decimal Job_Unit_Cost { get; set; }
        public decimal Job_Total_Cost { get; set; }
        public decimal Job_Line_Discount_Amount { get; set; }
        public decimal Job_Line_Amount { get; set; }
        public decimal Job_Total_Cost__LCY_ { get; set; }
        public decimal Job_Line_Amount__LCY_ { get; set; }
        public decimal Job_Currency_Factor { get; set; }
        public string Job_Currency_Code { get; set; }
        public int Job_Planning_Line_No_ { get; set; }
        public decimal Job_Remaining_Qty_ { get; set; }
        public string Direct_Debit_Mandate_ID { get; set; }
        public int Posting_Exch__Entry_No_ { get; set; }
        public string Payer_Information { get; set; }
        public string Transaction_Information { get; set; }
        public int Posting_Exch__Line_No_ { get; set; }
        public byte Applied_Automatically { get; set; }
        public string Campaign_No_ { get; set; }
        public string Prod__Order_No_ { get; set; }
        public System.DateTime FA_Posting_Date { get; set; }
        public int FA_Posting_Type { get; set; }
        public string Depreciation_Book_Code { get; set; }
        public decimal Salvage_Value { get; set; }
        public int No__of_Depreciation_Days { get; set; }
        public byte Depr__until_FA_Posting_Date { get; set; }
        public byte Depr__Acquisition_Cost { get; set; }
        public string Maintenance_Code { get; set; }
        public string Insurance_No_ { get; set; }
        public string Budgeted_FA_No_ { get; set; }
        public string Duplicate_in_Depreciation_Book { get; set; }
        public byte Use_Duplication_List { get; set; }
        public byte FA_Reclassification_Entry { get; set; }
        public int FA_Error_Entry_No_ { get; set; }
        public byte Index_Entry { get; set; }
        public int Source_Line_No_ { get; set; }
        public string Comment { get; set; }
        public byte Approved { get; set; }
        public byte Discrepancy { get; set; }
        public string GL_Description { get; set; }
        public byte First_Approval { get; set; }
        public string Payee_Name { get; set; }
    }
}
