using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class ProductImages
    {
        [Key]
        public int productimageId { get; set; }

        public string productImageName { get; set; }

        public string description { get; set; }
        
        public int? productId{ get; set; }

        [ForeignKey("productId")]
        public virtual Product product { get; set; }
    }
}
