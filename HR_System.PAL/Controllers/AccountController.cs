using HR_System.BAL.Interfaces;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM userVM) 
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userVM.userName);

                if(user != null) 
                {
                    var flag = await userManager.CheckPasswordAsync(user, userVM.password);
                    if(flag)
                    {
                        await signInManager.PasswordSignInAsync(user, userVM.password, userVM.rememberMe, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "كلمة السر خاطئة");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "اسم المستخدم خاطئ");
                }

            }
            return View(userVM);

        }


        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        public async Task<IActionResult> AccessDenied(string? ReturnUrl)
        {
            return View();
        }
    }
}
