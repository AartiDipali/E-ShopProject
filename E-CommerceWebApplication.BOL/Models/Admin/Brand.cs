using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Brand
    {
        [Key]
        public int brandId { get; set; }
        public string brandName { get; set; }

        public virtual ICollection<Sizes> sizes { get; set; }
        public virtual ICollection<Product> products { get; set; }
    }

}
