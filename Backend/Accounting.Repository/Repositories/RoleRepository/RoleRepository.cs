using Accounting.Core.Models;
using Accounting.Core.Repositories.RoleInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.RoleRepository
{
    public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context), IRoleRepository
    {
    }
}
