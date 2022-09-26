using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
