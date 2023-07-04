using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Interfaces
{
    public interface IAttendenceRepository : IGenericRepository<Attendence>
    {
        Task<IEnumerable<Attendence>> GetAllAttendnce();


    }
}
