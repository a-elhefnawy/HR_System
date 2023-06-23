using System.ComponentModel.DataAnnotations;
namespace HR_System.PAL.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name ="اسم المجموعة")]
        [Required(ErrorMessage = "برجاء ادخال اسم المجموعة")]
        [MinLength(2, ErrorMessage ="min 2 ")]
        public string Name { get; set; }
    }
}
