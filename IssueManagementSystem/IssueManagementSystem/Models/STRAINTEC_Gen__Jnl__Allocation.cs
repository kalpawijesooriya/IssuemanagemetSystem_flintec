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
    
    public partial class STRAINTEC_Gen__Jnl__Allocation
    {
        public byte[] timestamp { get; set; }
        public string Journal_Template_Name { get; set; }
        public string Journal_Batch_Name { get; set; }
        public int Journal_Line_No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Account_No_ { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public decimal Allocation_Quantity { get; set; }
        public decimal Allocation__ { get; set; }
        public decimal Amount { get; set; }
        public int Gen__Posting_Type { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public string Gen__Prod__Posting_Group { get; set; }
        public int VAT_Calculation_Type { get; set; }
        public decimal VAT_Amount { get; set; }
        public decimal VAT__ { get; set; }
        public string Tax_Area_Code { get; set; }
        public byte Tax_Liable { get; set; }
        public string Tax_Group_Code { get; set; }
        public byte Use_Tax { get; set; }
        public string VAT_Bus__Posting_Group { get; set; }
        public string VAT_Prod__Posting_Group { get; set; }
        public decimal Additional_Currency_Amount { get; set; }
        public int Dimension_Set_ID { get; set; }
    }
}
