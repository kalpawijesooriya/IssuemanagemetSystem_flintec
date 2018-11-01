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

        Communication com = new Communication();
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
                   
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 1;//Issue id is 1 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.date_time = date;
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
                  
                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 3;//Issue id is 2 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.date_time = date;
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

                    issueModel.line_line_id = lineInfo.line_line_id;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 5;//Issue id is 5 for IT Issue
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.location = (string)Session["location"];
                    var date=DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); 
                    issueModel.date_time = date;

                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault(); 
                        string msg = line.line_name + " line IT/SoftWare issue has been occurred at "+date+". Special Note of Line supervisor - "+ issueModel.description;
                        var displayInfo = db.displays.Where(x => x.line_id == lineInfo.line_line_id).FirstOrDefault();
                        com.lightON("5", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineInfo.line_line_id, 5,msg,"IT/Software Issue has been occered");
                       
                    }
                    ModelState.Clear();
                }
            }
            return RedirectToAction("selectIssue", "Supervisor");
        }


        private void sendCD(int? line_line_id, int issueId,string msg,string subject)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
            var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
           
           
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
                    var personInfo=db.User_tbl.Where(y => y.EmployeeNumber==item.EmployeeNumber).FirstOrDefault();
                    CommunicationData cd = new CommunicationData(personInfo.Phone,msg,personInfo.EMail,item.email,item.call,item.message,personInfo.EmployeeNumber, subject);
                    com.setCD(cd);

                }
            }

        [HttpPost]//solovedIssueMethod
        public JsonResult SolvedIssue(int? issueId,int? issueOccourId)
            {  
                //update Issueststus as 0
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
                    //com.lightOFF(issueId.ToString(), displayInfo.raspberry_ip_address);
                }
                return Json(true);
            }
    }
}