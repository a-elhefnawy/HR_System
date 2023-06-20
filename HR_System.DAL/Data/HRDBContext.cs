using HR_System.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_System.DAL.Data
{
    public class HRDBContext:IdentityDbContext<AppUser>
    {
        public HRDBContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PermissionsDB>()
                .HasKey(p => new { p.RoleId, p.ControllerName });

            base.OnModelCreating(builder);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<PermissionsDB> Permissions { get; set; }
    }
}
