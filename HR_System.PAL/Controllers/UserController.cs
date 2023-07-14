using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.Constants;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using HR_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_System.PAL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> appuserRepo;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<AppUser> _appuserRepo, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            appuserRepo = _appuserRepo;
        }

        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> Index()
        {
            var users=  appuserRepo.Users.ToList();
            List<UserVM> userList = new List<UserVM>();
            foreach (var user in users)
            {
                UserVM userVM = new UserVM()
                {
                    Id = user.Id,
                    fullName=user.fullName,
                    userName=user.UserName,
                    password=user.PasswordHash,
                    email=user.Email,
                    Role=appuserRepo.GetRolesAsync(user).Result
                };
                userList.Add(userVM);
            }
            return View(userList);
        }
        [Authorize(Permissions.Users.Create)]
        public async Task<IActionResult> Add() 
        {
            List<IdentityRole>? roles = await roleManager.Roles.ToListAsync();
            var viewModel = new AppUserVM
            {
                Roles = roles.Select(p => new CheckboxVM
                {
                    DisplayID = p.Id,
                    DisplayValue = p.Name,
                    IsSelected = false
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AppUserVM userVM)
        {
            

                if (!userVM.Roles.Any(x => x.IsSelected))
                {
                    ModelState.AddModelError("Roles", "من فضلك اختر مجموعة");
                    return View(userVM);
                }

                if (await appuserRepo.FindByEmailAsync(userVM.userName) != null)
                {
                    ModelState.AddModelError("UserName", "هذا الاسم تم استخدامه من قبل");
                    return View(userVM);
                }

                AppUser user = new AppUser()
                {
                    fullName = userVM.fullName,
                    UserName = userVM.userName,
                    PasswordHash = userVM.password,
                    Email = userVM.email,
                };
                var res=await appuserRepo.CreateAsync(user,userVM.password);
                if (!res.Succeeded)
                {
                return View(userVM);

            }
            await appuserRepo.AddToRolesAsync(user, userVM.Roles.Where(p => p.IsSelected).Select(p => p.DisplayValue));
            return RedirectToAction("Index");

        }
        

        
        [Authorize(Permissions.Users.Delete)]
        public async Task<IActionResult> Delete(string Id)
        {
            var user=await appuserRepo.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            var result=await appuserRepo.DeleteAsync(user);
            if(result.Succeeded)
            {
               return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content("error");
            }
        }



    }
}
