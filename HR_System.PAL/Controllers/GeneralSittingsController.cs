using HR_System.BAL.Interfaces;
using HR_System.Constants;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class GeneralSittingsController : Controller
    {
        private readonly IGeneralSittingsRepository gsRepo;

        public GeneralSittingsController(IGeneralSittingsRepository _gsRepo)
        {
            gsRepo = _gsRepo;
        }

        [Authorize(Permissions.GeneralSettings.View)]
        public async Task<IActionResult> Index()
        {
            var generalSittings=await gsRepo.GetGeneralSettings();
            return View(generalSittings);
        }

        [Authorize(Permissions.GeneralSettings.Create)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GeneralSittingsVM newSittings) 
        {
            if(ModelState.IsValid)
            {
                GeneralSittings generalSittings = new GeneralSittings()
                {
                    overTime = newSittings.overTime,
                    underTime = newSittings.underTime,
                    week_end_Day1= newSittings.week_end_Day1,
                    week_end_Day2=newSittings.week_end_Day2,
                    date=newSittings.date,
                };
                if(newSittings.week_end_Day1==newSittings
                    .week_end_Day2)
                {
                    ModelState.AddModelError("", "يجب اختيار يومين اجازة مختلفين");
                    return View(newSittings);
                }
                int result= await gsRepo.AddNewSittings(generalSittings);
                if(result>0) 
                {
                    return RedirectToAction("Index");
                }
            }
            return View(newSittings);
        }
    }
}
