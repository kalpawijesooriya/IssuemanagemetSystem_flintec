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
    
    public partial class STRAINTEC_Bank_Export_Import_Setup
    {
        public byte[] timestamp { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Direction { get; set; }
        public int Processing_Codeunit_ID { get; set; }
        public int Processing_XMLport_ID { get; set; }
        public string Posting_Exch__Def__Code { get; set; }
        public byte Preserve_Non_Latin_Characters { get; set; }
    }
}
