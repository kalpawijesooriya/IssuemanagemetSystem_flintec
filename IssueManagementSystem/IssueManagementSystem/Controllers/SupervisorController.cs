using IssueManagementSystem.Models;
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

namespace IssueManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {
        Communication com = new Communication(); //make a object for communication class
        // GET: Supervisor
        public ActionResult selectIssue()// select issue view
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                int userID = (int)Session["userID"];
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                List<issue_occurrence> issue = db.issue_occurrence.ToList();
                
                   
                  
               
                
                return View(issue);
            }
               

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
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();

            }

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
        public ActionResult AddIssueBreakedown(IssueManagementSystem.Models.issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");//get today to string variable

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                   
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 1;//Issue id is 1 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        ViewBag.Success = "Inserted";
                        com.lightON("1", displayInfo.raspberry_ip_address);
                        com.sendMail();
                       // com.Send_SMS();
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("selectIssue", "Supervisor");
        }

        [HttpPost]//add Tecnical Issues to database
        public ActionResult AddIssueTechnical(IssueManagementSystem.Models.issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                  
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 3;//Issue id is 2 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); ;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        ViewBag.Success = "Inserted";
                        com.lightON("3", displayInfo.raspberry_ip_address);
                        com.sendMail();
                        com.Send_SMS();
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("selectIssue", "Supervisor");
        }

        [HttpPost]//add IT Issues to database
        public ActionResult AddIssueIT(IssueManagementSystem.Models.issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
          
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();

                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 5;//Issue id is 5 for IT Issue
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); ;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        ViewBag.Success = "Inserted";
                        com.lightON("5", displayInfo.raspberry_ip_address);
                        com.sendMail();
                        com.Send_SMS();
                        
                       
                    }
                    ModelState.Clear();
                }

            }

            return null;
        }

        [HttpPost]//solovedIssueMethod
        public JsonResult SolvedIssue(int? issueId,int? issueOccourId)
        {  //update Issueststus as 0
            issue_management_systemEntities1 db = new issue_management_systemEntities1();
            var issueoccourInfo= db.issue_occurrence.Where(x => x.issue_occurrence_id == issueOccourId).FirstOrDefault();
            issueoccourInfo.issue_satus = "0";
            db.SaveChanges();

            int userID = (int)Session["userID"];
            var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();

            var line_id = lineInfo.line_line_id;


            //get the list of Issuueoccurrence table
            List<issue_occurrence> issue = db.issue_occurrence.ToList();
            int count = 0;
            foreach(var item in issue )
            {    //check issue id == to clicked issueid
                if (item.issue_issue_ID== issueId && item.line_line_id==line_id) { 
                  //if any status is there under selected issueid and line id cout will up
                if (item.issue_satus=="1")
                {
                    count++;
                }
                }
            }
            if (count == 0) {// if cout ==0 light will off
                var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                com.lightOFF(issueId.ToString(), displayInfo.raspberry_ip_address);
            }
            return Json(true);
        }  
       
    }
}