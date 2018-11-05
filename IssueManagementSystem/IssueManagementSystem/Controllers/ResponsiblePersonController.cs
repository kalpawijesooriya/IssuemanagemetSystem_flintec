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
        public JsonResult GetNotification()
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
                    line_name = lineInfo.line_name,
                   
                };
                return Json(line, JsonRequestBehavior.AllowGet);
            }
               
            
        }
    }
   
}