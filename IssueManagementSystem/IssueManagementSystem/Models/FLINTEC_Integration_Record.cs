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
    
    public partial class FLINTEC_Integration_Record
    {
        public byte[] timestamp { get; set; }
        public System.Guid Integration_ID { get; set; }
        public int Table_ID { get; set; }
        public int Page_ID { get; set; }
        public byte[] Record_ID { get; set; }
        public System.DateTime Deleted_On { get; set; }
        public System.DateTime Modified_On { get; set; }
    }
}