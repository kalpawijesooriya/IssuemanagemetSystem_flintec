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
    
    public partial class STRAINTEC_Service_Order_Allocation
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public int Status { get; set; }
        public string Document_No_ { get; set; }
        public System.DateTime Allocation_Date { get; set; }
        public string Resource_No_ { get; set; }
        public string Resource_Group_No_ { get; set; }
        public int Service_Item_Line_No_ { get; set; }
        public decimal Allocated_Hours { get; set; }
        public System.DateTime Starting_Time { get; set; }
        public System.DateTime Finishing_Time { get; set; }
        public string Description { get; set; }
        public string Reason_Code { get; set; }
        public string Service_Item_No_ { get; set; }
        public byte Posted { get; set; }
        public string Service_Item_Serial_No_ { get; set; }
        public byte Service_Started { get; set; }
        public int Document_Type { get; set; }
    }
}