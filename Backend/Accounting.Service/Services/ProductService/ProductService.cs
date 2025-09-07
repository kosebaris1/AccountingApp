using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.ProductService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.ProductService
{
    public class ProductService(IUnitOfWorks unitOfWorks, IGenericRepository<Product> repository) : Service<Product>(unitOfWorks, repository), IProductService
    {
    }
}
