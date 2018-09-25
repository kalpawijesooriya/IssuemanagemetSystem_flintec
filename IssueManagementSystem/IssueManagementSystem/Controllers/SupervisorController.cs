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
    }
}