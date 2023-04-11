using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using E_CommerceWebApplication.BOL.Models.Admin;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace E_CommerceWebApplication.DAL.Data
{
    public class ApplicationDbcontext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbcontext>());
        }



        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Subcategory> Subcategory { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Gender> genders { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductSizeQuantity> productSizeQuantities { get; set; }

        public DbSet<Sizes> sizes { get; set; }


    }

}
