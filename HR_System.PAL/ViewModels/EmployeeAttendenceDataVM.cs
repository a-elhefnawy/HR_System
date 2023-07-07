namespace HR_System.PAL.ViewModels
{
    public class EmployeeAttendenceDataVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName  { get; set; }
        public DateTime AttendenceTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public DateTime DayDate { get; set; }
    }
}
