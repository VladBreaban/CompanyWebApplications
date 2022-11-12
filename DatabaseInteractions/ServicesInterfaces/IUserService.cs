using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.ServicesInterfaces
{
    public interface IUserService
    {
        Task<APIModels.UserLogin> GetByEmail(string userEmail);

        Task<Guid> Create(string email, string hashPass)
    }
}
