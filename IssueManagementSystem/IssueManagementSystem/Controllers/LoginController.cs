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
        tbl_PPA_User auhorized_user = new tbl_PPA_User();

        // GET: Login
        public ActionResult Index()
        {
            HttpCookie cookies_clients = Request.Cookies["login_data"];
            if (cookies_clients != null)
            {
                tbl_PPA_User obj = new tbl_PPA_User();
                obj.EmployeeNumber = System.Convert.ToInt32(cookies_clients["uid"]);
                obj.Password = cookies_clients["pwd"];

                ActionResult ar = Authorize(obj);
                return ar;
            }
            else
                return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Authorize(IssueManagementSystem.Models.tbl_PPA_User userModel)
        {
            using (BigRedEntities db =new BigRedEntities())
            {
                HttpCookie cookie = new HttpCookie("login_data");
                cookie["uid"] = userModel.EmployeeNumber.ToString();
                cookie["pwd"] = userModel.Password;
                cookie.Expires = DateTime.Now.AddDays(30);

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

                    Response.Cookies.Add(cookie);

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
                        return RedirectToAction("DashBord", "CellEngineer", new { lineid = Session["lineId"] });
                    }
                    else if (role.Equals("display")) //if user is display goto the display page
                        return RedirectToAction("Rasp", "Display");   
                    else if (role.Equals("admin"))
                        return RedirectToAction("Index", "Admin");
                    else if (role.Equals("manager"))
                        return RedirectToAction("Index", "Manager",Response);
                    else if (role.Equals("responsiblePerson"))
                        return RedirectToAction("Index", "ResponsiblePerson");
                    else
                        return RedirectToAction("Index", "Login");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Response.Cookies["login_data"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Login");
        }
    }
}