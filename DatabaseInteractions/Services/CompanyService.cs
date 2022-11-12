using DatabaseInteractions.APIModels;
using DatabaseInteractions.Models;
using DatabaseInteractions.RepositoriesInterfaces;
using DatabaseInteractions.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            CompanyApiModel result = null;
            // could use a mapper here
            var companyEntity = await _companyRepository.GetById(companyId);
            if(companyEntity is not null )
            {
                result = new CompanyApiModel
                {
                    Id = companyEntity.Id,
                    Name = companyEntity.Name,
                    Exchange = companyEntity.Exchange,
                    Ticker = companyEntity.Ticker,
                    Isin = companyEntity.Isin,
                    Website = companyEntity.Website
                };
            }

            return result;
        }

        public async Task<CompanyApiModel> GetByIsin(string companyIsin)
        {
            CompanyApiModel result = null;
            // could use a mapper here
            var companyEntity = await _companyRepository.GetByIsin(companyIsin);
            if (companyEntity is not null)
            {
                result = new CompanyApiModel
                {
                    Id = companyEntity.Id,
                    Name = companyEntity.Name,
                    Exchange = companyEntity.Exchange,
                    Ticker = companyEntity.Ticker,
                    Isin = companyEntity.Isin,
                    Website = companyEntity.Website
                };
            }

            return result;
        }

        public async Task<List<CompanyApiModel>> GetAll()
        {
            List<CompanyApiModel> result = new List<CompanyApiModel>();
            // could use a mapper here
            var companies = await _companyRepository.GetAll();
            if (companies.Count != 0)
            {
                foreach (var companyEntity in companies)
                {
                    var company = new CompanyApiModel
                    {
                        Id = companyEntity.Id,
                        Name = companyEntity.Name,
                        Exchange = companyEntity.Exchange,
                        Ticker = companyEntity.Ticker,
                        Isin = companyEntity.Isin,
                        Website = companyEntity.Website
                    };
                    result.Add(company);
                }              
            }
            return result;
        }
        public async Task<bool> UpdateCompanyById(Guid companyId, CompanyApiModel company)
        {
            try
            {
                if (company != null)
                {
                    var companyEnitity = await _companyRepository.GetById(companyId.ToString());

                    if(companyEnitity != null)
                    {
                        companyEnitity.Id = companyId;
                        companyEnitity.Isin = company.Isin;
                        companyEnitity.Exchange = company.Exchange;
                        companyEnitity.Ticker = company.Ticker;
                        companyEnitity.Website = company.Website;
                        companyEnitity.Name = company.Name;

                        await _companyRepository.UpdateEntity(companyEnitity).ConfigureAwait(false);
                    }                   
                }
            }
            catch(Exception ex)
            {
                //would be a great idea to have a logger here
                return false;
            }

            return true;
        }
        public async Task<Guid> AddCompany(CompanyApiModel company)
        {
            try
            {
                if (company != null)
                {
                    Company companyEnitity = new Company();
                    companyEnitity.Id = Guid.NewGuid();
                    companyEnitity.Isin = company.Isin;
                    companyEnitity.Exchange = company.Exchange;
                    companyEnitity.Ticker = company.Ticker;
                    companyEnitity.Website = company.Website;
                    companyEnitity.Name = company.Name;

                   await _companyRepository.CreateEntity(companyEnitity).ConfigureAwait(false);
                   return companyEnitity.Id;
                }  
            }
            catch (Exception ex)
            {
                //would be a great idea to have a logger here
                return Guid.Empty;
            }

            return Guid.Empty;

        }
    }
}
