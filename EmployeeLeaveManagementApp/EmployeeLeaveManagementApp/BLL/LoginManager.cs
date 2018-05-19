using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.DLL;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;


namespace EmployeeLeaveManagementApp.BLL
{
    public class LoginManager
    {
        private LoginGateway loginGateway = new LoginGateway();
        public List<LoginInfo> SuperadminLogin(LoginInfo login)
        {
            return loginGateway.SuperadminLogin(login);
        }

        public List<LoginInfo> AdminLogin(LoginInfo login)
        {
            return loginGateway.AdminLogin(login);
        }

        public List<LoginInfo> UserLogin(LoginInfo login)
        {
            return loginGateway.UserLogin(login);
        }
    }
}