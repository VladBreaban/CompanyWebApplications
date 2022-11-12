using DatabaseInteractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatabaseInteractions.RepositoriesInterfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(string companyId);
        Task<Company> GetByIsin(string companyIsin);
        Task<List<Company>> GetAll();
        Task UpdateEntity(Company company);
        Task CreateEntity(Company company);
    }
}
