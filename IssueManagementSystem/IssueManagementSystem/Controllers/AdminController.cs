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

            issue_management_systemEntities1 imsDbContext = new issue_management_systemEntities1();
            List<machine> mList = imsDbContext.machines.ToList();

            dynamic machine_list = new System.Dynamic.ExpandoObject();
            machine_list.machine_list = mList;

            return View(machine_list);
        }

        [HttpPost]
        public ActionResult addMap(String mapJSON,String department_id, String line, String ipAddress,String issues)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) 
            {
                System.Diagnostics.Debug.WriteLine("Map json string:" + mapJSON);
                System.Diagnostics.Debug.WriteLine("ipAddress :" + ipAddress);
               
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