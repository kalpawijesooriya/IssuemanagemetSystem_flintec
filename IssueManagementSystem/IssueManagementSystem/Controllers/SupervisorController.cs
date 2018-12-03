using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Net.Mime;
using Newtonsoft.Json.Linq;

namespace IssueManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {

        Communication com = new Communication();
        // GET: Supervisor
        public ActionResult selectIssue()// select issue view
        {
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewBag.LineId = mapInfo.line_id;
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetIssues()
        {
            return Json(IssueService.GetIssue(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult MachinBreakdown()//machine breakedown view
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
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

                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();
            }

        }

        public ActionResult MaterialDelay()//MaterialDelay View
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            dynamic mat_List = new System.Dynamic.ExpandoObject();

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                mat_List.issue_occurrence = db.issue_occurrence;
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
            }

            FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
            List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();

            mat_List.materialList = mList;

            return View(mat_List);
        }

        public ActionResult ITIssue()//IT Issue view
        {
            ViewBag.rol = Session["Role"];
            int userID = (int)Session["userID"];// get current supervisorID
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                var lineInfo = db.line_supervisor.Where(x => x.supervisor_emp_id == userID).FirstOrDefault();
                ViewBag.lineID = lineInfo.line_line_id;
                var mapInfo = db.line_map.Where(y => y.line_id == lineInfo.line_line_id).FirstOrDefault();
                ViewData["map"] = mapInfo.map.ToString().Trim(); //get the map arry to ViewData
                return View();
            }

        }

        [HttpPost] //add Breakedown to database
        public ActionResult AddIssueBreakedown(issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");//get today to string variable

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int userID = (int)Session["userID"];
                    int lineId = Int32.Parse(issueModel.lineid);
                    issueModel.responsible_person_confirm_status = 1;
                    issueModel.line_line_id = lineId;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 1;//Issue id is 1 for Machine Brakedown

                    var respPersonID = db.issue_line_person.Where(x => x.line_id == lineId && x.levelOfResponsibility == 1 && x.issue_id == 1).FirstOrDefault();
                    issueModel.responsible_person_emp_id = Int32.Parse(respPersonID.EmployeeNumber.ToString());

                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    issueModel.issue_date = date;
                    issueModel.location = (string)Session["location"];
                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineId).FirstOrDefault();
                        string msg = line.line_name + " line Breakedown has been occurred on " + day +" at "+time1+".  Note- " + issueModel.description;
                        string callNote = line.line_name + " line Breakedown has been occurred on" + day + " at "+time1;
                        var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
                        com.lightON("1", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineId, 1, msg, "Machine Brakedown has been occurred", callNote);
                    }
                    ModelState.Clear();
                }
            }
            if (issueModel.Role == "supervisor") { return RedirectToAction("selectIssue", "Supervisor"); }
            else { return RedirectToAction("DashBord", "CellEngineer"); }
        }

        [HttpPost]//add Tecnical Issues to database
        public ActionResult AddIssueTechnical(issue_occurrence issueModel)
        {

            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                { 
                    try
                    {
                        int userID = (int)Session["userID"];
                        int lineId = Int32.Parse(issueModel.lineid);

                        issueModel.responsible_person_confirm_status = 1;

                        issueModel.line_line_id = lineId;
                        issueModel.issue_satus = "1";
                        issueModel.issue_issue_ID = 3;//Issue id is 2 for Machine Brakedown

                        var respPersonID = db.issue_line_person.Where(x => x.line_id == lineId && x.levelOfResponsibility == 1 && x.issue_id == 3).FirstOrDefault();
                        issueModel.responsible_person_emp_id = Int32.Parse(respPersonID.EmployeeNumber.ToString());

                        var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        var dayitem = current_time.Split(' ');
                        var day = dayitem[0];
                        var time1 = dayitem[1];
                        issueModel.issue_date = date;
                        issueModel.location = (string)Session["location"];
                        db.issue_occurrence.Add(issueModel);
                        db.SaveChanges();
                        if (issueModel.issue_occurrence_id > 0)
                        {

                            var line = db.lines.Where(x => x.line_id == lineId).FirstOrDefault();
                            var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
                            string msg = line.line_name + " line Technical issue has been occurred at " + date + " at " + time1 + ". Special Note of Line supervisor - " + issueModel.description;
                            string callNote = line.line_name + " line Breakedown has been occurred on" + day + " at " + time1;
                            com.lightON("3", displayInfo.raspberry_ip_address);//turn on the Light
                            sendCD(lineId, 3, msg, "Tecnical Issue has been occered", callNote);

                            ModelState.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error occured ############00000000###############" + ex.ToString());

                    }
                    

                }


                if (issueModel.Role == "supervisor") { return RedirectToAction("selectIssue", "Supervisor"); }
                else { return RedirectToAction("DashBord", "CellEngineer"); }

            }
        }
        [HttpPost]//add IT Issues to database
        public ActionResult AddIssueIT(issue_occurrence issueModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    int lineId = Int32.Parse(issueModel.lineid);
                    int userID = (int)Session["userID"];
                   
                    issueModel.responsible_person_confirm_status = 1;
                    issueModel.line_line_id = lineId;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 5;//Issue id is 5 for IT Issue

                    //get certain employee assigned for a certain issue in a certain line and the level of resp. should be one
                    var respPersonID = db.issue_line_person.Where(x => x.line_id == lineId && x.levelOfResponsibility == 1 && x.issue_id == 5).FirstOrDefault();
                    issueModel.responsible_person_emp_id = Int32.Parse(respPersonID.EmployeeNumber.ToString());

                    issueModel.location = (string)Session["location"];
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    issueModel.issue_date = date;

                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineId).FirstOrDefault();
                        string msg = line.line_name + " line IT/SoftWare issue has been occurred on " + day + " at "+ time1 + ". Special Note of Line supervisor - " + issueModel.description;
                        string callNote = line.line_name + " line Breakedown has been occurred on" + day + " at " + time1;
                        var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
                        com.lightON("5", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineId, 5, msg, "IT/Software Issue has been occered", callNote);
                    }
                    ModelState.Clear();
                }
            }
            if (issueModel.Role == "supervisor") { return RedirectToAction("selectIssue", "Supervisor"); }
            else { return RedirectToAction("DashBord", "CellEngineer"); }
        }

        [HttpPost]//add Material Delay to database
        public ActionResult AddMaterialDelay(string issueJson)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) //method for load the map acordinto the surevisor line
            {
                JArray issueData = JArray.Parse(issueJson) as JArray;
                System.Diagnostics.Debug.WriteLine(issueData);
                var time = DateTime.Now;
                string current_time = time.ToString("yyyy-MM-dd HH:mm");//get today to string variable
                var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                var dayitem = current_time.Split(' ');
                var day = dayitem[0];
                var time1 = dayitem[1];
                foreach (JObject item in issueData)
                {
                    int line_id = Int32.Parse(item["line_line_id"].ToString());
                    int issue_id = Int32.Parse(item["issue_issue_ID"].ToString());
                    var resp_person = db.issue_line_person.Where(x => x.levelOfResponsibility == 1 && x.line_id == line_id && x.issue_id== issue_id).FirstOrDefault();
                    try
                    {
                        var responsible_person_emp_id = resp_person.EmployeeNumber;

                        string query_1 = @"INSERT INTO [dbo].[issue_occurrence]
                                                      (  [issue_date]
                                                        ,[material_id]
                                                        ,[description]
                                                        ,[line_line_id]
                                                        ,[issue_issue_ID]
                                                        ,[responsible_person_emp_id]
                                                        ,[issue_satus]
                                                        ,[location]
                                                        ,[responsible_person_confirm_status])
                                                    VALUES
                                                        ('" + date + "','"
                                                           + item["material_id"] + "','"
                                                           + item["description"] + "',"
                                                           + line_id + ","
                                                           + item["issue_issue_ID"] + ","
                                                           + resp_person.EmployeeNumber + ","
                                                           + '1' + ",'"
                                                           + item["location"] + "',"
                                                           + 1 + ")";
                        db.Database.ExecuteSqlCommand(query_1);
                        var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                        var line = db.lines.Where(x => x.line_id == line_id).FirstOrDefault();
                        string msg = line.line_name + " line MaterialDelay has been occurred on " + day + " at "+ time1 + ". Special Note of Line supervisor - " + item["description"];
                        string callNote = line.line_name + " line Breakedown has been occurred on" + day + " at " + time1;
                        com.lightON("2", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(line_id, 2, msg, "MaterialDelay has been occered", callNote);

                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);
                    }
                }
                return Content("Material Delay Recorded", MediaTypeNames.Text.Plain);
            }
        }

        private void sendCD(int? line_line_id, int issueId, string msg, string subject, string callNote)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");
            var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            Thread t = new Thread(() =>
            {
                using (BigRedEntities db = new BigRedEntities())
                {
                    var userDetails = db.tbl_PPA_User.Where(x => x.Role == "manager").ToList();
                    using (issue_management_systemEntities1 ismdb = new issue_management_systemEntities1())
                    {
                        foreach (var item in userDetails)
                        {
                            string query = "INSERT INTO tbl_Notifications ([Status],[Message],[Type],[EmployeeNumber],[Date]) VALUES( 1,'" + msg + "','issue','" + item.EmployeeNumber + "','" + date + "') ";
                            ismdb.Database.ExecuteSqlCommand(query);
                        }
                    }
                }
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                {
                    var communicationInfo = db.issue_line_person.Where(x => x.line_id == line_line_id && x.issue_id == issueId).OrderBy(x => x.levelOfResponsibility).ToList();
                    foreach (var item in communicationInfo)
                    {
                        using (BigRedEntities BR = new BigRedEntities())
                        {
                            var personInfo = BR.tbl_PPA_User.Where(y => y.EmployeeNumber == item.EmployeeNumber).FirstOrDefault();
                            CommunicationData cd = new CommunicationData(personInfo.Phone, msg, personInfo.EMail, item.email, item.call, item.message, personInfo.EmployeeNumber, subject, callNote);
                            com.setCD(cd);
                        }

                    }
                }
            });
            t.Start();
        }

        //solovedIssueMethod
        public JsonResult SolvedIssue(int? issueId, int? issueOccourId ,int? lineId)
        {
            System.Diagnostics.Debug.WriteLine("issueOccourId : " + issueOccourId);
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");
            var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            //update Issueststus as 0
            issue_management_systemEntities1 db = new issue_management_systemEntities1();
            var issueoccourInfo = db.issue_occurrence.Where(x => x.issue_occurrence_id == issueOccourId).FirstOrDefault();
            issueoccourInfo.issue_satus = "0";
            issueoccourInfo.solved_date = date;
            db.SaveChanges();

           

            //get the list of Issuueoccurrence table

            List<issue_occurrence> issue = db.issue_occurrence.ToList();
            int count = 0;
            foreach (var item in issue)
            {    //check issue id == to clicked issueid
                if (item.issue_issue_ID == issueId && item.line_line_id == lineId)
                {
                    //if any status is there under selected issueid and line id count will up
                    if (item.issue_satus == "1")
                    {
                        count++;
                    }
                }

            }
            if (count == 0)
            {// if cout ==0 light will off
                var issueCount = db.issue_occurrence.Where(x => x.issue_issue_ID == issueId && x.line_line_id == lineId && x.issue_satus == "1").ToList().Count();

                if (issueCount == 0)
                {// if cout ==0 light will off
                    var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
                    com.lightOFF(issueId.ToString(), displayInfo.raspberry_ip_address);
                }
                return Json(true);
            }

           return Json(true);
        }
    }
}