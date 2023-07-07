using HR_System.BAL.Interfaces;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly HRDBContext context;

        public ClaimRepository(HRDBContext context)
        {
            this.context = context;
        }
        public void AddClaimToRole(string roleName, Claim newClaim)
        {
            var role=context.Roles.FirstOrDefault(x => x.Name == roleName);
            if(role != null)
            {
                role.Claims.Add(newClaim);
            }
            
        }
    }
}
