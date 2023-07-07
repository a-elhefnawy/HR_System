using HR_System.DAL.Data;
using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.DataSeeding
{
    public class HRDBIntiallizer
    {
        public static void SeedingPagesNames(HRDBContext _db)
        {
            _db.Database.EnsureCreated();

            if (!_db.pagesNames.Any())
            {
                _db.pagesNames.Add(new PagesName()
                {
                    Name = "الموظفين"
                });
                _db.pagesNames.Add(new PagesName()
                {
                    Name = "الإعدادات العامه"
                });
                _db.pagesNames.Add(new PagesName()
                {
                    Name = "الحضور و الانصراف"
                });
                _db.pagesNames.Add(new PagesName()
                {
                    Name = "تقرير الرواتب"
                });

                _db.SaveChanges();
            }
        }

    }
}
