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
    
    public partial class STRAINTEC_Handled_IC_Inbox_Jnl__Line
    {
        public byte[] timestamp { get; set; }
        public int Transaction_No_ { get; set; }
        public string IC_Partner_Code { get; set; }
        public int Transaction_Source { get; set; }
        public int Line_No_ { get; set; }
        public int Account_Type { get; set; }
        public string Account_No_ { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal VAT_Amount { get; set; }
        public string Currency_Code { get; set; }
        public System.DateTime Due_Date { get; set; }
        public decimal Payment_Discount__ { get; set; }
        public System.DateTime Payment_Discount_Date { get; set; }
        public decimal Quantity { get; set; }
        public string Document_No_ { get; set; }
    }
}