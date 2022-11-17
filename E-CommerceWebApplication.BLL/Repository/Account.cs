using Azure.Core;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Service;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace E_CommerceWebApplication.BLL.Repository
{
    public class Account : IAccount
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
         

        public Account(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       

        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,loginViewModel.Password,isPersistent:true,false);
            return result;
           
        }

        public async Task<IdentityResult> Register(RegistrationViewModel registrationViewModel)
        {
            var userdata = new ApplicationUser
            {
                UserName = registrationViewModel.firstName,
                Email = registrationViewModel.Email,
                PasswordHash = registrationViewModel.Password
            };
            var result = await _userManager.CreateAsync(userdata, registrationViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userdata, "Customer");
                

               
            }
            return result;
        }
        
    }
}
