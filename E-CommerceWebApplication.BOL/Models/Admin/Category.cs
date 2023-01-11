using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Boolean IsActive { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        public byte[] dataFiles { get; set; }
        [NotMapped]
        [DisplayName("Category Image")]
        public IFormFile ImageFile { get; set; }

        public DateTime? CreatedOn { get; set; }

    }
}
