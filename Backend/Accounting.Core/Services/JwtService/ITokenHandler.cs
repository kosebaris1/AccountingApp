using Accounting.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Core.Services.JwtService
{
    public interface ITokenHandler
    {
        Token CreateToken(User user,List<Role> roles);
        string CreateRefleshToken();

        IEnumerable<Claim> SetClaims(User user, List<Role> roles);
    }
}
