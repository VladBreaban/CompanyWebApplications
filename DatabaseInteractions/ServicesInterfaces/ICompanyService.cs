using DatabaseInteractions.APIModels;
using DatabaseInteractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions.ServicesInterfaces
{
    public interface ICompanyService
    {
        Task<CompanyApiModel> GetById(string companyId);
        Task<CompanyApiModel> GetByIsin(string companyIsin);
        Task<List<CompanyApiModel>> GetAll();
        Task<bool> UpdateCompanyById(Guid companyId, CompanyApiModel company);
        Task<Guid> AddCompany(CompanyApiModel company);
    }
}
