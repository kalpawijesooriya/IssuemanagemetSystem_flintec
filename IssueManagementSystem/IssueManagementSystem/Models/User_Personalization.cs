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
    
    public partial class User_Personalization
    {
        public byte[] timestamp { get; set; }
        public System.Guid User_SID { get; set; }
        public string Profile_ID { get; set; }
        public int Language_ID { get; set; }
        public string Company { get; set; }
        public byte Debugger_Break_On_Error { get; set; }
        public byte Debugger_Break_On_Rec_Changes { get; set; }
        public byte Debugger_Skip_System_Triggers { get; set; }
    }
}
