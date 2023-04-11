using AspNetCoreHero.ToastNotification.Abstractions;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Repository;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> GetAllCategories()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllCategories(Category category)
        {
            try
            {
                _unitOfWork.category.Create(category);
                _unitOfWork.Save();
                TempData["Success"] = "Added Successfully!";
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");

            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AjaxMethod()
        {
            var result = _unitOfWork.category.GetAll();
            return Json(result);
        }

       
        public async Task<JsonResult> DeleteCategory(int id)
        {
            try

            {
                _unitOfWork.category.Delete(id);

                _unitOfWork.Save();
                //_notyfService.Success("Sucessfully added category");

            }

            catch (DataException)

            {

                ModelState.AddModelError("", "Unable to save changes.");

            }


            return new JsonResult("data deleted");
        }

      
        public JsonResult UpadteEmployee([FromBody] Category categoryModel)
        {
            var category = _unitOfWork.category.GetById(categoryModel.Id);

            category.Name = categoryModel.Name;
            
                category.IsActive = categoryModel.IsActive;
            _unitOfWork.Save();
            return new JsonResult("data update");
        }
        public JsonResult GetDataForEdit(int id)
        {
            var data = _unitOfWork.category.GetById(id);
            return Json(data);
        }
       

        
    }



}
