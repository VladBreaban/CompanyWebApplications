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
            var result = await _companyContext.Companies.Where(x=>x.Id.ToString() == companyId).FirstOrDefaultAsync();
            return result;
        }
    }
}
