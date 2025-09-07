using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Services.GroupInRoleService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.GroupInRoleService
{
    public class GroupInRoleService(IUnitOfWorks unitOfWorks, IGenericRepository<GroupInRole> repository) : Service<GroupInRole>(unitOfWorks, repository), IGroupInRoleService
    {
    }
}
