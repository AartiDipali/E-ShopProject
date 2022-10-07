using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.ViewModels
{
    public class RegistrationViewModel
    {
       
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public string Password { get; set; }

        public string Email
        {
            get; set;
        }
    }
}
