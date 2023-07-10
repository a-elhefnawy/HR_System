using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HR_System.BAL.Reposatories
{
    public class AppUsersRepository : GenericRepository<AppUser>
    {
        public AppUsersRepository(HRDBContext dbContext) : base(dbContext)
        {
        }
    }
}
