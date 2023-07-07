using HR_System.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.BAL.Interfaces
{
    public interface IClaimRepository
    {
        public void AddClaimToRole(String roleName, Claim newClaim);
    }
}
