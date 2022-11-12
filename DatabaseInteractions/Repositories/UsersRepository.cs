using DatabaseInteractions.Models;
using DatabaseInteractions.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatabaseInteractions.Repositories;

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

    public async Task Create(User user)
    {
        _companyContext.Users.Add(user);

        await _companyContext.SaveChangesAsync();
    }
}

