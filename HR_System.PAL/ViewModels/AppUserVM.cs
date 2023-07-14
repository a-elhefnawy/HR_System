using HR_System.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace HR_System.PAL.ViewModels
{
    public class AppUserVM
    {

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string userName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", ErrorMessage = "كلمة المرور يجب أن تكون على الاقل مكونة من 8 خانات وان تحتوي على الاقل على حرف كبير و حرف صغير و رقم")]
        public string password { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "من فضلك ادخل اسم بريد صالح")]
        public string email { get; set; }

        public List<CheckboxVM> Roles { get; set; }

    }
}
