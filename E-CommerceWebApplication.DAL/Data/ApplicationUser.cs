using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.DAL.Data
{
    public class ApplicationUser:IdentityUser
    {
      public string? firstName { get; set; }
      public string? lastName { get; set; }
      public string? address1 { get; set; }
      public string? address2 { get; set; }
      public string? Phone { get; set; }
      public string? Zipcode { get; set; }
      public string? city { get; set; }
      public string? State { get; set; }
        
      
    }
}
