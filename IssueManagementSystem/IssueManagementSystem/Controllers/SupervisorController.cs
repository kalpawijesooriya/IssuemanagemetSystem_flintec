using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
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
                    issueModel.issue_satus = "";
                    issueModel.issue_issue_ID = 1;//Issue id is 1 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        ViewBag.Success = "Inserted";
                        LightON("1");//call to LightON fuction
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("MachinBreakdown", "Supervisor");
        }

        [HttpPost]
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
                    issueModel.issue_satus = "";
                    issueModel.issue_issue_ID = 2;//Issue id is 2 for Machine Brakedown
                    issueModel.responsible_person_emp_id = 5;//get specific employee 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); ;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        ViewBag.Success = "Inserted";
                        LightON("2");//call to LightON fuction
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("TechnicalIssue", "Supervisor");
        }

        //TurnOn Specific Light To Machine Breakedown
        public void LightON (String Light)
        {
            string url = "http://192.168.137.104/led.php?on="+ Light;
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }
        [HttpPost]
        public JsonResult SolvedIssue(int? issueId,int? issueOccourId)
        {   

            string url = "http://192.168.137.104/led.php?off="+ issueId.ToString();
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
            return Json(true);
        }

    }
}