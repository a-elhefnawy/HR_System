using System.ComponentModel.DataAnnotations;
namespace HR_System.PAL.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name="أسم المجموعة")]
        [Required(ErrorMessage = "من فضلك ادخل اسم المجموعة ")]
        public string Name { get; set; }
        
        
        public Dictionary<string, List<int>> Permissions { get; set; }

    }
}
