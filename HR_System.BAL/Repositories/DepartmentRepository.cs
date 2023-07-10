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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HRDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _dbContext.Departments.Include(x => x.Employees).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByDeptId(int deptId)
        {
            return await _dbContext.Employees.Where(x => x.DepartmentId == deptId).ToListAsync();
        }

    }
}
