using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
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

        /*
         
        -->  Managers should be able to see how many machine have been broken in a certain time period
        -->  Machines with highest breakdown rate / Materials coursed for most issues
        -->  Current issues
        -->  History of solved issues (All)
                    ....How much of time has been taken to solve a issue
                    ....responsible person
                    ....sort by time
         --> 

         */
 

        private class tempClass
        {
            public String machine_machine_id { get; set; }
            public int count { get; set; }

        }

        [HttpPost]
        public JsonResult fillChart1()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1() )
            {
                String query = "SELECT issue_occurrence.machine_machine_id,count(issue_occurrence.machine_machine_id) AS count FROM issue_occurrence WHERE issue_occurrence.issue_issue_ID = 1 GROUP BY issue_occurrence.machine_machine_id  ORDER BY count Desc";

                var chart1Data = db.Database.SqlQuery<tempClass>(query).ToList();

                System.Diagnostics.Debug.Print("###############################################"+chart1Data[0]);

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
        public JsonResult fillChart2()
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
                                WHERE
                                BigRed.dbo.tbl_PPA_User.UserName LIKE issue_management_system.dbo.issue_occurrence.responsible_person_emp_id AND
                                issue_management_system.dbo.issue_occurrence.issue_issue_ID = issue_management_system.dbo.issues.issue_id
                                ORDER BY DateDiff DESC";

                var chart1Data = db.Database.SqlQuery<tempClass2>(query).ToList();

                System.Diagnostics.Debug.Print("###############################################" + chart1Data[0]);

                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            }
        }


        private class tempClass3
        {
            public String issue { get; set; }
            public int count { get; set; }
        }

        [HttpPost]
        public JsonResult fillChart3()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                String query = "SELECT TOP 10 issues.issue , count(issue_issue_ID) AS 'count' FROM issue_occurrence,issues WHERE issue_date BETWEEN '2018-12-18 00:00:00.000' AND '2018-12-19 00:00:00.000' AND issues.issue_id = issue_occurrence.issue_issue_ID GROUP BY issue";
                var chart1Data = db.Database.SqlQuery<tempClass3>(query).ToList();
                return Json(chart1Data, JsonRequestBehavior.AllowGet); 
            }
        }

    }
}