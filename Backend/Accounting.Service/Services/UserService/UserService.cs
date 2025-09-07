using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.UserService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.UserService
{
    public class UserService(IUnitOfWorks unitOfWorks, IGenericRepository<User> repository) : Service<User>(unitOfWorks, repository), IUserService
    {
    }
}
