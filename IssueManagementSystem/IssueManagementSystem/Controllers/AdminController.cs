using IssueManagementSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IssueManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
     
        public ActionResult Index()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {

            string location = Session["location"].ToString();
            ViewBag.BrakedownCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 1 && location == x.location).Count();
            ViewBag.MaterialDelayCount = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 2 && x.location == location).Count();
            ViewBag.TechnicalIssue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 3 && x.location == location).Count();
            ViewBag.QualityIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 4 && x.location == location).Count();
            ViewBag.ITIsuue = db.issue_occurrence.Where(x => x.issue_satus == "1" && x.issue_issue_ID == 5 && x.location == location).Count();
              
            }
            return View();
        }
      
        public ActionResult Settings()
        {
            // issue_management_systemEntities db = new issue_management_systemEntities();
            // line line = db.line().Lines.SilgleOrdefault();

            BigRedEntities imsDbContext = new BigRedEntities();
            List<tblWorkstation_Config> mList = imsDbContext.tblWorkstation_Config.ToList();

            dynamic machine_list = new ExpandoObject();
            machine_list.machine_list = mList;

            return View(machine_list);
        }

        [HttpPost]
        public ActionResult addMap(String mapJSON,String department_id, String line, String ipAddress,String issues)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) 
            {
                Debug.WriteLine("Map json string:" + mapJSON);
                Debug.WriteLine("ipAddress :" + ipAddress);
               
                //get line id refered by selected line name
                var lineInfo = db.lines.Where(x => x.line_name == line).FirstOrDefault();
                int lineID = lineInfo.line_id;
              
                //insert data in to line_map table
                string query = "INSERT INTO [dbo].[line_map]([line_id],[map],[red],[green],[yellow],[blue],issues)VALUES('" + lineID + "','" + mapJSON + "','0','0','0','0','" + issues + "')";
                db.Database.ExecuteSqlCommand(query);

                string query1 = "INSERT INTO display(line_id,raspberry_ip_address) VALUES('" + lineID + "','" + ipAddress + "') ";
                db.Database.ExecuteSqlCommand(query1);

            }

            return Content("query executed", MediaTypeNames.Text.Plain);
        }


        [HttpPost]
        public ActionResult retrieveData() {

            return View();
            // return Json(persons, JsonRequestBehavior.AllowGet);
        }


    
      



      
        
    }
}