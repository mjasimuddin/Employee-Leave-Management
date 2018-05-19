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
    public class UserController : Controller
    {
        private UserManager userManager = new UserManager();
        private SuperadminManager superadminManager = new SuperadminManager();

        // GET: /User/
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult UserLeaveStatus(int? leave)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
                {
                    ViewBag.designations = superadminManager.GetDesignationList();
                    leave = (int) Session["user"];
                    List<EmployeeLeaveInfo> GetAllLeaveApplication = userManager.UserLeaveStatus(leave);
                    return View(GetAllLeaveApplication);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
        }
        public ActionResult EmployeeLeaveRequest()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            int employeeId1 = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId1);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
            {
                int employeeId = (int)Session["user"];
                ViewBag.casualLeaveLeft = superadminManager.CasualLeaveLeft(employeeId);
                ViewBag.totalCasualkLeave = superadminManager.TotalCasualLeave(employeeId);
                ViewBag.sickLeaveLeft = superadminManager.SickLeaveLeft(employeeId);
                ViewBag.totalSickLeave = superadminManager.TotalSickLeave(employeeId);
                ViewBag.ListOfLeaveType = superadminManager.GetLeaveType();
                ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EmployeeLeaveRequest(EmployeeLeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (superadminManager.IsLeaveRequest(leaveRequest))
                    {
                        ViewBag.ShowMsg = "Date Overlapping Problem!";
                    }
                    else
                    {
                        leaveRequest.Status = "Pending";
                        leaveRequest.EntryDate = DateTime.Now;
                        int message = userManager.SendEmployeeLeaveApplication(leaveRequest);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Leave Application Submit Successfully!";
                            List<SubmitedApplicationInfo> userEmail = superadminManager.GetUserEmailAndName(leaveRequest.EmployeeId);
                            bool result = superadminManager.SendEmail(userEmail[0].Email, "About your leave application",
                                "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                                leaveRequest.StartDate.ToString("dd/MM/yyyy") + "' and end date '" + leaveRequest.EndDate.ToString("dd/MM/yyyy") + "', total day " +
                                leaveRequest.TotalDay + " are received by HR Admin<br/>Thank You<br/>PBL-001</p>");
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Application Not Saved! Try Again Please";
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }

            ViewBag.casualLeaveLeft = superadminManager.CasualLeaveLeft(leaveRequest.EmployeeId);
            ViewBag.totalCasualkLeave = superadminManager.TotalCasualLeave(leaveRequest.EmployeeId);
            ViewBag.sickLeaveLeft = superadminManager.SickLeaveLeft(leaveRequest.EmployeeId);
            ViewBag.totalSickLeave = superadminManager.TotalSickLeave(leaveRequest.EmployeeId);
            ViewBag.ListOfLeaveType = superadminManager.GetLeaveType();
            ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();

            return View();
        }
	}
}