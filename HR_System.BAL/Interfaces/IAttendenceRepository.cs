using HR_System.DAL.Models;
using HR_System.DAL.ViewModelsForUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Interfaces
{
    public interface IAttendenceRepository : IGenericRepository<Attendence>
    {
        Task<List<Attendence>> GetAllAttendnce(int year, int month);
        //Task<List<Attendence>> GetAllAttendnce();
        Task<List<Attendence>> GetAllAttendnce(DateTime startDate, DateTime endDate);
        Task<Attendence> GetEmployeeAttendenceById(int id);
        Task UpdateAttendence(UpdateEmployeeAttendence attendenctModel);
       
    }
}
