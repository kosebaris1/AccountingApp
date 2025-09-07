using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.PaymentService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.PaymentService
{
    public class PaymentService(IUnitOfWorks unitOfWorks, IGenericRepository<Payment> repository) : Service<Payment>(unitOfWorks, repository), IPaymentService
    {

    }
}
