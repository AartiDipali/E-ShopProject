using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BOL.Models.Admin;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbcontext _context;
       private readonly ILogger _logger;
        private ICategory _categoryRepository;
        private ISubCategory _subcategoryRepository;

        public UnitOfWork(ApplicationDbcontext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

           
        }

        public ICategory category
        {
            get { 
                
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context,_logger); 
            
            }
        }
      
        public ISubCategory subcategory
        {
            get
            {
                return _subcategoryRepository = _subcategoryRepository ?? new SubCategoryRepo(_context, _logger);
            }
        }

        //public async Task CompleteAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        public void Save()
        {
            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
