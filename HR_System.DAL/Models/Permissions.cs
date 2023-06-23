
namespace HR_System.DAL.Models
{
   
   [Flags]
   public enum Permissions
   {
        Add = 1,           
        Update = 2,
        Delete = 4,
        Display = 8,
   }
}
