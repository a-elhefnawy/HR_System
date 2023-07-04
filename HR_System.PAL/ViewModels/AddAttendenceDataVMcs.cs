using System.ComponentModel.DataAnnotations;

namespace HR_System.PAL.ViewModels
{
    public class AddAttendenceDataVMcs
    {

        
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime AttendenceTime { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime LeavingTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public DateTime DayDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string DepartmentId { get; set; }


    }
}
