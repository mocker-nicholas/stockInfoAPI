using AutoFixture;
using Moq;
using Moq.EntityFrameworkCore;
using stockInfoApi.DAL.ControllerFeatures;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;

namespace UnitTests
{
    public class AccountsTests
    {
        private readonly AccountFeatures _accountFeatures;
        private readonly Mock<DevDbContext> _contextMock = new Mock<DevDbContext>();
        private readonly Fixture fixture = new Fixture();

        public AccountsTests()
        {
            _accountFeatures = new AccountFeatures(_contextMock.Object);
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