
using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.BAL.Repositories;
using HR_System.DAL.Data;
using HR_System.DAL.Models;
using HR_System.Filter;
using HR_System.PAL.ViewModels;
using HR_System.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR_System.PAL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HRDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });


            builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IGeneralSittingsRepository,GeneralSittingsRepository>();
            builder.Services.AddScoped<IGenericRepository<OfficialHoliday>, GenericRepository<OfficialHoliday>>();
            builder.Services.AddScoped<ISalariesReository, SalariesRepository>();
            builder.Services.AddScoped<IOfficialHolidaysRepository, OfficialHolidaysRepository>();
            //check error and user Controller

            //Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<HRDBContext>();

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();




            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });



            var app = builder.Build();


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var looggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = looggerFactory.CreateLogger("app");
            try
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await DefultRoles.seedAsync(roleManager);
                await DefultUseres.seedUser(userManager, roleManager);
                logger.LogInformation("Data seeded");
                logger.LogInformation("App started");

            }
            catch (Exception ex)
            {

                logger.LogWarning(ex, "an error ecured while seeding data");
            }


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

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}