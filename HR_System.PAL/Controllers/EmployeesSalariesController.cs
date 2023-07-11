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
        private readonly IOfficialHolidaysRepository holidaysRepo;
        public EmployeesSalariesController(HRDBContext context,
            IAttendenceRepository attendenceRepository,
            IGeneralSittingsRepository generalSittingsRepository,
            ISalariesReository salariesReository,
            IOfficialHolidaysRepository holidaysRepo)
        {
            this.context = context;
            this.attendenceRepository = attendenceRepository;
            this.generalSittingsRepository = generalSittingsRepository;
            this.salariesReository = salariesReository;
            this.holidaysRepo = holidaysRepo;
        }

        public async Task<IActionResult> Index()
        {
           
            var employeesSalaries = await CalculateEmployeeSalary(DateTime.Now.Year, DateTime.Now.Month);

            return View(employeesSalaries);
        }


        public async Task<IActionResult> EmployeesSalaries(int year, int month)
        {
            //var employeeSalary = await salariesReository.GetEmployeesSalariesInYearAndMonth(year, month);

            var employeeSalary = await CalculateEmployeeSalary(year, month);

            return Json(employeeSalary);
        }

        private async Task<List<EmployeesSalaries>> CalculateEmployeeSalary(int year, int month)
        {
            Salary salary = new Salary(context);
            var employeesAttendance = await attendenceRepository.GetAllAttendnce(year, month);
            employeesAttendance = employeesAttendance.DistinctBy(e => e.EmoloyeeId).ToList();
            var employeesSalaries = new List<EmployeesSalaries>();
            var generalSettings = await generalSittingsRepository.GetGeneralSettings();
            foreach (var item in employeesAttendance)
            {
                //var employeeSalary = await salariesReository.GetEmployeeSalaryInYearAndMonthById((int)item.EmoloyeeId, date.Year, date.Month);


                // Calculations
                int officialHolidays = holidaysRepo.GetOfficialHolidays(year,month);
                int attendance = await salary.CalcAttendanceDays(item.EmoloyeeId, year, month);
                int absence = salary.CalcAbsenceDays(attendance, officialHolidays);
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
                    Month = month,
                    Year = year,
                };

                //await salariesReository.Add(empSalary);
                employeesSalaries.Add(empSalary);
                //}
                ////else
                ////{
                ////    employeesSalaries.Add(employeeSalary);
                ////}
            }
            return employeesSalaries;
        }
    }


}
