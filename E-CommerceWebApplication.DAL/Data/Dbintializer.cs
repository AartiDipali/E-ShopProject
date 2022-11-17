using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.DAL.Data
{
    public static class Dbintializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var roles = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] rolenames = { "Admin" };
            IdentityResult result;
            //foreach (var roleName in rolenames)
            //{
            //    var roleExist = await roles.RoleExistsAsync(roleName);
            //    // ensure that the role does not exist
            //    if (!roleExist)
            //    {
            //        //create the roles and seed them to the database: 
            //        result = await roles.CreateAsync(new IdentityRole(roleName));
            //    }
            //}

            // find the user with the admin email 
            var _user = await userManager.FindByEmailAsync("admin@email.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new ApplicationUser
                {
                    UserName = "aartichame2015@gmail.com",
                    Email = "aartichame2015@gmail.com",
                };
                string adminPassword = "Admin@123#17";

                var createPowerUser = await userManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await userManager.AddToRoleAsync(poweruser, "Admin");

                }
            }

        }

        public static object InitializeAsync(IServiceProvider services, UserManager<ApplicationUser> userManager)
        {
            throw new NotImplementedException();
        }
    }
}
