
using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR_System.PAL
    

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HRDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            
            builder.Services.AddScoped<IAppUserRepository,AppUsersRepository>();
            builder.Services.AddScoped<IGeneralSittingsRepository,GeneralSittingsRepository>();

            //check error and user Controller
            
            //Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<HRDBContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}