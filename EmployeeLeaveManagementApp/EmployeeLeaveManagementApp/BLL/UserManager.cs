using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.DLL;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.BLL
{
    public class UserManager
    {
        private UserGateway userGateway = new UserGateway();
        public int SendEmployeeLeaveApplication(EmployeeLeaveRequest leaveRequest)
        {
            return userGateway.SendEmployeeLeaveApplication(leaveRequest);
        }

        public List<EmployeeLeaveInfo> UserLeaveStatus(int? leave)
        {
            return userGateway.UserLeaveStatus(leave);
        }
    }
}