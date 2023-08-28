using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Repository;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.BOL.Models.Customer;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        private readonly ILogger<ManageController> logger1;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbcontext _context;
       private readonly IManageData _manageData;
        public ManageController(ILogger<ManageController> logger1,IUnitOfWork _unitOfWork, ApplicationDbcontext context,IManageData manageData)
        {
            this.logger1 = logger1;
            this.unitOfWork = _unitOfWork;
            this._context = context;
            this._manageData = manageData;
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
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }
   
        
        [HttpPost]
        public IActionResult AddBrand(Brand brandModel)
        {
            try
            {
                _manageData.AddBrand(brandModel);
              
                TempData["Success"] = "Added Successfully!";
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");

            }
            return View();
        }
        [HttpGet]
        public IActionResult AddSizes()
        {
            //bind Brand
            List<SelectListItem> brandList = new List<SelectListItem>();

            var brandData = _context.Brands.ToList();

            foreach (var item in brandData)
            {
                brandList.Add(new SelectListItem { Text = item.brandName, Value = item.brandId.ToString() });
               
            }

            brandList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewData["BrandList"] = brandList;

            //Bind Gender

            List<SelectListItem> genderList = new List<SelectListItem>();

            var GenderData = _context.genders.ToList();

            foreach (var item in GenderData)
            {
                genderList.Add(new SelectListItem { Text = item.genderName, Value = item.genderId.ToString() });
                
            }
            genderList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewData["GenderList"] = genderList;

            //Bind Category

            List<SelectListItem> cateogoryList = new List<SelectListItem>();

            var categoryData = _context.Categories.ToList();

            foreach (var item in categoryData)
            {
                cateogoryList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });

            }
            cateogoryList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            ViewData["CateogoryList"] = cateogoryList;

            //Bind SubCategory
            List<SelectListItem> subCatList = new List<SelectListItem>();
            var subData = _context.Subcategory.ToList();

            foreach (var item in subData)
            {
                subCatList.Add(new SelectListItem { Text = item.subcategoryName, Value = item.subcategoryId.ToString() });

            }

            subCatList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewData["subCatData"] = subCatList;

            //Fetch All Related data with size

            

            return View();
        }
        //Fetch data on ajax request
        [HttpPost]
        public  IActionResult GettblSizeDataAjax()
        {
            var allSizeRelatedData =_manageData.GetAllSizeData();
           
            return Json(allSizeRelatedData);
        }
        [HttpPost]
        public IActionResult AddSizes(Sizes sizesModel)
        {
            try
            {
                _manageData.AddSize(sizesModel);

                TempData["Success"] = "Added Successfully!";
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindSubCat(string categoryId)
        {
            int catId;
            catId = Convert.ToInt32(categoryId);
           
            List<Subcategory> SubLists = new List<Subcategory>();
            if (!string.IsNullOrEmpty(categoryId))
            {
                catId = Convert.ToInt32(categoryId);
                SubLists = _context.Subcategory.Where(s => s.categoryId.Equals(catId)).ToList();

            }
            return Json(SubLists);
        }
        
        [HttpGet]
        public IActionResult AddGender()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGender(Gender genderModel)
        {
            try
            {
                _manageData.AddGender(genderModel);

                TempData["Success"] = "Added Successfully!";
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");

            }
            return View();
        }

    }
}
