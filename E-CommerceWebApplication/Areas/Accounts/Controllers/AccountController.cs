using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Accounts
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
