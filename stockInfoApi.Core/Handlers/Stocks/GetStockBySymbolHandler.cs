using MediatR;
using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Queries.Stocks;
using stockInfoApi.DAL.Services;

namespace stockInfoApi.DAL.Handlers.Stocks
{
    public class GetStockBySymbolHandler : IRequestHandler<GetStockBySymbolQuery, Result>
    {
        private readonly IConfiguration _config;
        private readonly StockQuotes _request;

        public GetStockBySymbolHandler(IConfiguration config, StockQuotes request)
        {
            _config = config;
            _request = request;
        }

        public async Task<Result> Handle(GetStockBySymbolQuery request, CancellationToken cancellationToken)
        {
            QuoteDto details = await _request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], request.Symbol);
            Result result = details.QuoteResponse.Result[0];
            return result;
        }
    }
}
