using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        private readonly ILogger<ManageController> logger1;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbcontext _context;
        public ManageController(ILogger<ManageController> logger1,IUnitOfWork _unitOfWork, ApplicationDbcontext context)
        {
            this.logger1 = logger1;
            this.unitOfWork = _unitOfWork;
            this._context = context;
        }

        public IActionResult GetAllSubcategorys()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddSubcategory()
        {

            //ViewBag.categoryName = new SelectList(_context.Categories, "Id","Name");

            var categoryList = (from category in _context.Categories
                                select new SelectListItem()
                                {
                                    Text = category.Name,
                                    Value = Convert.ToString(category.Id)
                                }).ToList();

            categoryList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            ManageViewModel manageViewModel = new ManageViewModel();
            manageViewModel.listOfCategory = categoryList;
            //ViewBag.categoryName = categoryList;

            return View(manageViewModel);

        }

        [HttpPost]
        public IActionResult AddSubcategory(Subcategory subcategoryModel)
        {
            try
            {
                unitOfWork.subcategory.Create(subcategoryModel);
                unitOfWork.Save();

            }
            catch (NullReferenceException ex)
            {
                logger1.LogError(ex, "{Repo} All function error", typeof(ManageController));
            }
            var categoryList = (from category in _context.Categories
                                select new SelectListItem()
                                {
                                    Text = category.Name,
                                    Value = Convert.ToString(category.Id)
                                }).ToList();

            categoryList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            ManageViewModel manageViewModel = new ManageViewModel();
            manageViewModel.listOfCategory = categoryList;

            return View(manageViewModel);
        }
        public IActionResult GetAllBrands()
        {
            return View();
        }
        public IActionResult AddBrand()
        {
            return View();
        }

        public IActionResult GetAllSizes()
        {
            return View();
        }
        public IActionResult AddSizes()
        {
            return View();
        }

        public IActionResult AddGender()
        {
            return View();
        }

    }
}
