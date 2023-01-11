using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.DAL.Data
{
    public static class Dbintializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
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
            var _user = await userManager.FindByEmailAsync("amoldipuecom1217@gmail.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new ApplicationUser
                {
                    UserName = "amoldipuecom1217@gmail.com",
                    Email = "amoldipuecom1217@gmail.com",
                    firstName="abc",
lastName="xyz",
address1="pqr",
   address2="gfh",


Zipcode ="ghdgusgus", 
city="hgsgudg",     
State="hgdusgus"
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

        
    }
}
