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
    
    public partial class STRAINTEC_Fixed_Asset
    {
        public byte[] timestamp { get; set; }
        public string No_ { get; set; }
        public string Description { get; set; }
        public string Search_Description { get; set; }
        public string Description_2 { get; set; }
        public string FA_Class_Code { get; set; }
        public string FA_Subclass_Code { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Location_Code { get; set; }
        public string FA_Location_Code { get; set; }
        public string Vendor_No_ { get; set; }
        public int Main_Asset_Component { get; set; }
        public string Component_of_Main_Asset { get; set; }
        public byte Budgeted_Asset { get; set; }
        public System.DateTime Warranty_Date { get; set; }
        public string Responsible_Employee { get; set; }
        public string Serial_No_ { get; set; }
        public System.DateTime Last_Date_Modified { get; set; }
        public byte Blocked { get; set; }
        public byte[] Picture { get; set; }
        public string Maintenance_Vendor_No_ { get; set; }
        public byte Under_Maintenance { get; set; }
        public System.DateTime Next_Service_Date { get; set; }
        public byte Inactive { get; set; }
        public string No__Series { get; set; }
        public string FA_Posting_Group { get; set; }
        public string Model_No_ { get; set; }
    }
}
