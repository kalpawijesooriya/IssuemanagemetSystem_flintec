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
    
    public partial class STRAINTEC_Job_Queue
    {
        public byte[] timestamp { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Job_Queue_Category_Filter { get; set; }
        public int Server_Instance_ID { get; set; }
        public int Session_ID { get; set; }
        public System.DateTime Last_Heartbeat { get; set; }
        public byte Started { get; set; }
        public byte Start_Automatically_From_NAS { get; set; }
        public string Start_on_This_NAS_Computer { get; set; }
        public string Start_on_This_NAS_Instance { get; set; }
        public string Running_as_User_ID { get; set; }
    }
}
