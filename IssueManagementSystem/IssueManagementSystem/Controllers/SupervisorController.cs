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
        public ActionResult selectIssue()
        {
            return View();

        }
        public ActionResult MachinBreakdown()
        {
            int userID = (int)Session["userID"];
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim();
                return View();

            }

        }
        [HttpPost]
        public ActionResult AddIssue(IssueManagementSystem.Models.issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    issueModel.issue_satus = "";
                    issueModel.issue_issue_ID = 1;
                    issueModel.responsible_person_emp_id = 5;//get specific employe 
                    issueModel.date_time = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);;
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        ViewBag.Success = "Inserted";
                        LightON(1);//call to LightON fuction
                    }
                    ModelState.Clear();
                }

            }

            return RedirectToAction("MachinBreakdown", "Supervisor");
        }

        //TurnOn Specific Light To Machine Breakedown
        public void LightON (int machineID)
        {
         
            string url = "http://192.168.137.104/led.php?on=1";
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webReq.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        }

    }
}