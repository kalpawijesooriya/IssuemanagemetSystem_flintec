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
    
    public partial class STRAINTEC_Item_Analysis_View_Entry
    {
        public byte[] timestamp { get; set; }
        public int Analysis_Area { get; set; }
        public string Analysis_View_Code { get; set; }
        public string Item_No_ { get; set; }
        public int Item_Ledger_Entry_Type { get; set; }
        public int Entry_Type { get; set; }
        public int Source_Type { get; set; }
        public string Source_No_ { get; set; }
        public string Dimension_1_Value_Code { get; set; }
        public string Dimension_2_Value_Code { get; set; }
        public string Dimension_3_Value_Code { get; set; }
        public string Location_Code { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public int Entry_No_ { get; set; }
        public decimal Invoiced_Quantity { get; set; }
        public decimal Sales_Amount__Actual_ { get; set; }
        public decimal Cost_Amount__Actual_ { get; set; }
        public decimal Cost_Amount__Non_Invtbl__ { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sales_Amount__Expected_ { get; set; }
        public decimal Cost_Amount__Expected_ { get; set; }
    }
}