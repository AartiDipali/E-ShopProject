using AspNetCoreHero.ToastNotification.Abstractions;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Accounts
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IAccount _Account;
        public AccountController(UserManager<IdentityUser> userManager,INotyfService notyfService,IAccount account)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _Account = account;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            var userdata = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                PasswordHash = model.Password
            };
            var result = await _userManager.CreateAsync(userdata, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userdata, "Customer");
                // Persistend creates either session or permanent cookie
                //await signInManager.SignInAsync(user, isPersistent: false);
                _notyfService.Success("You have successfully saved the data");
                return View();
                
            }
            foreach (var error in result.Errors)
            {
                // Adding this to Modelstate goes to validation-summary in register.cshtml
                ModelState.AddModelError("", error.Description);
            }



            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel userModel)
        {
           if(ModelState.IsValid)
            {
                var result = await _Account.LoginAsync(userModel);
                if (result==true)
                {
                    _notyfService.Success("Login Sucessfull");
                    return RedirectToAction("Index", "Home", new { area = "Customer" });

                }
                else
                {
                    return View();
                }
            }
           else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

    }
}
