using AspNetCoreHero.ToastNotification.Abstractions;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Repository;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Runtime.InteropServices;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ApplicationDbcontext _context;
        private IUnitOfWork _unitOfWork;
        private readonly INotyfService _notyfService;
        public CategoryController(ILogger<CategoryController> logger, ApplicationDbcontext context, IUnitOfWork unitOfWork, INotyfService notyfService)
        
        {
                _logger = logger;
                _context = context;
                _unitOfWork = unitOfWork;
                _notyfService = notyfService;

        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {

            

            return View();
        }

        [HttpPost]
        public IActionResult AjaxMethod()
        {
            var result=_unitOfWork.category.GetAll();
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = _unitOfWork.category.GetById(id);
            ViewBag.Result = data.Name;
            return View();
        }

        //[HttpGet]
        //public IActionResult AddCategory()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult GetAllCategories(Category category)
        {
            try

            {
                

                    _unitOfWork.category.Create(category);

                    _unitOfWork.CompleteAsync();


                    _notyfService.Success("Sucessfully added category");
                

            }

            catch (DataException)

            {

                ModelState.AddModelError("", "Unable to save changes.");

            }

            return View(category);
        }
    }
}
