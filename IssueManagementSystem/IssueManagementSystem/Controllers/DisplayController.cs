using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class DisplayController : Controller
    {
       
      
        public ActionResult slect()
        {
            return View();
        }

 
        public ActionResult Rasp()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) {



                ViewBag.issueoccourInfo = db.issue_occurrence.Where(x => x.line_line_id == 3 && x.issue_satus=="1").ToList();
               
               
            }

            return View();
        }
        [HttpPost]//solovedIssueMethod
        public JsonResult updateScreen()
        {

            return Json(true);
        }
    }
}