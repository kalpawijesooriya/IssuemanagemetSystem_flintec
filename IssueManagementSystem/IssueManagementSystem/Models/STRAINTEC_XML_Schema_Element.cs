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
    
    public partial class STRAINTEC_XML_Schema_Element
    {
        public byte[] timestamp { get; set; }
        public string XML_Schema_Code { get; set; }
        public int ID { get; set; }
        public int Parent_ID { get; set; }
        public string Node_Name { get; set; }
        public int Node_Type { get; set; }
        public string Data_Type { get; set; }
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public byte Choice { get; set; }
        public string Sort_Key { get; set; }
        public int Indentation { get; set; }
        public byte Visible { get; set; }
        public byte Selected { get; set; }
        public string Simple_Data_Type { get; set; }
    }
}