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
    
    public partial class FLINTEC_Cost_Register
    {
        public byte[] timestamp { get; set; }
        public int No_ { get; set; }
        public int Source { get; set; }
        public string Text { get; set; }
        public int From_G_L_Entry_No_ { get; set; }
        public int To_G_L_Entry_No_ { get; set; }
        public int From_Cost_Entry_No_ { get; set; }
        public int To_Cost_Entry_No_ { get; set; }
        public int No__of_Entries { get; set; }
        public decimal Debit_Amount { get; set; }
        public decimal Credit_Amount { get; set; }
        public System.DateTime Processed_Date { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public byte Closed { get; set; }
        public int Level { get; set; }
        public string User_ID { get; set; }
        public string Journal_Batch_Name { get; set; }
    }
}