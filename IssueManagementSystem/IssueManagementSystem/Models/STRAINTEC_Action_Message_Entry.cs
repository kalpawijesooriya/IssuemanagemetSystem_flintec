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
    
    public partial class STRAINTEC_Action_Message_Entry
    {
        public byte[] timestamp { get; set; }
        public int Entry_No_ { get; set; }
        public int Type { get; set; }
        public int Reservation_Entry { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime New_Date { get; set; }
        public int Calculation { get; set; }
        public byte Suppressed_Action_Msg_ { get; set; }
        public int Source_Type { get; set; }
        public int Source_Subtype { get; set; }
        public string Source_ID { get; set; }
        public string Source_Batch_Name { get; set; }
        public int Source_Prod__Order_Line { get; set; }
        public int Source_Ref__No_ { get; set; }
        public string Location_Code { get; set; }
        public string Bin_Code { get; set; }
        public string Variant_Code { get; set; }
        public string Item_No_ { get; set; }
    }
}
