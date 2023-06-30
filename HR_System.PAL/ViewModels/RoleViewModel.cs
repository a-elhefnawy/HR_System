using System.ComponentModel.DataAnnotations;
namespace HR_System.PAL.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name="أسم المجموعة")]
        [Required(ErrorMessage ="يجب إدخال اسم المجموعة")]
        public string Name { get; set; }
    }
}
