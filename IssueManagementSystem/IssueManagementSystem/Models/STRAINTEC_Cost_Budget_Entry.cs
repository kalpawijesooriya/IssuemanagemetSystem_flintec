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
    
    public partial class STRAINTEC_Cost_Budget_Entry
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public string Budget_Name { get; set; }
        public string Cost_Type_No_ { get; set; }
        public System.DateTime Date { get; set; }
        public string Cost_Center_Code { get; set; }
        public string Cost_Object_Code { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Document_No_ { get; set; }
        public string Source_Code { get; set; }
        public byte System_Created_Entry { get; set; }
        public byte Allocated { get; set; }
        public int Allocated_with_Journal_No_ { get; set; }
        public string Last_Modified_By_User { get; set; }
        public System.DateTime Last_Date_Modified { get; set; }
        public string Allocation_Description { get; set; }
        public string Allocation_ID { get; set; }
    }
}
