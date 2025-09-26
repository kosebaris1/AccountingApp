using Accounting.Core.DTOs.AuthDTOs;
using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Services.UserService
{
    public interface IUserService : IService<User>
    {
        User GetByEmail(string email);

        Task<Token> Login(UserLoginDto userLoginDto);
    }
}
