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
    
    public partial class STRAINTEC_Job_Resource_Price
    {
        public byte[] timestamp { get; set; }
        public string Job_No_ { get; set; }
        public string Job_Task_No_ { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Work_Type_Code { get; set; }
        public string Currency_Code { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Unit_Cost_Factor { get; set; }
        public decimal Line_Discount__ { get; set; }
        public string Description { get; set; }
        public byte Apply_Job_Price { get; set; }
        public byte Apply_Job_Discount { get; set; }
    }
}
