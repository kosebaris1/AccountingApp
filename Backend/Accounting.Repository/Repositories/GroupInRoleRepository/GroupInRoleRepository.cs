using Accounting.Core.Models;
using Accounting.Core.Repositories.GroupInRoleInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.GroupInRoleRepository
{
    public class GroupInRoleRepository(AppDbContext context) : GenericRepository<GroupInRole>(context), IGroupInRoleRepository
    {
    }
}
