using Accounting.Core.Models;
using Accounting.Core.Services.JwtService;
using Accounting.Service.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.JwtService
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration Configuration;

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string CreateRefleshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);

        }

        public Token CreateToken(User user, List<Role> roles)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token.Expriration = DateTime.Now.AddMinutes(15);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.Expriration,
                claims: SetClaims(user, roles),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            token.RefleshToken = CreateRefleshToken();
            return token;
        }

        public IEnumerable<Claim> SetClaims(User user, List<Role> roles)
        {
            Claim claim = new("sub", user.Id.ToString());
            List<Claim> claims = new List<Claim>();
            claims.Add(claim);
            claims.AddName(user.Name);
            claims.AddRoles(roles.Select(r => r.Name).ToArray());

            return claims;

        }
    }
}
