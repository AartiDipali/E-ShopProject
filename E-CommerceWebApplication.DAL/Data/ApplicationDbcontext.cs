using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using E_CommerceWebApplication.BOL.Models.Admin;

namespace E_CommerceWebApplication.DAL.Data
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }

}
