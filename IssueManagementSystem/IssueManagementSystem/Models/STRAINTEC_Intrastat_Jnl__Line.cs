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
    
    public partial class STRAINTEC_Intrastat_Jnl__Line
    {
        public byte[] timestamp { get; set; }
        public string Journal_Template_Name { get; set; }
        public string Journal_Batch_Name { get; set; }
        public int Line_No_ { get; set; }
        public int Type { get; set; }
        public System.DateTime Date { get; set; }
        public string Tariff_No_ { get; set; }
        public string Item_Description { get; set; }
        public string Country_Region_Code { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public int Source_Type { get; set; }
        public int Source_Entry_No_ { get; set; }
        public decimal Net_Weight { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost_Regulation__ { get; set; }
        public decimal Indirect_Cost { get; set; }
        public decimal Statistical_Value { get; set; }
        public string Document_No_ { get; set; }
        public string Item_No_ { get; set; }
        public string Name { get; set; }
        public decimal Total_Weight { get; set; }
        public byte Supplementary_Units { get; set; }
        public string Internal_Ref__No_ { get; set; }
        public string Country_Region_of_Origin_Code { get; set; }
        public string Entry_Exit_Point { get; set; }
        public string Area { get; set; }
        public string Transaction_Specification { get; set; }
    }
}