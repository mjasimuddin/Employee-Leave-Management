using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class EmployeeLeaveAllocation
    {
        public int Id { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int TotalLeave { get; set; }
    }
}