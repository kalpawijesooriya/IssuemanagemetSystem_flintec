using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class VoiceController : Controller
    {
        [HttpGet]
        public ActionResult getTodayIssues()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                DateTime startDateTime = DateTime.Today; //Today at 00:00:00
                DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

             
                String query = @"SELECT issue_occurrence.issue_date,
                                issue_occurrence.issue_occurrence_id,
                                issues.issue,lines.line_name,issue_occurrence.issue_satus,
                                issue_occurrence.location,issue_occurrence.description
                                FROM issue_occurrence,lines,issues WHERE
                                lines.line_id = issue_occurrence.line_line_id AND
                                issues.issue_id LIKE issue_occurrence.issue_issue_ID";

                var data = db.Database.SqlQuery<tempClass5>(query).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }


        private class tempClass5
        {
            public DateTime issue_date { get; set; }
            public int issue_occurrence_id { get; set; }
            public String issue { get; set; }
            public String line_name { get; set; }
            public String issue_satus { get; set; }
            public String location { get; set; }
            public String description { get; set; }
        }

    }
}