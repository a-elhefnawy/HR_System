using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using HR_System.DAL.Models.Calculations;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.PAL.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IAttendenceRepository attendenceRepo;

        private readonly IDepartmentRepository deptRepo;
        HRDBContext context;
        public AttendenceController(IAttendenceRepository attendenceRepo, IDepartmentRepository deptRepo)
        {
            this.attendenceRepo = attendenceRepo;
            this.deptRepo = deptRepo;
            context = new HRDBContext();
        }
        
        public async Task<IActionResult> Index()
        {
            Salary salary = new Salary(context);
            // Calculations 
            int attendance = await salary.CalcAttendanceDays("30012421401856", 2023, 7);
            int absence = salary.CalcAbsenceDays(attendance, 0);
            int overtime = salary.CalcOvertimePerHours(2);
            int late = salary.CalcDeductionPerHours(2);
            decimal salaryOvertime = salary.CalcSalaryOverTime(overtime);
            decimal salaryDeduction = salary.CalcSalaryDeduction(late, absence);
            decimal actualSalary = salary.CalcSalary(salaryOvertime, salaryDeduction);
            // end of Calculations 

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
                    EmployeeName= item.Employee.Name,
                    EmployeeId =  item.EmoloyeeId,
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

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var attendace = await attendenceRepo.Get(id);
        //    if(attendace is null)
        //    {
        //        return NotFound();
        //    }
            
        //    AddAttendenceDataVMcs attendanceData = new AddAttendenceDataVMcs()
        //    {
        //        AttendenceTime = attendace.AttendenceTime,
        //        DayDate = attendace.Day,
        //        LeavingTime = attendace.LeavingTime,
        //        EmployeeId = attendace.EmoloyeeId
        //    };

        //    return View(attendanceData);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(AddAttendenceDataVMcs newAttendence)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Attendence attendence = new Attendence()
        //        {
        //            AttendenceTime = newAttendence.AttendenceTime,
        //            LeavingTime = newAttendence.LeavingTime,
        //            Day = newAttendence.DayDate,
        //            EmoloyeeId = newAttendence.EmployeeId,
        //        };
        //        await attendenceRepo.Add(attendence);
        //    }

        //    return View(newAttendence);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await attendenceRepo.Get(id);
            if (attendance is null) return BadRequest();
            attendenceRepo.Delete(attendance);
            return Ok(attendance);
        }

        public IActionResult GetEmployeesByDeptId(int id)
        {
            var employees = context.Employees.Where(x => x.DepartmentId == id).Select(x => new { x.NationalID, x.Name }).ToList();

            return Json(employees);
        }
    }
}
