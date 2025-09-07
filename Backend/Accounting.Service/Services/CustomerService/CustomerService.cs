using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.CustomerInterface;
using Accounting.Core.Services.CustomerService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.CustomerService
{
    public class CustomerService(IUnitOfWorks unitOfWorks, IGenericRepository<Customer> repository,ICustomerRepository customerRepository) : Service<Customer>(unitOfWorks, repository), ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
    }
}
