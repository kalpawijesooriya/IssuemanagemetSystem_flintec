using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class VoiceController : Controller
    {
        [HttpGet]
        public ActionResult getTodayIssues()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                DateTime startDateTime = DateTime.Today; //Today at 00:00:00
                DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

                var result = db.issue_occurrence.Where(a=>a.issue_date >= startDateTime && a.issue_date <= endDateTime).ToList();

              

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
    }
}