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
    
    public partial class STRAINTEC_Posted_Assemble_to_Order_Link
    {
        public byte[] timestamp { get; set; }
        public int Assembly_Document_Type { get; set; }
        public string Assembly_Document_No_ { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public int Document_Line_No_ { get; set; }
        public decimal Assembled_Quantity { get; set; }
        public decimal Assembled_Quantity__Base_ { get; set; }
        public string Assembly_Order_No_ { get; set; }
        public string Order_No_ { get; set; }
        public int Order_Line_No_ { get; set; }
    }
}
