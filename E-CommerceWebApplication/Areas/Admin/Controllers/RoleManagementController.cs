using E_CommerceWebApplication.BOL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_CommerceWebApplication.Areas.Admin.Controllers
{
    public class RoleManagementController : Controller
    {
        private RoleManager<IdentityRole> roleManager;

        public RoleManagementController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
                ViewBag.resultdata = result;
            }

            return View();
        }
    }
}
