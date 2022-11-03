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
          

         //Call registration method here 

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
                var result = await _Account.Login(userModel);
                if (result==true)
                {
                    //_notyfService.Success("Login Sucessfull");
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                    _notyfService.Success("Login Sucessfull");
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
         return View(userModel);
        }

    }
}
