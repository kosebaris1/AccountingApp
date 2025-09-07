using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.GroupInterface;
using Accounting.Core.Services.GroupService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.GroupService
{
    public class GroupService(IUnitOfWorks unitOfWorks, IGenericRepository<Group> repository,IGroupRepository groupRepository) : Service<Group>(unitOfWorks, repository), IGroupService
    {
        private readonly IGroupRepository _groupRepository = groupRepository;   
    }
}
