using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.RoleService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.RoleService
{
    public class RoleService(IUnitOfWorks unitOfWorks, IGenericRepository<Role> repository) : Service<Role>(unitOfWorks, repository), IRoleService
    {
    }
}
