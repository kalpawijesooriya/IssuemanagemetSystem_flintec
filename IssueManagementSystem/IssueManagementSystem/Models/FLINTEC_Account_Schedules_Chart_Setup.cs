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
    
    public partial class FLINTEC_Account_Schedules_Chart_Setup
    {
        public byte[] timestamp { get; set; }
        public string User_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Account_Schedule_Name { get; set; }
        public string Column_Layout_Name { get; set; }
        public int Base_X_Axis_on { get; set; }
        public System.DateTime Start_Date { get; set; }
        public System.DateTime End_Date { get; set; }
        public int Period_Length { get; set; }
        public int No__of_Periods { get; set; }
        public byte Last_Viewed { get; set; }
        public byte Look_Ahead { get; set; }
    }
}
