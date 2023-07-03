using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace HR_System.PAL.ViewModels
{
    public class AppUserVM
    {
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MinLength(10)]
        public string fullName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MinLength(5)]
        public string userName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", ErrorMessage = "كلمة المرور يجب أن تكون على الاقل مكونة من 8 خانات وان تحتوي على الاقل على حرف كبير و حرف صغير و رقم")]
        public string password { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "من فضلك ادخل اسم بريد صالح")]
        public string email { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "من فضلك ادخل اسم المجموعة")]
        public string role { get; set; }
        public int role_Id { get; set; }
    }
}
