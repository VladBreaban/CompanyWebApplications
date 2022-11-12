using DatabaseInteractions.APIModels;
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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyDbContext _companyContext;

        public CompanyRepository(CompanyDbContext companyContext)
        {
                _companyContext = companyContext;
        }
        public async Task<Company> GetById(string companyId)
        {
            var result = await _companyContext.Companies.FirstOrDefaultAsync(x => x.Id.ToString() == companyId);
            return result;
        }

        public async Task<Company> GetByIsin(string companyIsin)
        {
            var result = await _companyContext.Companies.FirstOrDefaultAsync(x => x.Isin == companyIsin);
            return result;
        }

        public async Task<List<Company>> GetAll()
        {
            return _companyContext.Companies.ToList();
        }

        public async Task UpdateEntity(Company company)
        {           
            _companyContext.Companies.Update(company);
            
            await _companyContext.SaveChangesAsync();           
        }

        public async Task CreateEntity(Company company)
        {
            _companyContext.Companies.Add(company);

            await _companyContext.SaveChangesAsync();
        }
    }
}
