using DatabaseInteractions.APIModels;
using DatabaseInteractions.Models;
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
            try
            {
                var entity = await _userRepository.GetUserByEmail(userEmail);
                if (entity is not null)
                {
                    var result = new UserLogin
                    {
                        email = entity.email,
                        password = entity.password
                    };
                    return result;
                }
            }
            catch(Exception ex)
            {
                //looggin here
                return null;
            }


            return null;

        }

        public async Task<Guid> Create(string email, string hashPass)
        {
            try
            {
                var entity = new User
                {
                    Id = Guid.NewGuid(),
                    email = email,
                    password = hashPass

                };

                await _userRepository.Create(entity);

                return entity.Id;
            }
            catch(Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}
