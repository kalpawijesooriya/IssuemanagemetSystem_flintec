using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Models
{
    public class TempClasses
    {
        public class tempClass
        {
            public String machine_machine_id { get; set; }
            public int count { get; set; }
        }
        public class tempClass4
        {
            public String Search_Description { get; set; }
            public int count { get; set; }

        }

        public class tempClass2
        {
            public DateTime issue_date { get; set; }
            public String issue { get; set; }
            public String machine_machine_id { get; set; }
            public String material_id { get; set; }
            public String Name { get; set; }
            public int DateDiff { get; set; }

        }
        public class tempClass3
        {
            public String issue { get; set; }
            public int count { get; set; }
        }

        public class tempClass5
        {
            public Nullable<DateTime> issue_date { get; set; }
            public Nullable<int> issue_occurrence_id { get; set; }
            public String issue { get; set; }
            public String line_name { get; set; }
            public String issue_satus { get; set; }
            public String location { get; set; }
            public String description { get; set; }
            public String Name { get; set; }
            public String material_id { get; set; }
            public String machine_machine_id { get; set; }
            public Nullable<int> line_line_id { get; set; }
            public Nullable<int> issue_issue_ID { get; set; }
            public Nullable<int> responsible_person_confirm_status { get; set; }
            public String responsible_person_confirm_feedback { get; set; }
            public Nullable<DateTime> solved_date { get; set; }
            public Nullable<DateTime> commented_date { get; set; }
            public String department { get; set; }
            public String solved_emp { get; set; }
            public String buzzer_off_by { get; set; }
            public Nullable<DateTime> buzzer_off_time { get; set; }
            public int dep_occured { get; set; }
        }


    }
}