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
    
    public partial class STRAINTEC_Job_WIP_Buffer
    {
        public byte[] timestamp { get; set; }
        public string Job_No_ { get; set; }
        public int Job_WIP_Total_Entry_No_ { get; set; }
        public int Type { get; set; }
        public string Posting_Group { get; set; }
        public int Dim_Combination_ID { get; set; }
        public byte Reverse { get; set; }
        public string G_L_Account_No_ { get; set; }
        public decimal WIP_Entry_Amount { get; set; }
        public string Bal__G_L_Account_No_ { get; set; }
        public string WIP_Method { get; set; }
        public byte Job_Complete { get; set; }
        public int WIP_Posting_Method_Used { get; set; }
    }
}