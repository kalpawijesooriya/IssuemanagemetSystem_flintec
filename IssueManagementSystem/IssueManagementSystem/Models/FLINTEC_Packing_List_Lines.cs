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
    
    public partial class FLINTEC_Packing_List_Lines
    {
        public byte[] timestamp { get; set; }
        public string Document_No_ { get; set; }
        public int Line_No_ { get; set; }
        public string Item_No_ { get; set; }
        public string Item_Description { get; set; }
        public string UoM { get; set; }
        public decimal Quantity_to_Ship { get; set; }
        public System.DateTime Delivery_Date { get; set; }
        public byte Select { get; set; }
        public string Customer_No_ { get; set; }
        public int Package_Type { get; set; }
        public int Package_No_ { get; set; }
        public string Dimension__cm_ { get; set; }
        public decimal Weight__kg_ { get; set; }
        public string PO__ { get; set; }
        public string Customer_Part_ { get; set; }
        public string Cust__Part_Desc { get; set; }
        public string Shipment_Method_Code { get; set; }
        public int GSP { get; set; }
        public string User_ID { get; set; }
        public string Sales_Order_No { get; set; }
        public int PakageTypeRep { get; set; }
        public string TempPackingList_No { get; set; }
        public string Sales_Inv_No { get; set; }
        public int Box_In_Pallet { get; set; }
        public string Customer_Name { get; set; }
    }
}
