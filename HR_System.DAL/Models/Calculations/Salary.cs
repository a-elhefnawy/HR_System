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
        private const int WORK_DAYS_IN_MONTH = 22;
        private const int WORK_HOURS_PER_DAY = 8;
        private const int MONTH_DAYS = 30;
        private const int HALF_HOUR = 30;
        private const int DIGITS_AFTER_POINT = 2;
        private Employee? employee;
        private decimal employeeWage;
        private List<Attendence>? employeeAttendance;
        
        public Salary(HRDBContext context)
        {
            this.context = context;
        }
        
        public async Task<int> CalcAttendanceDays(int? EmployeeId ,int year, int month)
        {
            employeeAttendance = await context.Attendences.Include(e => e.Employee)
                                              .Where(attend => 
                                                     attend.EmoloyeeId == EmployeeId &&
                                                     attend.Day.Year == year &&
                                                     attend.Day.Month == month)
                                              .AsNoTracking()
                                              .ToListAsync();

            employee = employeeAttendance.FirstOrDefault()?.Employee;
            employeeWage = (employee?.Salary / MONTH_DAYS) / WORK_HOURS_PER_DAY ?? 0m;
            
            return employeeAttendance.Count();
        }

        public int CalcAbsenceDays(int attendanceDays, int vacationsDays)
        {
            return WORK_DAYS_IN_MONTH - attendanceDays - vacationsDays;  
        }

        public int CalcOvertimePerHours(int hourOvertimeValue)
        {
            int overtime = 0;
            employeeAttendance?.ForEach(employeeAttendance =>
            {
                int calculatedHours = employeeAttendance.LeavingTime.Hour - 
                                      employeeAttendance?.Employee?.DepartureTime.Hour?? 0;
                overtime += calculatedHours >= 0 ?
                                 (calculatedHours + (employeeAttendance?.LeavingTime.Minute > HALF_HOUR? 1 : 0)) : 0;
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
                                 (calculatedHours + (employeeAttendance?.AttendenceTime.Minute > HALF_HOUR ? 1 : 0)) : 0;
            });
            return lateHours * hourDeductionValue;
        }

        public decimal CalcSalaryOverTime(int overTimePerHours)
        {
            return Math.Round(overTimePerHours * employeeWage, DIGITS_AFTER_POINT);
        }

        public decimal CalcSalaryDeduction(int deductionPerHours)
        {
            decimal deduction = Math.Round((deductionPerHours  * employeeWage ));
            return deduction >= employee?.Salary? Math.Round(employee.Salary,DIGITS_AFTER_POINT)  : deduction ;
        }

        public decimal CalcSalary(decimal salaryOvertime, decimal salaryDeduction)
        {
            //decimal employeeSalary = employee?.Salary?? 0m;
            decimal employeeSalary = employeeWage * WORK_HOURS_PER_DAY * employeeAttendance.Count();
            decimal salary = employeeSalary + salaryOvertime - salaryDeduction;
            return Math.Max(Math.Round(salary, DIGITS_AFTER_POINT), 0m);
        } 

    }
}