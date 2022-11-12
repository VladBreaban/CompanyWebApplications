using DatabaseInteractions.APIModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CompanyWebApplications.Helpers;

public class PasswordHasher : IPasswordHasher<UserLogin>
{

    private readonly IOptionsMonitor<PasswordOptionsMonitor> _optionsMonitor;
    public PasswordHasher(IOptionsMonitor<PasswordOptionsMonitor> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public string HashPassword(UserLogin user, string password)
    {
        return PasswordHelper.HashPassword(password, _optionsMonitor.CurrentValue.PasswordSalt);
    }

    public PasswordVerificationResult VerifyHashedPassword(UserLogin user, string hashedPassword, string providedPassword)
    {
        return hashedPassword != HashPassword(user, providedPassword) ? PasswordVerificationResult.Failed : PasswordVerificationResult.Success;
    }
}