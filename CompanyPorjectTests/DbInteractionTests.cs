using DatabaseInteractions;
using DatabaseInteractions.Repositories;
using DatabaseInteractions.RepositoriesInterfaces;
using DatabaseInteractions.Services;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPorjectTests
{
    public class DbInteractionTests
    {
        [Fact]
        public async Task TestGetUser()
        {
            try
            {
                //Arrange
                var companyService = Setup().GetService<ICompanyService>();

                //ACT

                var existingCompany = await companyService.GetById("BBC316EA-E25C-4B97-B577-2EFACCC47117");
                var unexisting = await companyService.GetById(Guid.NewGuid().ToString());

                //Assert

                Assert.NotNull(existingCompany);
                Assert.Null(unexisting);
            }
            catch(Exception ex)
            {
                var test = ex.Message;
            }

        }
        private ServiceProvider Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var services = new ServiceCollection();
           services
                .AddDbContext<CompanyDbContext>
                (o => o.UseSqlServer(configuration.
                   GetConnectionString("DefaultConnection")))
                  .AddScoped<ICompanyRepository, CompanyRepository>()
                  .AddScoped<ICompanyService, CompanyService>();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}