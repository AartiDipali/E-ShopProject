using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Infrastrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Create(T entity);

        void Update(int id, T entity);

        void Delete(object id);

        void Save();
    }
}
