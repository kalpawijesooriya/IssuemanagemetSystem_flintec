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
    
    public partial class FLINTEC_Job_WIP_Method
    {
        public byte[] timestamp { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public byte WIP_Cost { get; set; }
        public byte WIP_Sales { get; set; }
        public int Recognized_Costs { get; set; }
        public int Recognized_Sales { get; set; }
        public byte Valid { get; set; }
        public byte System_Defined { get; set; }
        public int System_Defined_Index { get; set; }
    }
}