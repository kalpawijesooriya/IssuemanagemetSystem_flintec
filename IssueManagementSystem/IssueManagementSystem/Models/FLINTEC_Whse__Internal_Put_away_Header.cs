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
    
    public partial class FLINTEC_Whse__Internal_Put_away_Header
    {
        public byte[] timestamp { get; set; }
        public string No_ { get; set; }
        public string Location_Code { get; set; }
        public string Assigned_User_ID { get; set; }
        public System.DateTime Assignment_Date { get; set; }
        public System.DateTime Assignment_Time { get; set; }
        public string No__Series { get; set; }
        public string From_Bin_Code { get; set; }
        public string From_Zone_Code { get; set; }
        public System.DateTime Due_Date { get; set; }
        public int Document_Status { get; set; }
        public int Sorting_Method { get; set; }
        public int Status { get; set; }
    }
}
