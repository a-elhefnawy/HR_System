namespace HR_System.PAL.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public IEnumerable<string> Role { get; set; }
    }
}
