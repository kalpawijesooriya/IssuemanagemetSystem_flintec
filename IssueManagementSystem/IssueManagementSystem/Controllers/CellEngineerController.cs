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
        public ActionResult MachinBreakdown(int lineid)//Machine Breakedown view
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

        public ActionResult MaterialDelay(int lineid)//Material Delay View
        {   
            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");
            }

            dynamic jobCardsList_issue_occurrence = new System.Dynamic.ExpandoObject();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                jobCardsList_issue_occurrence.issue_occurrence = db.issue_occurrence;
            }

            FLINTEC_Prod_Order_Line_Context jobCardContext = new FLINTEC_Prod_Order_Line_Context();
            List<FLINTEC_Prod_Order_Line> jcList = jobCardContext.FLINTEC_Prod_Order_Line.Where(x => x.Status == 3).ToList();
            jobCardsList_issue_occurrence.jobCards = jcList;
            ViewBag.lineid = lineid;
            return View(jobCardsList_issue_occurrence);
        }

        [HttpPost]
        public ActionResult loadMaterialList(String lineID)
        {
            dynamic materials = new ExpandoObject();

/////////////////////////////////////////////////////////////////////////////
            LocationAdapter location = new LocationAdapter(System.Convert.ToInt32(lineID));

            String loc_ID = location.get_NAV_Location();

            if (loc_ID.Contains("/")) {
                var loc = loc_ID.Split('/');
                int i = 0;
                foreach (String x in loc) {
                    loc_ID = ((i > 0) ? (loc_ID + "','") : ("")) + x;
                    i++;
                }
            }

            String s_or_f = "";
            String line_name = location.get_LineName();
            if (line_name == "Straintec") {
                s_or_f = "STRAINTEC";
            }
            else {
                s_or_f = "FLINTEC";
            }
        
            ////////////////////////////////////////////////////////////////////////////////////////

            var sql_query = @"SELECT DISTINCT
                                poc.[Item No_] AS Item_No_ , '' AS Prod_Order_No_, poc.Description
                                FROM
                                FLINTEC.dbo.[" + s_or_f + @"$Prod_ Order Line] pol,
                                FLINTEC.dbo.[" + s_or_f + @"$Prod_ Order Component] poc
                                WHERE
                                poc.[Location Code] IN( '"+loc_ID+@"') AND
                                poc.[Prod_ Order No_] = pol.[Prod_ Order No_] AND
                                poc.Status = 3 ORDER BY Item_No_";

            using (FLINTEC_Prod_Order_Component_Context Context = new FLINTEC_Prod_Order_Component_Context())
            {
                List<FLINTEC_Prod_Order_Component> matList = Context.Database.SqlQuery<FLINTEC_Prod_Order_Component>(sql_query).ToList();
                materials.materials = matList;
            }

            //FLINTEC_Prod_Order_Component_Context jobCardContext = new FLINTEC_Prod_Order_Component_Context();
            //List<FLINTEC_Prod_Order_Component> matList = jobCardContext.FLINTEC_Prod_Order_Component.Where(x => (x.Prod_Order_No_.Equals(selectedJobCard))).ToList();
            //materials.materials = matList;
            return Json(materials);
        }

        [HttpPost]
        public ActionResult loadJobCardList(String item_No, Boolean loadAll, String lineID)
        {

            String sql_query = "";

            //////////////////////////////////////////////////////////////////////////////////////
            
            LocationAdapter location = new LocationAdapter(System.Convert.ToInt32(lineID));
            String loc_ID = location.get_NAV_Location();

            if (loc_ID.Contains("/"))
            {
                var loc = loc_ID.Split('/');
                int i = 0;
                foreach (String x in loc)
                {
                    loc_ID = ((i > 0) ? (loc_ID + "','") : ("")) + x;
                    i++;
                }
            }

            String s_or_f = "";
            String line_name = location.get_LineName();
            if (line_name == "Straintec")
            {
                s_or_f = "STRAINTEC";
            }
            else
            {
                s_or_f = "FLINTEC";
            }

            //////////////////////////////////////////////////////////////////////////////////////

            if (loadAll)
            {
                sql_query = @"SELECT pol.[Prod_ Order No_] AS Prod_Order_No_,pol.Status, pol.Description FROM FLINTEC.dbo.[" + s_or_f+@"$Prod_ Order Line] pol WHERE pol.Status = 3";
                //List<FLINTEC_Prod_Order_Line> jcList = jobCardContext.FLINTEC_Prod_Order_Line.Where(x => x.Status == 3).ToList();
            }
            else {
                sql_query = @"SELECT DISTINCT
                pol.[Prod_ Order No_] AS Prod_Order_No_,pol.Status, pol.Description
                FROM
                FLINTEC.dbo.[" + s_or_f + @"$Prod_ Order Line] pol,
                FLINTEC.dbo.[" + s_or_f + @"$Prod_ Order Component] poc
                WHERE
                poc.[Item No_] LIKE '"+item_No+@"' AND
                poc.[Prod_ Order No_] = pol.[Prod_ Order No_] AND
                poc.Status = 3 ORDER BY Prod_Order_No_";

            }

            dynamic jobCards = new ExpandoObject();

            using (FLINTEC_Prod_Order_Line_Context jobCardContext = new FLINTEC_Prod_Order_Line_Context())
            {
                List<FLINTEC_Prod_Order_Line> jobCardList = jobCardContext.Database.SqlQuery<FLINTEC_Prod_Order_Line>(sql_query).ToList();
                jobCards.jobCards = jobCardList;
            }

            return Json(jobCards);
        }

        [HttpPost]
        public JsonResult loadNonBOMMaterials()
        {
            dynamic materials = new ExpandoObject();
            FLINTEC_Context itemContext = new FLINTEC_Context();
            List<FLINTEC_Item> matList = itemContext.FLINTEC_Items.Where(x => (x.Major_Prod_Component.Equals(0))).ToList();
            materials.materials = matList;
            return Json(materials);
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


        public ActionResult QualityIssue(int lineid)//qualityISSUE View
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "CellEngineer"))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.lineid = lineid;
            return View();
        }


  

        [HttpPost]
        public ActionResult getCellEngLins(int empId)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "SELECT line_id FROM line_cell_eng WHERE cell_eng_emp_id=" + empId;
                var chart1Data = db.Database.SqlQuery<TempClasses.tempClass6>(query).ToList();
                return Json(chart1Data);
            }
        }

        [HttpPost]
        public ActionResult getCellEngLinName (int lineid)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                string query = "SELECT line_name FROM lines WHERE line_id=" + lineid;
                var lineData = db.Database.SqlQuery<TempClasses.tempClass7>(query).ToList();
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

        public ActionResult NotificationsManagement(int lineid)
        {
            ViewBag.lineid = lineid;

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
                dynamic mymodel = new ExpandoObject();
                mymodel.lineID = lineid;
                mymodel.users = userList;
                mymodel.departments = departments;
                return View(mymodel);
            }
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

        [HttpPost]
        public ActionResult getUserDetails(string userID)
        {
            using (BigRedEntities db = new BigRedEntities()) //method for load the map acordinto the surevisor line
            {
                string query_1 = "SELECT Name,Department,Role,Phone,EMail,EmployeeNumber FROM tbl_PPA_User WHERE tbl_PPA_User.EmployeeNumber ='" + userID + "'";
                var c = db.Database.SqlQuery<TempClasses.user_temp>(query_1).ToList();
                return Json(c, JsonRequestBehavior.AllowGet);
            }
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
           
                    //get issue id of particular issue
                    int issue_id = (Int32)user["issue_id"];
                    int lineid = (Int32)user["lineid"];
                    //var issuelist = db.issue_line_person.Where(x => x.issue_id == issue_id && x.line_id == lineid).ToList();

                    if ( delete)
                    {
                        try
                        {
                            string query = "DELETE FROM issue_line_person WHERE issue_id ="+ issue_id + "AND line_id ="+ lineid;
                            db.Database.ExecuteSqlCommand(query);
                        }

                        catch (Exception ex) { }
                        delete = false;
                    }
                    string query_1 = @"INSERT INTO [dbo].[issue_line_person]
                                     ([issue_id],[line_id],[EmployeeNumber],[assigned_date],[email],[call],
                                     [message],[callRepetitionTime],[sendAlertAfter],[levelOfResponsibility],[issue_line_person_id],[person_level])
                                     VALUES(" + issue_id + "," + lineid + "," + user["EmployeeNumber"] + ",'" + user["assigned_date"] + "',"
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