using E_CommerceWebApplication.BOL.Models.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.ViewModels
{
    public class ManageViewModel
    {
        public int Id { get; set; }
        public List<SelectListItem> listOfCategory { get; set; }
        public Subcategory subcategory { get; set; }
        public Category category { get; set; }
       public string subcategoryName { get; set; }
       public int? categoryId { get; set; }
    }
}
