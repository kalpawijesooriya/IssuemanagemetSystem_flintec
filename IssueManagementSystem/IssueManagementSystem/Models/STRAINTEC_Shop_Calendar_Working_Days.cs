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
    
    public partial class STRAINTEC_Shop_Calendar_Working_Days
    {
        public byte[] timestamp { get; set; }
        public string Shop_Calendar_Code { get; set; }
        public int Day { get; set; }
        public System.DateTime Starting_Time { get; set; }
        public System.DateTime Ending_Time { get; set; }
        public string Work_Shift_Code { get; set; }
    }
}