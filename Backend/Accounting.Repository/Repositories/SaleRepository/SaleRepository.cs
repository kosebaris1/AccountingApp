using Accounting.Core.Models;
using Accounting.Core.Repositories.SaleInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.SaleRepository
{
    public class SaleRepository(AppDbContext context) : GenericRepository<Sale>(context), ISaleRepository
    {
    }
}
