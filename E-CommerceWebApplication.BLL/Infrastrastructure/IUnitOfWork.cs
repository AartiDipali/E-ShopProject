using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.Infrastrastructure
{
    public interface IUnitOfWork
    {
        ICategory category { get; }

        Task CompleteAsync();

        
    }
}
