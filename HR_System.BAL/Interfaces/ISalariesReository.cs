using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Interfaces
{
    public interface ISalariesReository
    {
        Task<List<EmployeesSalaries>> GetEmployeesSalariesInYearAndMonth(int year, int month);
        Task<EmployeesSalaries?> GetEmployeeSalaryInYearAndMonthById(int id, int year, int month);
        Task<int> Add(EmployeesSalaries employeeSalary);
    }
}
