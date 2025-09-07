using Accounting.Core.Models;
using Accounting.Core.Repositories.GroupInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.GroupRepository
{
    public class GroupRepository(AppDbContext context) : GenericRepository<Group>(context), IGroupRepository
    {
    }
}
