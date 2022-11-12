
using CompanyWebApplications.Helpers;

namespace CompanyPorjectTests;

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

        [Fact]
        public async Task Get_By_Isin_Should_Succeed()
        {
            //Arrange
            var isinName = "asdasd" + GetRandomNumber().ToString();
            var companyService = Setup().GetService<ICompanyService>();
            //ACT
            var company = new DatabaseInteractions.APIModels.CompanyApiModel { Name = "Test 1", Exchange = "asdasd", Isin = isinName, Ticker = "asd", Website = "test1.com" };

            var insertedId = await companyService.AddCompany(company);

            var companyCheck = await companyService.GetByIsin(isinName);

            //Assert

            Assert.NotNull(companyCheck);
            Assert.Equal(insertedId, companyCheck.Id);
        }

    [Fact]
    public async Task CreateUser()
    {
        //Arrange
        var userService = Setup().GetService<IUserService>();
        //ACT
        var userFromAngular = new DatabaseInteractions.APIModels.UserLogin { email = "test@yahoo.com", password = "parolaTest"};
        var hashedEnteredPass = PasswordHelper.HashPassword(userFromAngular.password, "My%MX&z0up9WmTQseudpjoZZEHxRbe6H@1sfT8*wopqnr9wFmCD#rxfwQkx65pOBT&Z89&SXG=Z!Zyt0H1PdZTuJBpNjJ#Q9CyzQ");


        await userService.Create(userFromAngular.email, hashedEnteredPass);

        var userCheck = await userService.GetByEmail(userFromAngular.email);

        //Assert

        Assert.NotEqual(hashedEnteredPass, userFromAngular.password);
        Assert.NotNull(userCheck);
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
               .AddScoped<ICompanyService, CompanyService>()
               .AddScoped<IUserRepository, UsersRepository>()
               .AddScoped<IUserService, UserService>();
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
