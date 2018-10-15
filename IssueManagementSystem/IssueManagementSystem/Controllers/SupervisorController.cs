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

namespace IssueManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {
      
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
                        ViewBag.Success = "Inserted";
                        LightON("1");//call to LightON fuction
                        sendMail();
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("MachinBreakdown", "Supervisor");
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
                    issueModel.issue_issue_ID = 2;//Issue id is 2 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); ;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        ViewBag.Success = "Inserted";
                        LightON("2");//call to LightON fuction
                        sendMail();
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("TechnicalIssue", "Supervisor");
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
                string url = "http://192.168.137.104/led.php?off=" + issueId.ToString();
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webReq.Method = "GET";
                HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
                
            }
            return Json(true);
        }

        //TurnOn Specific Light To Machine Breakedown
        public void LightON(String Light)
        {
            string url = "http://192.168.137.104/led.php?on=" + Light;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }

        private void sendMail()
        {
            using (MailMessage mm = new MailMessage("flintec.ism.alerts@gmail.com", "kalpa.wijesooriya94@gmail.com"))
            {
                mm.Subject = "machine breakedown Notification";
                mm.Body = "This is test message";

                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("	flintec.ism.alerts@gmail.com", "ism@flintec");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }

        }


    }
}