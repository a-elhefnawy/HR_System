using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_System.PAL.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserRepository _usersRepository;
        private readonly IGenericRepository<Role> genericRole;

        public UserController(IAppUserRepository usersRepository, IGenericRepository<Role> genericRole)
        {
            _usersRepository = usersRepository;
            this.genericRole = genericRole;
        }
        public async Task<IActionResult> Index()
        {
            var users= await _usersRepository.GetAll();
            List<AppUserVM> userList = new List<AppUserVM>();
            foreach (var user in users)
            {
                AppUserVM userVM = new AppUserVM()
                {
                    Id=user.Id,
                    fullName=user.fullName,
                    userName=user.UserName,
                    password=user.PasswordHash,
                    email=user.Email,
                    role=user.Role.Name,
                    role_Id=user.RoleId,
                };
                userList.Add(userVM);
            }
            return View(userList);
        }

        public async Task<IActionResult> Search(string name) 
        {
            var user=await _usersRepository.GetByName(name);
            return View(user);
        }

        public async Task<IActionResult> Add() 
        {
            ViewBag.Roles = new SelectList(await this.genericRole.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AppUserVM userVM)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    fullName = userVM.fullName,
                    UserName = userVM.userName,
                    PasswordHash = userVM.password,
                    Email = userVM.email,
                    RoleId = userVM.role_Id
                };
                int result=await _usersRepository.Add(user);
                if(result>0)
                      return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(await this.genericRole.GetAll(), "Id", "Name");
            return View(userVM);
        }

        public  async Task<IActionResult> Edit(string id)
        {
            var user = await _usersRepository.GetById(id);
            AppUserVM userVM = new AppUserVM()
            {
                Id= id,
                fullName = user.fullName,
                userName = user.UserName,
                password = user.PasswordHash,
                email = user.Email,
                role_Id=user.RoleId
            };
            ViewBag.Roles = new SelectList(await this.genericRole.GetAll(), "Id", "Name");
            return View(userVM); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,AppUserVM userVM)
        {
            if (ModelState.IsValid) {
                var user = await _usersRepository.GetById(id);
                user.fullName = userVM.fullName;
                user.UserName = userVM.userName;
                user.PasswordHash = userVM.password;
                user.Email = userVM.email;
                user.RoleId = userVM.role_Id;
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(await this.genericRole.GetAll(), "Id", "Name");
            return View(userVM);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user= await _usersRepository.GetById(id);
            if (user == null) {
                return NotFound();
            }
             _usersRepository.Delete(user);
            return RedirectToAction("index");
        }




    }
}
