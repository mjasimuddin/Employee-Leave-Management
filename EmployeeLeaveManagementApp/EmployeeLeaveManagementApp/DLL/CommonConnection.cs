using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EmployeeLeaveManagementApp.DLL
{
    public class CommonConnection
    {
        public string connectionString = WebConfigurationManager.ConnectionStrings["EmployeeLeaveDB"].ConnectionString;
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }

        public CommonConnection()
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
    }
}