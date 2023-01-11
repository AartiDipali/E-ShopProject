//using Azure.Core;
using Azure.Core;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Service;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RestSharp;
//using Microsoft.SqlServer.Management.Smo;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public Account(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider provider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
           
        }
       

        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {
            CookieOptions options = new CookieOptions();
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,loginViewModel.Password,isPersistent:true,false);
            if (loginViewModel.RememberMe == true)
            {

                options.Expires = DateTime.Now.AddDays(60);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserName", loginViewModel.Email, options);
                //encrypt password here
                byte[] b = ASCIIEncoding.ASCII.GetBytes(loginViewModel.Password);
                string encryptPassword=Convert.ToBase64String(b);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("Password", encryptPassword, options);
            }
            //Work on that part later
            else
            {
                options.Expires = DateTime.Now.AddDays(-60);
                //_httpContextAccessor.HttpContext.Response.Cookies.Delete("UserName");
                //_httpContextAccessor.HttpContext.Response.Cookies.Delete("Password");
            }

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
