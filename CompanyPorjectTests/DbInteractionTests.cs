using DatabaseInteractions;
using DatabaseInteractions.Models;
using DatabaseInteractions.Repositories;
using DatabaseInteractions.RepositoriesInterfaces;
using DatabaseInteractions.Services;
using DatabaseInteractions.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPorjectTests
{
    public class DbInteractionTests
    {
        private static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();

        [Fact]
        public async Task Create_Test_Should_Succeed()
        {
            //GetRandomNumber to generate unique name for Isin is not required, in memory database used for testing has no DB constraints
                //Arrange

                var companyService = Setup().GetService<ICompanyService>();
                //ACT
                var company = new DatabaseInteractions.APIModels.CompanyApiModel { Name = "Test 1", Exchange = "asdasd", Isin = "asdsaasd"+GetRandomNumber().ToString(), Ticker = "asd", Website = "test1.com" };
                var result = await companyService.AddCompany(company);
                var companyCheck = (await companyService.GetAll()).FirstOrDefault(x=>x.Id == result);

                //Assert

                Assert.NotNull(companyCheck);
        }
        [Fact]
        public async Task Update_Should_Succeed()
        {
            //Arrange

            var companyService = Setup().GetService<ICompanyService>();
            //ACT
            var company1 = new DatabaseInteractions.APIModels.CompanyApiModel { Name = "Test 1", Exchange = "asdasd", Isin = "asdsaasd" + GetRandomNumber().ToString(), Ticker = "asd", Website = "test1.com" };
            var insertedId = await companyService.AddCompany(company1);

            var updateCompanyFromAngular = new DatabaseInteractions.APIModels.CompanyApiModel { Name = "Test Angular", Exchange = "angular", Isin = "angular" + GetRandomNumber().ToString(), Ticker = "sssss", Website = "test1.com" };
            var isUpdated = await companyService.UpdateCompanyById(insertedId, updateCompanyFromAngular);

            var updatedEntity = await companyService.GetById(insertedId.ToString());
            //Assert
            Assert.True(isUpdated);
            Assert.NotNull(updatedEntity);
            Assert.True(updatedEntity.Name == "Test Angular");
        }
        [Fact]
        public async Task Get_All_Succeed()
        {
            //Arrange

            var companyService = Setup().GetService<ICompanyService>();
            //ACT
            var company = new DatabaseInteractions.APIModels.CompanyApiModel { Name = "Test 1", Exchange = "asdasd", Isin = "asdsaasd" + GetRandomNumber().ToString(), Ticker = "asd", Website = "test1.com" };
            await companyService.AddCompany(company);
            var list = await companyService.GetAll();

            //Assert

            Assert.NotEmpty(list);
        }


        private ServiceProvider Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            var services = new ServiceCollection();
           services
                .AddDbContext<CompanyDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Companies", InMemoryDatabaseRoot);

                })
                  .AddScoped<ICompanyRepository, CompanyRepository>()
                  .AddScoped<ICompanyService, CompanyService>();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private int GetRandomNumber()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            return num;
        }

    }
}