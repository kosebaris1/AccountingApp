using Accounting.Core.DTOs.AuthDTOs;
using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.UserInterface;
using Accounting.Core.Services.JwtService;
using Accounting.Core.Services.UserService;
using Accounting.Core.UnitOfWorks;
using Accounting.Service.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.UserService
{
    public class UserService: Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public UserService(IUnitOfWorks unitOfWorks, IGenericRepository<User> repository, IUserRepository userRepository, ITokenHandler tokenHandler)
            : base(unitOfWorks, repository)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        public User GetByEmail(string email)
        {
            User user= _userRepository.Where(x=>x.Email == email).FirstOrDefault();

                return user ?? user;
        }

        public async Task<Token> Login(UserLoginDto userLoginDto)
        {
            Token token = new Token();

            var user= GetByEmail(userLoginDto.Email);

            if(user == null)
            {
                return null;
            }

            var result = false;

            result= HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);

            List<Role> role = new List<Role>();

            if (result)
            {
                token = _tokenHandler.CreateToken(user, role);
                return token;
            }

            return null;
        }
    }
}
