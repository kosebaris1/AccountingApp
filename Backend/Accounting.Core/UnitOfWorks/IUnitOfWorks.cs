using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.UnitOfWorks
{
    public interface IUnitOfWorks 
    {
        void commit();
        Task CommitAsync();
    }
}
