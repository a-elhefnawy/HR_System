using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using HR_System.DAL.Models.Calculations;
using HR_System.DAL.ViewModelsForUpdate;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.PAL.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IAttendenceRepository attendenceRepo;

        private readonly IDepartmentRepository deptRepo;
        private readonly IEmployeeRepository employeeRepo;
        HRDBContext context;
        public AttendenceController(IAttendenceRepository attendenceRepo, IDepartmentRepository deptRepo, IEmployeeRepository employeeRepo)
        {
            this.attendenceRepo = attendenceRepo;
            this.deptRepo = deptRepo;
            this.employeeRepo = employeeRepo;
            context = new HRDBContext();
        }

        public async Task<IActionResult> Index()
        {
            //Salary salary = new Salary(context);
            //// Calculations 
            //int attendance = await salary.CalcAttendanceDays("30001241401856", 2023, 7);
            //int absence = salary.CalcAbsenceDays(attendance, 0);
            //int overtime = salary.CalcOvertimePerHours(2);
            //int late = salary.CalcDeductionPerHours(2);
            //decimal salaryOvertime = salary.CalcSalaryOverTime(overtime);
            //decimal salaryDeduction = salary.CalcSalaryDeduction(late, absence,2);
            //decimal actualSalary = salary.CalcSalary(salaryOvertime, salaryDeduction);
            //// end of Calculations 

            var attendence = await attendenceRepo.GetAllAttendnce();

            List<EmployeeAttendenceDataVM> employeesAttendence = new List<EmployeeAttendenceDataVM>();
            foreach (var item in attendence)
            {
                EmployeeAttendenceDataVM attendenceVM = new EmployeeAttendenceDataVM()
                {
                    AttendenceTime = item.AttendenceTime,
                    DayDate = item.Day,
                    DepartmentName = item.Employee.Department is null ? "" : item.Employee.Department.Name,
                    LeavingTime = item.LeavingTime,
                    EmployeeName = item.Employee.Name,
                    EmployeeId = item.Id,
                    Id = item.Id


                };
                employeesAttendence.Add(attendenceVM);
            }
            return View(employeesAttendence);
        }


        public async Task<IActionResult> Add()
        {
            var departments = await deptRepo.GetAll();

            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAttendenceDataVMcs newAttendence)
        {
            if (ModelState.IsValid)
            {
                Attendence attendence = new Attendence()
                {
                    AttendenceTime = newAttendence.AttendenceTime,
                    LeavingTime = newAttendence.LeavingTime,
                    Day = newAttendence.DayDate,
                    EmoloyeeId = newAttendence.EmployeeId,
                };
                await attendenceRepo.Add(attendence);
                return RedirectToAction("Index");

            }

            var departments = await deptRepo.GetAll();
            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");

            return View(newAttendence);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employeeAttendace = await attendenceRepo.GetEmployeeAttendenceById(id);
            if (employeeAttendace is null)
            {
                return View("NotFound");
            }

           

            var departments = await deptRepo.GetAllDepartments();
            ViewBag.Departments = departments.
                Where(x => x.Id == employeeAttendace.Employee.DepartmentId);

            var employeesFromDb = await employeeRepo.GetAllEmployees();
            ViewBag.Employees = employeesFromDb.
                Where(x => x.DepartmentId == employeeAttendace.Employee.DepartmentId && x.Id == employeeAttendace.Employee.Id);

            ////ViewBag.Employees = employeesFromDb.FirstOrDefault();
            //ViewBag.Employees = new List<Employee> { employeesFromDb.FirstOrDefault() };




            UpdateEmployeeAttendence attendanceData = new UpdateEmployeeAttendence()
            {
                Id = employeeAttendace.Id,
                AttendenceTime = employeeAttendace.AttendenceTime,
                DayDate = employeeAttendace.Day,
                LeavingTime = employeeAttendace.LeavingTime,
                EmployeeId = employeeAttendace.Employee.Id,
                DepartmentId = employeeAttendace.Employee.DepartmentId



            };

            return View(attendanceData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeAttendence newAttendence)
        {
            var departments = await deptRepo.GetAllDepartments();
            ViewBag.Departments = departments;

            

            if (ModelState.IsValid)
            {
                
                await attendenceRepo.UpdateAttendence(newAttendence);
                return RedirectToAction("Index");

            }

            return View(newAttendence);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await attendenceRepo.Get(id);
            if (attendance is null) return BadRequest();
            attendenceRepo.Delete(attendance);
            return Ok(attendance);
        }

        public IActionResult GetEmployeesByDeptId(int id)
        {
            var employees = context.Employees.Where(x => x.DepartmentId == id).Select(x => new { x.Id, x.Name }).ToList();

            return Json(employees);
        }
    }
}
