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
        public ActionResult Autherize(IssueManagementSystem.Models.User_tbl userModel)
        {
            using (issue_management_systemEntities1 db =new issue_management_systemEntities1())
            {
                var userDetails =db.User_tbl.Where(x => x.UserName == userModel.UserName && x.Password== userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong UserName or Password.";//show login erroe message
                    return View("Index", userModel);
                }
                else {
                    Session["userID"] = userDetails.EmployeeNumber;//retrive Userid of login user
                    Session["userName"] = userDetails.UserName.Trim();//retrive USerName of login user
                    Session["name"] = userDetails.Name.Trim();
                    Session["email"] = userDetails.EMail.Trim();
                    Session["location"] = userDetails.Location.Trim();
                    Session["department"] = userDetails.Department.Trim();

                    string username = Session["userName"].ToString();
                    string role = userDetails.Role.ToString().Trim();//retrive the user role
                    if (role.Equals("supervisor"))//if user is supervisor goto the supervisor page
                    {
                        return RedirectToAction("selectIssue", "Supervisor");
                    }
                   
                    else if (role.Equals("display")) //if user is display goto the display page
                    {
                        return RedirectToAction("Rasp", "Display");
                    }
                    else if (role.Equals("admin"))
                        return RedirectToAction("SelectBranch", "Admin");

                    else if (role.Equals("manager"))
                        return RedirectToAction("Index", "Manager");

                    else if (role.Equals("CellEngineer"))
                        return RedirectToAction("DashBord", "CellEngineer");

                    else
                        return RedirectToAction("Index", "Login");

                }
            }         
        }

        public ActionResult LogOut()// logout methord
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}