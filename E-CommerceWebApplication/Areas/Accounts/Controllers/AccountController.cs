using AspNetCoreHero.ToastNotification.Abstractions;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Service;
using E_CommerceWebApplication.BOL.Models.Account;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using Serilog;
namespace E_CommerceWebApplication.Areas.Accounts
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IAccount _Account;
        private readonly IEmail _Email;
        public AccountController(UserManager<IdentityUser> userManager,INotyfService notyfService, IAccount account, IEmail email)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _Account = account;
            _Email = email;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Register user
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {

            var userdata = new ApplicationUser
            {
                UserName = model.firstName,
                Email = model.Email,
                PasswordHash = model.Password
            };
            var result = await _userManager.CreateAsync(userdata, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userdata, "Customer");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userdata);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = userdata.Email }, Request.Scheme);
                
                bool emailResponse = _Email.SendEmail(userdata.Email, confirmationLink);

                if (emailResponse)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyfService.Error("something went wrong");
                }


            }
            return View();
        }
        #endregion


        #region Email confirmation

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _notyfService.Error("user can't be exist");
               
            }
            else
            {
                 await _userManager.ConfirmEmailAsync(user, token);
                return View("ConfirmEmail");
            }
            return View();
        }

        #endregion


        #region Reset Password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
           // var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            bool response =_Email.SendForgetPasswordEmail(user.Email, callback);
            if(response)
            {
                _notyfService.Success("Sent reset password Link on your email account");
            }
            else
            {
                _notyfService.Error("Something went wrong");
            }
            
            return View(forgotPasswordModel);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion


        #region Login

        //Login Method
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (ModelState.IsValid)
            {
                var result = await _Account.Login(userModel);
                if (result.Succeeded)
                {
                    //_notyfService.Success("Login Sucessfull");

                    var rolename = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                    var dataresult = rolename.FirstOrDefault();
                    if (dataresult == "Admin")
                    {

                        return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });
                       
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                        
                    
                }
                _notyfService.Error("Invalid UserName or Password");
                //ModelState.AddModelError("", "Invalid UserName or Password");
            }
            //Log.Information("sucessfull login");
            return View(userModel);
        }
        #endregion

    }
}
