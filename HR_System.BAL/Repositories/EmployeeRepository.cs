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
            return await _dbContext.Employees.Include(x => x.Department).AsNoTracking().ToListAsync();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var EmployeeFromDB = await _dbContext.Employees.FirstOrDefaultAsync(x => x.NationalID == employee.NationalID);

            if (EmployeeFromDB != null)
            {

                EmployeeFromDB.Name = employee.Name;
                EmployeeFromDB.Address = employee.Address;
                EmployeeFromDB.AttendanceTime = employee.AttendanceTime;
                EmployeeFromDB.Birthdate = employee.Birthdate;
                EmployeeFromDB.PhoneNumber = employee.PhoneNumber;
                EmployeeFromDB.Gender = employee.Gender;
                EmployeeFromDB.Nationality = employee.Nationality;
                EmployeeFromDB.NationalID = employee.NationalID;
                EmployeeFromDB.DateOfContract = employee.DateOfContract;
                EmployeeFromDB.Salary = employee.Salary;
                EmployeeFromDB.DepartureTime = employee.DepartureTime;
                EmployeeFromDB.DepartmentId = employee.DepartmentId;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
