using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_CommerceWebApplication.BOL.Models.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        [Display(Name = "This field is required")]
        public string RoleName { get; set; } = default!;
    }
}
