using HR_System.BAL.Interfaces;
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
    public class SalariesRepository : ISalariesReository
    {
        private readonly HRDBContext context;

        public SalariesRepository(HRDBContext context)
        {
            this.context = context;
        }

        public Task<List<EmployeesSalaries>> GetEmployeesSalariesInYearAndMonth(int year, int month)
        {
            return context.EmployeesSalaries.Where(emp => emp.Year == year && emp.Month == month).AsNoTracking().ToListAsync();
        }

        public Task<EmployeesSalaries?> GetEmployeeSalaryInYearAndMonthById(int id,int year, int month)
        {
            return context.EmployeesSalaries.FirstOrDefaultAsync(emp => emp.EmployeeId == id && emp.Year == year && emp.Month == month);
        }

        public async Task<int> Add(EmployeesSalaries employeeSalary)
        {
            await context.EmployeesSalaries.AddAsync(employeeSalary);
            return await context.SaveChangesAsync();
        }
    }
}
