using AspNetCoreHero.ToastNotification.Abstractions;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Service;
using E_CommerceWebApplication.BOL.Models.Account;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.SqlServer.Management.Smo;
using Serilog;
using System.Text;
using Serilog;
using System;
using static System.Collections.Specialized.BitVector32;

namespace E_CommerceWebApplication.Areas.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IAccount _Account;
        private readonly IEmail _Email;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, INotyfService notyfService, IAccount account, IEmail email, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _Account = account;
            _Email = email;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
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
            try
            {
                var userdata = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    PhoneNumber = model.Phone,
                    address1 = model.address1,
                    address2 = model.address2,
                    city = model.city,
                    State = model.State,
                    Zipcode = model.Zipcode,
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
                        ViewBag.Result = true;


                    }
                    else
                    {
                        _notyfService.Error("something went wrong");
                    }


                }
            }
            catch(Exception ex)
            {
       
                Log.Error(ex.Message.ToString());
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
                _notyfService.Success("Sucessfully confirmed Email");
            }
            return RedirectToAction("Index", "Home", new { area = "Customer" });
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
            bool response = _Email.SendForgetPasswordEmail(user.Email, callback);
            if (response)
            {
                _notyfService.Success("The link has been sent, please check your email to reset your password.");
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
            //if (!ModelState.IsValid)
            //    return View(resetPasswordModel);
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
            _notyfService.Success("Your password has been reset");
            return RedirectToAction("Login", "Account", new { area = "Accounts" });
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
            byte[] b;
            string decryptPassword=null;
            //Get cookie value using IhttpcontectAcessor
            // var data = HttpContext.Request.Cookies["UserName"].ToString();
            //Through request return function
            //string userNameCookie = Request.Cookies["UserName"];
            string userNameCookie = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            string passwordCookie = _httpContextAccessor.HttpContext.Request.Cookies["Password"];
            if (userNameCookie != null && passwordCookie!=null)
            {
                ViewBag.userName = userNameCookie;
                 b = Convert.FromBase64String(passwordCookie);
                decryptPassword = ASCIIEncoding.ASCII.GetString(b);
                ViewBag.password = decryptPassword;

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (ModelState.IsValid)
            {
                var result = await _Account.Login(userModel);
                //if (result.Succeeded && user.EmailConfirmed==true)
                if (result.Succeeded)
                {
                

                    var rolename = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                    var dataresult = rolename.FirstOrDefault();
                    if (dataresult == "Admin")
                    {
                       // HttpContext.Session.SetString("SessionUser", user.Email);
                        return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }


                }
                _logger.LogError("Invalid UserName or Password");
                _notyfService.Error("Invalid UserName or Password");
               
               
            }
            
            return View(userModel);
        }
        #endregion
      
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
