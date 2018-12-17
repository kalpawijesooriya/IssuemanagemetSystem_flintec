using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if ((string)Session["department"] == "MD")
                {
                    ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1).Count();
                    ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2).Count();
                    ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3).Count();
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5).Count();
                }
                else
                {
                    string location = Session["location"].ToString();
                    ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1 && location == x.location).Count();
                    ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2 && x.location == location).Count();
                    ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3 && x.location == location).Count();
                    ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4 && x.location == location).Count();
                    ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5 && x.location == location).Count();
                }
            }
                return View();
        }
        [HttpGet]
        public JsonResult GetNotification()
        {
            return Json(NotificaionService.GetNotification(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult hideNotification(int? notificationId)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "UPDATE tbl_Notifications SET Status = 0 WHERE NotificationId = "+ notificationId ;
                db.Database.ExecuteSqlCommand(query);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult drawLineChart()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
               // string query = "";
               // db.Database.ExecuteSqlCommand(query);
            }

            /*
              data.addRows(
             [
             ['Jan', 37.8, 80.8, 41.8, 37.8, 80.8, 20],
             ['Feb', 30.9, 69.5, 32.4, 37.8, 80.8, 20],
             ['Mar', 25.4, 57, 25.7, 37.8, 80.8, 20],
             ['Apr', 11.7, 18.8, 10.5, 37.8, 80.8, 20],
             ['May', 5.3, 7.9, 4.7, 37.8, 80.8, 20],
             ['Jun', 11.9, 17.6, 10.4, 37.8, 80.8, 20],
             ['Jul', 8.8, 13.6, 7.7, 37.8, 80.8, 20],
             ['Aug', 7.6, 12.3, 9.6, 37.8, 80.8, 20],
             ['Sep', 12.3, 29.2, 10.6, 37.8, 80.8, 20],
             ['Oct', 16.9, 42.9, 14.8, 37.8, 80.8, 20],
             ['Nov', 12.8, 30.9, 11.6, 37.8, 80.8, 20],
             ['Dec', 6.6, 8.4, 5.2, 37.8, 80.8, 20]
             ]
         );
         */
           // List<String> mList = imsDbContext.tblWorkstation_Config.ToList();



            //       var c = "{'['['Jan', 37.8, 80.8, 41.8, 37.8, 80.8, 20]','['Feb', 30.9, 69.5, 32.4, 37.8, 80.8, 20]','['Mar', 25.4, 57, 25.7, 37.8, 80.8, 20]']'}";

            return Json("", JsonRequestBehavior.AllowGet);
        }

        /*
        -->  Managers should be able to see how many machine have been broken in a certain time period
        -->  Machines with highest breakdown rate / Materials coursed for most issues
        -->  Current issues
        -->  History of solved issues (All)
                    ....How much of time has been taken to solve a issue
                    ....responsible person
                    ....sort by time
         --> 
         */

    }
}