using HR_System.BAL.Interfaces;
using HR_System.BAL.Repositories;
using HR_System.Constants;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.PAL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        [Authorize(Permissions.Department.View)]
        public async Task<IActionResult> Index()
        {
            var allDepartments = await departmentRepository.GetAll();
            return View(allDepartments);
        }
        [Authorize(Permissions.Department.Create)]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Department department)
        {
            if (ModelState.IsValid)
            {
                await departmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [Authorize(Permissions.Department.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await departmentRepository.Get(id);
            if (department is null) return View("Not Found");
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepository.Update(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [Authorize(Permissions.Department.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await departmentRepository.Get(id);
            if (department is null) return NotFound();
            var departmentEmployees = await departmentRepository.GetEmployeeByDeptId(id);
            if (departmentEmployees.Count() > 0) return BadRequest();
            departmentRepository.Delete(department);
            return Ok(department);
        }
    }
}
