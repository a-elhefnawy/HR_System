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
    public class AttendenceRepository : GenericRepository<Attendence>, IAttendenceRepository
    {
        public AttendenceRepository(HRDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Attendence>> GetAllAttendnce()
        {
            return await _dbContext.Attendences.Include(x => x.Employee).ThenInclude(x=>x.Department).ToListAsync();
        }
        
    }
}
