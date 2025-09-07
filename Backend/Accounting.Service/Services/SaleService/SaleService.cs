using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.SaleService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.SaleService
{
    public class SaleService(IUnitOfWorks unitOfWorks, IGenericRepository<Sale> repository) : Service<Sale>(unitOfWorks, repository), ISaleService
    {
    }
}
