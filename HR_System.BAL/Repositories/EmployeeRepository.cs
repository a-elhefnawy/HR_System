using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _dbContext.Employees.Where(x=>x.IsDelete == false).Include(x => x.Department).AsNoTracking().ToListAsync();
        }

        public async Task SoftDelete(Employee employee)
        {
            employee.IsDelete = true;
            await UpdateEmployee(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
