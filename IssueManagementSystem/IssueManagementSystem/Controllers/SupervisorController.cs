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
using System.Collections;
using IssueManagementSystem.Models.IssueManagementSystem.Models;

namespace IssueManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {

        Communication com = new Communication();
        // GET: Supervisor
        public ActionResult selectIssue()// select issue view
        {

            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");

            }
        
            return View();
        }

        [HttpGet]
        public JsonResult GetIssues()
        {
            return Json(IssueService.GetIssue(), JsonRequestBehavior.AllowGet);

        }
       [HttpGet]
        public JsonResult GetBreakedown()
        {
          return Json(BreakeDownService.GetBreakedown(), JsonRequestBehavior.AllowGet);

       }

        public ActionResult MachinBreakdown()//machine breakedown view
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");

            }
         
            return View();
        }
        public ActionResult QualityIssue()//Qulity view
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");

            }
            return View();
        }

        public ActionResult TechnicalIssue()//Technical Issue View
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");

            }
          
            return View();
        }

        public ActionResult MaterialDelay()//MaterialDelay View
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");
            }
            dynamic mat_List = new System.Dynamic.ExpandoObject();
            FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
            List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();
            mat_List.materialList = mList;
            return View(mat_List);
        }

        public ActionResult ITIssue()//IT Issue view
        {
            if ((Session["userID"] == null) || ((string)Session["Role"] != "supervisor"))
            {
                return RedirectToAction("Index", "Login");

            }

            return View();
        }

        [HttpPost] //add Breakedown to database
        public ActionResult AddIssueBreakedown(issue_occurrence issueModel, string issueJson, notification_handling notification_HandlingModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");//get today to string variable
        
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    JArray issueData = JArray.Parse(issueJson) as JArray;
                    System.Diagnostics.Debug.WriteLine(issueData);

                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    var HHMM = time1.Split(':');
                    try
                    {

                        foreach (JObject item in issueData)
                        {
                            int line_id = Int32.Parse(item["line_line_id"].ToString());
                            int issue_id = 1;
                            var resp_person = db.issue_line_person.Where(x => x.levelOfResponsibility == 1 && x.line_id == line_id && x.issue_id == issue_id && x.person_level == 1).FirstOrDefault();
                            var responsible_person_emp_id = resp_person.EmployeeNumber; //get certain employee assigned for a certain issue in a certain line and the level of resp. should be one
                            issueModel.machine_machine_id = item["machine"].ToString();
                            issueModel.line_line_id = line_id;
                            issueModel.responsible_person_confirm_status = 1;
                            issueModel.department = "Maintenance";
                            issueModel.issue_satus = "1";
                            issueModel.issue_issue_ID = 1;//Issue id is 1 for Breakdown
                            issueModel.responsible_person_emp_id = resp_person.EmployeeNumber;
                            issueModel.location = item["location"].ToString();
                            issueModel.description = item["description"].ToString();
                        
                            issueModel.issue_date = date;
                            db.issue_occurrence.Add(issueModel);
                            db.SaveChanges();// end of the save


                            string machine = issueModel.machine_machine_id;
                            var respPersonID = db.issue_line_person.Where(x => x.line_id == line_id && x.levelOfResponsibility == 1 && x.issue_id == 1 && x.person_level == 1).FirstOrDefault();
                            issueModel.responsible_person_emp_id = Int32.Parse(respPersonID.EmployeeNumber.ToString());


                            var issue_occour_id = issueModel.issue_occurrence_id;

                            if (issueModel.issue_occurrence_id > 0)
                            {
                                var line = db.lines.Where(x => x.line_id == line_id).FirstOrDefault();
                                string msg = " Breakdown has occurred.@@ Line : " + line.line_name + " Line @ Date : " + day + " @ Time : " + time1 + " @ Machine : " + machine + " @ Note : " + issueModel.description;
                                msg = msg.Replace("@", Environment.NewLine);
                                string callNote = line.line_name + " line Breakdown has occurred at " + time1;
                                var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                                com.lightON("1", displayInfo.raspberry_ip_address);//turn on the Light
                                com.maintenancesbuzzerOn();
                                sendCD(line_id, 1, msg, "Machine Breakdown has occurred", callNote, issue_occour_id, notification_HandlingModel);
                            }
                            ModelState.Clear();
                        }

                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);

                    }

                }
            }
            return Content("Breakedown Issue Recorded", MediaTypeNames.Text.Plain);
        }

        [HttpPost]//add Tecnical Issues to database
        public ActionResult AddIssueTechnical(string issueJson, issue_occurrence issueModel, notification_handling notification_HandlingModel)
        {
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    JArray issueData = JArray.Parse(issueJson) as JArray;
                    System.Diagnostics.Debug.WriteLine(issueData);

                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    var HHMM = time1.Split(':');
                    try
                    {

                        foreach (JObject item in issueData)
                        {
                            int line_id = Int32.Parse(item["line_line_id"].ToString());
                            int issue_id = 3;
                            var resp_person = db.issue_line_person.Where(x => x.levelOfResponsibility == 1 && x.line_id == line_id && x.issue_id == issue_id && x.person_level == 1).FirstOrDefault();
                            var responsible_person_emp_id = resp_person.EmployeeNumber; //get certain employee assigned for a certain issue in a certain line and the level of resp. should be one

                            issueModel.line_line_id = line_id;
                            issueModel.responsible_person_confirm_status = 1;
                            issueModel.department = "Engineering";
                            issueModel.issue_satus = "1";
                            issueModel.issue_issue_ID = 3;//Issue id is 3 for Tecnical
                            issueModel.responsible_person_emp_id = resp_person.EmployeeNumber;
                            issueModel.location = item["location"].ToString();
                            issueModel.description = item["description"].ToString();
                    
                            issueModel.issue_date = date;
                            db.issue_occurrence.Add(issueModel);
                            db.SaveChanges();// end of the save

                            if (issueModel.issue_occurrence_id > 0)
                            {
                                var issue_occour_id = issueModel.issue_occurrence_id;
                                var line = db.lines.Where(x => x.line_id == line_id).FirstOrDefault();
                                var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                                string msg = " Technical issue has occurred @@ Line : " + line.line_name + " Line @ Date" + day + " @ Time : " + HHMM[0] + ":" + HHMM[1] + "@ Special Note : " + issueModel.description;
                                msg = msg.Replace("@", Environment.NewLine);
                                string callNote = "Technical issue has occurred in " + line.line_name + " line at " + HHMM[0] + ":" + HHMM[1];
                                com.lightON("3", displayInfo.raspberry_ip_address);//turn on the Light
                                sendCD(line_id, 3, msg, "Tecnical Issue has occered", callNote, issue_occour_id, notification_HandlingModel);

                                ModelState.Clear();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);

                    }
                    

                }
            }
            return Content("Technical Issue Recorded", MediaTypeNames.Text.Plain);
        }
        [HttpPost]//add IT Issues to database
        public ActionResult AddIssueIT(string issueJson, issue_occurrence issueModel, notification_handling notification_HandlingModel)
        {
          
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    JArray issueData = JArray.Parse(issueJson) as JArray;
                    System.Diagnostics.Debug.WriteLine(issueData);


                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    try
                    {
                        foreach (JObject item in issueData)
                        {
                            int line_id = Int32.Parse(item["line_line_id"].ToString());
                            int issue_id = 5;
                            var resp_person = db.issue_line_person.Where(x => x.levelOfResponsibility == 1 && x.line_id == line_id && x.issue_id == issue_id && x.person_level == 1).FirstOrDefault();
                            var responsible_person_emp_id = resp_person.EmployeeNumber; //get certain employee assigned for a certain issue in a certain line and the level of resp. should be one
                             //save IT Issue to databse
                                issueModel.department = "IT";
                                issueModel.responsible_person_confirm_status = 1;
                                issueModel.line_line_id = line_id;
                                issueModel.issue_satus = "1";
                                issueModel.issue_issue_ID = 5;   //Issue id is 5 for IT Issue
                                issueModel.issue_date = date;
                                issueModel.responsible_person_emp_id = resp_person.EmployeeNumber;
                                issueModel.location = item["location"].ToString();
                                issueModel.description = item["description"].ToString();
                     
                                db.issue_occurrence.Add(issueModel);
                                db.SaveChanges();// end of the save

                                //send notifications
                                var issue_occour_id = issueModel.issue_occurrence_id;
                                if (issueModel.issue_occurrence_id > 0)
                                {
                                    var line = db.lines.Where(x => x.line_id == line_id).FirstOrDefault();
                                    string msg = "IT/SoftWare issue has occurred @@ Line : " + line.line_name + " Line @ Date : " + day + "@ Time : " + time1 + "@ Note : " + issueModel.description;
                                    msg = msg.Replace("@", Environment.NewLine);
                                    string callNote = "IT Software issue has occurred in " + line.line_name + " line at " + time1;
                                    var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                                    com.lightON("5", displayInfo.raspberry_ip_address);//turn on the Light
                                    sendCD(line_id, 5, msg, "IT/Software Issue has occered", callNote, issue_occour_id, notification_HandlingModel);
                                }
                                ModelState.Clear();
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);
                    }
                }
            }
            return Content("IT Issue Recorded", MediaTypeNames.Text.Plain);
        }

        [HttpPost]//add Quality Issues to database
        public ActionResult AddIssueQuality(issue_occurrence issueModel, notification_handling notification_HandlingModel)
        {
            int lineId = 1;
            var time = DateTime.Now;
            string current_time = time.ToString("yyyy-MM-dd HH:mm");

            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                if (ModelState.IsValid)
                {
                    lineId = Int32.Parse(issueModel.lineid);
                    int userID = (int)Session["userID"];
                    issueModel.department = "Quality";
                    issueModel.responsible_person_confirm_status = 1;
                    issueModel.line_line_id = lineId;
                    issueModel.issue_satus = "1";
                    issueModel.issue_issue_ID = 4;//Issue id is 5 for IT Issue

                    //get certain employee assigned for a certain issue in a certain line and the level of resp. should be one
                    var respPersonID = db.issue_line_person.Where(x => x.line_id == lineId && x.levelOfResponsibility == 1 && x.issue_id == 4 && x.person_level == 1).FirstOrDefault();
                    issueModel.responsible_person_emp_id = Int32.Parse(respPersonID.EmployeeNumber.ToString());

                    issueModel.location = (string)Session["location"];
                    var date = DateTime.ParseExact(current_time, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    var dayitem = current_time.Split(' ');
                    var day = dayitem[0];
                    var time1 = dayitem[1];
                    issueModel.issue_date = date;

                    db.issue_occurrence.Add(issueModel);
                    db.SaveChanges();
                    var issue_occour_id = issueModel.issue_occurrence_id;
                    if (issueModel.issue_occurrence_id > 0)
                    {
                        var line = db.lines.Where(x => x.line_id == lineId).FirstOrDefault();
                        string msg = "Quality issue has occurred @@ Line :" + line.line_name + " Line @ Date : " + day + " @ Time : " + time1 + ". @ Note : " + issueModel.description;
                        msg = msg.Replace("@", Environment.NewLine);
                        string callNote = "Quality issue has occurred in " + line.line_name + " line at " + time1;
                        var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
                        com.lightON("4", displayInfo.raspberry_ip_address);//turn on the Light
                        sendCD(lineId, 4, msg, "Quality Issue has occered", callNote, issue_occour_id, notification_HandlingModel);
                    }
                    ModelState.Clear();
                }
            }
            if (issueModel.Role == "supervisor") { return RedirectToAction("selectIssue", "Supervisor"); }
            else { return RedirectToAction("DashBord", "CellEngineer", new { lineid = lineId }); }
        }


        [HttpPost]//add Material Delay to database
        public ActionResult AddMaterialDelay(string issueJson, issue_occurrence issueModel ,notification_handling notification_HandlingModel)
        {
            
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1()) 
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
                    var resp_person = db.issue_line_person.Where(x => x.levelOfResponsibility == 1 && x.line_id == line_id && x.issue_id== issue_id && x.person_level == 1).FirstOrDefault();
                    try
                    {
                        var responsible_person_emp_id = resp_person.EmployeeNumber;

             
                        if (ModelState.IsValid)
                        {

                            issueModel.responsible_person_confirm_status = 1;
                            issueModel.line_line_id = line_id;
                            issueModel.issue_satus = "1";
                            issueModel.issue_issue_ID = 2;
                            issueModel.issue_date = date;
                            issueModel.department = "Stores";
                            issueModel.material_id = item["material_id"].ToString();
                            issueModel.responsible_person_emp_id = resp_person.EmployeeNumber;
                            issueModel.location = item["location"].ToString();
                            issueModel.description = item["description"].ToString();
                            db.issue_occurrence.Add(issueModel);

                          
                            db.SaveChanges();
                            var issue_occour_id = issueModel.issue_occurrence_id;
                            
                          System.Diagnostics.Debug.WriteLine("New Issue ID" + issue_occour_id);

                            if (issueModel.issue_occurrence_id > 0)
                            {
                                var displayInfo = db.displays.Where(x => x.line_id == line_id).FirstOrDefault();
                                var line = db.lines.Where(x => x.line_id == line_id).FirstOrDefault();
                                string msg = "MaterialDelay has occurred @@ Line : " + line.line_name + " Line @ Date :" + day + " @Time : " + time1 + " @ Material : " + item["material"] + " @ Note : " + item["description"];
                                msg = msg.Replace("@", Environment.NewLine);
                                string callNote = "MaterialDelay has occurred in " + line.line_name + " line at " + time1;
                                com.lightON("2", displayInfo.raspberry_ip_address);//turn on the Light
                                com.storesbuzzerOn();
                                sendCD(line_id, 2, msg, "MaterialDelay has occered", callNote, issue_occour_id, notification_HandlingModel);
                                ModelState.Clear();
                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        return Content("Error Occured" + ex.ToString(), MediaTypeNames.Text.Plain);
                    }
                }
                return Content("Material Delay Recorded", MediaTypeNames.Text.Plain);
            }
        }

        private void sendCD(int? line_line_id, int issueId, string msg, string subject, string callNote,int  issue_occour_id, notification_handling notification_HandlingModel)
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
                    var communicationInfo = db.issue_line_person.Where(x => x.line_id == line_line_id && x.issue_id == issueId ).OrderBy(x => x.levelOfResponsibility).ToList();
                    Queue numberList = new Queue();
                    foreach (var item in communicationInfo)
                    {
                        if (item.person_level == 2)
                        {
                            notification_HandlingModel.issue_occurrence_id = issue_occour_id;
                            notification_HandlingModel.EmployeeNumber = item.EmployeeNumber;
                            notification_HandlingModel.notification_status = 1;
                            db.notification_handling.Add(notification_HandlingModel);
                            db.SaveChanges();
                        }
                        if (item.person_level == 1)
                        {
                            using (BigRedEntities BR = new BigRedEntities())
                            {

                                var personInfo = BR.tbl_PPA_User.Where(y => y.EmployeeNumber == item.EmployeeNumber).FirstOrDefault();
                                CommunicationData cd = new CommunicationData(personInfo.Phone, msg, personInfo.EMail, item.email, item.call, item.message, personInfo.EmployeeNumber, subject, callNote, item.callRepetitionTime, item.sendAlertAfter, issue_occour_id);
                                numberList.Enqueue(cd);

                            }
                        }
                      

                    }
                    com.setCD(numberList);
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

            var count = db.issue_occurrence.Where(x=>x.issue_issue_ID==issueId && x.line_line_id==lineId && x.issue_satus=="1").Count();
   
            if (count == 0)
            {
              
            var displayInfo = db.displays.Where(x => x.line_id == lineId).FirstOrDefault();
            com.lightOFF(issueId.ToString(), displayInfo.raspberry_ip_address);
           
            }

           return Json(true);
        }
    }
}