using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository<Employee> EmployeeRepo;
        public EmployeeController(IGenericRepository<Employee> _empRepo)
        {
            EmployeeRepo = _empRepo;            
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var emps = await EmployeeRepo.GetAll();
            return View(emps);
        }

        //[Authorize]
        public async Task<IActionResult> Detailes(string id)
        {
            var emp = await EmployeeRepo.Get(id);
            return View(emp);
        }

        //[Authorize]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Employee employee)
        {
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
        public async Task<IActionResult> Edit(string id)
        {
            var emp = await EmployeeRepo.Get(id);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                int result = EmployeeRepo.Update(employee);
                if(result > 0)
                    return RedirectToAction("Index");
            }
            return View(employee);
        }

        //[Authorize]
        public async Task<IActionResult> Delete(string Id)
        {
            var emp = await EmployeeRepo.Get(Id);
            if (emp == null) return BadRequest();
            EmployeeRepo.Delete(emp);    
            return RedirectToAction(nameof(Index));
        }

    }
}
