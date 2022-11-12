using DatabaseInteractions.APIModels;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace CompanyWebApplications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CompanyController : ControllerBase
    {

        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet("GetCompanyById")]
        public async Task<ActionResult<CompanyApiModel>> GetCompanyById(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                return BadRequest("Received empty id");
            }

            return await _companyService.GetById(companyId.ToString());
        }
     

        [HttpGet("GetCompanyByIsin")]
        public async Task<ActionResult<CompanyApiModel>> GetCompanyByIsin(string companyIsin)
        {
            if (companyIsin.IsNullOrEmpty())
            {
                return BadRequest("Received empty isin");
            }

            return await _companyService.GetByIsin(companyIsin);
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<List<CompanyApiModel>>> GetAllCompanies()
        {
            return await _companyService.GetAll();
        }

        [HttpPost("UpdateCompanyById")]
        public async Task<ActionResult<CompanyApiModel>> UpdateCompanyById([FromBody] CompanyApiModel company)
        {
           if(company == null)
            {
                return BadRequest();
            }          
            var result = await _companyService.UpdateCompanyById(company.Id, company);
            return result==true? Ok(company) : BadRequest("Could not update entity");
        }

        [HttpPost("CreateCompany")]
        public async Task<ActionResult<CompanyApiModel>> CreateCompany([FromBody] CompanyApiModel company)
        {

            var result = await _companyService.AddCompany(company);
            return result != Guid.Empty ? Ok(company) : BadRequest("Could not create entity");
        }
    }
}