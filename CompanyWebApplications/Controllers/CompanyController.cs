using DatabaseInteractions.APIModels;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CompanyWebApplications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {

        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyApiModel>> GetExperience(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                return BadRequest("Received empty id");
            }

            return await _companyService.GetById(companyId.ToString());
        }

        [HttpPost("{companyId}")]
        public async Task<ActionResult<CompanyApiModel>> CreateCompany(Guid companyId, [FromBody] CompanyApiModel company)
        {
            if (companyId == Guid.Empty)
            {
                return BadRequest("Received empty id");
            }
            //acelasi tip de controller si pt update
           // var result = await _companyService.CreateCompany(companyId, company);
        }
    }
}