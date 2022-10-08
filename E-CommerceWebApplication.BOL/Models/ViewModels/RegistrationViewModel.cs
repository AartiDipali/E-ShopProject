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
        public int PhoneNumber { get; set; }

        public string City { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }
    }
}
