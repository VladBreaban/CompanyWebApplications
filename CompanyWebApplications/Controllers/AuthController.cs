using CompanyWebApplications.Helpers;
using DatabaseInteractions.APIModels;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
namespace CompanyWebApplications.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    private readonly IOptionsMonitor<AuthentificationOptionsMonitor> _optionsMonitor;
    private readonly IOptionsMonitor<PasswordOptionsMonitor> _passwordOptionsMonitor;
    public AuthController(ILogger<AuthController> logger, IUserService userService, IOptionsMonitor<PasswordOptionsMonitor> passwordOptionsMonitor, IOptionsMonitor<AuthentificationOptionsMonitor> optionsMonitor)
    {
        _logger = logger;
        _userService = userService;
        _optionsMonitor = optionsMonitor;
        _passwordOptionsMonitor = passwordOptionsMonitor;
    }
    [HttpPost("login")]
    public IActionResult Login(UserLogin model)
    {

        if (String.IsNullOrEmpty(model.email) || String.IsNullOrEmpty(model.password ))
        {
            return BadRequest("Invalid client request");
        }
        var realUserFromDb = _userService.GetByEmail(model.email).ConfigureAwait(false).GetAwaiter().GetResult();

        if (realUserFromDb is null)
        {
            return BadRequest("Account does not exist");
        }
        var hashedEnteredPass = PasswordHelper.HashPassword(model.password, _passwordOptionsMonitor.CurrentValue.PasswordSalt);
        if (String.Equals(model.email, realUserFromDb.email) && hashedEnteredPass == realUserFromDb.password)
        {
            string tokenString = PasswordHelper.GetTokenString(_optionsMonitor.CurrentValue.JwtToken, _optionsMonitor.CurrentValue.JwtTokenExpirationTimeMinutes);
            return Ok(new DatabaseInteractions.Authentification.AuthenticatedResponse { Token = tokenString, Email = realUserFromDb.email });
        }
        return Unauthorized();
    }
    [HttpPost("register")]
    public  IActionResult Register(UserLogin model)
    {

        if (String.IsNullOrEmpty(model.email) || String.IsNullOrEmpty(model.password))
        {
            return BadRequest("Invalid client request");
        }

        var hashedEnteredPass = PasswordHelper.HashPassword(model.password, _passwordOptionsMonitor.CurrentValue.PasswordSalt);
        var result = _userService.Create(model.email, hashedEnteredPass).ConfigureAwait(false).GetAwaiter().GetResult();
        return result != Guid.Empty ? Ok() : BadRequest("Could not create User");
    }


}

