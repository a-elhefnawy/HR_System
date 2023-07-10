using HR_System.ViewModel;

namespace HR_System.PAL.ViewModels
{
    public class UserRoleVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<CheckboxVM> Roles { get; set; }

    }
}
