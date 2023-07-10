using HR_System.DAL.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [UniqueName]
        public string Name { get; set; }

        public List<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
