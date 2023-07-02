using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR_System.PAL.Controllers
{
    public class RolesController : Controller 
    {
        private readonly IGenericRepository<Role> _genericRepository;
        private readonly IGenericRepository<PermissionsDB> _perDBRepo;
        private readonly IGenericRepository<PagesName> _pagesName;

        public RolesController(
            IGenericRepository<Role> genericRepository, 
            IGenericRepository<PermissionsDB> perDBRepo, 
            IGenericRepository<PagesName> pagesName)
        {
            _genericRepository = genericRepository;
            _perDBRepo = perDBRepo;
            _pagesName = pagesName;
        }

        [HttpGet]
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
        public async Task<IActionResult> Add([FromBody] RoleViewModel role)
        {
            
            if (ModelState.IsValid)
            {
                var newRole = new Role()
                {
                    Name = role.Name,
                };
               await _genericRepository.Add(newRole);

            }
            var allRoles = await _genericRepository.GetAll();
            var lastRoleId = allRoles.Last().Id;

            foreach (var key in role.Permissions.Keys)
            {
                int sum = 0;
                if (role.Permissions[key].Count > 0) { 
                    foreach(var val in role.Permissions[key])
                    {
                        sum += Convert.ToInt32(val);
                    }
                }
                var permissonObj = new PermissionsDB
                {
                    RoleId = lastRoleId,
                    PageNameId = Convert.ToInt32(key),
                    PermissionNumber = Convert.ToByte(sum)
                };
                await _perDBRepo.Add(permissonObj);
            }



            return View(role);
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


            var role = await _perDBRepo.GetAll();
            var filteredRole = role.Where(p => p.RoleId == id);

         
            return View(filteredRole);
        }
            
    }
}
