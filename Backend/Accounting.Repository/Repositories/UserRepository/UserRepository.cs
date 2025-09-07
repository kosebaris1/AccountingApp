using Accounting.Core.Models;
using Accounting.Core.Repositories.UserInterface;
using Accounting.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Repository.Repositories.UserRepository
{
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
    {
    }
}
