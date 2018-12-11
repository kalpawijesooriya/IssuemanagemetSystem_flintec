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
    public class CellEngineerController : Controller
    {
        // GET: CellEngineer
        public ActionResult DashBord()
        {
            int userID = (int)Session["userID"];
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_cell_eng.Where(x => x.cell_eng_emp_id == userID).FirstOrDefault();
              
                ViewBag.LineId = lineInfo.line_id;
                return View();
            }
           
        }
        public ActionResult MachinBreakdown()//machine breakedown view
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_cell_eng.Where(x => x.cell_eng_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim();//get the map arry to ViewData
                return View();
            }
        }

        public ActionResult TechnicalIssue()//Technical Issue View
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                var lineInfo = db.line_cell_eng.Where(x => x.cell_eng_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_id).FirstOrDefault();
               // ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();
            }

        }

        public ActionResult MaterialDelay()//MaterialDelay View
        {
            int userID = (int)Session["userID"];// get current supervisorID
            dynamic mat_List = new System.Dynamic.ExpandoObject();
            
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                mat_List.issue_occurrence = db.issue_occurrence;
                var lineInfo = db.line_cell_eng.Where(x => x.cell_eng_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_id;
            }


            FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
            List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();


            mat_List.materialList = mList;

            return View(mat_List);
        }



        public ActionResult ITIssue()//IT ISSUE View
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                var lineInfo = db.line_cell_eng.Where(x => x.cell_eng_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_id;
                
                return View();

            }

        }

        public ActionResult NotificationsManagement()
        {
            List<tbl_PPA_User> userList;
            using (BigRedEntities bigRed = new BigRedEntities())
            {
                userList = bigRed.tbl_PPA_User.ToList();
            }
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

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

            return Json("9989", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult fillNameDropDown(string department)
        {
            using (BigRedEntities db = new BigRedEntities()) //method for load the map acordinto the surevisor line
            {
                string query_1 = "SELECT CONCAT(tbl_PPA_User.EmployeeNumber,' - ',tbl_PPA_User.Name) AS Users FROM tbl_PPA_User WHERE tbl_PPA_User.Department='" + department + "'";
                var c = db.Database.SqlQuery<string>(query_1).ToList();
                return Json(c, JsonRequestBehavior.AllowGet);
            }

        }

        //[HttpPost]
        //public ActionResult fillmanagerNameDropDown(string department)
        //{
        //    using (BigRedEntities db = new BigRedEntities()) //method for load the map acordinto the surevisor line
        //    {
        //        string query_1 = "SELECT CONCAT(tbl_PPA_User.EmployeeNumber,' - ',tbl_PPA_User.Name) AS Users FROM tbl_PPA_User WHERE tbl_PPA_User.Department='" + department + "'AND tbl_PPA_User.Role='manager'";
        //        var c = db.Database.SqlQuery<string>(query_1).ToList();
        //        return Json(c, JsonRequestBehavior.AllowGet);
        //    }

        //}
        class user_temp
        {

            public string Name { get; set; }
            public string Department { get; set; }
            public string Role { get; set; }
            public string Phone { get; set; }
            public string EMail { get; set; }
            public int EmployeeNumber { get; set; }

        }

        [HttpPost]
        public ActionResult getUserDetails(string userID)
        {

            using (BigRedEntities db = new BigRedEntities()) //method for load the map acordinto the surevisor line
            {
                string query_1 = "SELECT Name,Department,Role,Phone,EMail,EmployeeNumber FROM tbl_PPA_User WHERE tbl_PPA_User.EmployeeNumber ='" + userID + "'";
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


                foreach (JObject user in list_user)
                {
                    //get line id of particular cell_eng
                    int user_id = Int32.Parse(user["user_id"].ToString());
                    var Cell_Eng_info = db.line_cell_eng.Where(x => x.cell_eng_emp_id == user_id).FirstOrDefault();

                    //get issue id of particular issue
                    String issue_name = user["issue_id"].ToString();
                    var issue = db.issues.Where(x => x.issue1 == issue_name).FirstOrDefault();

                    string query_1 = @"INSERT INTO [dbo].[issue_line_person]
                                     ([issue_id],[line_id],[EmployeeNumber],[assigned_date],[email],[call],
                                     [message],[callRepetitionTime],[sendAlertAfter],[levelOfResponsibility],[issue_line_person_id],[person_level])
                                     VALUES(" + issue.issue_id + "," + Cell_Eng_info.line_id + "," + user["EmployeeNumber"] + ",'" + user["assigned_date"] + "',"
                                     + user["email"] + "," + user["call"] + "," + user["message"] + ",'" + user["callRepetitionTime"] + "','"
                                     + user["sendAlertAfter"] + "'," + user["levelOfResponsibility"] + "," + user["issue_line_person_id"] +","+ user["person_level"] + ")";

                    System.Diagnostics.Debug.WriteLine(query_1);

                    try
                        {
                          db.Database.ExecuteSqlCommand(query_1);
                        }

                    catch (Exception ex)
                        {
                           return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                        }
                }
                return Json("List Saved!", JsonRequestBehavior.AllowGet);
            }
        }
    }
}