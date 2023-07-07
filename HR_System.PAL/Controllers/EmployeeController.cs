using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.PAL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepo;
        private readonly IDepartmentRepository departmentRepo;

        public EmployeeController(IEmployeeRepository _empRepo, IDepartmentRepository departmentRepo)
        {
            EmployeeRepo = _empRepo;
            this.departmentRepo = departmentRepo;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var emps = await EmployeeRepo.GetAllEmployees();
            return View(emps);
        }

        //[Authorize]
        public async Task<IActionResult> Detailes(int id)
        {
            var emp = await EmployeeRepo.Get(id);
            return View(emp);
        }

        //[Authorize]
        public async Task<IActionResult> Add()
        {
            var departments = await departmentRepo.GetAll();

            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Employee employee)
        {
            var departments = await departmentRepo.GetAll();

            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");
            if (ModelState.IsValid)
            {
                employee.Birthdate = employee.Birthdate.Date;
                employee.DateOfContract = employee.DateOfContract.Date;
               
                await EmployeeRepo.Add(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //[Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var excistingEmployee = await EmployeeRepo.Get(id);
            if (excistingEmployee is null) return View("NotFound");

            var departments = await departmentRepo.GetAll();

            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");

            return View(excistingEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Employee employee)
        {
            if (id != employee.Id) return View("NotFound");

            var departments = await departmentRepo.GetAll();

            ViewBag.Deptartments = new SelectList(departments, "Id", "Name");

            if (ModelState.IsValid)
            {

                await EmployeeRepo.UpdateEmployee(employee);
                return RedirectToAction("Index");

            }
            return View(employee);
        }

        //[Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
            var emp = await EmployeeRepo.Get(Id);
            if (emp == null) return BadRequest();
            EmployeeRepo.Delete(emp);    
            return RedirectToAction(nameof(Index));
        }

    }
}
