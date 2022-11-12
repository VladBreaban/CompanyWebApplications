using DatabaseInteractions.APIModels;
using DatabaseInteractions.RepositoriesInterfaces;
using DatabaseInteractions.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserLogin> GetByEmail(string userEmail)
        {
            var entity = await _userRepository.GetUserByEmail(userEmail);
            if(entity is not null)
            {
                var result = new UserLogin
                {
                    email = entity.email,
                    password = entity.password
                };
                return result;
            }

            return null;

        }
    }
}
