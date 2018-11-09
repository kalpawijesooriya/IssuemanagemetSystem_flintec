using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Models
{
    public class LineIssue
    {
        public int issue_occurrence_id { get; set; }
        public string material_id { get; set; }
        public string description { get; set; }
        public string machine_machineName { get; set; }
        public Nullable<int> issue_issue_ID { get; set; }
        public string responsible_person_name { get; set; }
        public Nullable<int> responsible_person_confirm_status { get; set; }
        public string issueDate { get; set; }
        public string commentedDate { get; set; }
        
    }
}