using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Repositories
{
    public class OfficialHolidaysRepository : GenericRepository<OfficialHoliday>, IOfficialHolidaysRepository
    {
        public OfficialHolidaysRepository(HRDBContext dbContext) : base(dbContext)
        {
        }

        public  int GetOfficialHolidays(int year, int month)
        {
            return  _dbContext.OfficialHolidays.
                Where(h => h.Date.Year == year && h.Date.Month == month).Count();
        }
    }
}
