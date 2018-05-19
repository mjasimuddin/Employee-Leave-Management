using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;


namespace EmployeeLeaveManagementApp.DLL
{
    public class SuperadminGateway : CommonConnection
    {
        private readonly SqlConnection _conn = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["EmployeeLeaveDB"].ConnectionString);
        public int AddEmployee(Employee login)
        {
            string query = "INSERT INTO Employee (EmployeeUserId,EmployeeName,Email,DesignationId,PhoneNo,Address) VALUES ('" + login.EmployeeUserId + "','" +
                           login.EmployeeName + "','" + login.Email + "','" + login.DesignationId + "','" +
                           login.PhoneNo + "','" + login.Address + "')";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public int SetEmployeePassword(EmployeePassword employee)
        {
            string query = "INSERT INTO EmployeePassword (EmployeeId,Password) VALUES ('" + employee.EmployeeId + "','" + employee.Password + "')";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }


        
        public int SetEmployeeUserType(EmployeeUserType employee)
        {
            string query = "INSERT INTO EmployeeUserType (EmployeeId,UserTypeId) VALUES ('" + employee.EmployeeId + "','" + employee.UserTypeId + "')";
            try
            {
                _conn.Open();
                SqlCommand command = new SqlCommand(query, _conn);
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                _conn.Close();
            }
        }


        public List<Employee> GetEmployeeById(int id)
        {
            string query1 = @"Select e.Id, e.EmployeeUserId,e.EmployeeName, e.Email, d.DesignationName
            from Employee e
            inner join Designation d on d.Id = e.DesignationId 
            where e.Id = '" + id + "'";
            try
            {
                var command = new SqlCommand(query1, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var employees = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        Id = (int) reader["Id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["DesignationName"].ToString()
                    };
                    employees.Add(employee);
                }
                reader.Close();
                return employees;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<Designation> GetDesignationList()
        {
            const string query = "SELECT * FROM Designation";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var designationList = new List<Designation>();
                while (reader.Read())
                {
                    var designation = new Designation();
                    designation.Id = (int)reader["Id"];
                    designation.DesignationName = reader["DesignationName"].ToString();
                    designationList.Add(designation);
                }
                reader.Close();
                return designationList;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<LeaveType> GetLeaveType()
        {
            const string query = "SELECT * FROM LeaveType";
            try
            {
                var Command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                var leaveTypes = new List<LeaveType>();
                while (reader.Read())
                {
                    var leaveType = new LeaveType
                    {
                        Id = (int)reader["Id"],
                        LeaveTypeName = reader["LeaveTypeName"].ToString()
                    };
                    leaveTypes.Add(leaveType);
                }
                reader.Close();
                return leaveTypes;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<UserType> GetUserType()
        {
            const string query = "SELECT * FROM UserType";
            try
            {
                var Command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                var userTypes = new List<UserType>();
                while (reader.Read())
                {
                    var userType = new UserType
                    {
                        Id = (int)reader["Id"],
                        UserTypeName = reader["UserTypeName"].ToString()
                    };
                    userTypes.Add(userType);
                }
                reader.Close();
                Connection.Close();
                return userTypes;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public int EmployeeLeaveAllocation(EmployeeLeaveAllocation employeeLeave)
        {
            string query = "INSERT INTO EmployeeLeaveAllocation (DesignationId,LeaveTypeId,TotalLeave) VALUES ('" + employeeLeave.DesignationId + "','" + employeeLeave.LeaveTypeId + "','" + employeeLeave.TotalLeave + "')";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<EmployeeLeaveAllocation> AllLeaveInfo(int employeeId)
        {
            string query = @"select e.Id, a.LeaveTypeId, a.TotalLeave
            from Employee e
            inner join EmployeeLeaveAllocation a on e.DesignationId = a.DesignationId
            where e.Id = '" + employeeId + "' and a.LeaveTypeId = '" + 1 + "'";
            try
            {
                var com = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader dr = com.ExecuteReader();
                var totalLeave = new List<EmployeeLeaveAllocation>();
                while (dr.Read())
                {
                    var leave = new EmployeeLeaveAllocation();
                    leave.Id = (int)dr["Id"];
                    leave.TotalLeave = (int)dr["TotalLeave"];
                    totalLeave.Add(leave);
                }
                dr.Close();
                return totalLeave.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<Employee> GetAllEmployees()
        {
            string query = "SELECT *FROM Employee";
            try
            {
                SqlCommand command = new SqlCommand(query, _conn);
                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                var listOfEmployee = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        Id = (int)reader["Id"],
                        DesignationId = (int)reader["DesignationId"],
                        EmployeeUserId = reader["EmployeeUserId"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString()
                    };
                    listOfEmployee.Add(employee);
                }
                reader.Close();
                return listOfEmployee;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<EmployeeLeaveInfo> GetEmployeeLeaveApplication()
        {
            const string query = @"SELECT e.Id,e.Reason, CONVERT(NVARCHAR,e.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,e.EndDate, 100) AS[EndDate],e.TotalDay, e.Status,CONVERT(NVARCHAR,e.EntryDate, 100) AS[EntryDate], a.EmployeeName,a.Email,p.LeaveTypeName
            FROM EmployeeLeaveRequest e
            INNER JOIN Employee a ON e.EmployeeId = a.Id
            INNER JOIN LeaveType p ON e.LeaveTypeId = p.Id Where e.Status = '" + "Pending" + "'";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var employeeLeaveInfo = new List<EmployeeLeaveInfo>();
                while (reader.Read())
                {
                    var employeeLeave = new EmployeeLeaveInfo
                    {
                        Id = (int) reader["Id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        LeaveTypeName = reader["LeaveTypeName"].ToString(),
                        Reason = reader["Reason"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        TotalDay = (int) reader["TotalDay"],
                        EntryDate = reader["EntryDate"].ToString(),
                        Status = reader["Status"].ToString()
                    };

                    employeeLeaveInfo.Add(employeeLeave);
                }
                reader.Close();
                return employeeLeaveInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<EmployeeLeaveInfo> ShowAllLeaveStatus()
        {
            const string query = @"SELECT e.Id,e.Reason, CONVERT(NVARCHAR,e.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,e.EndDate, 100) AS[EndDate],e.TotalDay, e.Status,CONVERT(NVARCHAR,e.EntryDate, 100) AS[EntryDate], a.EmployeeName,a.Email,p.LeaveTypeName
            FROM EmployeeLeaveRequest e
            INNER JOIN Employee a ON e.EmployeeId = a.Id
            INNER JOIN LeaveType p ON e.LeaveTypeId = p.Id";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var employeeLeaveInfo = new List<EmployeeLeaveInfo>();
                int serial = 1;
                while (reader.Read())
                {
                    var employeeLeave = new EmployeeLeaveInfo
                    {
                        Id = serial,
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        LeaveTypeName = reader["LeaveTypeName"].ToString(),
                        Reason = reader["Reason"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        TotalDay = (int) reader["TotalDay"],
                        EntryDate = reader["EntryDate"].ToString(),
                        Status = reader["Status"].ToString()
                    };

                    employeeLeaveInfo.Add(employeeLeave);
                    serial++;
                }
                reader.Close();
                return employeeLeaveInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public int Approve(int? id)
        {
            string query = @"UPDATE [dbo].[EmployeeLeaveRequest] SET [Status] = '" + "Approved" + "' WHERE [Id] = " + id + "";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public int Reject(int? id)
        {
            string query = @"UPDATE [dbo].[EmployeeLeaveRequest] SET [Status] = '" + "Rejected" + "' WHERE [Id] = " + id + "";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<SubmitedApplicationInfo> GetUserEmail(int? id)
        {
            string query = @"select p.EmployeeName, p.Email, p.Id,e.Reason,CONVERT(NVARCHAR,e.StartDate, 100) AS[StartDate], CONVERT(NVARCHAR,e.EndDate, 100) AS[EndDate], e.TotalDay, CONVERT(NVARCHAR,e.EntryDate, 100) AS[EntryDate]
            From EmployeeLeaveRequest e
            inner join Employee p on e.EmployeeId=p.Id
            where e.Id = '" + id + "' ";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var ListOfEmployee = new List<SubmitedApplicationInfo>();
                while (reader.Read())
                {
                    var employee = new SubmitedApplicationInfo
                    {
                        Id = (int) reader["Id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Reason = reader["Reason"].ToString(),
                        StartDate = reader["StartDate"].ToString(),
                        EndDate = reader["EndDate"].ToString(),
                        EntryDate = reader["EntryDate"].ToString(),
                        TotalDay = reader["TotalDay"].ToString()
                    };
                    ListOfEmployee.Add(employee);
                }
                reader.Close();
                return ListOfEmployee;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<SubmitedApplicationInfo> GetUserEmailAndName(int? id)
        {
            string query = "Select EmployeeName, Email from Employee where Id = '" + id + "'";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var listOfEmployee = new List<SubmitedApplicationInfo>();
                while (reader.Read())
                {
                    var employee = new SubmitedApplicationInfo();
                    employee.EmployeeName = reader["EmployeeName"].ToString();
                    employee.Email = reader["Email"].ToString();
                    listOfEmployee.Add(employee);
                }
                reader.Close();
                return listOfEmployee;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<EmployeeLeaveRequest> GetTotalSickLeaveByEmployeeId(int employeeId)
        {
            string query = @"SELECT SUM(TotalDay) as number
            FROM EmployeeLeaveRequest
            WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 1 + "' and Status = '" + "Approve" + "'";
            try
            {
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var totalSickLeaves = new List<EmployeeLeaveRequest>();
                while (reader.Read())
                {
                    EmployeeLeaveRequest leave = new EmployeeLeaveRequest();
                    if (!object.ReferenceEquals(reader["number"], DBNull.Value))
                    {
                        leave.TotalDay = Convert.ToInt32(reader["number"]);

                    }
                    else
                    {
                        leave.TotalDay = 0;
                    }
                    totalSickLeaves.Add(leave);
                }
                reader.Close();
                return totalSickLeaves.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<EmployeeLeaveRequest> GetTotalCasualLeaveByEmployeeId(int employeeId)
        {
            string query = @"SELECT SUM(TotalDay) as number
            FROM EmployeeLeaveRequest
            WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 2 + "' and Status = '" + "Approve" + "'";
            try
            {
                var com = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader dr = com.ExecuteReader();
                var totalSickLeaves = new List<EmployeeLeaveRequest>();
                while (dr.Read())
                {
                    var leave = new EmployeeLeaveRequest
                    {
                        TotalDay = !object.ReferenceEquals(dr["number"], DBNull.Value) ? Convert.ToInt32(dr["number"]) : 0
                    };

                    totalSickLeaves.Add(leave);
                }
                dr.Close();
                return totalSickLeaves.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public List<EmployeeLeaveAllocation> TotalCasualLeave(int employeeId)
        {
            string query = @"select e.Id, a.LeaveTypeId, a.TotalLeave
            from Employee e
            inner join EmployeeLeaveAllocation a on e.DesignationId = a.DesignationId
            where e.Id = '" + employeeId + "' and a.LeaveTypeId = '" + 2 + "'";
            try
            {
                var com = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader dr = com.ExecuteReader();
                var totalLeave = new List<EmployeeLeaveAllocation>();
                while (dr.Read())
                {
                    EmployeeLeaveAllocation leave = new EmployeeLeaveAllocation();
                    leave.Id = (int)dr["Id"];
                    leave.TotalLeave = (int)dr["TotalLeave"];
                    totalLeave.Add(leave);
                }
                dr.Close();
                return totalLeave.ToList();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public bool IsEmailExist(string email)
        {
            try
            {
                bool isExists = false;

                const string query = "SELECT Email FROM Employee WHERE (Email= @Email) ";
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                command.Parameters.Clear();

                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isExists = true;
                }

                reader.Close();
                return isExists;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
        public bool IsPasswordSet(EmployeePassword employee)
        {
            try
            {
                const string query = "select * from EmployeePassword where (EmployeeId = @EmployeeId)";
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                command.Parameters.Clear();
                command.Parameters.Add("EmployeeId", SqlDbType.Int);
                command.Parameters["EmployeeId"].Value = employee.EmployeeId;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isExist = reader.HasRows;
                reader.Close();
                return isExist;
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
        public bool IsUserRoleSet(EmployeeUserType leaveRequest)
        {
            try
            {
                const string query = "select * from EmployeeUserType where (EmployeeId = @EmployeeId and UserTypeId = @UserTypeId)";
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                command.Parameters.Clear();
                command.Parameters.Add("EmployeeId", SqlDbType.Int);
                command.Parameters["EmployeeId"].Value = leaveRequest.EmployeeId;
                command.Parameters.Add("UserTypeId", SqlDbType.Int);
                command.Parameters["UserTypeId"].Value = leaveRequest.UserTypeId;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isExist = reader.HasRows;
                reader.Close();
                return isExist;
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to Connect Server", exception);
            }
        }
        public bool IsLeaveAllocated(EmployeeLeaveAllocation leaveAllocation)
        {
            try
            {
                const string query = "select * from EmployeeLeaveAllocation where (DesignationId = @DesignationId and LeaveTypeId = @LeaveTypeId)";
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                command.Parameters.Clear();
                command.Parameters.Add("DesignationId", SqlDbType.Int);
                command.Parameters["DesignationId"].Value = leaveAllocation.DesignationId;
                command.Parameters.Add("LeaveTypeId", SqlDbType.Int);
                command.Parameters["LeaveTypeId"].Value = leaveAllocation.LeaveTypeId;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isExist = reader.HasRows;
                reader.Close();
                return isExist;
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public bool IsLeaveRequest(EmployeeLeaveRequest employeeLeaveRequest)
        {
            try
            {
                const string query = "select * from EmployeeLeaveRequest where (StartDate <= @EndDate AND EndDate >= @StartDate AND EmployeeId = @EmployeeId)";
                var command = new SqlCommand(query, Connection);
                Connection.Open();
                command.Parameters.Clear();
                command.Parameters.Add("StartDate", SqlDbType.Date);
                command.Parameters["StartDate"].Value = employeeLeaveRequest.StartDate;
                command.Parameters.Add("EndDate", SqlDbType.Date);
                command.Parameters["EndDate"].Value = employeeLeaveRequest.EndDate;
                command.Parameters.Add("EmployeeId", SqlDbType.Int);
                command.Parameters["EmployeeId"].Value = employeeLeaveRequest.EmployeeId;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isExist = reader.HasRows;
                reader.Close();
                return isExist;
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
        public List<Employee> GetallEmployeeByDesignationId(int designationId)
        {
            string query = "Select Id,EmployeeId from Employee where DesignationId=@DesignationId";
            try
            {
                var cmd = new SqlCommand(query, Connection);
                Connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("DesignationId", designationId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = (int) reader["Id"];
                    employee.EmployeeUserId = reader["EmployeeId"].ToString();
                    employees.Add(employee);
                }
                reader.Close();
                return employees;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server",exception);
            }
            finally
            {
                Connection.Close();
            }
        }
        public List<LoginInfo> GetUserRole(int id)
        {
            string query1 = @"SELECT e.Id, e.EmployeeName, e.Email, p.Password, u.UserTypeId
            FROM Employee e
            INNER JOIN EmployeePassword p on p.EmployeeId = e.Id
            INNER JOIN EmployeeUserType u on u.EmployeeId = e.Id
            where e.Id = '" + id + "'";
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
                        Id = (int)reader["Id"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        UserTypeId = (int)reader["UserTypeId"]
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