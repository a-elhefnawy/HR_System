using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.ViewModelsForUpdate
{
    public class UpdateEmployeeAttendence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime AttendenceTime { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime LeavingTime { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public DateTime DayDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? DepartmentId { get; set; }

    }
}
