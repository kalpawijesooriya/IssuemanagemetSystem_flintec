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
    
    public partial class FLINTEC_Calendar_Absence_Entry
    {
        public byte[] timestamp { get; set; }
        public int Capacity_Type { get; set; }
        public string No_ { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime Starting_Time { get; set; }
        public System.DateTime Ending_Time { get; set; }
        public string Work_Center_No_ { get; set; }
        public string Work_Center_Group_Code { get; set; }
        public decimal Capacity { get; set; }
        public System.DateTime Starting_Date_Time { get; set; }
        public System.DateTime Ending_Date_Time { get; set; }
        public string Description { get; set; }
        public byte Updated { get; set; }
    }
}
