using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey;

namespace Survey.Server.Data
{
    public static class SeedAdministratorAndUser
    {
        internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            await SeedRoles(roleManager);
            await SeedAdministratorUser(userManager);
        }


        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            IEnumerable<string> roleNames = new List<string>()
            {
                Survey.Shared.Constants.ROLE_NAMES.Admin.ToString(),
                Survey.Shared.Constants.ROLE_NAMES.BoardAdmin.ToString(),
                Survey.Shared.Constants.ROLE_NAMES.BoardFiller.ToString()
            };

            foreach (var roleName in roleNames)
            {
                await RoleHelper(roleManager, roleName);
            }

        }


        private async static Task RoleHelper(RoleManager<IdentityRole> roleManager, string roleName)
        {
            bool administratorRoleExist = await roleManager.RoleExistsAsync(roleName);

            if (!administratorRoleExist)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                await roleManager.CreateAsync(role);
            }
        }

        private async static Task SeedAdministratorUser(UserManager<IdentityUser> userManager)
        {
            bool administratorUserExists = await userManager.FindByEmailAsync("admin@a.hu") != null;

            if (!administratorUserExists)
            {
                var administratorUser = new IdentityUser()
                {
                    UserName = "admin@a.hu",
                    Email = "admin@a.hu"
                };
                IdentityResult identityResult = await userManager.CreateAsync(administratorUser, "Aa123456!");

                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(administratorUser, Survey.Shared.Constants.ROLE_NAMES.Admin.ToString());
                }
            }

        }

    }
}
