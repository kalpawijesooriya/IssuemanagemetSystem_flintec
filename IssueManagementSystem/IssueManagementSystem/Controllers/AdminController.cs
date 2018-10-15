
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mime;
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
            return View();

        }
    }
}