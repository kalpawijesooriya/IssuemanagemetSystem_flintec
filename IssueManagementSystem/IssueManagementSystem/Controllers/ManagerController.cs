using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IssueManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        Communication com= new Communication();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void sms(IssueManagementSystem.Models.User_tbl userModel)
        {
            string number = userModel.Phone;
            string msg = userModel.UserName;
           // CommunicationData cd = new CommunicationData(number, msg);
            
           // com.setCD(cd); 
        }
    }
}