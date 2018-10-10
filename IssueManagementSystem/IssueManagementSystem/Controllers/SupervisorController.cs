using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == 3).FirstOrDefault();
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim();
                return View();
               
            }
           
        }
    }
}