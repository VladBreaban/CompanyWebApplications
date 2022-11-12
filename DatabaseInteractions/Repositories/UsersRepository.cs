using DatabaseInteractions.Models;
using DatabaseInteractions.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly CompanyDbContext _companyContext;

        public UsersRepository(CompanyDbContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            var result = await _companyContext.Users.Where(x => x.email == userEmail).FirstOrDefaultAsync();
            return result;
        }
    }
}
