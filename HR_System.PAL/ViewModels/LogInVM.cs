using System.ComponentModel.DataAnnotations;

namespace HR_System.PAL.ViewModels
{
    public class LogInVM
    {
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public string userName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}
