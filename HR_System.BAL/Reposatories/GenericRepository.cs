using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Reposatories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HRDBContext _dbContext;

        public GenericRepository(HRDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return _dbContext.SaveChanges();    
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public int Update(T entity)
        {
            _dbContext
                .Set<T>().Update(entity);

            return _dbContext.SaveChanges();

        }
    }
}
