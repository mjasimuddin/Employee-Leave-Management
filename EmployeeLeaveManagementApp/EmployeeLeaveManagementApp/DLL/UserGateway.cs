using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.DLL
{
    public class UserGateway : CommonConnection
    {
        public int SendEmployeeLeaveApplication(EmployeeLeaveRequest leaveRequest)
        {
            string query ="INSERT INTO EmployeeLeaveRequest (EmployeeId,LeaveTypeId,Reason,StartDate,EndDate,EntryDate,TotalDay,Status) VALUES ('" + leaveRequest.EmployeeId + "','" + leaveRequest.LeaveTypeId + "','" + leaveRequest.Reason + "','" + leaveRequest.StartDate + "','" + leaveRequest.EndDate + "','" + leaveRequest.EntryDate + "','" + leaveRequest.TotalDay + "','" + leaveRequest.Status + "')";
            try
            {
                SqlCommand Command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = Command.ExecuteNonQuery();
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

        public List<EmployeeLeaveInfo> UserLeaveStatus(int? leave)
        {
            string query = @"SELECT e.Id,e.Reason, CONVERT(NVARCHAR,e.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,e.EndDate, 100) AS[EndDate],e.TotalDay, e.Status,CONVERT(NVARCHAR,e.EntryDate, 100) AS[EntryDate], a.EmployeeName,a.Email,p.LeaveTypeName
            FROM EmployeeLeaveRequest e
            INNER JOIN Employee a ON e.EmployeeId = a.Id
            INNER JOIN LeaveType p ON e.LeaveTypeId = p.Id Where e.EmployeeId = '" + leave + "'";
            try
            {
                SqlCommand Command = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<EmployeeLeaveInfo> employeeLeaveInfo = new List<EmployeeLeaveInfo>();
                int serial = 1;
                while (reader.Read())
                {
                    EmployeeLeaveInfo employeeLeave = new EmployeeLeaveInfo();

                    employeeLeave.Id = serial;
                    employeeLeave.EmployeeName = reader["EmployeeName"].ToString();
                    employeeLeave.Email = reader["Email"].ToString();
                    employeeLeave.LeaveTypeName = reader["LeaveTypeName"].ToString();
                    employeeLeave.Reason = reader["Reason"].ToString();
                    employeeLeave.StartDate = reader["StartDate"].ToString();
                    employeeLeave.EndDate = reader["EndDate"].ToString();
                    employeeLeave.TotalDay = (int)reader["TotalDay"];
                    employeeLeave.EntryDate = reader["EntryDate"].ToString();
                    employeeLeave.Status = reader["Status"].ToString();

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
    }
}