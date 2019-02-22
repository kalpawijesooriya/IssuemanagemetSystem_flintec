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
                    ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1).Count();
                    ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2).Count();
                    ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3).Count();
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4 ).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5 ).Count();
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

        [HttpPost]
        public JsonResult fillChart1(string barChart, string startDate, string endDate, string plantLocation)
        {
            var chart1Data = new List<TempClasses.tempClass>();
            var chart2Data = new List<TempClasses.tempClass4>();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (barChart.Equals("1") ) {
                    String query = @"SELECT TOP 10 issue_occurrence.machine_machine_id,
                                    count(issue_occurrence.machine_machine_id) AS count FROM issue_occurrence 
                                    WHERE issue_occurrence.issue_issue_ID = 1 AND issue_occurrence.location IN ('"+plantLocation+@"')
                                    AND  issue_occurrence.issue_date BETWEEN '"+startDate+@"' AND '"+endDate+@"' 
                                    GROUP BY issue_occurrence.machine_machine_id  ORDER BY count Desc";

                    System.Diagnostics.Debug.Print(query);
                    chart1Data = db.Database.SqlQuery<TempClasses.tempClass>(query).ToList();
                    return Json(chart1Data, JsonRequestBehavior.AllowGet);
                }

                if (barChart.Equals("2")) {
                    String query = @"SELECT TOP 10 FLINTEC.dbo.FLINTEC$Item.[Search Description] AS Search_Description,
                                    count(issue_management_system.dbo.issue_occurrence.material_id) AS count 
                                    FROM issue_management_system.dbo.issue_occurrence,FLINTEC.dbo.FLINTEC$Item
                                    WHERE issue_occurrence.issue_issue_ID = 2
                                    AND issue_occurrence.location IN ('"+plantLocation+@"') AND 
                                    issue_management_system.dbo.issue_occurrence.material_id  COLLATE SQL_Latin1_General_CP1_CS_AS LIKE FLINTEC.dbo.FLINTEC$Item.No_ COLLATE SQL_Latin1_General_CP1_CS_AS
                                    AND issue_occurrence.issue_date BETWEEN '"+startDate+@"' AND '"+endDate+@"' 
                                    GROUP BY FLINTEC.dbo.FLINTEC$Item.[Search Description]
                                    ORDER BY count Desc";

                    chart2Data = db.Database.SqlQuery<TempClasses.tempClass4>(query).ToList();
                    return Json(chart2Data, JsonRequestBehavior.AllowGet);
                }
                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            }
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

                var chart1Data = db.Database.SqlQuery<TempClasses.tempClass2>(query).ToList();
                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            }
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

                var chart1Data = db.Database.SqlQuery<TempClasses.tempClass3>(query).ToList();
                return Json(chart1Data, JsonRequestBehavior.AllowGet); 
            }
        }

        [HttpPost]
        public JsonResult loadIssueList() {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                String query = @"SELECT(lines.department_id)AS dep_occured,issues.issue,lines.line_name,f.issue_occurrence_id,issue_date,g.Name,
                                CONCAT(FLINTEC.dbo.FLINTEC$Item.No_,'  ',FLINTEC.dbo.FLINTEC$Item.Description) AS material_id,description,
                                machine_machine_id,line_line_id,issue_issue_ID,issue_satus,f.location,responsible_person_confirm_status,
                                responsible_person_confirm_feedback,solved_date,commented_date,f.department,
                                (SELECT  a.Name FROM BigRed.dbo.tbl_PPA_User a  WHERE a.UserName LIKE f.solved_emp_id ) AS solved_emp,
                                (SELECT  i.Name FROM BigRed.dbo.tbl_PPA_User i WHERE i.UserName LIKE f.buzzer_off_by  ) AS buzzer_off_by
                                ,buzzer_off_time,job_card

                                FROM 

                                issue_occurrence f,lines,issues,FLINTEC.dbo.FLINTEC$Item,BigRed.dbo.tbl_PPA_User g

                                WHERE

                                lines.line_id = f.line_line_id AND issues.issue_id = f.issue_issue_ID AND
                                (FLINTEC.dbo.FLINTEC$Item.No_ COLLATE SQL_Latin1_General_CP1_CS_AS LIKE f.material_id COLLATE SQL_Latin1_General_CP1_CS_AS)
                                AND g.UserName LIKE f.responsible_person_emp_id
 
                                UNION 

                                SELECT(lines.department_id)AS dep_occured,issues.issue,lines.line_name,e.issue_occurrence_id,issue_date,h.Name,
                                (NULL) AS material_id,description,machine_machine_id,line_line_id,issue_issue_ID,issue_satus,
                                e.location,responsible_person_confirm_status,responsible_person_confirm_feedback,solved_date,commented_date,
                                e.department,
                                (SELECT  c.Name FROM BigRed.dbo.tbl_PPA_User c WHERE c.UserName LIKE e.solved_emp_id  ) AS solved_emp,
                                (SELECT  i.Name FROM BigRed.dbo.tbl_PPA_User i WHERE i.UserName LIKE e.buzzer_off_by  ) AS buzzer_off_by
                                ,buzzer_off_time,job_card

                                FROM

                                issue_occurrence e,lines,issues,BigRed.dbo.tbl_PPA_User h
                                WHERE

                                lines.line_id = e.line_line_id AND issues.issue_id = e.issue_issue_ID AND 
                                h.UserName LIKE e.responsible_person_emp_id AND
                                e.material_id IS NULL ORDER BY issue_date DESC";

                var data = db.Database.SqlQuery<TempClasses.tempClass5>(query).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult notificationOnOff(string issue_line_person_id, string issue_occurrence_id, string status)
        {
            /*
              String query = @"UPDATE c SET  c.email = 0,c.call = 0,c.message = 0 FROM issue_line_person AS c ,issues,lines
                  WHERE c.assigned_date = (
                  SELECT MAX(d.assigned_date)
                  FROM issue_line_person d,lines,issues 
                  WHERE issues.issue_id = d.issue_id 
                  AND lines.line_id = d.line_id
                  AND lines.line_name ='"+line+@"' AND issues.issue = '"+issue+ @"' AND d.issue_line_person_id='"+issue_line_person_id+@"'
                  )AND lines.line_name ='" + line+@"' AND issues.issue ='"+issue+ @"' AND c.issue_line_person_id='"+issue_line_person_id+@"'";
            */
            String query = @"UPDATE notification_handling 
                            SET 
                            notification_handling.notification_status="+status+@"
                            WHERE 
                            notification_handling.issue_occurrence_id='"+issue_occurrence_id+@"' AND 
                            notification_handling.EmployeeNumber='"+issue_line_person_id+@"'";

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                db.Database.ExecuteSqlCommand(query);
            }

           return Json("Changes Updated", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult checkNotificationList_formanagers(int empID, string issueOccID) {


            String query = @"SELECT CASE WHEN
                            ("+empID+@" IN(SELECT notification_handling.EmployeeNumber 
                                  FROM notification_handling 
                                  WHERE notification_handling.issue_occurrence_id = "+issueOccID+@"  
                                  AND notification_handling.notification_status=1))
                            THEN CAST(1 AS BIT)
                            ELSE CAST(0 AS BIT) END";
            Boolean resultVar;
                 
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                resultVar = db.Database.SqlQuery<Boolean>(query).Single();
            }
            return Json(resultVar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult totalNumberOfIssues(string startDate, string endDate) {

            String query = @"SELECT COUNT(issue_occurrence.issue_occurrence_id) AS issuesCount 
                                FROM issue_occurrence 
                                WHERE issue_occurrence.issue_date 
                                BETWEEN '"+ startDate + @"' AND '"+ endDate + @"'";

            int resultVar;

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                resultVar = db.Database.SqlQuery<int>(query).Single();
            }
            return Json(resultVar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult voiceDataFilter(String issue, String date) {

            String query = @"";
            List<String> resultVar;

            System.Diagnostics.Debug.Print("issue:"+issue+" date:"+date);

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                resultVar = db.Database.SqlQuery<String>(query).ToList();
            }

            return Json(resultVar, JsonRequestBehavior.AllowGet);
        }
    }
}