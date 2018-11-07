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
    
    public partial class STRAINTEC_Sales_Header
    {
        public byte[] timestamp { get; set; }
        public int Document_Type { get; set; }
        public string No_ { get; set; }
        public string Sell_to_Customer_No_ { get; set; }
        public string Bill_to_Customer_No_ { get; set; }
        public string Bill_to_Name { get; set; }
        public string Bill_to_Name_2 { get; set; }
        public string Bill_to_Address { get; set; }
        public string Bill_to_Address_2 { get; set; }
        public string Bill_to_City { get; set; }
        public string Bill_to_Contact { get; set; }
        public string Your_Reference { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Name_2 { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_Contact { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Shipment_Date { get; set; }
        public string Posting_Description { get; set; }
        public string Payment_Terms_Code { get; set; }
        public System.DateTime Due_Date { get; set; }
        public decimal Payment_Discount__ { get; set; }
        public System.DateTime Pmt__Discount_Date { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Location_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Customer_Posting_Group { get; set; }
        public string Currency_Code { get; set; }
        public decimal Currency_Factor { get; set; }
        public string Customer_Price_Group { get; set; }
        public byte Prices_Including_VAT { get; set; }
        public string Invoice_Disc__Code { get; set; }
        public string Customer_Disc__Group { get; set; }
        public string Language_Code { get; set; }
        public string Salesperson_Code { get; set; }
        public string Order_Class { get; set; }
        public int No__Printed { get; set; }
        public string On_Hold { get; set; }
        public int Applies_to_Doc__Type { get; set; }
        public string Applies_to_Doc__No_ { get; set; }
        public string Bal__Account_No_ { get; set; }
        public byte Ship { get; set; }
        public byte Invoice { get; set; }
        public byte Print_Posted_Documents { get; set; }
        public string Shipping_No_ { get; set; }
        public string Posting_No_ { get; set; }
        public string Last_Shipping_No_ { get; set; }
        public string Last_Posting_No_ { get; set; }
        public string Prepayment_No_ { get; set; }
        public string Last_Prepayment_No_ { get; set; }
        public string Prepmt__Cr__Memo_No_ { get; set; }
        public string Last_Prepmt__Cr__Memo_No_ { get; set; }
        public string VAT_Registration_No_ { get; set; }
        public byte Combine_Shipments { get; set; }
        public string Reason_Code { get; set; }
        public string Gen__Bus__Posting_Group { get; set; }
        public byte EU_3_Party_Trade { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public string VAT_Country_Region_Code { get; set; }
        public string Sell_to_Customer_Name { get; set; }
        public string Sell_to_Customer_Name_2 { get; set; }
        public string Sell_to_Address { get; set; }
        public string Sell_to_Address_2 { get; set; }
        public string Sell_to_City { get; set; }
        public string Sell_to_Contact { get; set; }
        public string Bill_to_Post_Code { get; set; }
        public string Bill_to_County { get; set; }
        public string Bill_to_Country_Region_Code { get; set; }
        public string Sell_to_Post_Code { get; set; }
        public string Sell_to_County { get; set; }
        public string Sell_to_Country_Region_Code { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_County { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public int Bal__Account_Type { get; set; }
        public string Exit_Point { get; set; }
        public byte Correction { get; set; }
        public System.DateTime Document_Date { get; set; }
        public string External_Document_No_ { get; set; }
        public string Area { get; set; }
        public string Transaction_Specification { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Shipping_Agent_Code { get; set; }
        public string Package_Tracking_No_ { get; set; }
        public string No__Series { get; set; }
        public string Posting_No__Series { get; set; }
        public string Shipping_No__Series { get; set; }
        public string Tax_Area_Code { get; set; }
        public byte Tax_Liable { get; set; }
        public string VAT_Bus__Posting_Group { get; set; }
        public int Reserve { get; set; }
        public string Applies_to_ID { get; set; }
        public decimal VAT_Base_Discount__ { get; set; }
        public int Status { get; set; }
        public int Invoice_Discount_Calculation { get; set; }
        public decimal Invoice_Discount_Value { get; set; }
        public byte Send_IC_Document { get; set; }
        public int IC_Status { get; set; }
        public string Sell_to_IC_Partner_Code { get; set; }
        public string Bill_to_IC_Partner_Code { get; set; }
        public int IC_Direction { get; set; }
        public decimal Prepayment__ { get; set; }
        public string Prepayment_No__Series { get; set; }
        public byte Compress_Prepayment { get; set; }
        public System.DateTime Prepayment_Due_Date { get; set; }
        public string Prepmt__Cr__Memo_No__Series { get; set; }
        public string Prepmt__Posting_Description { get; set; }
        public System.DateTime Prepmt__Pmt__Discount_Date { get; set; }
        public string Prepmt__Payment_Terms_Code { get; set; }
        public decimal Prepmt__Payment_Discount__ { get; set; }
        public string Quote_No_ { get; set; }
        public int Job_Queue_Status { get; set; }
        public System.Guid Job_Queue_Entry_ID { get; set; }
        public int Incoming_Document_Entry_No_ { get; set; }
        public int Dimension_Set_ID { get; set; }
        public byte Authorization_Required { get; set; }
        public string Credit_Card_No_ { get; set; }
        public string Direct_Debit_Mandate_ID { get; set; }
        public int Doc__No__Occurrence { get; set; }
        public string Campaign_No_ { get; set; }
        public string Sell_to_Customer_Template_Code { get; set; }
        public string Sell_to_Contact_No_ { get; set; }
        public string Bill_to_Contact_No_ { get; set; }
        public string Bill_to_Customer_Template_Code { get; set; }
        public string Opportunity_No_ { get; set; }
        public string Responsibility_Center { get; set; }
        public int Shipping_Advice { get; set; }
        public int Posting_from_Whse__Ref_ { get; set; }
        public System.DateTime Requested_Delivery_Date { get; set; }
        public System.DateTime Promised_Delivery_Date { get; set; }
        public string Shipping_Time { get; set; }
        public string Outbound_Whse__Handling_Time { get; set; }
        public string Shipping_Agent_Service_Code { get; set; }
        public byte Receive { get; set; }
        public string Return_Receipt_No_ { get; set; }
        public string Return_Receipt_No__Series { get; set; }
        public string Last_Return_Receipt_No_ { get; set; }
        public byte Allow_Line_Disc_ { get; set; }
        public byte Get_Shipment_Used { get; set; }
        public string Assigned_User_ID { get; set; }
        public System.DateTime Order_Received_Date { get; set; }
        public string Reason_for_Delay_Code { get; set; }
        public string Reason_for_Delay { get; set; }
        public string End_Customer { get; set; }
        public string End_Customer_Name { get; set; }
        public string Bank { get; set; }
        public string Customer_Order_No_ { get; set; }
        public string RMA_No_ { get; set; }
        public System.DateTime Date_Received { get; set; }
        public string SVAT_Registration_No_ { get; set; }
    }
}
