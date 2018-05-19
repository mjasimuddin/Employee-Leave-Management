using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using EmployeeLeaveManagementApp.DLL;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.BLL
{
    public class SuperadminManager
    {
        private readonly SuperadminGateway _superadminGateway = new SuperadminGateway();

        public int AddEmployee(Employee employee)
        {
            return _superadminGateway.AddEmployee(employee);
        }

        public List<Employee> GetallEmployeeByDesignationId(int designationId)
        {
            return _superadminGateway.GetallEmployeeByDesignationId(designationId);
        }

        public int EmployeePassword(EmployeePassword employee)
        {
            return _superadminGateway.SetEmployeePassword(employee);
        }

        public int EmployeeUserType(EmployeeUserType employee)
        {
            return _superadminGateway.SetEmployeeUserType(employee);
        }

        public int EmployeeLeaveAllocation(EmployeeLeaveAllocation employeeLeave)
        {
            return _superadminGateway.EmployeeLeaveAllocation(employeeLeave);
        }

        public List<Employee> GetEmployeeById(int id)
        {
            return _superadminGateway.GetEmployeeById(id);
        }

        public List<Employee> ListOfEmployee()
        {
            return _superadminGateway.GetAllEmployees();
        }

        public List<Designation> GetDesignationList()
        {
            return _superadminGateway.GetDesignationList();
        }

        public List<UserType> GetUserType()
        {
            return _superadminGateway.GetUserType();
        }

        public List<LeaveType> GetLeaveType()
        {
            return _superadminGateway.GetLeaveType();
        }

        public List<SubmitedApplicationInfo> GetUserEmail(int? id)
        {
            return _superadminGateway.GetUserEmail(id);
        }
        public List<SubmitedApplicationInfo> GetUserEmailAndName(int? id)
        {
            return _superadminGateway.GetUserEmailAndName(id);
        }

        public List<EmployeeLeaveAllocation> AllLeaveInfo(int employeeId)
        {
            return _superadminGateway.AllLeaveInfo(employeeId);
        }
        public List<EmployeeLeaveInfo> ShowAllLeaveStatus()
        {
            return _superadminGateway.ShowAllLeaveStatus();
        }

        public List<EmployeeLeaveInfo> GetEmployeeLeaveApplication()
        {
            return _superadminGateway.GetEmployeeLeaveApplication();
        }

        public int Approve(int? id)
        {
            return _superadminGateway.Approve(id);
        }

        public int Reject(int? id)
        {
            return _superadminGateway.Reject(id);
        }

        public bool IsEmailExists(string email)
        {
            return _superadminGateway.IsEmailExist(email);
        }

        public bool IsPasswordSet(EmployeePassword employee)
        {
            return _superadminGateway.IsPasswordSet(employee);
        }

        public bool IsUserRoleSet(EmployeeUserType leaveRequest)
        {
            return _superadminGateway.IsUserRoleSet(leaveRequest);
        }

        public bool IsLeaveAllocated(EmployeeLeaveAllocation leaveAllocation)
        {
            return _superadminGateway.IsLeaveAllocated(leaveAllocation);
        }

        public bool IsLeaveRequest(EmployeeLeaveRequest employeeLeaveRequest)
        {
            return _superadminGateway.IsLeaveRequest(employeeLeaveRequest);
        }

        public int SickLeaveLeft(int employeeId)
        {
            var totalSickLeave = _superadminGateway.AllLeaveInfo(employeeId);
            var sickLeaveTaken = _superadminGateway.GetTotalSickLeaveByEmployeeId(employeeId);
            int remaingSickLeave = 0;
            if (sickLeaveTaken.FirstOrDefault().TotalDay == 0)
            {
                remaingSickLeave = 0;
            }
            else
            {
                remaingSickLeave = sickLeaveTaken.FirstOrDefault().TotalDay;
            }

            return remaingSickLeave;
        }
        public int TotalSickLeave(int employeeId)
        {
            int totalSickLeave = 0;
            try
            {
                var totalSickLeaves = _superadminGateway.AllLeaveInfo(employeeId);
                totalSickLeave = totalSickLeaves.FirstOrDefault().TotalLeave;
            }
            catch (Exception)
            {
                totalSickLeave = 0;

            }

            return totalSickLeave;
        }

        public int CasualLeaveLeft(int employeeId)
        {
            var totalSickLeave = _superadminGateway.TotalCasualLeave(employeeId);
            var sickLeaveTaken = _superadminGateway.GetTotalCasualLeaveByEmployeeId(employeeId);
            int remaingSickLeave;
            if (sickLeaveTaken.FirstOrDefault().TotalDay == 0)
            {
                remaingSickLeave = 0;
            }
            else
            {
                 remaingSickLeave = sickLeaveTaken.FirstOrDefault().TotalDay;   
            }

            return remaingSickLeave;
        }
        public int TotalCasualLeave(int employeeId)
        {
            int totalSickLeave = 0;
            try
            {
                var totalSickLeaves = _superadminGateway.TotalCasualLeave(employeeId);
                totalSickLeave = totalSickLeaves.FirstOrDefault().TotalLeave;
            }
            catch (Exception)
            {
                totalSickLeave = 0;
            }

            return totalSickLeave;
        }

        public List<LoginInfo> GetUserRole(int id)
        {
            return _superadminGateway.GetUserRole(id);
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail,senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject,emailBody);

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}