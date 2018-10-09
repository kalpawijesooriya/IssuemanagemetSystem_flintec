using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueManagementSystem.Models;

namespace IssueManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(IssueManagementSystem.Models.User userModel)
        {
            using (issue_management_systemEntities1 db =new issue_management_systemEntities1())
            {
                var userDetails =db.Users.Where(x => x.UserName == userModel.UserName && x.PassWord== userModel.PassWord).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Woring UserName or Password.";
                    return View("Index", userModel);
                }
                else {
                    Session["userID"] = userDetails.emp_id;
                    Session["userName"] = userDetails.UserName.Trim();
                    string username = Session["userName"].ToString();
                    string admin = "admin";
                  
                    if (admin.Equals(Session["userName"]))
                    {
                        return RedirectToAction("SelectBranch", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("selectIssue", "Supervisor");
                    }
                    

                }
            }
        

           
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}