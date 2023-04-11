using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
    public class SubCategoryRepo : GenericRepository<Subcategory>, ISubCategory
    {
        public SubCategoryRepo(ApplicationDbcontext context, ILogger logger) : base(context, logger)
        {
        }

        public override void Create(Subcategory subcategory)
        {
            try
            {
                var subCatdata = new Subcategory()
                {
                    subcategoryName = subcategory.subcategoryName,
                    categoryId= subcategory.categoryId

                };
                _dbSet.Add(subCatdata);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(Subcategory));
            }
        }
    }
}
