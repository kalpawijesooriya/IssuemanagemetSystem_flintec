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
    
    public partial class FLINTEC_Change_Log_Entry
    {
        public byte[] timestamp { get; set; }
        public long Entry_No_ { get; set; }
        public System.DateTime Date_and_Time { get; set; }
        public System.DateTime Time { get; set; }
        public string User_ID { get; set; }
        public int Table_No_ { get; set; }
        public int Field_No_ { get; set; }
        public int Type_of_Change { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Primary_Key { get; set; }
        public int Primary_Key_Field_1_No_ { get; set; }
        public string Primary_Key_Field_1_Value { get; set; }
        public int Primary_Key_Field_2_No_ { get; set; }
        public string Primary_Key_Field_2_Value { get; set; }
        public int Primary_Key_Field_3_No_ { get; set; }
        public string Primary_Key_Field_3_Value { get; set; }
        public byte[] Record_ID { get; set; }
    }
}