using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.DepartmentInterface;
using Accounting.Core.Services.DepartmentService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.DepartmentService
{
    public class DepartmentService : Service<Department>, IDepartmentService
    {
        // Bu eski sistem primary constructer yapmak daha mantıklı örnek olması açışından bu şekilde koydum.
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentService(IUnitOfWorks unitOfWorks, IGenericRepository<Department> repository, IDepartmentRepository departmentRepository) : base(unitOfWorks, repository)
        {
            this.departmentRepository = departmentRepository;
        }
    }
}
