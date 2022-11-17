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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string Phone { get; set; }
        public string Zipcode { get; set; }
        public string city { get; set; }
        public string State { get; set; }
        public string Email { get; set;}
        public string Password { get; set; }
        public string confirmPassword { get; set; }
    }
}
