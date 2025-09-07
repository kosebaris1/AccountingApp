using Accounting.Core.UnitOfWorks;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.UnitOfWorks
{
    public class UnitOfWorks(AppDbContext context) : IUnitOfWorks
    {

        private readonly AppDbContext _context = context;
        public void commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
          await  _context.SaveChangesAsync();
        }
    }
}
