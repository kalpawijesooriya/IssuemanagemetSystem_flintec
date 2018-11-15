using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IssueManagementSystem.Controllers
{
    public class DisplayController : Controller
    {
       
      
        public ActionResult slect()
        {
            return View();
        }

 
        public ActionResult Rasp(int id)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) {


                ViewBag.id = id;
                ViewBag.issueoccourInfo = db.issue_occurrence.Where(x => x.line_line_id == id && x.issue_satus=="1").ToList();
               
               
            }

            return View();
        }
        [HttpPost]//solvedIssueMethod
        public JsonResult updateScreen()
        {

            return Json(true);
        }
        class machines_temp{
            public string machine_machine_id { get; set; }
        }

        [HttpPost]
        public ActionResult getBlinkingMachinesList(string line)
        {
            int lineID = Int32.Parse(line);

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) 
            {
                //List<issue_occurrence> c = db.issue_occurrence.Where(x=>x.line_line_id== lineID && x.issue_issue_ID == 1).ToList();
                //foreach (issue_occurrence x in c) { System.Diagnostics.Debug.WriteLine(x.machine_machine_id); }
                String query_1 = "SELECT issue_occurrence.machine_machine_id FROM issue_occurrence WHERE issue_occurrence.line_line_id ="+line+ " AND issue_occurrence.issue_issue_ID=1 AND issue_occurrence.issue_satus=1";
                var c = db.Database.SqlQuery<machines_temp>(query_1).ToList();

                return Json(c, JsonRequestBehavior.AllowGet);
            }
        }
    }
}