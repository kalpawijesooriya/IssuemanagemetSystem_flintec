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
    
    public partial class STRAINTEC_ATO_Sales_Buffer
    {
        public byte[] timestamp { get; set; }
        public int Type { get; set; }
        public string Order_No_ { get; set; }
        public string Item_No_ { get; set; }
        public string Parent_Item_No_ { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sales_Cost { get; set; }
        public decimal Sales_Amount { get; set; }
        public decimal Profit__ { get; set; }
        public string Parent_Description { get; set; }
    }
}
