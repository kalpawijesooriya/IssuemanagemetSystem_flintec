using IssueManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IssueManagementSystem.Controllers
{



    class  tempClass10
    {
        public int department_id { get; set; }
        public string department_name { get; set; }
        public int line_id { get; set; }
        public string line_name { get; set; }
        public string issues { get; set; }
    }


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
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5).Count();
                }

                return View();
            }
        }

        public JsonResult filterSelectBoxes() {
            //Departments //Lines //Issues
            string query1 = @"SELECT DISTINCT deps.department_id,deps.department_name,
                                lines.line_id,lines.line_name,l_map.issues AS issues
                                FROM issue_management_system.dbo.departments deps , issue_management_system.dbo.lines lines,
                                issue_management_system.dbo.line_map l_map
                                WHERE lines.department_id = deps.department_id AND l_map.line_id = lines.line_id";
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) {

                var d_obj1 = db.Database.SqlQuery<tempClass10>(query1).ToList();
                return Json(d_obj1);
            }
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

        //fill 
        [HttpPost]
        public JsonResult fillChart1(string barChart, string startDate, string endDate, string plantLocation)
        {
            var chart1Data = new List<TempClasses.tempClass>();
            var chart2Data = new List<TempClasses.tempClass4>();


                if (barChart.Equals("1") ) {


                    String query = @"SELECT TOP 10 issue_occurrence.machine_machine_id,
                                    count(issue_occurrence.machine_machine_id) AS count FROM issue_occurrence 
                                    WHERE issue_occurrence.issue_issue_ID = 1 AND issue_occurrence.location IN ('"+plantLocation+@"')
                                    AND  issue_occurrence.issue_date BETWEEN '"+startDate+@"' AND '"+endDate+@"' 
                                    GROUP BY issue_occurrence.machine_machine_id  ORDER BY count Desc";

                    System.Diagnostics.Debug.Print(query);
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                {
                    chart1Data = db.Database.SqlQuery<TempClasses.tempClass>(query).ToList();
                }
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

                try {

                    using (FLINTEC_Context db = new FLINTEC_Context())
                    {
                        chart2Data = db.Database.SqlQuery<TempClasses.tempClass4>(query).ToList();
                    }

                } catch (Exception ex) { }

                    return Json(chart2Data, JsonRequestBehavior.AllowGet);
                }
                return Json(chart1Data, JsonRequestBehavior.AllowGet);
            
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
                                WHERE issue_occurrence.location IN ('"+plantLocation+@"') AND 
                                BigRed.dbo.tbl_PPA_User.UserName LIKE issue_management_system.dbo.issue_occurrence.responsible_person_emp_id AND
                                issue_management_system.dbo.issue_occurrence.issue_issue_ID = issue_management_system.dbo.issues.issue_id 
                                AND issue_occurrence.issue_date BETWEEN '"+startDate+@"' AND '"+endDate+@"' 
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
                                 WHERE issue_occurrence.location IN ('"+plantLocation+@"') AND  issue_date BETWEEN  '" + startDate + @"' AND '" + endDate + @"' 
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

                String query1 = @"SELECT
                                issue_date,f.issue_occurrence_id,
                                issues.issue,lines.line_name,
                                issue_satus,f.location,
                                description,g.Name,
                                ''+material_id AS material_id,machine_machine_id,
                                line_line_id,issue_issue_ID,
                                responsible_person_confirm_status,responsible_person_confirm_feedback,
                                solved_date,commented_date,
                                f.department,(SELECT  a.Name FROM BigRed.dbo.tbl_PPA_User a  WHERE a.UserName LIKE f.solved_emp_id ) AS solved_emp,
                                (SELECT  i.Name FROM BigRed.dbo.tbl_PPA_User i WHERE i.UserName LIKE f.buzzer_off_by  ) AS buzzer_off_by,buzzer_off_time,
                                (lines.department_id)AS dep_occured,job_card

                                FROM 

                                issue_occurrence f,lines,issues,BigRed.dbo.tbl_PPA_User g

                                WHERE

                                lines.line_id = f.line_line_id AND issues.issue_id = f.issue_issue_ID
                                AND g.UserName LIKE f.responsible_person_emp_id AND f.issue_issue_ID = 2";



               String query2 = @"SELECT
                                issue_date,e.issue_occurrence_id,
                                issues.issue,lines.line_name,
                                issue_satus,e.location,
                                description,h.Name,
                                ''+material_id AS material_id,machine_machine_id,
                                line_line_id,issue_issue_ID,
                                responsible_person_confirm_status,responsible_person_confirm_feedback,
                                solved_date,commented_date,
                                e.department,(SELECT  a.Name FROM BigRed.dbo.tbl_PPA_User a  WHERE a.UserName LIKE e.solved_emp_id ) AS solved_emp,
                                (SELECT  i.Name FROM BigRed.dbo.tbl_PPA_User i WHERE i.UserName LIKE e.buzzer_off_by  ) AS buzzer_off_by,buzzer_off_time,
                                (lines.department_id)AS dep_occured,job_card

                                FROM

                                issue_occurrence e,lines,issues,BigRed.dbo.tbl_PPA_User h
                                WHERE

                                lines.line_id = e.line_line_id AND issues.issue_id = e.issue_issue_ID AND 
                                h.UserName LIKE e.responsible_person_emp_id AND
                                e.material_id IS NULL ORDER BY issue_date DESC";

                //List<TempClasses.tempClass5> data = db.Database.SqlQuery<TempClasses.tempClass5>(query).ToList();
                var cmd = db.Database.Connection.CreateCommand();
                cmd.Connection.Open();

                cmd.CommandText = query1;
                DataTable dt1 = new DataTable();
                dt1.Load(cmd.ExecuteReader());

                cmd.CommandText = query2;
                DataTable dt2 = new DataTable();
                dt2.Load(cmd.ExecuteReader());

                //DataRow[] check_val = dt1.Select("issue_occurrence_id = '"+5610+"'");

                dt1.Columns["material_id"].ReadOnly = false;
                dt1.Columns["material_id"].MaxLength = 100;
                List<TempClasses.tempClass5> dt1_data = new List<TempClasses.tempClass5>();


                foreach (DataRow row in dt1.Rows)
                {
                    using (FLINTEC_Context db1 = new FLINTEC_Context()) {
                        String query3 = "SELECT FLINTEC.dbo.FLINTEC$Item.Description FROM  FLINTEC.dbo.FLINTEC$Item WHERE FLINTEC.dbo.FLINTEC$Item.No_ = '" + (string)row["material_id"] + "'";
                        var cmd1 = db1.Database.Connection.CreateCommand();
                        cmd1.CommandText = query3;
                        cmd1.Connection.Open();
                        DbDataReader reader = cmd1.ExecuteReader();
                        while (reader.Read())
                        {
                            row.SetField("material_id", (string)row["material_id"] + " - " + reader[0]);
                        }

                        cmd1.Connection.Close();
                    }

                    var data_object = new TempClasses.tempClass5();
                    data_object.issue_date = (DateTime)row["issue_date"];//"issue_date"
                    data_object.issue_occurrence_id = (int)row["issue_occurrence_id"];//"issue_occurrence_id"
                    data_object.issue = (string)row["issue"];//"issue"
                    data_object.line_name = (string)row["line_name"];//"line_name"
                    data_object.issue_satus = (string)row["issue_satus"];//"issue_satus"
                    data_object.location = (string)row["location"];//"location"
                    data_object.description = DBNull.Value.Equals(row["description"]) ? null : (string)row["description"];//"description"
                    data_object.Name = (string)row["Name"];//"Name"
                    data_object.material_id = DBNull.Value.Equals(row["material_id"]) ? null : (string)row["material_id"];//"material_id"
                    data_object.machine_machine_id = DBNull.Value.Equals(row["machine_machine_id"]) ? null : (string)row["machine_machine_id"];//"machine_machine_id"
                    data_object.line_line_id = (int)row["line_line_id"];//"line_line_id"
                    data_object.issue_issue_ID = (int)row["issue_issue_ID"];//"issue_issue_ID"
                    data_object.responsible_person_confirm_status = (int)row["responsible_person_confirm_status"];//"responsible_person_confirm_status"
                    data_object.responsible_person_confirm_feedback = DBNull.Value.Equals(row["responsible_person_confirm_feedback"]) ? null : (string)row["responsible_person_confirm_feedback"];//"responsible_person_confirm_feedback"
                    data_object.solved_date = DBNull.Value.Equals(row["solved_date"]) ? (DateTime?)null : (DateTime)row["solved_date"];//"solved_date"
                    data_object.commented_date = DBNull.Value.Equals(row["commented_date"])? (DateTime?)null : (DateTime)row["commented_date"];//"commented_date"
                    data_object.department = row["department"] != DBNull.Value ? (string)row["department"] : "";//"department"
                    data_object.solved_emp = DBNull.Value.Equals(row["solved_emp"]) ?  null : (string)row["solved_emp"];//"solved_emp"
                    data_object.buzzer_off_by = DBNull.Value.Equals(row["buzzer_off_by"]) ?  null : (string)row["buzzer_off_by"];//"buzzer_off_by"
                    data_object.buzzer_off_time = DBNull.Value.Equals(row["buzzer_off_time"]) ? (DateTime?)null : (DateTime)row["buzzer_off_time"];//"buzzer_off_time"
                    data_object.dep_occured = (int)row["dep_occured"];//"dep_occured"
                    data_object.job_card = DBNull.Value.Equals(row["job_card"]) ? null :(string)row["job_card"];//"job_card"
                        
                    dt1_data.Add(data_object);
                }

                foreach (DataRow row in dt2.Rows)
                {
                   
                    var data_object = new TempClasses.tempClass5();
                    data_object.issue_date = (DateTime)row["issue_date"];//"issue_date"
                    data_object.issue_occurrence_id = (int)row["issue_occurrence_id"];//"issue_occurrence_id"
                    data_object.issue = (string)row["issue"];//"issue"
                    data_object.line_name = (string)row["line_name"];//"line_name"
                    data_object.issue_satus = (string)row["issue_satus"];//"issue_satus"
                    data_object.location = (string)row["location"];//"location"
                    data_object.description = DBNull.Value.Equals(row["description"]) ? null : (string)row["description"];//"description"
                    data_object.Name = (string)row["Name"];//"Name"
                    data_object.material_id = DBNull.Value.Equals(row["material_id"]) ? null : (string)row["material_id"];//"material_id"
                    data_object.machine_machine_id = DBNull.Value.Equals(row["machine_machine_id"]) ? null : (string)row["machine_machine_id"];//"machine_machine_id"
                    data_object.line_line_id = (int)row["line_line_id"];//"line_line_id"
                    data_object.issue_issue_ID = (int)row["issue_issue_ID"];//"issue_issue_ID"
                    data_object.responsible_person_confirm_status = (int)row["responsible_person_confirm_status"];//"responsible_person_confirm_status"
                    data_object.responsible_person_confirm_feedback = DBNull.Value.Equals(row["responsible_person_confirm_feedback"]) ? null : (string)row["responsible_person_confirm_feedback"];//"responsible_person_confirm_feedback"
                    data_object.solved_date = DBNull.Value.Equals(row["solved_date"]) ? (DateTime?)null : (DateTime)row["solved_date"];//"solved_date"
                    data_object.commented_date = DBNull.Value.Equals(row["commented_date"]) ? (DateTime?)null : (DateTime)row["commented_date"];//"commented_date"
                    data_object.department = row["department"] != DBNull.Value ? (string)row["department"] : "";//"department"
                    data_object.solved_emp = DBNull.Value.Equals(row["solved_emp"]) ? null : (string)row["solved_emp"];//"solved_emp"
                    data_object.buzzer_off_by = DBNull.Value.Equals(row["buzzer_off_by"]) ? null : (string)row["buzzer_off_by"];//"buzzer_off_by"
                    data_object.buzzer_off_time = DBNull.Value.Equals(row["buzzer_off_time"]) ? (DateTime?)null : (DateTime)row["buzzer_off_time"];//"buzzer_off_time"
                    data_object.dep_occured = (int)row["dep_occured"];//"dep_occured"
                    data_object.job_card = DBNull.Value.Equals(row["job_card"]) ? null : (string)row["job_card"];//"job_card"

                    dt1_data.Add(data_object);
                }
                //dt1_data.Sort("Column_name desc");
                cmd.Connection.Close();
                return Json(dt1_data, JsonRequestBehavior.AllowGet);
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