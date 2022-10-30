using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.YFDto;
using stockInfoApi.DAL.Queries.Accounts;
using stockInfoApi.DAL.Services;

namespace stockInfoApi.DAL.Handlers.Accounts
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, AccountDbo>
    {
        private readonly DevDbContext _context;
        private readonly IConfiguration _config;
        private readonly StockQuotes _request;
        public GetAccountByIdHandler(DevDbContext context, IConfiguration config, StockQuotes request)
        {
            _context = context;
            _config = config;
            _request = request;
        }

        public async Task<AccountDbo> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            AccountDbo account =
                await _context.Accounts.FirstOrDefaultAsync(x => request.Id == x.AccountId);

            if (account == null)
                return null;
            await GetUpToDateData(request.Id);
            return account;
        }

        private async Task GetUpToDateData(Guid id)
        {
            AccountDbo account = await _context.Accounts.FirstOrDefaultAsync(x => id == x.AccountId);
            List<StockDbo> stocks = await (from Stock in _context.Stocks
                                           where Stock.AccountId == id
                                           select Stock).ToListAsync();
            if (stocks.Any())
            {
                foreach (StockDbo stock in stocks)
                {
                    QuoteDto details = await _request.NewQuote(_config["YF_BASE_URL"], _config["YF_API_KEY"], stock.Symbol);
                    Result result = details.QuoteResponse.Result[0];
                    stock.TotalHoldings = result.Ask * stock.NumShares;
                }

                account.StockHoldings = Math.Round(stocks.Aggregate((double)0, (curr, accum) => curr + accum.TotalHoldings), 2, MidpointRounding.AwayFromZero);
                await _context.SaveChangesAsync();
            }
        }

    }
}
