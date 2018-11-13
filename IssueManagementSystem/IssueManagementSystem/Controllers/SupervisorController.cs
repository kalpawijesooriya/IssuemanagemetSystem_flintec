﻿using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Net.Mime;
using Newtonsoft.Json.Linq;

namespace IssueManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {

        Communication com = new Communication();
        // GET: Supervisor
        public ActionResult selectIssue()// select issue view
        {
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewBag.LineId = mapInfo.line_id;
                return View();
            }

        }

        [HttpGet]
        public JsonResult GetIssues()
        {
            return Json(IssueService.GetIssue(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult MachinBreakdown()//machine breakedown view
        {
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim();//get the map arry to ViewData
                return View();
            }
        }

        public ActionResult TechnicalIssue()//Technical Issue View
        {
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();
            }

        }

        public ActionResult MaterialDelay()//MaterialDelay View
        {
            int userID = (int)Session["userID"];// get current supervisorID
            dynamic mat_List = new System.Dynamic.ExpandoObject();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                mat_List.issue_occurrence = db.issue_occurrence;
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
            }


            FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
            List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();


            mat_List.materialList = mList;

            return View(mat_List);
        }



        public ActionResult ITIssue()//IT ISSUE View
        {
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();

            }

        }

        [HttpPost] //add Breakedown to database
        public ActionResult AddIssueBreakedown(issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");//get today to string variable

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                    issueModel.responsible_person_confirm_status = 1;
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 1;//Issue id is 1 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 44;//get specific employee 
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.issue_date = date;
                    issueModel.location = (string)Session["location"];
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        string msg = line.line_name + " line IT/SoftWare issue has been occurred at " + date + ". Special Note of Line supervisor - " + issueModel.description;
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        com.lightON("1", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineInfo.line_line_id, 1, msg, "Machine Brakedown has been occurred");
                    }
                    ModelState.Clear();
                }
            }
            return RedirectToAction("selectIssue", "Supervisor");
        }

        [HttpPost]//add Tecnical Issues to database
        public ActionResult AddIssueTechnical(issue_occurrence issueModel)
        {

            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();

                    issueModel.responsible_person_confirm_status = 1;

                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 3;//Issue id is 2 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 44;//get specific employee 
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.issue_date = date;
                    issueModel.location = (string)Session["location"];
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        string msg = line.line_name + " line IT/SoftWare issue has been occurred at " + date + ". Special Note of Line supervisor - " + issueModel.description;
                        com.lightON("3", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineInfo.line_line_id, 3, msg, "Tecnical Issue has been occered");
                    }
                    ModelState.Clear();
                }
            }
            return RedirectToAction("selectIssue", "Supervisor");
        }

        [HttpPost]//add IT Issues to database
        public ActionResult AddIssueIT(issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                    issueModel.responsible_person_confirm_status = 1;
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 5;//Issue id is 5 for IT Issue
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.location = (string)Session["location"];
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.issue_date = date;

                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        string msg = line.line_name + " line IT/SoftWare issue has been occurred at " + date + ". Special Note of Line supervisor - " + issueModel.description;
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        com.lightON("5", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineInfo.line_line_id, 5, msg, "IT/Software Issue has been occered");
                    }
                    ModelState.Clear();
                }
            }
            return RedirectToAction("selectIssue", "Supervisor");
        }

        [HttpPost]//add Material Delay to database
        public ActionResult AddMaterialDelay(string issueJson)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                JArray issueData = JArray.Parse(issueJson) as JArray;
                System.Diagnostics.Debug.WriteLine(issueData);

                foreach (JObject item in issueData)
                {
                    int line_id = Int32.Parse(item["line_line_id"].ToString());
                    var resp_person = db.issue_line_person.Where(x => (x.levelOfResponsibility == 1) && (x.line_id == line_id)).FirstOrDefault();
                    try
                    {
                        var responsible_person_emp_id = resp_person.EmployeeNumber;

                        string query_1 = @"INSERT INTO [dbo].[issue_occurrence]
                                                      (  [issue_date]
                                                        ,[material_id]
                                                        ,[description]
                                                        ,[line_line_id]
                                                        ,[issue_issue_ID]
                                                        ,[responsible_person_emp_id]
                                                        ,[issue_satus]
                                                        ,[location]
                                                        ,[responsible_person_confirm_status])
                                                    VALUES
                                                        ('" + item["issue_date"] + "','"
                                                           + item["material_id"] + "','"
                                                           + item["description"] + "',"
                                                           + line_id + ","
                                                           + item["issue_issue_ID"] + ","
                                                           + resp_person.EmployeeNumber + ","
                                                           + item["issue_satus"] + ",'"
                                                           + item["location"] + "',"
                                                           + item["responsible_person_confirm_status"] + ")";
                        db.Database.ExecuteSqlCommand(query_1);
                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);
                    }
                }
                return Content("Material Delay Recorded", MediaTypeNames.Text.Plain);
            }
        }

        private void sendCD(int? line_line_id, int issueId, string msg, string subject)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
            var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Thread t = new Thread(() =>
            {
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                {
                    var userDetails = db.User_tbl.Where(x => x.Role == "manager").ToList();
                    foreach (var item in userDetails)
                    {
                        string query = "INSERT INTO tbl_Notifications ([Status],[Message],[Type],[EmployeeNumber],[Date]) VALUES( 1,'" + msg + "','issue','" + item.EmployeeNumber + "','" + date + "') ";
                        db.Database.ExecuteSqlCommand(query);
                    }

                    var communicationInfo = db.issue_line_person.Where(x => x.line_id == line_line_id && x.issue_id == issueId).ToList();
                    foreach (var item in communicationInfo)
                    {
                        var personInfo = db.User_tbl.Where(y => y.EmployeeNumber == item.EmployeeNumber).FirstOrDefault();
                        CommunicationData cd = new CommunicationData(personInfo.Phone, msg, personInfo.EMail, item.email, item.call, item.message, personInfo.EmployeeNumber, subject);
                        com.setCD(cd);
                    }

                }
            });
            t.Start();
        }

        //solovedIssueMethod
        public JsonResult SolvedIssue(int? issueId, int? issueOccourId)
        {
            System.Diagnostics.Debug.WriteLine("issueOccourId : " + issueOccourId);
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
            var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            //update Issueststus as 0
            issue_management_systemEntities1 db = new issue_management_systemEntities1();
            var issueoccourInfo = db.issue_occurrence.Where(x => x.issue_occurrence_id == issueOccourId).FirstOrDefault();
            issueoccourInfo.issue_satus = "0";
            issueoccourInfo.solved_date = date;
            db.SaveChanges();

            int userID = (int)Session["userID"];
            var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();

            var line_id = lineInfo.line_line_id;

            //get the list of Issuueoccurrence table

            List<issue_occurrence> issue = db.issue_occurrence.ToList();
            int count = 0;
            foreach (var item in issue)
            {    //check issue id == to clicked issueid
                if (item.issue_issue_ID == issueId && item.line_line_id == line_id)
                {
                    //if any status is there under selected issueid and line id cout will up
                    if (item.issue_satus == "1")
                    {
                        count++;
                    }
                }
            }
            if (count == 0)
            {// if cout ==0 light will off


                var issueCount = db.issue_occurrence.Where(x => x.issue_issue_ID == issueId && x.line_line_id == line_id && x.issue_satus == "1").ToList().Count();


                if (issueCount == 0)
                {// if cout ==0 light will off

                    var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                    com.lightOFF(issueId.ToString(), displayInfo.raspberry_ip_address);
                }
                return Json(true);
            }
            return Json(true);
        }
    }
}