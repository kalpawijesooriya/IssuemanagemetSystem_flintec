using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if ((string)Session["department"] == "MD")
                {
                    ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1).Count();
                    ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2).Count();
                    ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3).Count();
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5).Count();
                }
                else
                {
                    string location = Session["location"].ToString();
                    ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1 && location == x.location).Count();
                    ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2 && x.location == location).Count();
                    ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3 && x.location == location).Count();
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4 && x.location == location).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5 && x.location == location).Count();
                }
            }
            return View();
        }
        [HttpGet]
        public JsonResult GetNotification()
        {
            return Json(NotificaionService.GetNotification(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult hideNotification(int? notificationId)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "UPDATE tbl_Notifications SET Status = 0 WHERE NotificationId = " + notificationId;
                db.Database.ExecuteSqlCommand(query);


            }
            return Json(true);
        }


        private class tempClass
        {
            public String machine_machine_id { get; set; }
            public int count { get; set; }

        }

        private class tempClass4  
        {
            public String Search_Description { get; set; }
            public int count { get; set; }

        }

        [HttpPost]
        public JsonResult fillChart1(string barChart, string startDate, string endDate, string plantLocation)
        {
            var chart1Data = new List<tempClass>();
            var chart2Data = new List<tempClass4>();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (barChart.Equals("1") ) {
                    String query = @"SELECT TOP 10 issue_occurrence.machine_machine_id,
                                    count(issue_occurrence.machine_machine_id) AS count FROM issue_occurrence 
                                    WHERE issue_occurrence.issue_issue_ID = 1 AND issue_occurrence.location IN ('"+plantLocation+@"')
                                    AND  issue_occurrence.issue_date BETWEEN '" + startDate + @"' AND '" + endDate + @"' 
                                    GROUP BY issue_occurrence.machine_machine_id  ORDER BY count Desc";

                    System.Diagnostics.Debug.Print(query);
                    chart1Data = db.Database.SqlQuery<tempClass>(query).ToList();
                    return Json(chart1Data, JsonRequestBehavior.AllowGet);
                }

                if (barChart.Equals("2")) {
                    String query = @"SELECT TOP 10 FLINTEC.dbo.FLINTEC$Item.[Search Description] AS Search_Description,
                                    count(issue_management_system.dbo.issue_occurrence.material_id) AS count 
                                    FROM issue_management_system.dbo.issue_occurrence,FLINTEC.dbo.FLINTEC$Item
                                    WHERE issue_occurrence.issue_issue_ID = 2
                                    AND issue_occurrence.location IN ('" + plantLocation + @"') AND 
                                    issue_management_system.dbo.issue_occurrence.material_id  COLLATE SQL_Latin1_General_CP1_CS_AS LIKE FLINTEC.dbo.FLINTEC$Item.No_ COLLATE SQL_Latin1_General_CP1_CS_AS
                                    AND issue_occurrence.issue_date BETWEEN '" + startDate +@"' AND '"+ endDate +@"' 
                                    GROUP BY FLINTEC.dbo.FLINTEC$Item.[Search Description]
                                    ORDER BY count Desc";

                    chart2Data = db.Database.SqlQuery<tempClass4>(query).ToList();
                    return Json(chart2Data, JsonRequestBehavior.AllowGet);
                }

                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            }
        }


        private class tempClass2
        {
            public DateTime issue_date { get; set; }
            public String issue { get; set; }
            public String machine_machine_id { get; set; }
            public String material_id { get; set; }
            public String Name { get; set; }
            public int DateDiff { get; set; }

        }

        [HttpPost]
        public JsonResult fillChart2(string startDate, string endDate, string plantLocation)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                String query = @"SELECT TOP 10
                                issue_management_system.dbo.issue_occurrence.issue_date,
                                issue_management_system.dbo.issues.issue,
                                issue_management_system.dbo.issue_occurrence.machine_machine_id,
                                issue_management_system.dbo.issue_occurrence.material_id,
                                BigRed.dbo.tbl_PPA_User.Name,
                                DATEDIFF(minute, issue_occurrence.issue_date, issue_occurrence.solved_date) AS DateDiff
                                FROM
                                issue_management_system.dbo.issue_occurrence,BigRed.dbo.tbl_PPA_User,issue_management_system.dbo.issues
                                WHERE issue_occurrence.location IN ('" + plantLocation + @"') AND 
                                BigRed.dbo.tbl_PPA_User.UserName LIKE issue_management_system.dbo.issue_occurrence.responsible_person_emp_id AND
                                issue_management_system.dbo.issue_occurrence.issue_issue_ID = issue_management_system.dbo.issues.issue_id 
                                 AND issue_occurrence.issue_date BETWEEN '" + startDate + @"' AND '" + endDate + @"' 
                                ORDER BY DateDiff DESC";

                var chart1Data = db.Database.SqlQuery<tempClass2>(query).ToList();

               

                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            }
        }

        private class tempClass3
        {
            public String issue { get; set; }
            public int count { get; set; }
        }

        [HttpPost]
        public JsonResult fillChart3(string startDate, string endDate, string plantLocation)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                String query = @"SELECT TOP 10 issues.issue , count(issue_issue_ID) AS 'count'
                                 FROM issue_occurrence,issues 
                                 WHERE issue_occurrence.location IN ('" + plantLocation + @"') AND  issue_date BETWEEN  '" + startDate + @"' AND '" + endDate + @"' 
                                 AND issues.issue_id = issue_occurrence.issue_issue_ID GROUP BY issue";

                var chart1Data = db.Database.SqlQuery<tempClass3>(query).ToList();
                return Json(chart1Data, JsonRequestBehavior.AllowGet); 
            }
        }

        private class tempClass5 {
            public DateTime issue_date {get; set;}
            public int issue_occurrence_id {get; set;}
            public String issue {get;set;}
            public String line_name {get; set;}
            public String issue_satus {get; set;}
            public String location { get; set; }
            public String description { get; set; }
            public int responsible_person_emp_id { get; set; }
        }

        [HttpPost]
        public JsonResult loadIssueList() {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                String query = @"SELECT issue_occurrence.issue_date,
                                 issue_occurrence.issue_occurrence_id,
                                 issues.issue,lines.line_name,issue_occurrence.issue_satus,
                                 issue_occurrence.location,issue_occurrence.description,issue_occurrence.responsible_person_emp_id
                                 FROM issue_occurrence,lines,issues WHERE
                                 lines.line_id = issue_occurrence.line_line_id AND
                                 issues.issue_id LIKE issue_occurrence.issue_issue_ID";

                var data = db.Database.SqlQuery<tempClass5>(query).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        } 

    }

}