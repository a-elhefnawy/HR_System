using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class PagesName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PermissionsDB> PermissionsDB { get; set; } = new();
    }
}
