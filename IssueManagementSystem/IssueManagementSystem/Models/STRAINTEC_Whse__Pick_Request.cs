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
    
    public partial class STRAINTEC_Whse__Pick_Request
    {
        public byte[] timestamp { get; set; }
        public int Document_Type { get; set; }
        public int Document_Subtype { get; set; }
        public string Document_No_ { get; set; }
        public string Location_Code { get; set; }
        public string Zone_Code { get; set; }
        public string Bin_Code { get; set; }
        public int Status { get; set; }
        public byte Completely_Picked { get; set; }
    }
}
