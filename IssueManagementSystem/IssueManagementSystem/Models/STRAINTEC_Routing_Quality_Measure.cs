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
    
    public partial class STRAINTEC_Routing_Quality_Measure
    {
        public byte[] timestamp { get; set; }
        public string Routing_No_ { get; set; }
        public string Version_Code { get; set; }
        public string Operation_No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Qlty_Measure_Code { get; set; }
        public string Description { get; set; }
        public decimal Min__Value { get; set; }
        public decimal Max__Value { get; set; }
        public decimal Mean_Tolerance { get; set; }
    }
}
