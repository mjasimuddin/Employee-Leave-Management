using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.DLL
{
    public class LoginGateway : CommonConnection
    {
        public List<LoginInfo> SuperadminLogin(LoginInfo employee)
        {
            employee.UserTypeId = 1;
            string query1 = @"SELECT e.Id, e.EmployeeName, e.Email, p.Password, u.UserTypeId
              FROM Employee e
              INNER JOIN EmployeePassword p on p.EmployeeId = e.Id
              INNER JOIN EmployeeUserType u on u.EmployeeId = e.Id
              where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" +
                            employee.UserTypeId + "'";
            try
            {
                var Command = new SqlCommand(query1, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                var userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    var login = new LoginInfo
                    {
                        Id = (int) reader["id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        UserTypeId = (int) reader["UserTypeId"]
                    };
                    userInfo.Add(login);
                }
                reader.Close();
                Connection.Close();
                return userInfo;
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to connect server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<LoginInfo> AdminLogin(LoginInfo employee)
        {
            employee.UserTypeId = 2;
            string query1 = @"SELECT e.Id, e.EmployeeName, e.Email, p.Password, u.UserTypeId
              FROM Employee e
              INNER JOIN EmployeePassword p on p.EmployeeId = e.Id
              INNER JOIN EmployeeUserType u on u.EmployeeId = e.Id
              where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" +
                            employee.UserTypeId + "'";
            try
            {
                SqlCommand Command = new SqlCommand(query1, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo login = new LoginInfo();
                    login.Id = (int)reader["id"];
                    login.EmployeeName = reader["EmployeeName"].ToString();
                    login.Email = reader["Email"].ToString();
                    login.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(login);
                }
                reader.Close();
                Connection.Close();
                return userInfo;
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to connect server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<LoginInfo> UserLogin(LoginInfo employee)
        {
            employee.UserTypeId = 3;
            string query1 = @"SELECT e.Id, e.EmployeeName, e.Email, p.Password, u.UserTypeId
              FROM Employee e
              INNER JOIN EmployeePassword p on p.EmployeeId = e.Id
              INNER JOIN EmployeeUserType u on u.EmployeeId = e.Id
              where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" +
                            employee.UserTypeId + "'";
            try
            {
                var command = new SqlCommand(query1, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    var login = new LoginInfo
                    {
                        Id = (int) reader["id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        UserTypeId = (int) reader["UserTypeId"]
                    };
                    userInfo.Add(login);
                }
                reader.Close();
                Connection.Close();
                return userInfo;
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to connect server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}