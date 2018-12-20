using IssueManagementSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
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

        public ActionResult EditMap()
        {

            BigRedEntities imsDbContext = new BigRedEntities();
            List<tblWorkstation_Config> mList = imsDbContext.tblWorkstation_Config.ToList();

            dynamic machine_list = new ExpandoObject();
            machine_list.machine_list = mList;

            return View(machine_list);
        }

        [HttpPost]
        public ActionResult addMap(String mapJSON, String department_id, String lineID, String ipAddress, String issues, String mapImage)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                //insert data in to line_map table
                string query = "INSERT INTO [dbo].[line_map]([line_id],[map],[red],[green],[yellow],[blue],issues)VALUES('" + lineID + "','" + mapJSON + "','0','0','0','0','" + issues + "')";
                db.Database.ExecuteSqlCommand(query);

                string query1 = "INSERT INTO display(line_id,raspberry_ip_address) VALUES('" + lineID + "','" + ipAddress + "') ";
                db.Database.ExecuteSqlCommand(query1);

                saveBase64Image("~/Content/images/" + lineID + ".jpg", mapImage);
            }

            return Content("query executed", MediaTypeNames.Text.Plain);
        }

        private void saveBase64Image(string outputPath, string base64String)
        {
            try {
                byte[] bytes = Convert.FromBase64String(base64String);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                var path = System.Web.HttpContext.Current.Server.MapPath(outputPath);

                var i = new Bitmap(image);
                i.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex) {

            }
            // image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private string loadBase64Image(string inputPath)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(inputPath);

            if (!System.IO.File.Exists(path))
            {
                return "";
            }

            else {

                Image image = Image.FromFile(path);
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
        }

        class tempClass
        {
            public string map { get; set; }
            public string issues { get; set; }
        }


        [HttpPost]
        public ActionResult loadMap(String lineID)
        {
            List<string> c = new List<string>();

            try {
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                {
                    //retrieve data in to line_map table
                    string query1 = "SELECT TOP (1) line_map.map,line_map.issues FROM line_map WHERE line_id =" + lineID + " ORDER BY line_map_id DESC";

                    var map_issueList = db.Database.SqlQuery<tempClass>(query1).ToList();

                    var image = loadBase64Image("~/Content/images/" + lineID + ".jpg");

                    if (map_issueList.Count > 0)
                    {
                        c.Insert(c.Count, map_issueList[0].map);//map
                        c.Insert(c.Count, map_issueList[0].issues);//issues list

                        string query2 = "SELECT TOP(1) display.raspberry_ip_address FROM display WHERE display.line_id=" + lineID + " ORDER BY display_id DESC";
                        var query2Results = db.Database.SqlQuery<string>(query2).ToList();

                        c.Insert(c.Count, query2Results[0]); //IP
                        c.Insert(c.Count, image);//image
                    }

                    else
                    {
                        c.Insert(c.Count, "");//map 
                        c.Insert(c.Count, "");// issues list
                        c.Insert(c.Count, ""); //IP
                        c.Insert(c.Count, image);//image
                    }
                    return Json(c, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                c.Insert(c.Count, "");//map 
                c.Insert(c.Count, "");// issues list
                c.Insert(c.Count, ""); //IP
                c.Insert(c.Count, "");//image
                return Json(c, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult retrieveData() {

            return View();
            // return Json(persons, JsonRequestBehavior.AllowGet);
        }


    
      



      
        
    }
}