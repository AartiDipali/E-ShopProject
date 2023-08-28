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
    public class ProductRepo : GenericRepository<Product>, IGenericRepository<Product>
    {
        public ProductRepo(ApplicationDbcontext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
