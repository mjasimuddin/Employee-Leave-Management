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
    public class SuperadminController : Controller
    {
        private SuperadminManager superadminManager = new SuperadminManager();
        private UserManager userManager = new UserManager();

        //GET: /Admin/
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult AddEmployee()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            int employeeId = (int) Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.designations = superadminManager.GetDesignationList();
                ViewBag.userType = superadminManager.GetUserType();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int message = superadminManager.AddEmployee(employee);
                    if (message > 0)
                    {
                        ViewBag.ShowMsg = "Employee Information Saved Successfully!";
                    }
                    else
                    {
                        ViewBag.ShowMsg = "Sorry! Data Not Saved! Please Try Again";
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }
            //employeeInfo.Add(employee);
            ViewBag.designations = superadminManager.GetDesignationList();
            ViewBag.userType = superadminManager.GetUserType();
            return View();
        }
        public ActionResult EmployeePassword()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                //ViewBag.userType = adminManager.GetUserType();
                //ViewBag.designations = adminManager.GetDesignationList();
                ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult EmployeePassword(EmployeePassword employeeInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (superadminManager.IsPasswordSet(employeeInfo))
                    {
                        ViewBag.ShowMsg = "Password Was Already Set!";
                    }
                    else
                    {
                        int message = superadminManager.EmployeePassword(employeeInfo);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Employee Password Saved Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Sorry! Data Not Saved! Please Try Again";
                        }
                    }
                }
                catch (Exception exception)
                {

                    ViewBag.ShowMsg = exception.Message;
                }
            }

            ViewBag.userType = superadminManager.GetUserType();
            ViewBag.designations = superadminManager.GetDesignationList();
            ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();
            return View();

        }
        public ActionResult EmployeeUserType()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.userType = superadminManager.GetUserType();
                ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            } 
        }
        [HttpPost]
        public ActionResult EmployeeUserType(EmployeeUserType employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (superadminManager.IsUserRoleSet(employee))
                    {
                        ViewBag.ShowMsg = "User Role Was Already Set!";
                    }
                    else
                    {
                        int message = superadminManager.EmployeeUserType(employee);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Employee User Typed Saved Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Sorry! Data Not Saved! Please Try Again";
                        }
                    }
                }
                catch (Exception exception)
                {

                    ViewBag.ShowMsg =exception.Message;
                }
            }
            
            ViewBag.ListOfEmployees = superadminManager.ListOfEmployee();
            ViewBag.userType = superadminManager.GetUserType();
            return View();
        }
        public JsonResult GetallEmployeeByDesignationId(int designationId)
        {
            List<Employee> employees = superadminManager.GetallEmployeeByDesignationId(designationId);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeLeaveAllocation()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.leavetype = superadminManager.GetLeaveType();
                ViewBag.designations = superadminManager.GetDesignationList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult EmployeeLeaveAllocation(EmployeeLeaveAllocation employeeLeave)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (superadminManager.IsLeaveAllocated(employeeLeave))
                    {
                        ViewBag.ShowMsg = "Leave Allocated Was Already Set!";
                    }
                    else
                    {
                        int message = superadminManager.EmployeeLeaveAllocation(employeeLeave);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Employee Leave Allocation Saved Successfully";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Sorry!, Data Not Saved! Please Try Again";
                        }
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }
            
            ViewBag.leavetype = superadminManager.GetLeaveType();
            ViewBag.designations = superadminManager.GetDesignationList();
            return View();
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
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
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
        public JsonResult GetEmployeeById(int departmentId)
        {
            List<Employee> status = superadminManager.GetEmployeeById(departmentId);
            return Json(status.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowAllLeaveStatus()
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
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> getAllLeaveApplication = superadminManager.ShowAllLeaveStatus().ToList();
                return View(getAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ApproveOrReject()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> GetAllLeaveApplication = superadminManager.GetEmployeeLeaveApplication().ToList();
                return View(GetAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Approve(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int employeeId = (int) Session["user"];
            List<LoginInfo> userRole = superadminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = superadminManager.Approve(id);
                List<SubmitedApplicationInfo> userEmail = superadminManager.GetUserEmail(id);
                bool result = superadminManager.SendEmail(userEmail[0].Email, "About your leave application",
                    "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                    userEmail[0].StartDate + "' and end date '" + userEmail[0].EndDate + "', entry date " +
                    userEmail[0].EntryDate + " are Approved by HR Admin<br/>Thank You<br/>PBL-001</p>");

                return RedirectToAction("ApproveOrReject");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Reject(int? id)
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
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = superadminManager.Reject(id);
                List<SubmitedApplicationInfo> userEmail = superadminManager.GetUserEmail(id);
                bool result = superadminManager.SendEmail(userEmail[0].Email, "About your leave application",
                    "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                    userEmail[0].StartDate + "' and end date '" + userEmail[0].EndDate + "', entry date " +
                    userEmail[0].EntryDate + " are Rejected by HR Admin<br/>Thank You<br/>PBL-001</p>");

                return RedirectToAction("ApproveOrReject");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public JsonResult IsEmailExists(string email)
        {
            bool isCodeExists = superadminManager.IsEmailExists(email);

            if (isCodeExists)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }
	}
}