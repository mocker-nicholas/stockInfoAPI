using AutoFixture;
using Castle.Core.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using stockInfoApi.DAL.ControllerFeatures;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Services;

namespace UnitTests
{
    public class AccountsTests
    {
        private readonly AccountFeatures _accountFeatures;
        private readonly Mock<DevDbContext> _contextMock = new Mock<DevDbContext>();
        private readonly Mock<IConfiguration> _configMock = new Mock<IConfiguration>();
        private readonly Mock<StockQuotes> _requestMock = new Mock<StockQuotes>();
        private readonly Fixture fixture = new Fixture();

        public AccountsTests()
        {
            _accountFeatures = new AccountFeatures(_contextMock.Object, (Microsoft.Extensions.Configuration.IConfiguration)_configMock.Object, _requestMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
        }

        [Test]
        public async Task GetAccountById_ShouLdReturnAccount_IfAccountExists()
        {
            IList<AccountDbo> accounts = fixture.CreateMany<AccountDbo>(5).ToList();
            _contextMock.Setup(x => x.Accounts).ReturnsDbSet(accounts);

            Guid accountId = accounts[2].AccountId;
            var account = await _accountFeatures.GetAccountById(accountId);

            Assert.IsNotNull(account);
        }
    }
}