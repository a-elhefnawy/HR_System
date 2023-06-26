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
    public class AppUsersRepository : IAppUserRepository
    {
        protected readonly HRDBContext _dbContext;

        public AppUsersRepository(HRDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AppUser> GetByName(string name)
        {
            return await _dbContext.Users.Include(p=>p.Role).SingleOrDefaultAsync(p=>p.UserName==name);
        }

        public async Task<AppUser> GetById(string id) 
        {
            return await _dbContext.Users.Include(p=>p.Role).SingleOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _dbContext.Users.Include(p => p.Role).ToListAsync();
        }

        public async Task<AppUser> Get(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<int> Add(AppUser entity)
        {
            await _dbContext.Users.AddAsync(entity);
            return _dbContext.SaveChanges();

        }

        public int Update(AppUser entity)
        {
            _dbContext.Users.Update(entity);

            return _dbContext.SaveChanges();
        }

        public int Delete(AppUser entity)
        {
            _dbContext.Users.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
