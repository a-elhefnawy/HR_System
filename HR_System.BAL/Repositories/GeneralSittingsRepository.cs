using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Reposatories
{
    public class GeneralSittingsRepository : IGeneralSittingsRepository
    {
        private readonly HRDBContext db;

        public GeneralSittingsRepository(HRDBContext db)
        {
            this.db = db;
        }
        public async Task<int> AddNewSittings(GeneralSittings newSittings)
        {
             await db.GeneralSittings.AddAsync(newSittings);
             return db.SaveChanges();
        }

      

        public async Task<List<GeneralSittings>> GetGeneralSettings()
        {
            return await db.GeneralSittings.OrderByDescending(p => p.date).ToListAsync();

        }
    }
}
