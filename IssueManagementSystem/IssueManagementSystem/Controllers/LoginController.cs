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
        public ActionResult Autherize(IssueManagementSystem.Models.tbl_PPA_User userModel)
        {
            using (BigRedEntities db =new BigRedEntities())
            {
                var userDetails =db.tbl_PPA_User.Where(x => x.EmployeeNumber == userModel.EmployeeNumber && x.Password== userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Employee Number or Password.";//show login erroe message
                    return View("Index", userModel);
                }
                else {
                    Session["userID"] = userDetails.EmployeeNumber;//retrive Userid of login user
                    Session["userName"] = userDetails.UserName.Trim();//retrive USerName of login user
                    Session["name"] = userDetails.Name.Trim();
                    Session["email"] = userDetails.EMail.Trim();
                    Session["location"] = userDetails.Location.Trim();
                    Session["department"] = userDetails.Department.Trim();
                    Session["Role"] = userDetails.Role.Trim();
                  
                    string username = Session["userName"].ToString();
                    string role = userDetails.Role.ToString().Trim();//retrive the user role
                    if (role.Equals("supervisor"))//if user is supervisor goto the supervisor page
                    {
                        using (issue_management_systemEntities1 dbism = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
                        {
                            var lineInfo = dbism.line_supervisor.Where(x => x.supervisor_emp_id == userDetails.EmployeeNumber).FirstOrDefault();
                            Session["lineId"] = lineInfo.line_line_id;
                           
                        }
                        return RedirectToAction("selectIssue", "Supervisor");
                    } 

                    else if (role.Equals("CellEngineer"))
                    {
                        using (issue_management_systemEntities1 dbism = new issue_management_systemEntities1())//method for load the map acordinto the surevisor line
                        {
                            var lineInfo = dbism.line_cell_eng.Where(x => x.cell_eng_emp_id == userDetails.EmployeeNumber).FirstOrDefault();
                            Session["lineId"] = lineInfo.line_id;
                          
                        }
                        return RedirectToAction("DashBord", "CellEngineer");
                    }
                       

                    else if (role.Equals("display")) //if user is display goto the display page
                        return RedirectToAction("Rasp", "Display");
                    
                    else if (role.Equals("admin"))
                        return RedirectToAction("Index", "Admin");

                    else if (role.Equals("manager"))
                        return RedirectToAction("Index", "Manager");

              

                    else if (role.Equals("responsiblePerson"))
                        return RedirectToAction("Index", "ResponsiblePerson");


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