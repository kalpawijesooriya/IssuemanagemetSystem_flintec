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
    
    public partial class Location
    {
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public Nullable<long> ParentId { get; set; }
        public Nullable<int> LocationSeats { get; set; }
        public string efficiency { get; set; }
        public Nullable<int> weekNo { get; set; }
        public string DayWorkingTime { get; set; }
        public string NightWorkingTime { get; set; }
        public string Placement { get; set; }
    }
}