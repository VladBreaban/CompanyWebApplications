using DatabaseInteractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.RepositoriesInterfaces
{
    public interface IUserRepository 
    {
        Task<User> GetUserByEmail(string userEmail);

    }
}
