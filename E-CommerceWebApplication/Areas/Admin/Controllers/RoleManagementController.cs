using E_CommerceWebApplication.BOL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.SqlServer.Management.Smo;
using System.Data;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    //Crud with jquery ajax call
    public class RoleManagementController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private ILogger<RoleManagementController> logger;
        public RoleManagementController(RoleManager<IdentityRole> roleManager, ILogger<RoleManagementController> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }


        public JsonResult GetRoles()
        {
            var roles = roleManager.Roles.ToList();
            var vm = new List<RoleViewModel>();
            roles.ForEach(item => vm.Add(
                new RoleViewModel()
                {
                    RoleName=item.Name
                }


            ));
            return Json(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            try
            {
                
                    var roleExist = await roleManager.RoleExistsAsync(role.RoleName);
                    if (!roleExist)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
                        ViewBag.resultdata = result;
                    }
                
                
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message + "" + ex.InnerException+""+ex.StackTrace);
            }
            return RedirectToAction("GetRoles", "RoleManagement", new { area = "Admin" });
        }
        public async Task<IActionResult> Delete(string id)
        {

            var result = await roleManager.FindByIdAsync(id);
            if (result == null)
            {
                return NotFound();

            }
            var status = roleManager.DeleteAsync(result);
            return View(result);


        }


    }
}
