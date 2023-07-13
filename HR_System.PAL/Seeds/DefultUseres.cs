using HR_System.Constants;
using HR_System.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Permissions = HR_System.Constants.Permissions;

namespace HR_System.Seeds
{
    public static class DefultUseres
    {
        public static async Task seedUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defultUser = new AppUser
            {
                fullName = "Mohamed Samir",
                Email = "Samir@ITI.com",
                UserName = "SamirITI"
            };
            var user = await userManager.FindByEmailAsync(defultUser.Email);
            if(user == null)
            {
                await userManager.CreateAsync(defultUser, "Samir@123456");
                await userManager.AddToRoleAsync(defultUser, "Admin");
            }
            //TODO: seed Claims
            await roleManager.seedClaimsForAdmin();
        }
        public static async Task seedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.addPermissionsclaims(adminRole, "Users");
            await roleManager.addPermissionsclaims(adminRole, "Attendance");
            await roleManager.addPermissionsclaims(adminRole, "Employee");
            await roleManager.addPermissionsclaims(adminRole, "GeneralSettings");
            await roleManager.addPermissionsclaims(adminRole, "Holidays");
            await roleManager.addPermissionsclaims(adminRole, "Home");
            await roleManager.addPermissionsclaims(adminRole, "Roles");
            await roleManager.addPermissionsclaims(adminRole, "SalaryReport");
            await roleManager.addPermissionsclaims(adminRole, "Department");

        }
        public static async Task addPermissionsclaims(this RoleManager<IdentityRole> roleManager , IdentityRole role , string module) 
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.generatePermissionsList(module);
            foreach (var permission in allPermissions)
            {
                if(!allClaims.Any(c=>c.Type=="Permission" && c.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        } 

    }
}
