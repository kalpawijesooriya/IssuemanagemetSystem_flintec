using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class ResponsiblePersonController : Controller
    {
        // GET: ResponsiblePerson
        public ActionResult Index()
        {
          
                
                return View();
         
        }
        public JsonResult GetIssues()
        {
            return Json(IssueService.GetIssue(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult getlineInfo(int? list_id)
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                var lineInfo=db.lines.Where(x => x.line_id ==list_id).FirstOrDefault();
                line line=new line {
                    line_id = lineInfo.line_id,
                    line_name = lineInfo.line_name
                   
                };
                return Json(line, JsonRequestBehavior.AllowGet);
            }
               
            
        }

        public JsonResult savecomment(int? id ,string comment )
        {
            try
            {
                using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
                {
                    var time = DateTime.Now;
                    string current_time = time.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "UPDATE issue_occurrence SET responsible_person_confirm_status = 0 ,responsible_person_confirm_feedback='"+ comment + "',commented_date='"+ current_time + "'WHERE issue_occurrence_id =" + id ;
                    db.Database.ExecuteSqlCommand(query);
                    var issueinfo = db.issue_occurrence.Where(x => x.issue_occurrence_id ==id).First();
                    return this.Json(issueinfo.issue_satus, JsonRequestBehavior.AllowGet);
                   
                }
            }
            catch (Exception e)
            {
                return this.Json(e, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
   
}