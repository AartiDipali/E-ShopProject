using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BOL.Models.Admin
{
    public class Product
    {

        public int productId { get; set; }
        public string productName { get; set; }

        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
