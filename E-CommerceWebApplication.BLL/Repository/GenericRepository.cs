using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        public readonly ApplicationDbcontext _context;
        public readonly DbSet<T> _dbSet;
        public readonly ILogger _logger;
        public GenericRepository(ApplicationDbcontext context, ILogger logger)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
            _logger = logger;
        }

        public virtual void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public virtual  IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(object id)
        {
            return  _dbSet.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}
