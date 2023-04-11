using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Gender
    {
        [Key]
        public int genderId { get; set; }
        public string genderName { get; set; }
        public virtual ICollection<Sizes> sizes { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
