using HR_System.DAL.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class Employee
    {
        [Key]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "يجب أن يكون الرقم القومي 14 رقم")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [UniqueNationalId]
        public string NationalID { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "من فضلك أدخل رقم تليفون صحيح")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        [AgeGreaterThanOrEqual20]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        // هنا في validation في حالة ان تاريخ التعاقد أقل من تاريخ إنشاء الشركة
        public DateTime DateOfContract { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "من فضلك أدخل راتب صحيح")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Time)]
        public DateTime AttendanceTime { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }
    }
}
