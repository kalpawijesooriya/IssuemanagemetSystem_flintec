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
    
    public partial class STRAINTEC_Service_Document_Log
    {
        public byte[] timestamp { get; set; }
        public int Document_Type { get; set; }
        public string Document_No_ { get; set; }
        public int Entry_No_ { get; set; }
        public int Event_No_ { get; set; }
        public int Service_Item_Line_No_ { get; set; }
        public string After { get; set; }
        public string Before { get; set; }
        public System.DateTime Change_Date { get; set; }
        public System.DateTime Change_Time { get; set; }
        public string User_ID { get; set; }
    }
}