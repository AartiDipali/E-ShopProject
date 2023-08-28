using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
