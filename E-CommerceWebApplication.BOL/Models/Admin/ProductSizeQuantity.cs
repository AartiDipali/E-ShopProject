using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class ProductSizeQuantity
    {
        [Key]
        public int PsizeQuantityId { get; set; }

        public int Quantity { get; set; }

        public int? productId { get; set; }

        [ForeignKey("productId")]
        public virtual Product product { get; set; }

        public int? sizeId { get; set; }

        [ForeignKey("sizeId")]
        public virtual Sizes Sizes { get; set; }


    }
}
