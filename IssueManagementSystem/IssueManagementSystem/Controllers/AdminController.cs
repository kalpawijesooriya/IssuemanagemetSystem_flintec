
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
         
          //  line line = db.line().Lines.SilgleOrdefault();
            return View();
        }


        
    }
}