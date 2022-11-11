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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<CompanyApiModel> GetById(string companyId)
        {
            // could use a mapper here
            var companyEntity = await _companyRepository.GetById(companyId);
            var result = new CompanyApiModel
            {
                Id = companyEntity.Id,
                Name = companyEntity.Name,
                Exchange = companyEntity.Exchange,
                Ticker = companyEntity.Ticker,
                Isin = companyEntity.Isin,
                Website = companyEntity.Website
            };
            return result;
        }
    }
}
