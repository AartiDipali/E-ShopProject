using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public DateTime CreatedDate { get; set; }

        public int productPrice { get; set; }
   
        public int  productSellprice { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public string materialAndCare { get; set; }

        public string color { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string productImageOne{ get; set; }

        public byte[] dataFilesOne { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string productImageTwo { get; set; }

        public byte[] dataFilesTwo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string productImageThree { get; set; }

        public byte[] dataFilesThree { get; set; }

        [NotMapped]
        [DisplayName("Category Image")]
        public IFormFile ImageFile { get; set; }
        public Boolean IsActive { get; set; }
        public string stock { get; set; }

        public int? categoryId { get; set; }
        [ForeignKey("categoryId")]
        public virtual Category category { get; set; }
      
        [ForeignKey("subcategory")]
        public int? subcategoryId { get; set; }
        [ForeignKey("subcategoryId")]
        public virtual Subcategory subcategory { get; set; }
        public int? genderId { get; set; }
        [ForeignKey("genderId")]
        public Gender gender { get; set; }

        public int? sizeId { get; set; }
        [ForeignKey("sizeId")]
        public virtual Sizes Size { get; set; }
        public int? brandId { get; set; }
        [ForeignKey("brandId")]
        public virtual Brand brand { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductSizeQuantity> ProductSizeQuantities { get; set; }


    }
}
