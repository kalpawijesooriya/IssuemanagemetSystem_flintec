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
    
    public partial class STRAINTEC_Handled_IC_Outbox_Purch__Hdr
    {
        public byte[] timestamp { get; set; }
        public int IC_Transaction_No_ { get; set; }
        public string IC_Partner_Code { get; set; }
        public int Transaction_Source { get; set; }
        public int Document_Type { get; set; }
        public string Buy_from_Vendor_No_ { get; set; }
        public string No_ { get; set; }
        public string Pay_to_Vendor_No_ { get; set; }
        public string Your_Reference { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Expected_Receipt_Date { get; set; }
        public System.DateTime Due_Date { get; set; }
        public decimal Payment_Discount__ { get; set; }
        public System.DateTime Pmt__Discount_Date { get; set; }
        public string Currency_Code { get; set; }
        public byte Prices_Including_VAT { get; set; }
        public string Vendor_Invoice_No_ { get; set; }
        public string Vendor_Cr__Memo_No_ { get; set; }
        public string Sell_to_Customer_No_ { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public System.DateTime Document_Date { get; set; }
        public System.DateTime Requested_Receipt_Date { get; set; }
        public System.DateTime Promised_Receipt_Date { get; set; }
    }
}
