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
    
    public partial class FLINTEC_Inventory_Posting_Setup
    {
        public byte[] timestamp { get; set; }
        public string Location_Code { get; set; }
        public string Invt__Posting_Group_Code { get; set; }
        public string Inventory_Account { get; set; }
        public string Inventory_Account__Interim_ { get; set; }
        public string WIP_Account { get; set; }
        public string Material_Variance_Account { get; set; }
        public string Capacity_Variance_Account { get; set; }
        public string Mfg__Overhead_Variance_Account { get; set; }
        public string Cap__Overhead_Variance_Account { get; set; }
        public string Subcontracted_Variance_Account { get; set; }
    }
}
