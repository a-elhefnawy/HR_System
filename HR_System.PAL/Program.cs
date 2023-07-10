
using HR_System.BAL.Interfaces;
using HR_System.BAL.Reposatories;
using HR_System.BAL.Repositories;
using HR_System.DAL.Data;
using HR_System.DAL.DataSeeding;
using HR_System.DAL.Models;
using HR_System.PAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR_System.PAL
    

{
    public class Program
    {
        private readonly IGenericRepository<Role> roleRepo;

        public Program(IGenericRepository<Role> _role)
        {
            roleRepo = _role;
        }
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
            builder.Services.AddScoped<IGenericRepository<PermissionsDB>, GenericRepository<PermissionsDB>>();
            builder.Services.AddScoped<IGenericRepository<PagesName>, GenericRepository<PagesName>>();

            // عشان سمير ميزعلش
            builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IClaimRepository ,ClaimRepository>();    
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IAppUserRepository,AppUsersRepository>();
            builder.Services.AddScoped<IGeneralSittingsRepository,GeneralSittingsRepository>();
            builder.Services.AddScoped<ISalariesReository, SalariesRepository>();
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

            using var scope=app.Services.CreateScope();
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<HRDBContext>();
                HRDBIntiallizer.SeedingPagesNames(db);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            app.Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //-----------------Employee----------------------//
                options.AddPolicy("AddEmployee", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string ;
                        var role=roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c=>c.CliamType=="AddEmployee" && c.ClaimValue=="true")) 
                        {
                            return true;
                        }
                        return false;

                    });
                });
                //--------------------------------------//
                options.AddPolicy("EditEmployee", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "EditEmployee" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });
                //------------------------------------//
                options.AddPolicy("DeleteEmployee", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DeleteEmployee" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("DisplayEmployee", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DisplayEmployee" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //---------------GeneralSittings---------------------//
                options.AddPolicy("AddGeneralSittings", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "AddGeneralSittings" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //-------------------------------------//
                options.AddPolicy("EditGeneralSittings", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "EditGeneralSittings" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("DeleteGeneralSittings", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DeleteGeneralSittings" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("DisplayGeneralSittings", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DisplayGeneralSittings" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------Attendence--------------//
                options.AddPolicy("AddAttendence", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "AddAttendence" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("EditAttendence", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "EditAttendence" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("DeleteAttendence", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DeleteAttendence" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------------------------------//
                options.AddPolicy("DisplayAttendence", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DisplayAttendence" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //------------SalaryReport-----------//
                options.AddPolicy("AddReport", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "AddReport" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //-----------------------------------//
                options.AddPolicy("EditReport", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "EditReport" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //-----------------------------------//
                options.AddPolicy("DeleteReport", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DeleteReport" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });

                //----------------------------------//
                options.AddPolicy("DisplayReport", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleName = context.Resource as string;
                        var role = roleRepo.Get(roleName).Result;
                        if (role != null && role.Claims.Any(c => c.CliamType == "DisplayReport" && c.ClaimValue == "true"))
                        {
                            return true;
                        }
                        return false;

                    });
                });


                //---------------END----------------//

            });
        }
    }
}