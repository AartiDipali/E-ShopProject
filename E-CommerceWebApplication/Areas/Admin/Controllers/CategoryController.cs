using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
