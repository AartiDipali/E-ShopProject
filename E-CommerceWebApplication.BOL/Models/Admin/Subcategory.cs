using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Subcategory
    {
        [Key]
        public int subcategoryId { get; set; }
        public string subcategoryName { get; set; }
        public int? categoryId { get; set; }

        [ForeignKey("categoryId")]
        public virtual Category category { get; set; }

        public virtual ICollection<Sizes> sizes { get; set; }

        public virtual ICollection<Product> products { get; set; }
    }
}
