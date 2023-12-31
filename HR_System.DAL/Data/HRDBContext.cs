﻿using HR_System.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public HRDBContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PermissionsDB>()
                .HasKey(p => new { p.RoleId, p.PageNameId });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.;Database=HRDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<PermissionsDB> Permissions { get; set; }
        //public DbSet<PagesName> pagesNames { get; set; }
        public DbSet<GeneralSittings> GeneralSittings { get; set;}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<EmployeesSalaries> EmployeesSalaries { get; set; }
        public DbSet<OfficialHoliday> OfficialHolidays { get; set; }



    }
}
