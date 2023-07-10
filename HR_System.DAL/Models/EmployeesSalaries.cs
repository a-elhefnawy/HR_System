using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class EmployeesSalaries
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        public string? EmployeeName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? MainSalary { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsenceDays { get; set; }
        public int OvertimePerHours { get; set; }
        public int DeductionPerHours { get; set; }
        public decimal SalaryOverTime { get; set; }
        public decimal SalaryDeduction { get; set; }
        public decimal SalaryOfMonth { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
