using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Services;

namespace UnitTests
{
    public class Tests
    {
        private readonly IConfiguration _config;
        private readonly StockQuotes _request;

        public Tests(IConfiguration config, StockQuotes request)
        {
            _config = config;
            _request = request;
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
        }

        [Test]
        public void TestStockTransaction()
        {
            Console.WriteLine("Test One");
            Assert.Pass();
        }
    }
}