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
    
    public partial class FLINTEC_VAT_Entry
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public string Document_No_ { get; set; }
        public int Document_Type { get; set; }
        public int Type { get; set; }
        public decimal Base { get; set; }
        public decimal Amount { get; set; }
        public int VAT_Calculation_Type { get; set; }
        public string Bill_to_Pay_to_No_ { get; set; }
        public byte EU_3_Party_Trade { get; set; }
        public string User_ID { get; set; }
        public string Source_Code { get; set; }
        public string Reason_Code { get; set; }
        public int Closed_by_Entry_No_ { get; set; }
        public byte Closed { get; set; }
        public string Country_Region_Code { get; set; }
        public string Internal_Ref__No_ { get; set; }
        public int Transaction_No_ { get; set; }
        public decimal Unrealized_Amount { get; set; }
        public decimal Unrealized_Base { get; set; }
        public decimal Remaining_Unrealized_Amount { get; set; }
        public decimal Remaining_Unrealized_Base { get; set; }
        public string External_Document_No_ { get; set; }
        public string No__Series { get; set; }
        public string Tax_Area_Code { get; set; }
        public byte Tax_Liable { get; set; }
        public string Tax_Group_Code { get; set; }
        public byte Use_Tax { get; set; }
        public string Tax_Jurisdiction_Code { get; set; }
        public string Tax_Group_Used { get; set; }
        public int Tax_Type { get; set; }
        public byte Tax_on_Tax { get; set; }
        public int Sales_Tax_Connection_No_ { get; set; }
        public int Unrealized_VAT_Entry_No_ { get; set; }
        public string VAT_Bus__Posting_Group { get; set; }
        public string VAT_Prod__Posting_Group { get; set; }
        public decimal Additional_Currency_Amount { get; set; }
        public decimal Additional_Currency_Base { get; set; }
        public decimal Add__Currency_Unrealized_Amt_ { get; set; }
        public decimal Add__Currency_Unrealized_Base { get; set; }
        public decimal VAT_Base_Discount__ { get; set; }
        public decimal Add__Curr__Rem__Unreal__Amount { get; set; }
        public decimal Add__Curr__Rem__Unreal__Base { get; set; }
        public decimal VAT_Difference { get; set; }
        public decimal Add__Curr__VAT_Difference { get; set; }
        public string Ship_to_Order_Address_Code { get; set; }
        public System.DateTime Document_Date { get; set; }
        public string VAT_Registration_No_ { get; set; }
        public byte Reversed { get; set; }
        public int Reversed_by_Entry_No_ { get; set; }
        public int Reversed_Entry_No_ { get; set; }
        public byte EU_Service { get; set; }
    }
}
