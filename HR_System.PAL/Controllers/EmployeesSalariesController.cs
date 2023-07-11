using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using HR_System.DAL.Models.Calculations;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HR_System.PAL.Controllers
{
    public class EmployeesSalariesController : Controller
    {
        private readonly HRDBContext context;
        private readonly IAttendenceRepository attendenceRepository;
        private readonly IGeneralSittingsRepository generalSittingsRepository;
        private readonly ISalariesReository salariesReository;
        public EmployeesSalariesController(HRDBContext context, 
            IAttendenceRepository attendenceRepository, 
            IGeneralSittingsRepository generalSittingsRepository,
            ISalariesReository salariesReository)
        {
            this.context = context;
            this.attendenceRepository = attendenceRepository;
            this.generalSittingsRepository = generalSittingsRepository;
            this.salariesReository = salariesReository;
        }

        public async Task<IActionResult> Index()
        {
            Salary salary = new Salary(context);
            var date = DateTime.Now;
            var employeesAttendance = await attendenceRepository.GetAllAttendnce(date.Year, date.Month);
            employeesAttendance = employeesAttendance.DistinctBy(e => e.EmoloyeeId).ToList();
            var employeesSalaries = new List<EmployeesSalaries>();
            var generalSettings = await generalSittingsRepository.GetGeneralSettings();
            foreach (var item in employeesAttendance)
            {
                var employeeSalary = await salariesReository.GetEmployeeSalaryInYearAndMonthById((int)item.EmoloyeeId, date.Year, date.Month);

                if (employeeSalary is null)
                {
                    // Calculations
                    int attendance = await salary.CalcAttendanceDays(item.EmoloyeeId, date.Year, date.Month);
                    int absence = salary.CalcAbsenceDays(attendance, 0);
                    int overtime = salary.CalcOvertimePerHours(generalSettings.overTime);
                    int deduction = salary.CalcDeductionPerHours(generalSettings.underTime);
                    decimal salaryOvertime = salary.CalcSalaryOverTime(overtime);
                    decimal salaryDeduction = salary.CalcSalaryDeduction(deduction, absence, generalSettings.underTime, salaryOvertime);
                    decimal actualSalary = salary.CalcSalary(salaryOvertime, salaryDeduction);
                    // End of calculations

                    var empSalary = new EmployeesSalaries()
                    {
                        EmployeeId = item.EmoloyeeId,
                        EmployeeName = item.Employee?.Name,
                        MainSalary = item.Employee?.Salary,
                        DepartmentName = item.Employee?.Department?.Name,
                        AttendanceDays = attendance,
                        AbsenceDays = absence,
                        OvertimePerHours = overtime,
                        DeductionPerHours = deduction,
                        SalaryOverTime = salaryOvertime,
                        SalaryDeduction = salaryDeduction,
                        SalaryOfMonth = actualSalary,
                        Month = date.Month,
                        Year = date.Year,
                    };

                    await salariesReository.Add(empSalary);
                    employeesSalaries.Add(empSalary);
                }
                else
                {
                    employeesSalaries.Add(employeeSalary);
                }
            }
            return View(employeesSalaries);
        }


        //public async Task<IActionResult> Index()
        //{
        //    Salary salary = new Salary(context);
        //    var dateDay = await context.Attendences.Include(x=>x.Employee).Select(x => x.Day).ToListAsync();
        //    var employeesSalaries = new List<EmployeesSalaries>();


        //    foreach (var date in dateDay)
        //    {
        //        var employeesAttendance = await attendenceRepository.GetAllAttendnce(date.Year, date.Month);
        //        employeesAttendance = employeesAttendance.DistinctBy(e => e.EmoloyeeId).Distinct().ToList();

        //        var generalSettings = await generalSittingsRepository.GetGeneralSettings();
        //        foreach (var item in employeesAttendance)
        //        {
        //            var employeeSalary =
        //                await salariesReository.GetEmployeeSalaryInYearAndMonthById((int)item.EmoloyeeId, date.Year, date.Month);

        //            if (employeeSalary is null)
        //            {
        //                // Calculations
        //                int attendance = await salary.CalcAttendanceDays(item.EmoloyeeId, date.Year, date.Month);
        //                int absence = salary.CalcAbsenceDays(attendance, 0);
        //                int overtime = salary.CalcOvertimePerHours(generalSettings.overTime);
        //                int deduction = salary.CalcDeductionPerHours(generalSettings.underTime);
        //                decimal salaryOvertime = salary.CalcSalaryOverTime(overtime);
        //                decimal salaryDeduction = salary.CalcSalaryDeduction(deduction, absence, generalSettings.underTime, salaryOvertime);
        //                decimal actualSalary = salary.CalcSalary(salaryOvertime, salaryDeduction);
        //                // End of calculations

        //                var empSalary = new EmployeesSalaries()
        //                {
        //                    EmployeeId = item.EmoloyeeId,
        //                    EmployeeName = item.Employee?.Name,
        //                    MainSalary = item.Employee?.Salary,
        //                    DepartmentName = item.Employee?.Department?.Name,
        //                    AttendanceDays = attendance,
        //                    AbsenceDays = absence,
        //                    OvertimePerHours = overtime,
        //                    DeductionPerHours = deduction,
        //                    SalaryOverTime = salaryOvertime,
        //                    SalaryDeduction = salaryDeduction,
        //                    SalaryOfMonth = actualSalary,
        //                    Month = date.Month,
        //                    Year = date.Year,
        //                };

        //                await salariesReository.Add(empSalary);
        //                employeesSalaries.Add(empSalary);
        //            }
        //            else
        //            {
        //                employeesSalaries.Add(employeeSalary);
        //            }
        //        }
        //    }
        //    return View(employeesSalaries);
        //}

        public async Task<IActionResult> EmployeesSalaries(int year, int month)
        {
            var employeeSalary = await salariesReository.GetEmployeesSalariesInYearAndMonth(year, month);
            return Json(employeeSalary);
        }
    }
}
