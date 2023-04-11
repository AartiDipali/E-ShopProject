using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
	public class CategoryRepository : GenericRepository<Category>, ICategory
	{
		public CategoryRepository(ApplicationDbcontext context, ILogger logger) : base(context, logger)
		{
		}
		public override IEnumerable<Category> GetAll()
		{

			try
			{
				return _dbSet.ToList();

			}
			catch (NullReferenceException ex)
			{
				_logger.LogError(ex, "{Repo} All function error", typeof(CategoryRepository));
				return Enumerable.Empty<Category>();
			}

		}
		public override Category GetById(object id)
		{
			try
			{
				var result = _dbSet.Find(id);
				if (result == null)
				{
					throw new NullReferenceException();
				}
				return result;
			}
			catch (NullReferenceException ex)
			{
				_logger.LogError(ex, "{Repo} All function error", typeof(CategoryRepository));
			}
			return null;
		}

		public override void Create(Category category)
		{
			try
			{
                           //Getting FileName
                        var fileName = Path.GetFileName(category.ImageFile.FileName);
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var image = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                
                       var categoryData = new Category()
                        {

                            
							Name=category.Name,
							IsActive=category.IsActive,
							CreatedOn= DateTime.Now,
							ImageName=image,
                       };

				using (var target = new MemoryStream())
				{
					category.ImageFile.CopyTo(target);
                    categoryData.dataFiles = target.ToArray();
                }

				_dbSet.Add(categoryData);     
                
			}
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CategoryRepository));
            }
        }
        public override void Delete(object id)
        {
			try
			{
				var result = _dbSet.Find(id);
				_dbSet.Remove(result);
				
			}
			catch (NullReferenceException ex)
			{
				_logger.LogError(ex, "{Repo} All function error", typeof(CategoryRepository));
			}
        }

    }
}
