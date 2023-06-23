using HR_System.DAL.Data;
using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Reposatories
{
    public class RolesRepository : GenericRepository<Role> 
    {
        public RolesRepository(HRDBContext dbContext) : base(dbContext)
        {
        }
 
    }
}
