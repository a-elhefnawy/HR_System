using System.ComponentModel.DataAnnotations;

namespace HR_System.PAL.ViewModels
{
    public class LogInVM
    {
        public string userName { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}
