using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public  Task<IEnumerable<Employee>> GetAllEmployees();
        Task UpdateEmployee(Employee employee);

        Task SoftDelete(Employee employee);
    }
}
