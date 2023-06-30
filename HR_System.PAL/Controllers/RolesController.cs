using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class RolesController : Controller
    {
        private readonly IGenericRepository<Role> _genericRepository;
        private readonly IGenericRepository<PermissionsDB> _perDBRepo;
        private readonly IGenericRepository<PagesName> _pagesName;
        public RolesController(IGenericRepository<Role> genericRepository, 
            IGenericRepository<PermissionsDB> perDBRepo, 
            IGenericRepository<PagesName> pagesName)
        {
            _genericRepository = genericRepository;
            _perDBRepo = perDBRepo;
            _pagesName = pagesName;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _genericRepository.GetAll();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var pagesName = await _pagesName.GetAll();
            
            ViewBag.pagesName = pagesName;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel role,[FromBody] string x)
        {

            return Content(x);
            //if (ModelState.IsValid)
            //{
            //    var newRole = new Role()
            //    {
            //        Name = role.Name,   
            //    };
            //    int result = await _genericRepository.Add(newRole);
            //    if (result > 0)
            //        return RedirectToAction(nameof(Index));
            //}

            //return View(role);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var role = await _genericRepository.Get(id);
            if (role == null) return BadRequest();
            _genericRepository.Delete(role);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int id)
        {
            var roleDetails = await _genericRepository.Get(id);
            return View(roleDetails);
        }
            
    }
}
