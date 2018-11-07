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
    
    public partial class FLINTEC_CV_Ledger_Entry_Buffer
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public string CV_No_ { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public string Description { get; set; }
        public string Currency_Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Remaining_Amount { get; set; }
        public decimal Original_Amt___LCY_ { get; set; }
        public decimal Remaining_Amt___LCY_ { get; set; }
        public decimal Amount__LCY_ { get; set; }
        public decimal Sales_Purchase__LCY_ { get; set; }
        public decimal Profit__LCY_ { get; set; }
        public decimal Inv__Discount__LCY_ { get; set; }
        public string Bill_to_Pay_to_CV_No_ { get; set; }
        public string CV_Posting_Group { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Salesperson_Code { get; set; }
        public string User_ID { get; set; }
        public string Source_Code { get; set; }
        public string On_Hold { get; set; }
        public int Applies_to_Doc__Type { get; set; }
        public string Applies_to_Doc__No_ { get; set; }
        public byte Open { get; set; }
        public System.DateTime Due_Date { get; set; }
        public System.DateTime Pmt__Discount_Date { get; set; }
        public decimal Original_Pmt__Disc__Possible { get; set; }
        public decimal Pmt__Disc__Given__LCY_ { get; set; }
        public byte Positive { get; set; }
        public int Closed_by_Entry_No_ { get; set; }
        public System.DateTime Closed_at_Date { get; set; }
        public decimal Closed_by_Amount { get; set; }
        public string Applies_to_ID { get; set; }
        public string Journal_Batch_Name { get; set; }
        public string Reason_Code { get; set; }
        public int Bal__Account_Type { get; set; }
        public string Bal__Account_No_ { get; set; }
        public int Transaction_No_ { get; set; }
        public decimal Closed_by_Amount__LCY_ { get; set; }
        public decimal Debit_Amount { get; set; }
        public decimal Credit_Amount { get; set; }
        public decimal Debit_Amount__LCY_ { get; set; }
        public decimal Credit_Amount__LCY_ { get; set; }
        public System.DateTime Document_Date { get; set; }
        public string External_Document_No_ { get; set; }
        public byte Calculate_Interest { get; set; }
        public byte Closing_Interest_Calculated { get; set; }
        public string No__Series { get; set; }
        public string Closed_by_Currency_Code { get; set; }
        public decimal Closed_by_Currency_Amount { get; set; }
        public string Rounding_Currency { get; set; }
        public decimal Rounding_Amount { get; set; }
        public decimal Rounding_Amount__LCY_ { get; set; }
        public decimal Adjusted_Currency_Factor { get; set; }
        public decimal Original_Currency_Factor { get; set; }
        public decimal Original_Amount { get; set; }
        public decimal Remaining_Pmt__Disc__Possible { get; set; }
        public System.DateTime Pmt__Disc__Tolerance_Date { get; set; }
        public decimal Max__Payment_Tolerance { get; set; }
        public decimal Accepted_Payment_Tolerance { get; set; }
        public byte Accepted_Pmt__Disc__Tolerance { get; set; }
        public decimal Pmt__Tolerance__LCY_ { get; set; }
        public decimal Amount_to_Apply { get; set; }
        public byte Prepayment { get; set; }
        public int Dimension_Set_ID { get; set; }
    }
}