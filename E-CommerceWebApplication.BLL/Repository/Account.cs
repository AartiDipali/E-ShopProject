using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
    public class Account : IAccount
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly INotyfService _notyfService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public Account(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       

        public async Task<bool> Login(LoginViewModel loginViewModel)
        {

            
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,loginViewModel.Password, loginViewModel.RememberMe, false);
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (result.Succeeded)
            {

                var rolename = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                var dataresult = rolename.FirstOrDefault();
                if (dataresult == "Admin")
                //{

                //    HttpContext.Session.SetString(key: "Rolename", value: dataresult);

                //}
                return true;
            }
            return false;
        }

        public async void Register(RegistrationViewModel registrationViewModel)
        {
            var userdata = new ApplicationUser
            {
                UserName = registrationViewModel.Name,
                Email = registrationViewModel.Email,
                PasswordHash = registrationViewModel.Password
            };
            var result = await _userManager.CreateAsync(userdata, registrationViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userdata, "Customer");
                // Persistend creates either session or permanent cookie
                //await signInManager.SignInAsync(user, isPersistent: false);
                //_notyfService.Success("You have successfully saved the data");
              

            }
           // return 1;
            
            throw new NotImplementedException();
        }
        
    }
}
