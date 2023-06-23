using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class RolesController : Controller
    {
        private readonly IGenericRepository<Role> _genericRepository;

        public RolesController(IGenericRepository<Role> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _genericRepository.GetAll();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel role)
        {
            if(ModelState.IsValid)
            {
                var newRole = new Role()
                {
                    Name = role.Name,
                };
                int result = await _genericRepository.Add(newRole);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
    }
}
