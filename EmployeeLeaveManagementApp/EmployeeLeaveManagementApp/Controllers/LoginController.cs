using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagementApp.BLL;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private LoginManager loginManager = new LoginManager();
        // GET: /Login/
        public ActionResult Superadmin()
        {
            if (Session != null && Session["user"] != null)
            {
                Session["user"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Superadmin(LoginInfo employee)
        {
            try
            {
                List<LoginInfo> status = loginManager.SuperadminLogin(employee);
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Superadmin"] = "Superadmin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 1)
                        {
                            return RedirectToAction("../Superadmin/Index");

                        }

                    }

                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult Admin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Admin(LoginInfo employee)
        {
            try
            {
                List<LoginInfo> status = loginManager.AdminLogin(employee);
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Superadmin"] = "Admin";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 2)
                        {
                            return RedirectToAction("../Admin/Index");

                        }
                    }
                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }

        public ActionResult User()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult User(LoginInfo employee)
        {
            try
            {
                List<LoginInfo> status = loginManager.UserLogin(employee);
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        var name = status[0].EmployeeName;
                        Session["Superadmin"] = "User";
                        Session["User1"] = name;
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 3)
                        {
                            return RedirectToAction("../User/Index");

                        }
                    }
                }
                else
                {
                    ViewBag.Msg = "User name or passwoer mismatch!";
                }
            }
            catch (Exception exception)
            {
                ViewBag.Msg = exception.Message;
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}