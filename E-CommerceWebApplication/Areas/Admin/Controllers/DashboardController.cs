using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
