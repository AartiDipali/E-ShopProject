using AspNetCoreHero.ToastNotification.Abstractions;
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
        public AccountController(UserManager<IdentityUser> userManager,INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
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
                _notyfService.Custom("You have successfully saved the data.");
                return View();
                
            }
            foreach (var error in result.Errors)
            {
                // Adding this to Modelstate goes to validation-summary in register.cshtml
                ModelState.AddModelError("", error.Description);
            }



            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user != null &&
                await _userManager.CheckPasswordAsync(user, userModel.Password))
            {

                var rolename = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                var dataresult = rolename.FirstOrDefault();
                if (dataresult!=null)
                {
                    HttpContext.Session.SetString(key:"Rolename", value:dataresult);
                }

                return RedirectToAction("Index", "Home", new {area = "Customer" });
                //return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();

            }
           
        }

    }
}
