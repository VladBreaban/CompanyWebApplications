using DatabaseInteractions.APIModels;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.WebSockets;

namespace CompanyWebApplications.Controllers;

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

    [HttpGet("GetCompanyById/{companyId}")]
    public async Task<ActionResult<CompanyApiModel>> GetCompanyById(Guid companyId)
    {
        if (companyId == Guid.Empty)
        {
            return BadRequest("Received empty id");
        }

        return await _companyService.GetById(companyId.ToString());
    }


    [HttpGet("GetCompanyByIsin/{companyIsin}")]
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

    [HttpPost("UpdateCompany")]
    public async Task<ActionResult<CompanyApiModel>> UpdateCompany([FromBody] CompanyApiModel company)
    {
        if (company == null)
        {
            return BadRequest("Company should not be null");
        }
        try
        {
            var result = await _companyService.UpdateCompanyById(company.Id, company);
            return Ok(company);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("CreateCompany")]
    public async Task<ActionResult<CompanyApiModel>> CreateCompany([FromBody] CompanyApiModel company)
    {

        try
        {
            var result = await _companyService.AddCompany(company);
            return Ok(company);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }
}
