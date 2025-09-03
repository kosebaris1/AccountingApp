using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Repositories.CustomerInterface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        List<Customer> GetCustomersWithBalance(int balance);
    }
}
