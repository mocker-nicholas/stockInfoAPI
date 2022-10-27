using Moq;
using stockInfoApi.DAL.ControllerFeatures;
using stockInfoApi.DAL.Interfaces;

namespace UnitTests
{
    public class AccountsTests
    {
        private readonly AccountFeatures _accountFeatures;
        private readonly Mock<IDevDbContext> _contextMock = new Mock<IDevDbContext>();
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
            Guid accountId = Guid.NewGuid();
            var account = await _accountFeatures.GetAccountById(accountId);
            Assert.IsNotNull(account);
        }
    }
}