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
    
    public partial class STRAINTEC_Assemble_to_Order_Link
    {
        public byte[] timestamp { get; set; }
        public int Assembly_Document_Type { get; set; }
        public string Assembly_Document_No_ { get; set; }
        public int Type { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public int Document_Line_No_ { get; set; }
        public decimal Assembled_Quantity { get; set; }
    }
}
