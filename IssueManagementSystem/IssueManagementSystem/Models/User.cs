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
    
    public partial class User
    {
        public byte[] timestamp { get; set; }
        public System.Guid User_Security_ID { get; set; }
        public string User_Name { get; set; }
        public string Full_Name { get; set; }
        public int State { get; set; }
        public System.DateTime Expiry_Date { get; set; }
        public string Windows_Security_ID { get; set; }
        public byte Change_Password { get; set; }
        public int License_Type { get; set; }
        public string Authentication_Email { get; set; }
    }
}
