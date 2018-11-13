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
            return View();
        }

        [HttpPost]
        public ActionResult addMap(String mapJSON,String department_id, String line, String ipAddress,String issues)
        {

            dbController db = dbController.getInstance();
            System.Diagnostics.Debug.WriteLine("Map json string:" + mapJSON);
            System.Diagnostics.Debug.WriteLine("ipAddress :" + ipAddress);


            //get line id refered by selected line name
            String query1 = "SELECT lines.line_id FROM lines WHERE lines.line_name LIKE '"+line+"'";
            String lineID = db.get_1st_column_1st_row_data(query1);

            //insert data in to line_map table
            String query2 = "INSERT INTO [dbo].[line_map]([line_id],[map],[red],[green],[yellow],[blue],issues)VALUES('"+lineID+"','"+mapJSON+"','0','0','0','0','"+issues+"')";
            db.runQuery_update_or_delete(query2);

            //get last added line id
            String added_lineID=null;
            String query3 = "SELECT TOP 1 lines.line_id FROM lines ORDER BY line_id DESC";
            SqlDataReader reader = db.runQuery_select(query3);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    added_lineID = reader.GetValue(0).ToString();
                }
                reader.Close();
            }

            reader.Close();

            String query4 = "INSERT INTO display(line_id,raspberry_ip_address) VALUES('"+added_lineID+"','"+ipAddress+"') ";
            db.runQuery_update_or_delete(query4);


            return Content("query executed", MediaTypeNames.Text.Plain);
        }


        [HttpPost]
        public ActionResult retrieveData() {

            return View();
            // return Json(persons, JsonRequestBehavior.AllowGet);
        }


        public ActionResult NotificationsManagement()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                List<User_tbl> userList = db.User_tbl.ToList();
                List<department> departments = db.departments.ToList();

                dynamic mymodel = new ExpandoObject();
                mymodel.users = userList;
                mymodel.departments = departments;

                return View(mymodel);
            }
        }

        [HttpPost]
        public ActionResult fillDepartmentDropDown(string person_name)
        {
            return Json("What", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult fillNameDropDown(string department)
        {
          using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                String query_1 = "SELECT CONCAT(User_tbl.EmployeeNumber,' - ',User_tbl.Name) AS Users FROM User_tbl WHERE User_tbl.Department='"+department+"'";
                var c = db.Database.SqlQuery<string>(query_1).ToList();
                return Json(c, JsonRequestBehavior.AllowGet);
            }
        }
        class user_temp {
            public string Name { get; set; }
            public string Department { get; set; }
            public string Position { get; set; }
            public string Phone { get; set; }
            public string EMail { get; set; }
            public int EmployeeNumber { get; set; }
        }

        [HttpPost]
        public ActionResult getUserDetails(string userID)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                String query_1 = "SELECT Name,Department,Position,Phone,EMail,EmployeeNumber FROM User_tbl WHERE User_tbl.EmployeeNumber ='"+userID+"'";
                var c = db.Database.SqlQuery<user_temp>(query_1).ToList();
                return Json(c, JsonRequestBehavior.AllowGet);
            }
        }

        class issue_line_person_temp
        {
            public string issue_id { get; set; }
            public string line_id { get; set; }
            public string EmployeeNumber { get; set; }
            public string assigned_date { get; set; }
            public string email { get; set; }
            public int call { get; set; }
            public int message { get; set; }
            public int callRepetitionTime { get; set; }
            public int sendAlertAfter { get; set; }
            public int levelOfResponsibility { get; set; }
            public int issue_line_person_id { get; set; }
        }

        [HttpPost]
        public ActionResult saveList(string userList_json)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                JArray list_user = JArray.Parse(userList_json) as JArray;


                foreach (JObject user in list_user) {

                    //get line id of particular line name
                    String line_name = user["line_id"].ToString();
                    var line = db.lines.Where(x => x.line_name == line_name).FirstOrDefault();

                    //get issue id of particular issue
                    String issue_name = user["issue_id"].ToString();
                    var issue = db.issues.Where(x => x.issue1 == issue_name).FirstOrDefault();

                    string query_1 = @"INSERT INTO [dbo].[issue_line_person]
                                     ([issue_id],[line_id],[EmployeeNumber],[assigned_date],[email],[call],
                                     [message],[callRepetitionTime],[sendAlertAfter],[levelOfResponsibility],[issue_line_person_id])
                                     VALUES("+issue.issue_id+","+line.line_id+","+user["EmployeeNumber"]+",'"+user["assigned_date"]+"',"
                                     +user["email"] + "," + user["call"] + "," + user["message"] + ",'" + user["callRepetitionTime"] + "','"
                                     +user["sendAlertAfter"]+"',"+user["levelOfResponsibility"]+","+user["issue_line_person_id"]+")";

                   System.Diagnostics.Debug.WriteLine(query_1);

                   db.Database.ExecuteSqlCommand(query_1);
                }

                return Content("List Saved", MediaTypeNames.Text.Plain);
            }
        }
    }
}