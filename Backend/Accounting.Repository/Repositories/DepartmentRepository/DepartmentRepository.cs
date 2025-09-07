using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.DepartmentInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.DepartmentRepository
{
    public class DepartmentRepository(AppDbContext context) : GenericRepository<Department>(context),IDepartmentRepository
    {
    }
}
