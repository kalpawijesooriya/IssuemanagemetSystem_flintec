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
    
    public partial class FLINTEC_Payment_Buffer
    {
        public byte[] timestamp { get; set; }
        public string Vendor_No_ { get; set; }
        public string Currency_Code { get; set; }
        public int Vendor_Ledg__Entry_No_ { get; set; }
        public int Dimension_Entry_No_ { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Document_No_ { get; set; }
        public decimal Amount { get; set; }
        public int Vendor_Ledg__Entry_Doc__Type { get; set; }
        public string Vendor_Ledg__Entry_Doc__No_ { get; set; }
        public string Creditor_No_ { get; set; }
        public string Payment_Reference { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Applies_to_Ext__Doc__No_ { get; set; }
        public byte Exported_to_Payment_File { get; set; }
        public int Dimension_Set_ID { get; set; }
    }
}