using Azure.Core.GeoJson;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.BOL.Models.ViewModels;
using E_CommerceWebApplication.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
    public class ManageDataRepo : IManageData
    {
        readonly private ApplicationDbcontext dbcontext;
        public ManageDataRepo(ApplicationDbcontext _Context)
        {
            dbcontext = _Context;
        }
        public void AddBrand(Brand brand)
        {
            var brandData = new Brand()
            {
                brandName = brand.brandName
            };
            dbcontext.Add(brandData);
            dbcontext.SaveChanges();
        }

        public void AddGender(Gender gender)
        {
            var genderData = new Gender()
            {
                genderName = gender.genderName
            };
            dbcontext.Add(genderData);
            dbcontext.SaveChanges();
        }

        //Add size 
        public void AddSize(Sizes size)
        {
            var sizeData = new Sizes()
            {
                sizeName = size.sizeName,
                brandId=size.brandId,
                subcategoryId=size.subcategoryId,
                categoryId=size.categoryId,
                genderId=size.genderId,

            };
            dbcontext.Add(sizeData);
            dbcontext.SaveChanges();
        }

        //This function get sizedata using join
        public IEnumerable<SizesViewModel> GetAllSizeData()
        {
          
            var allsizedata = (from s in dbcontext.sizes
                               join ct in dbcontext.Categories
                               on s.categoryId equals ct.Id into tblcat
                               from c in tblcat
                               join br in dbcontext.Brands
                               on s.brandId equals br.brandId into tblBrand
                               from b in tblBrand
                               join gd in dbcontext.genders
                               on s.genderId equals gd.genderId into tblGender
                               from g in tblGender
                               join sb in dbcontext.Subcategory
                               on s.subcategoryId equals sb.subcategoryId into tblSubcategory
                               from subCat in tblSubcategory
                               select new SizesViewModel
                               {
                                   sizeName = s.sizeName,
                                   categoryName = c.Name,
                                   brandName = b.brandName,
                                   genderName = g.genderName,
                                   subCategoryName = subCat.subcategoryName

                               }).ToList();
           

                return allsizedata;
        }

    }
}
