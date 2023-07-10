namespace HR_System.PAL.ViewModels
{
    public class EmployeesSalariesVM
    {
        public int? EmployeeId { get; set; }
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
    }
}
