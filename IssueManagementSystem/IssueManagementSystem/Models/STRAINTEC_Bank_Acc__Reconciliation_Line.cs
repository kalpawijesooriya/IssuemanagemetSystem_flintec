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
    
    public partial class STRAINTEC_Bank_Acc__Reconciliation_Line
    {
        public byte[] timestamp { get; set; }
        public string Bank_Account_No_ { get; set; }
        public string Statement_No_ { get; set; }
        public int Statement_Line_No_ { get; set; }
        public string Document_No_ { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public string Description { get; set; }
        public decimal Statement_Amount { get; set; }
        public decimal Difference { get; set; }
        public decimal Applied_Amount { get; set; }
        public int Type { get; set; }
        public int Applied_Entries { get; set; }
        public System.DateTime Value_Date { get; set; }
        public byte Ready_for_Application { get; set; }
        public string Check_No_ { get; set; }
        public string Payer_Information { get; set; }
        public string Transaction_Information { get; set; }
        public int Posting_Exch__Entry_No_ { get; set; }
        public int Posting_Exch__Line_No_ { get; set; }
        public string External_Document_No_ { get; set; }
    }
}