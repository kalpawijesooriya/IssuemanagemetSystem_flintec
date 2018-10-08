using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult SelectBranch()
        {
            return View();
        }
        public ActionResult Katunayake()
        {
            return View();
        }
        public ActionResult SelectSection()
        {
            return View();
        }
        public ActionResult Settings()
        {
            // issue_management_systemEntities db = new issue_management_systemEntities();
            // line line = db.line().Lines.SilgleOrdefault();
            return View();
        }

        [HttpPost]
        public ActionResult addMap(String mapJSON)
        {

            System.Diagnostics.Debug.WriteLine("map json string:"  +mapJSON);

            dbController db = dbController.getInstance();
            String query = "UPDATE line SET line.map='"+ mapJSON +"' where line_id = '1'";
            db.runQuery_update_or_delete(query);

            return View();
        }
    }
}