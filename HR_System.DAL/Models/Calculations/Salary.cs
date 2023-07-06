using HR_System.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Models.Calculations
{
    public class Salary
    {
        private readonly HRDBContext context;
        private const int workDaysInMonth = 22;
        private const int WorkHoursPerDay = 8;
        private const int monthDays = 30;
        private const int halfHour = 30;
        private Employee? employee;
        private decimal employeeWage;
        private List<Attendence>? employeeAttendance;
        
        public Salary(HRDBContext context)
        {
            this.context = context;
        }

        public async Task<int> CalcAttendanceDays(string EmployeeId ,int year, int month)
        {
            employeeAttendance = await context.Attendences.Include(e => e.Employee)
                                              .Where(attend => 
                                                     attend.EmoloyeeId == EmployeeId &&
                                                     attend.Day.Year == year &&
                                                     attend.Day.Month == month)
                                              .AsNoTracking()
                                              .ToListAsync();

            employee = employeeAttendance.FirstOrDefault()?.Employee;
            employeeWage = (employee?.Salary / monthDays) / WorkHoursPerDay ?? 0m;
            
            return employeeAttendance.Count();
        }

        public int CalcAbsenceDays(int attendanceDays, int vacationsDays)
        {
            return workDaysInMonth - attendanceDays - vacationsDays;
        }

        public int CalcOvertimePerHours(int hourOvertimeValue)
        {
            int overtime = 0;
            employeeAttendance?.ForEach(employeeAttendance =>
            {
                int calculatedHours = employeeAttendance.LeavingTime.Hour - 
                                      employeeAttendance?.Employee?.DepartureTime.Hour?? 0;
                overtime += calculatedHours >= 0 ?
                                 (calculatedHours + (employeeAttendance?.LeavingTime.Minute > halfHour? 1 : 0)) : 0;
            });
            return overtime * hourOvertimeValue;
        }

        public int CalcDeductionPerHours(int hourDeductionValue)
        {
            int lateHours = 0;
            employeeAttendance?.ForEach(employeeAttendance =>
            {
                int calculatedHours = employeeAttendance.AttendenceTime.Hour - 
                                      employeeAttendance?.Employee?.AttendanceTime.Hour?? 0;
                lateHours += calculatedHours >= 0 ?
                                 (calculatedHours + (employeeAttendance?.AttendenceTime.Minute > halfHour ? 1 : 0)) : 0;
            });
            return lateHours * hourDeductionValue;
        }

        public decimal CalcSalaryOverTime(int overTimePerHours)
        {
            return overTimePerHours * employeeWage;
        }

        public decimal CalcSalaryDeduction(int deductionPerHours, int absenceDays)
        {
            return (deductionPerHours + (absenceDays * WorkHoursPerDay) ) * employeeWage;
        }

        public decimal CalcSalary(decimal salaryOvertime, decimal salaryDeduction)
        {
            decimal employeeSalary = employee?.Salary?? 0m;
            decimal salary = employeeSalary + salaryOvertime - salaryDeduction;
            return Math.Max(salary, 0m);
        } 

    }
}
