using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string CliamType { get; set; }
        public string ClaimValue { get; set; }

        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
