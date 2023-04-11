using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Sizes
    {
        [Key]
        public int sizeId { get; set; }
        public string sizeName { get; set; }

        public int? brandId { get; set; }
        [ForeignKey("brandId")]
        public virtual Brand brand { get; set; }
        public int? subcategoryId { get; set; }

        [ForeignKey("subcategoryId")]
        public virtual Subcategory subcategory { get; set; }


        public int? genderId { get; set; }
        [ForeignKey("genderId")]
        public Gender gender { get; set; }

        public int? categoryId { get; set; }
        [ForeignKey("categoryId")]
        public virtual Category category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSizeQuantity> ProductSizeQuantities { get; set; }



    }
}
