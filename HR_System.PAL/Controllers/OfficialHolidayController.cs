using HR_System.BAL.Interfaces;
using HR_System.Constants;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PAL.Controllers
{
    public class OfficialHolidayController : Controller
    {
        private readonly IGenericRepository<OfficialHoliday> holidayRepo;
        public OfficialHolidayController(IGenericRepository<OfficialHoliday> holidayRepo)
        {
            this.holidayRepo = holidayRepo;
        }

        [HttpGet]
        [Authorize(Permissions.Holidays.View)]
        public async Task<IActionResult> Index()
        {
            var officialHolidays = await holidayRepo.GetAll();
            return View(officialHolidays.OrderBy(x=>x.Date).ToList());
        }

        //[HttpPost]
        //[Authorize(Permissions.Holidays.Create)]
        //public async Task<IActionResult> Index(OfficialHoliday officialHoliday)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        OfficialHoliday newHoliday = new OfficialHoliday()
        //        {
        //            Name = officialHoliday.Name,
        //            Date = officialHoliday.Date
        //        };

        //        int result = await holidayRepo.Add(newHoliday);

        //        if (result > 0)
        //        {
        //            var holidayss = await holidayRepo.GetAll();
        //            return View(holidayss);
        //        }
        //    }
        //    var holidays = await holidayRepo.GetAll();
        //    return View(holidays);
        //}

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Holidays.Create)]
        public async Task<IActionResult> Add(OfficialHoliday officialHoliday)
        {
            if (ModelState.IsValid)
            {
                OfficialHoliday newHoliday = new OfficialHoliday()
                {
                    Name = officialHoliday.Name,
                    Date = officialHoliday.Date
                };

                await holidayRepo.Add(newHoliday);
                return RedirectToAction("Index");
            }
            return View(officialHoliday);
        }

        [HttpGet]
        [Authorize(Permissions.Holidays.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var officialHoliday = await holidayRepo.Get(id);
            if (officialHoliday is null) return View("NotFound");

            return View(officialHoliday);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OfficialHoliday officialHoliday)
        {
            if (ModelState.IsValid)
            {
                holidayRepo.Update(officialHoliday);
                return RedirectToAction("Index");

            }
            return View(officialHoliday);
        }

        [Authorize(Permissions.Holidays.Delete)]
        public async Task<IActionResult> Delete(int Id)
        {
            var officialHoliday = await holidayRepo.Get(Id);
            if (officialHoliday == null) return BadRequest();
            holidayRepo.Delete(officialHoliday);
            return Json(officialHoliday);
        }
    }
}
