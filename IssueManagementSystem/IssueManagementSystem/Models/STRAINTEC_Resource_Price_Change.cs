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
    
    public partial class STRAINTEC_Resource_Price_Change
    {
        public byte[] timestamp { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Work_Type_Code { get; set; }
        public string Currency_Code { get; set; }
        public decimal Current_Unit_Price { get; set; }
        public decimal New_Unit_Price { get; set; }
    }
}
