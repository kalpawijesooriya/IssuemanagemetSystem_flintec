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

        public static int lineId;


        // GET: CellEngineer

        public ActionResult DashBord(int lineid)
        {
            ViewBag.lineId = lineid;
            return View(); 
        }
        public ActionResult MachinBreakdown(int lineid)//machine breakedown view
        {

            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }
            ViewBag.lineid = lineid;
            return View();
        }

        public ActionResult TechnicalIssue(int lineid)//Technical Issue View
        {

            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }
            ViewBag.lineid = lineid;
            return View();
        }

        public ActionResult MaterialDelay(int lineid)//MaterialDelay View
        {

            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }
      
            dynamic mat_List = new System.Dynamic.ExpandoObject();
            
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                mat_List.issue_occurrence = db.issue_occurrence;
               
            }


            FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
            List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();


            mat_List.materialList = mList;
            ViewBag.lineid = lineid;
            return View(mat_List);
        }



        public ActionResult ITIssue(int lineid)//IT ISSUE View
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }

            ViewBag.lineid = lineid;
            return View();


        }
        private class tempClass3
        {
            public int line_id { get; set; }
            
        }
        [HttpPost]
        public ActionResult getCellEngLins(int empId)
        {
         
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "SELECT line_id FROM line_cell_eng WHERE cell_eng_emp_id=" + empId;
         
                var chart1Data = db.Database.SqlQuery<tempClass3>(query).ToList();
                return Json(chart1Data);
            }
           
        }
        private class tempClass2
        {
            public string line_name { get; set; }

        }

        [HttpPost]
        public ActionResult getCellEngLinName (int lineid)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "SELECT line_name FROM lines WHERE line_id=" + lineid;

            
                var lineData = db.Database.SqlQuery<tempClass2>(query).ToList();
                return Json(lineData);
            }
        }


        public ActionResult QualtyIssue()// Qualty Issue View
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }


            return View();


        }


        public ActionResult NotificationsManagement()
        {

            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");

            }
            List<tbl_PPA_User> userList;
            using (BigRedEntities bigRed = new BigRedEntities())
            {
                userList = bigRed.tbl_PPA_User.ToList();
            }
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {

                List<department> departments = db.departments.ToList();

                Debug.Print(" .....    "+ CellEngineerController.lineId.ToString());

                dynamic mymodel = new ExpandoObject();
                mymodel.lineID = CellEngineerController.lineId;
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

        [HttpPost]
        public ActionResult getRespList(string line,string issue)
        {
            var c = new List<issue_line_person>(); 
            var c1 = new List<tbl_PPA_User>();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) 
            {
                string query_1 = "SELECT * FROM issue_line_person WHERE issue_line_person.issue_id = '"+ issue + "' AND issue_line_person.line_id = '"+ line + "' ORDER BY issue_line_person.levelOfResponsibility";
                Debug.Print(query_1);
                 c= db.Database.SqlQuery<issue_line_person>(query_1).ToList();
            }

            using (BigRedEntities db = new BigRedEntities()) 
            {
                foreach(issue_line_person item in c){

                    string query_1 = "SELECT * FROM tbl_PPA_User WHERE tbl_PPA_User.EmployeeNumber =" + item.EmployeeNumber;
                    tbl_PPA_User tempItem = db.Database.SqlQuery<tbl_PPA_User>(query_1).FirstOrDefault();
                    Debug.Print("######" + tempItem.ToString());
                    Debug.Print("###" + tempItem.Name);
                    c1.Add(tempItem);
                }
            }

            dynamic obj = new ExpandoObject();
            obj.issue_line_person = c;
            obj.userData = c1;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }




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
                Boolean delete = true;

                foreach (JObject user in list_user)
                {
                    //get line id of particular cell_eng
                    int user_id = Int32.Parse(user["user_id"].ToString());
                    var Cell_Eng_info = db.line_cell_eng.Where(x => x.cell_eng_emp_id == user_id).FirstOrDefault();

                    //get issue id of particular issue
                    int issue_id = (Int32)user["issue_id"];
                    
                    var issuelist = db.issue_line_person.Where(x => x.issue_id == issue_id && x.line_id == Cell_Eng_info.line_id).ToList();
                    
                    if (issuelist != null && delete)
                    {
                        foreach (var item in issuelist)
                        {
                            db.issue_line_person.Remove(item);
                            db.SaveChanges();
                        }
                        delete = false;
                    }
                    string query_1 = @"INSERT INTO [dbo].[issue_line_person]
                                     ([issue_id],[line_id],[EmployeeNumber],[assigned_date],[email],[call],
                                     [message],[callRepetitionTime],[sendAlertAfter],[levelOfResponsibility],[issue_line_person_id],[person_level])
                                     VALUES(" + issue_id + "," + Cell_Eng_info.line_id + "," + user["EmployeeNumber"] + ",'" + user["assigned_date"] + "',"
                                     + user["email"] + "," + user["call"] + "," + user["message"] + ",'" + user["callRepetitionTime"] + "','"
                                     + user["sendAlertAfter"] + "'," + user["levelOfResponsibility"] + "," + user["issue_line_person_id"] +","+ user["person_level"] + ")";

                    Debug.WriteLine(query_1);

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